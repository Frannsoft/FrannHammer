using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class SpotdodgeScraper : AttributeScraper
    {
        public override string AttributeName => "Spotdodge";

        public SpotdodgeScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/Spotdodge", scrapingServices)
        { }
    }
}
