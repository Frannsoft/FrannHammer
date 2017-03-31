using System;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IScrapingServices
    {
        IPageDownloader PageDownloader { get; }
        IWebClientProvider WebClientProvider { get; }
        IHtmlParserProvider HtmlParserProvider { get; }
        IAttributeProvider AttributeProvider { get; }

        string DownloadPageSource(Uri uri);
        IHtmlParser CreateHtmlParser(string html);
        IAttribute CreateAttribute();
    }
}
