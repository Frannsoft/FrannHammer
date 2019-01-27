using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class ReflectorScraper : AttributeScraper
    {
        public override string AttributeName => "Reflectors";

        public ReflectorScraper(IAttributeScrapingServices scrapingServices, string baseUrl)
            : base(baseUrl, scrapingServices)
        {
        }
    }
}