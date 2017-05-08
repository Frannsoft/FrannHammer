using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class JumpSquatScraper : AttributeScraper
    {
        public override string AttributeName => "JumpSquat";

        public JumpSquatScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/Jumpsquat", scrapingServices)
        { }
    }
}
