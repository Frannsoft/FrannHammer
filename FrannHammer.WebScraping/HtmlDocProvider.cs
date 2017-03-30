using FrannHammer.WebScraping.Contracts;

namespace FrannHammer.WebScraping
{
    public class HtmlDocProvider : IHtmlDocProvider
    {
        public IHtmlDoc Create(string html) => new HtmlDoc(html);
    }
}
