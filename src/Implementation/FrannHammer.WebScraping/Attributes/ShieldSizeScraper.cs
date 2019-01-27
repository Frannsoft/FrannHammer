using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class ShieldSizeScraper : AttributeScraper
    {
        public override string AttributeName => "ShieldSize";

        public ShieldSizeScraper(IAttributeScrapingServices scrapingServices, string baseUrl)
            : base(baseUrl, scrapingServices)
        { }
    }
}