using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class RollScraper : AttributeScraper
    {
        public override string AttributeName => "Rolls";

        public RollScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/Rolls", scrapingServices)
        { }
    }
}