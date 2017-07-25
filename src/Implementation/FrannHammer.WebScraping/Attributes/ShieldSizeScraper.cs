using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class ShieldSizeScraper : AttributeScraper
    {
        public override string AttributeName => "ShieldSize";

        public ShieldSizeScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/ShieldSize", scrapingServices)
        { }
    }
}