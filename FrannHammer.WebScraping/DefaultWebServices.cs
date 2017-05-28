using System;
using System.Collections.Generic;
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

        private readonly IDictionary<string, string> _cachedUrls;

        public DefaultWebServices(IHtmlParserProvider htmlParserProvider, IWebClientProvider webClientProvider, IPageDownloader pageDownloader)
        {
            Guard.VerifyObjectNotNull(htmlParserProvider, nameof(htmlParserProvider));
            Guard.VerifyObjectNotNull(webClientProvider, nameof(webClientProvider));
            Guard.VerifyObjectNotNull(pageDownloader, nameof(pageDownloader));

            _htmlParserProvider = htmlParserProvider;
            _webClientProvider = webClientProvider;
            _pageDownloader = pageDownloader;

            _cachedUrls = new Dictionary<string, string>();
        }

        public IHtmlParser CreateParserFromSourceUrl(string url)
        {
            //if the url has already been downloaded once we store it in memory for this instance of the default web services.
            //helps avoid multiple downloads of a file that likely hasn't changed in this short time span.
            if (!_cachedUrls.ContainsKey(url))
            {
                _cachedUrls[url] = _pageDownloader.DownloadPageSource(new Uri(url), _webClientProvider);
            }
            return _htmlParserProvider.Create(_cachedUrls[url]);
        }
    }
}
