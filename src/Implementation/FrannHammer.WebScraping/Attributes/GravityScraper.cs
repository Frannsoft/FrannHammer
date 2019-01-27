using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class GravityScraper : AttributeScraper
    {
        public override string AttributeName => "Gravity";

        public GravityScraper(IAttributeScrapingServices scrapingServices, string baseUrl)
            : base(baseUrl, scrapingServices)
        { }
    }
}
