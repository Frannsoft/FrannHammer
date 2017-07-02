using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class ShortHopScraper : AttributeScraper
    {
        public override string AttributeName => "ShortHop";

        public ShortHopScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/ShortHop", scrapingServices)
        { }
    }
}
