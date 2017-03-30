using System.Collections.Generic;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;

namespace FrannHammer.WebScraping
{
    public class HtmlParser : IHtmlParser
    {
        private readonly IHtmlDoc _document;

        public HtmlParser(string html, IHtmlDocProvider htmlDocProvider)
        {
            Guard.VerifyStringIsNotNullOrEmpty(html, nameof(html));
            Guard.VerifyObjectNotNull(htmlDocProvider, nameof(htmlDocProvider));

            _document = htmlDocProvider.Create(html);
        }

        public IEnumerable<string> GetCollection(string xpath)
        {
            Guard.VerifyStringIsNotNullOrEmpty(xpath, nameof(xpath));
            return _document?.SelectHtmlFromManyNodes(xpath);
        }

        public string GetSingle(string xpath)
        {
            Guard.VerifyStringIsNotNullOrEmpty(xpath, nameof(xpath));
            return _document?.SelectSingleNodeHtml(xpath);
        }

        public string GetAttributeFromSingleNavigable(string attributeName, string xpath)
        {
            Guard.VerifyStringIsNotNullOrEmpty(xpath, nameof(xpath));
            return _document?.GetAttributeFromSingleNavigable(attributeName, xpath);
        }

    }
}
