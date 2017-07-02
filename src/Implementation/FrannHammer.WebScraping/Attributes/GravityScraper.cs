using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class GravityScraper : AttributeScraper
    {
        public override string AttributeName => "Gravity";

        public GravityScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/Gravity", scrapingServices)
        { }
    }
}
