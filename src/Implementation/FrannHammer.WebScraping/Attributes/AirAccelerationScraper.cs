using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class AirAccelerationScraper : AttributeScraper
    {
        public override string AttributeName => "AirAcceleration";

        public AirAccelerationScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/AirAcceleration", scrapingServices)
        { }
    }
}
