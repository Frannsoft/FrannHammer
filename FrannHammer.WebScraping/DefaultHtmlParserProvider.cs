using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;

namespace FrannHammer.WebScraping
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
