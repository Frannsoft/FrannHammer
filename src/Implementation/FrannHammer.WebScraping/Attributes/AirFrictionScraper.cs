using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class AirFrictionScraper : AttributeScraper
    {
        public override string AttributeName => "AirFriction";

        public AirFrictionScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/AirFriction", scrapingServices)
        { }
    }
}
