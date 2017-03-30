using System.Net;

namespace FrannHammer.WebScraping
{
    public interface IWebClientProvider
    {
        WebClient Create();
    }
}