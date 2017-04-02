using System.Net;

namespace FrannHammer.WebScraping
{
    public class DefaultWebClientProvider : IWebClientProvider
    {
        public WebClient Create() => new WebClient();
    }
}
