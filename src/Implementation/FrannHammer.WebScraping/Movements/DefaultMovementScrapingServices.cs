using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.Contracts.Movements;

namespace FrannHammer.WebScraping.Movements
{
    public class DefaultMovementScrapingServices : IMovementScrapingServices
    {
        private readonly IMovementProvider _movementProvider;

        private readonly IWebServices _webServices;

        public DefaultMovementScrapingServices(IMovementProvider movementProvider, IWebServices webServices)
        {
            Guard.VerifyObjectNotNull(movementProvider, nameof(movementProvider));

            _movementProvider = movementProvider;
            _webServices = webServices;
        }

        public IMovement CreateMovement() => _movementProvider.Create();
        public IHtmlParser CreateParserFromSourceUrl(string url)
        {
            return _webServices.CreateParserFromSourceUrl(url);
        }
    }
}