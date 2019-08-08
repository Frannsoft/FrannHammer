using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.HtmlDocs;

namespace FrannHammer.WebScraping.HtmlParsing
{
    public class DefaultHtmlParserProvider : IHtmlParserProvider
    {
        public IHtmlParser Create(string html)
        {
            Guard.VerifyStringIsNotNullOrEmpty(html, nameof(html));
            return new HtmlParser(html, new HtmlDocProvider());
        }
    }
}
