using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class FallSpeedScraper : AttributeScraper
    {
        public override string AttributeName => "FallSpeed";

        public FallSpeedScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/FallSpeed", scrapingServices)
        { }

    }
}
