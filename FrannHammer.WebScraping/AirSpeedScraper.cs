using FrannHammer.WebScraping.Contracts;

namespace FrannHammer.WebScraping
{
    public class AirSpeedScraper : AttributeScraper
    {
        public override string AttributeName => "AirSpeed";

        public AirSpeedScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/AirSpeed", scrapingServices)
        { }
    }

    public class AirDodgeScraper : AttributeScraper
    {
        public override string AttributeName => "AirDodge";

        public AirDodgeScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/Airdodge", scrapingServices)
        { }
    }
}
