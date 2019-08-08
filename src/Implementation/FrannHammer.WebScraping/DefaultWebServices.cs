using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.Contracts.PageDownloading;
using FrannHammer.WebScraping.Contracts.WebClients;
using FrannHammer.WebScraping.PageDownloading;
using System;
using System.Collections.Generic;

namespace FrannHammer.WebScraping
{
    public class DefaultWebServices : IWebServices
    {
        private readonly IHtmlParserProvider _htmlParserProvider;
        private readonly IWebClientProvider _webClientProvider;
        private readonly IPageDownloader _pageDownloader;

        private readonly IDictionary<string, CacheableUrlResponse> _cachedUrls;

        private class CacheableUrlResponse
        {
            public string PageSource { get; set; }
            public Exception ResponseException { get; set; }
        }

        public DefaultWebServices(IHtmlParserProvider htmlParserProvider, IWebClientProvider webClientProvider, IPageDownloader pageDownloader)
        {
            Guard.VerifyObjectNotNull(htmlParserProvider, nameof(htmlParserProvider));
            Guard.VerifyObjectNotNull(webClientProvider, nameof(webClientProvider));
            Guard.VerifyObjectNotNull(pageDownloader, nameof(pageDownloader));

            _htmlParserProvider = htmlParserProvider;
            _webClientProvider = webClientProvider;
            _pageDownloader = pageDownloader;

            _cachedUrls = new Dictionary<string, CacheableUrlResponse>();
        }

        public IHtmlParser CreateParserFromSourceUrl(string url)
        {
            var htmlParser = default(IHtmlParser);

            //if the url has already been downloaded once we store it in memory for this instance of the default web services.
            //helps avoid multiple downloads of a file that likely hasn't changed in this short time span.
            if (!_cachedUrls.ContainsKey(url))
            {
                try
                {
                    string pageSource = _pageDownloader.DownloadPageSource(new Uri(url), _webClientProvider);
                    _cachedUrls[url] = new CacheableUrlResponse
                    {
                        PageSource = pageSource
                    };
                    htmlParser = _htmlParserProvider.Create(_cachedUrls[url].PageSource);
                }
                catch (PageNotFoundException ex)
                {
                    var cacheableResponse = new CacheableUrlResponse
                    {
                        PageSource = string.Empty,
                        ResponseException = ex
                    };
                    _cachedUrls[url] = cacheableResponse;
                    throw _cachedUrls[url].ResponseException;
                }
            }
            else
            {
                var cachedValue = _cachedUrls[url];
                if (cachedValue.ResponseException != null)
                {
                    throw cachedValue.ResponseException;
                }
                else
                {
                    htmlParser = _htmlParserProvider.Create(cachedValue.PageSource);
                }
            }

            return htmlParser;
        }
    }
}
