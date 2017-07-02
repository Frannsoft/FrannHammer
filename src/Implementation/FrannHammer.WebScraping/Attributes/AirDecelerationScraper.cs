using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class AirDecelerationScraper : AttributeScraper
    {
        public override string AttributeName => "AirDeceleration";

        public AirDecelerationScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/AirDeceleration", scrapingServices)
        { }
    }
}
