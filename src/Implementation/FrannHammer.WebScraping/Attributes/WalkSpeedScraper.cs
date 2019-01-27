using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class WalkSpeedScraper : AttributeScraper
    {
        public override string AttributeName => "WalkSpeed";

        public WalkSpeedScraper(IAttributeScrapingServices scrapingServices, string baseUrl)
            : base(baseUrl, scrapingServices)
        { }
    }
}
