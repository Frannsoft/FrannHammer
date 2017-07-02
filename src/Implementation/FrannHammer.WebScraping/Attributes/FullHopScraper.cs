using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class FullHopScraper : AttributeScraper
    {
        public override string AttributeName => "FullHop";

        public FullHopScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/FullHop", scrapingServices)
        { }
    }
}
