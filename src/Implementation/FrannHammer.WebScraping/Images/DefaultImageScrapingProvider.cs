using System.Drawing;
using System.IO;
using System.Net.Http;
using FrannHammer.WebScraping.Contracts.Images;

namespace FrannHammer.WebScraping.Images
{
    public class DefaultImageScrapingProvider : IImageScrapingProvider
    {
        public HttpClient CreateHttpClient() => new HttpClient();
        public MemoryStream CreateMemoryStream() => new MemoryStream();
        public Bitmap CreateBitmap(Stream stream) => new Bitmap(stream);
    }
}
