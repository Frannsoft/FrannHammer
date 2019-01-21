using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Images;
using System.Net;
using System.Threading.Tasks;

namespace FrannHammer.WebScraping.Images
{
    public class DefaultImageScrapingService : IImageScrapingService
    {
        private readonly IImageScrapingProvider _imageScrapingProvider;

        public DefaultImageScrapingService(IImageScrapingProvider imageScrapingProvider)
        {
            Guard.VerifyObjectNotNull(imageScrapingProvider, nameof(imageScrapingProvider));

            _imageScrapingProvider = imageScrapingProvider;
        }

        public async Task<string> GetColorHexValue(string imageSourceUrl)
        {
            string colorHexValue = string.Empty;
            using (var client = _imageScrapingProvider.CreateHttpClient())
            {
                using (var response = await client.GetAsync(imageSourceUrl))
                {
                    if (response.StatusCode == HttpStatusCode.NotFound) //ultimate logos not ready yet on kh site.
                    {
                        return colorHexValue;
                    }

                    response.EnsureSuccessStatusCode();

                    using (var inputStream = _imageScrapingProvider.CreateMemoryStream())
                    {
                        await response.Content.ReadAsStreamAsync().Result.CopyToAsync(inputStream);
                        using (var bitmapResult = _imageScrapingProvider.CreateBitmap(inputStream))
                        {
                            var color = bitmapResult.GetPixel(110, 90); //corner bounds
                            colorHexValue = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                        }
                    }
                }
            }

            return colorHexValue;
        }
    }
}
