using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.Contracts.UniqueData;

namespace FrannHammer.WebScraping.Unique
{
    public class DefaultUniqueDataScrapingServices : IUniqueDataScrapingServices
    {
        private readonly IWebServices _webServices;
        private readonly IUniqueDataProvider _uniqueDataProvider;

        public DefaultUniqueDataScrapingServices(IUniqueDataProvider uniqueDataProvider, IWebServices webServices)
        {
            Guard.VerifyObjectNotNull(uniqueDataProvider, nameof(uniqueDataProvider));
            Guard.VerifyObjectNotNull(webServices, nameof(webServices));

            _uniqueDataProvider = uniqueDataProvider;
            _webServices = webServices;
        }

        public IHtmlParser CreateParserFromSourceUrl(string url) => _webServices.CreateParserFromSourceUrl(url);
        public IUniqueData Create() => _uniqueDataProvider.Create();
    }
}
