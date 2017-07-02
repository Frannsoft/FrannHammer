using FrannHammer.WebScraping.Contracts.HtmlDocs;

namespace FrannHammer.WebScraping.HtmlDocs
{
    public class HtmlDocProvider : IHtmlDocProvider
    {
        public IHtmlDoc Create(string html) => new HtmlDoc(html);
    }
}
