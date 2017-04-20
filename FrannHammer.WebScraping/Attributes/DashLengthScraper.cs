using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class DashLengthScraper : AttributeScraper
    {
        public override string AttributeName => "DashLength";

        public DashLengthScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/DashLength", scrapingServices)
        { }
    }
}
