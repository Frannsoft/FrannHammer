using System;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;

namespace FrannHammer.WebScraping
{
    public class DefaultScrapingServices : IScrapingServices
    {
        public IPageDownloader PageDownloader { get; }
        public IWebClientProvider WebClientProvider { get; }
        public IHtmlParserProvider HtmlParserProvider { get; }
        public IAttributeProvider AttributeProvider { get; }

        public DefaultScrapingServices(IHtmlParserProvider htmlParserProvider, IAttributeProvider attributeProvider, IPageDownloader pageDownloader, IWebClientProvider webClientProvider)
        {
            Guard.VerifyObjectNotNull(htmlParserProvider, nameof(htmlParserProvider));
            Guard.VerifyObjectNotNull(attributeProvider, nameof(attributeProvider));
            Guard.VerifyObjectNotNull(pageDownloader, nameof(pageDownloader));
            Guard.VerifyObjectNotNull(webClientProvider, nameof(webClientProvider));

            HtmlParserProvider = htmlParserProvider;
            AttributeProvider = attributeProvider;
            PageDownloader = pageDownloader;
            WebClientProvider = webClientProvider;
        }

        public string DownloadPageSource(Uri uri) => PageDownloader.DownloadPageSource(uri, WebClientProvider);

        public IHtmlParser CreateHtmlParser(string html) => HtmlParserProvider.Create(html);

        public IAttribute CreateAttribute() => AttributeProvider.CreateAttribute();
    }
}
