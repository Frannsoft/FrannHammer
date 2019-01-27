using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Attributes;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Attributes
{
    public static class AttributeScrapers
    {
        public static IEnumerable<AttributeScraper> AllWithScrapingServices(IAttributeScrapingServices scrapingServices, string baseUrl = "")
        {
            Guard.VerifyObjectNotNull(scrapingServices, nameof(scrapingServices));
            return new List<AttributeScraper>
            {
                new AerialJumpScraper(scrapingServices, baseUrl),
                new CounterScraper(scrapingServices, baseUrl),
                new AirAccelerationScraper(scrapingServices, baseUrl),
                new AirFrictionScraper(scrapingServices, baseUrl),
                new DashLengthScraper(scrapingServices, baseUrl),
                new FallSpeedScraper(scrapingServices, baseUrl),
                new FullHopScraper(scrapingServices, baseUrl),
                new GravityScraper(scrapingServices, baseUrl),
                new JumpSquatScraper(scrapingServices, baseUrl),
                new LedgeHopScraper(scrapingServices, baseUrl),
                new ShortHopScraper(scrapingServices, baseUrl),
                new SpotdodgeScraper(scrapingServices, baseUrl),
                new TractionScraper(scrapingServices, baseUrl),
                new WalkSpeedScraper(scrapingServices, baseUrl),
                new AirSpeedScraper(scrapingServices, baseUrl),
                new AirDodgeScraper(scrapingServices, baseUrl),
                new RollScraper(scrapingServices, baseUrl),
                new RunSpeedScraper(scrapingServices, baseUrl),
                new ShieldSizeScraper(scrapingServices, baseUrl),
                new ReflectorScraper(scrapingServices, baseUrl)
            };
        }
    }
}
