namespace FrannHammer.WebScraping.Contracts.HtmlDocs
{
    public interface IHtmlDocProvider
    {
        IHtmlDoc Create(string html);
    }
}
