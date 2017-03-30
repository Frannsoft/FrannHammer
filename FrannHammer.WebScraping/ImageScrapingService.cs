using System.Drawing;
using System.Threading.Tasks;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;

namespace FrannHammer.WebScraping
{
    public class ImageScrapingService : IImageScrapingService
    {
        private readonly IImageScrapingProvider _imageScrapingProvider;

        public ImageScrapingService(IImageScrapingProvider imageScrapingProvider)
        {
            Guard.VerifyObjectNotNull(imageScrapingProvider, nameof(imageScrapingProvider));

            _imageScrapingProvider = imageScrapingProvider;
        }

        public async Task<string> GetColorHexValue(string imageSourceUrl)
        {
            Bitmap bitmapResult;

            using (var client = _imageScrapingProvider.CreateHttpClient())
            {
                using (var response = await client.GetAsync(imageSourceUrl))
                {
                    response.EnsureSuccessStatusCode();

                    using (var inputStream = _imageScrapingProvider.CreateMemoryStream())
                    {
                        await response.Content.ReadAsStreamAsync().Result.CopyToAsync(inputStream);
                        bitmapResult = _imageScrapingProvider.CreateBitmap(inputStream);
                    }
                }
            }

            var color = bitmapResult.GetPixel(110, 90); //corner bounds
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}
