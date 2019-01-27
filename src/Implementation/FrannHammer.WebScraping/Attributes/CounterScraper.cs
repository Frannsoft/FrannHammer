using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class CounterScraper : AttributeScraper
    {
        public override string AttributeName => "Counters";

        public CounterScraper(IAttributeScrapingServices scrapingServices, string baseUrl)
            : base(baseUrl, scrapingServices)
        { }
    }
}
