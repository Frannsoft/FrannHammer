using System.Net;
using FrannHammer.WebScraping.Contracts.WebClients;

namespace FrannHammer.WebScraping.WebClients
{
    public class DefaultWebClientProvider : IWebClientProvider
    {
        public WebClient Create() => new WebClient();
    }
}
