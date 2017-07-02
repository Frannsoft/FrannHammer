using System.Collections.Generic;
using System.Linq;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.HtmlDocs;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping.HtmlDocs
{
    public class HtmlDoc : IHtmlDoc
    {
        private readonly HtmlDocument _agilityDoc;

        public HtmlDoc(string html)
        {
            Guard.VerifyStringIsNotNullOrEmpty(html, nameof(html));

            _agilityDoc = new HtmlDocument();
            _agilityDoc.LoadHtml(html);
        }

        public IEnumerable<string> SelectHtmlFromManyNodes(string xpath)
        {
            Guard.VerifyStringIsNotNullOrEmpty(xpath, nameof(xpath));
            return _agilityDoc.DocumentNode.SelectNodes(xpath)?.Select(node => node.OuterHtml);
        }

        public string SelectSingleNodeHtml(string xpath)
        {
            Guard.VerifyStringIsNotNullOrEmpty(xpath, nameof(xpath));

            var node = _agilityDoc.DocumentNode.SelectSingleNode(xpath);
            return node?.OuterHtml;
        }

        public string GetAttributeFromSingleNavigable(string attributeName, string xpath)
        {
            Guard.VerifyStringIsNotNullOrEmpty(xpath, nameof(xpath));
            return _agilityDoc.DocumentNode.SelectSingleNode(xpath)?.Attributes[attributeName].Value;
        }
    }
}