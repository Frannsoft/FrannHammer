using System.Net;

namespace FrannHammer.WebScraping.Contracts.WebClients
{
    public interface IWebClientProvider
    {
        WebClient Create();
    }
}