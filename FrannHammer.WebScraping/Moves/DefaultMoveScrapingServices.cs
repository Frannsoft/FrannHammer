using System;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Contracts.PageDownloading;
using FrannHammer.WebScraping.Contracts.WebClients;

namespace FrannHammer.WebScraping.Moves
{
    public class DefaultMoveScrapingServices : IMoveScrapingServices
    {
        private readonly IMoveProvider _moveProvider;

        private readonly IWebServices _webServices;

        public DefaultMoveScrapingServices(IMoveProvider moveProvider, IWebServices webServices)
        {
            Guard.VerifyObjectNotNull(moveProvider, nameof(moveProvider));
            _moveProvider = moveProvider;
            _webServices = webServices;
        }

        public IMove CreateMove() => _moveProvider.Create();
        public IHtmlParser CreateParserFromSourceUrl(string url)
        {
            return _webServices.CreateParserFromSourceUrl(url);
        }
    }
}