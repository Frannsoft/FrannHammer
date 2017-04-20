using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class TractionScraper : AttributeScraper
    {
        public override string AttributeName => "Traction";

        public TractionScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/Traction", scrapingServices)
        { }
    }
}
