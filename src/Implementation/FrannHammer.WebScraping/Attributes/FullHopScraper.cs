using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class FullHopScraper : AttributeScraper
    {
        public override string AttributeName => "FullHop";

        public FullHopScraper(IAttributeScrapingServices scrapingServices, string baseUrl)
            : base(baseUrl, scrapingServices)
        { }
    }
}
