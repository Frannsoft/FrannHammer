using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class WeightScraper : AttributeScraper
    {
        public override string AttributeName => "Weight";

        public WeightScraper(IAttributeScrapingServices scrapingServices, string baseUrl)
            : base(baseUrl, scrapingServices)
        { }
    }
}
