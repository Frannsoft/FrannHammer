using System;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IMoveScrapingServices : IScrapingServices
    {
        IMove CreateMove();
        IMoveProvider MoveProvider { get; }
    }

    public interface IMovementScrapingServices : IScrapingServices
    {
        IMovement CreateMovement();
        IMovementProvider MovementProvider { get; }
    }

    public interface IAttributeScrapingServices : IScrapingServices
    {
        IAttribute CreateAttribute();
        IAttributeProvider AttributeProvider { get; }
    }

    public interface IScrapingServices
    {
        IPageDownloader PageDownloader { get; }
        IWebClientProvider WebClientProvider { get; }
        IHtmlParserProvider HtmlParserProvider { get; }

        IHtmlParser CreateHtmlParser(string html);
        string DownloadPageSource(Uri uri);
    }
}
