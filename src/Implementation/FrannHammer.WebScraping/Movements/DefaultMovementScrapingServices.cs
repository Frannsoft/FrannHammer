using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.Contracts.Movements;

namespace FrannHammer.WebScraping.Movements
{
    public class DefaultMovementScrapingServices : IMovementScrapingServices
    {
        public IMovementProvider MovementProvider { get; }

        private readonly IWebServices _webServices;

        public DefaultMovementScrapingServices(IMovementProvider movementProvider, IWebServices webServices)
        {
            Guard.VerifyObjectNotNull(movementProvider, nameof(movementProvider));

            MovementProvider = movementProvider;
            _webServices = webServices;
        }

        public IMovement CreateMovement() => MovementProvider.Create();
        public IHtmlParser CreateParserFromSourceUrl(string url)
        {
            return _webServices.CreateParserFromSourceUrl(url);
        }
    }
}