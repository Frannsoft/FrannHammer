using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class AerialJumpScraper : AttributeScraper
    {
        public override string AttributeName => "AerialJump";

        public AerialJumpScraper(IAttributeScrapingServices scrapingServices, string baseUrl)
            : base(baseUrl, scrapingServices)
        { }
    }
}
