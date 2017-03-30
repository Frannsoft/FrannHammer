namespace FrannHammer.WebScraping.Contracts
{
    public interface IHtmlParserProvider
    {
        IHtmlParser Create(string html);
    }
}
