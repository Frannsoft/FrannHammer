using FrannHammer.WebScraping.Contracts.HtmlParsing;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IWebServices
    {
        IHtmlParser CreateParserFromSourceUrl(string url);
    }
}
