using System.Net;

namespace FrannHammer.WebScraping
{
    public class WebClientProvider : IWebClientProvider
    {
        public WebClient Create() => new WebClient();
    }
}
