using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class AerialJumpScraper : AttributeScraper
    {
        public override string AttributeName => "AerialJump";

        public AerialJumpScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/AerialJump", scrapingServices)
        { }
    }
}
