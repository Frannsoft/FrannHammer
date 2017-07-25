using System.Collections.Generic;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public static class AttributeScrapers
    {
        public static IEnumerable<AttributeScraper> AllWithScrapingServices(IAttributeScrapingServices scrapingServices)
        {
            Guard.VerifyObjectNotNull(scrapingServices, nameof(scrapingServices));
            return new List<AttributeScraper>
            {
                new AerialJumpScraper(scrapingServices),
                new CounterScraper(scrapingServices),
                new AirAccelerationScraper(scrapingServices),
                new AirDecelerationScraper(scrapingServices),
                new AirFrictionScraper(scrapingServices),
                new DashLengthScraper(scrapingServices),
                new FallSpeedScraper(scrapingServices),
                new FullHopScraper(scrapingServices),
                new GravityScraper(scrapingServices),
                new JumpSquatScraper(scrapingServices),
                new LedgeHopScraper(scrapingServices),
                new ShortHopScraper(scrapingServices),
                new SpotdodgeScraper(scrapingServices),
                new TractionScraper(scrapingServices),
                new WalkSpeedScraper(scrapingServices),
                new AirSpeedScraper(scrapingServices),
                new AirDodgeScraper(scrapingServices),
                new RollScraper(scrapingServices),
                new RunSpeedScraper(scrapingServices),
                new ShieldSizeScraper(scrapingServices)
            };
        }
    }
}
