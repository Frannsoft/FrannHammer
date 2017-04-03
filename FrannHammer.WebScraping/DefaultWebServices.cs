using System;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.Contracts.PageDownloading;
using FrannHammer.WebScraping.Contracts.WebClients;

namespace FrannHammer.WebScraping
{
    public class DefaultWebServices : IWebServices
    {
        private readonly IHtmlParserProvider _htmlParserProvider;
        private readonly IWebClientProvider _webClientProvider;
        private readonly IPageDownloader _pageDownloader;

        public DefaultWebServices(IHtmlParserProvider htmlParserProvider, IWebClientProvider webClientProvider, IPageDownloader pageDownloader)
        {
            Guard.VerifyObjectNotNull(htmlParserProvider, nameof(htmlParserProvider));
            Guard.VerifyObjectNotNull(webClientProvider, nameof(webClientProvider));
            Guard.VerifyObjectNotNull(pageDownloader, nameof(pageDownloader));

            _htmlParserProvider = htmlParserProvider;
            _webClientProvider = webClientProvider;
            _pageDownloader = pageDownloader;
        }

        public IHtmlParser CreateParserFromSourceUrl(string url) =>
            _htmlParserProvider.Create(_pageDownloader.DownloadPageSource(new Uri(url), _webClientProvider));
    }
}
