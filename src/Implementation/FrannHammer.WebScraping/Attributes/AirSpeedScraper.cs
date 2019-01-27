using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class AirSpeedScraper : AttributeScraper
    {
        public override string AttributeName => "AirSpeed";

        public AirSpeedScraper(IAttributeScrapingServices scrapingServices, string baseUrl)
            : base(baseUrl, scrapingServices)
        { }
    }
}
