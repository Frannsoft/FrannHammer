using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class AirDodgeScraper : AttributeScraper
    {
        public override string AttributeName => "AirDodge";

        public AirDodgeScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/Airdodge", scrapingServices)
        { }
    }
}