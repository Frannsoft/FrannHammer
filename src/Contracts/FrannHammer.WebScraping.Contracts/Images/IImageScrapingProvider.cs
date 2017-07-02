using System.Drawing;
using System.IO;
using System.Net.Http;

namespace FrannHammer.WebScraping.Contracts.Images
{
    public interface IImageScrapingProvider
    {
        HttpClient CreateHttpClient();
        MemoryStream CreateMemoryStream();
        Bitmap CreateBitmap(Stream stream);
    }
}
