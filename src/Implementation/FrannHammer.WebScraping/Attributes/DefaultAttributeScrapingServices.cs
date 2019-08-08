using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Contracts.HtmlParsing;

namespace FrannHammer.WebScraping.Attributes
{
    public class DefaultAttributeScrapingServices : IAttributeScrapingServices
    {
        private readonly IAttributeProvider _attributeProvider;
        private readonly IWebServices _webServices;

        public DefaultAttributeScrapingServices(IAttributeProvider attributeProvider, IWebServices webServices)
        {
            Guard.VerifyObjectNotNull(attributeProvider, nameof(attributeProvider));
            _attributeProvider = attributeProvider;
            _webServices = webServices;
        }

        public IAttribute CreateAttribute() => _attributeProvider.CreateAttribute();
        public ICharacterAttributeRow CreateCharacterAttributeRow() => _attributeProvider.CreateCharacterAttributeRow();
        public IHtmlParser CreateParserFromSourceUrl(string url)
        {
            return _webServices.CreateParserFromSourceUrl(url);
        }
    }
}
