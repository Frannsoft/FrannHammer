using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class WalkSpeedScraper : AttributeScraper
    {
        public override string AttributeName => "WalkSpeed";

        public WalkSpeedScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/WalkSpeed", scrapingServices)
        { }
    }
}
