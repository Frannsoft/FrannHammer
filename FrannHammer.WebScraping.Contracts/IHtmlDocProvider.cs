namespace FrannHammer.WebScraping.Contracts
{
    public interface IHtmlDocProvider
    {
        IHtmlDoc Create(string html);
    }
}
