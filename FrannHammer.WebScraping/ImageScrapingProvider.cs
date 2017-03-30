using System.Drawing;
using System.IO;
using System.Net.Http;
using FrannHammer.WebScraping.Contracts;

namespace FrannHammer.WebScraping
{
    public class ImageScrapingProvider : IImageScrapingProvider
    {
        public HttpClient CreateHttpClient() => new HttpClient();
        public MemoryStream CreateMemoryStream() => new MemoryStream();
        public Bitmap CreateBitmap(Stream stream) => new Bitmap(stream);
    }
}
