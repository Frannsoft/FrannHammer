using System.Drawing;
using System.IO;
using System.Net.Http;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IImageScrapingProvider
    {
        HttpClient CreateHttpClient();
        MemoryStream CreateMemoryStream();
        Bitmap CreateBitmap(Stream stream);
    }
}
