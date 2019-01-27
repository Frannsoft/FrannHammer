using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class FallSpeedScraper : AttributeScraper
    {
        public override string AttributeName => "FallSpeed";

        public FallSpeedScraper(IAttributeScrapingServices scrapingServices, string baseUrl)
            : base(baseUrl, scrapingServices)
        { }

    }
}
