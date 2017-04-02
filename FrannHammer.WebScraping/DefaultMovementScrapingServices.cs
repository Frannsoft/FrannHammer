using System;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;

namespace FrannHammer.WebScraping
{
    public class DefaultMoveScrapingServices : IMoveScrapingServices
    {
        public IPageDownloader PageDownloader { get; }
        public IWebClientProvider WebClientProvider { get; }
        public IHtmlParserProvider HtmlParserProvider { get; }
        public IMoveProvider MoveProvider { get; }

        public DefaultMoveScrapingServices(IHtmlParserProvider htmlParserProvider, IMoveProvider moveProvider, IPageDownloader pageDownloader, IWebClientProvider webClientProvider)
        {
            Guard.VerifyObjectNotNull(htmlParserProvider, nameof(htmlParserProvider));
            Guard.VerifyObjectNotNull(moveProvider, nameof(moveProvider));
            Guard.VerifyObjectNotNull(pageDownloader, nameof(pageDownloader));
            Guard.VerifyObjectNotNull(webClientProvider, nameof(webClientProvider));

            HtmlParserProvider = htmlParserProvider;
            MoveProvider = moveProvider;
            PageDownloader = pageDownloader;
            WebClientProvider = webClientProvider;
        }

        public string DownloadPageSource(Uri uri) => PageDownloader.DownloadPageSource(uri, WebClientProvider);
        public IHtmlParser CreateHtmlParser(string html) => HtmlParserProvider.Create(html);
        public IMove CreateMove() => MoveProvider.Create();
    }

    public class DefaultMovementScrapingServices : IMovementScrapingServices
    {
        public IPageDownloader PageDownloader { get; }
        public IWebClientProvider WebClientProvider { get; }
        public IHtmlParserProvider HtmlParserProvider { get; }
        public IMovementProvider MovementProvider { get; }

        public DefaultMovementScrapingServices(IHtmlParserProvider htmlParserProvider, IMovementProvider movementProvider, IPageDownloader pageDownloader, IWebClientProvider webClientProvider)
        {
            Guard.VerifyObjectNotNull(htmlParserProvider, nameof(htmlParserProvider));
            Guard.VerifyObjectNotNull(movementProvider, nameof(movementProvider));
            Guard.VerifyObjectNotNull(pageDownloader, nameof(pageDownloader));
            Guard.VerifyObjectNotNull(webClientProvider, nameof(webClientProvider));

            HtmlParserProvider = htmlParserProvider;
            MovementProvider = movementProvider;
            PageDownloader = pageDownloader;
            WebClientProvider = webClientProvider;
        }

        public string DownloadPageSource(Uri uri) => PageDownloader.DownloadPageSource(uri, WebClientProvider);
        public IHtmlParser CreateHtmlParser(string html) => HtmlParserProvider.Create(html);
        public IMovement CreateMovement() => MovementProvider.Create();
    }
}