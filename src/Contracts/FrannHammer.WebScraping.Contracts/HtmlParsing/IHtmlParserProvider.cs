namespace FrannHammer.WebScraping.Contracts.HtmlParsing
{
    public interface IHtmlParserProvider
    {
        IHtmlParser Create(string html);
    }
}
