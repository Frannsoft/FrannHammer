using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Attributes;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Attributes
{
    public static class AttributeScrapers
    {
        public const string AerialJump = "AerialJump";
        public const string AirAcceleration = "AirAcceleration";
        public const string AirDodge = "AirDodge";
        public const string AirFriction = "AirFriction";
        public const string AirSpeed = "AirSpeed";
        public const string Counters = "Counters";
        public const string FallSpeed = "FallSpeed";
        public const string FullHop = "FullHop";
        public const string Gravity = "Gravity";
        public const string JumpSquat = "JumpSquat";
        public const string LedgeHop = "LedgeHop";
        public const string Reflector = "Reflectors";
        public const string Rolls = "Rolls";
        public const string RunSpeed = "RunSpeed";
        public const string ShieldSize = "ShieldSize";
        public const string ShortHop = "ShortHop";
        public const string Trip = "Trip";
        public const string SoftTrip = "SoftTrip";
        public const string HardTrip = "HardTrip";
        public const string Spotdodge = "Spotdodge";
        public const string Traction = "Traction";
        public const string WalkSpeed = "WalkSpeed";
        public const string Weight = "Weight";
        public const string GetUpStand = "GetUpStand";
        public const string GetUpStandOnBack = "GetUp On Back";
        public const string GetUpStandOnFront = "GetUp On Front";
        public const string GetUpBackRoll = "GetUpBackRoll";
        public const string GetUpBackRollBack = "GetUp Back Roll Back";
        public const string GetUpBackRollFront = "GetUp Back Roll Front";
        public const string GetUpForwardRoll = "GetUpForwardRoll";
        public const string GetUpForwardRollBack = "GetUp Forward Roll Back";
        public const string GetUpForwardRollFront = "GetUp Forward Roll Front";
        public const string Tech = "Tech";
        public const string TechFrameData = "Tech Frame Data";
        public const string TechRollForward = "Tech Roll Forward";
        public const string TechRollBackward = "Tech Roll Backward";
        public const string LedgeAttack = "LedgeAttack";
        public const string LedgeGetUp = "LedgeGetUp";
        public const string LedgeRoll = "LedgeRoll";
        public const string LedgeJump = "LedgeJump";
        public const string JabLock = "JabLock";
        public const string JabLockFront = "Jab Lock Front";
        public const string JabLockBack = "Jab Lock Back";

        public static IEnumerable<AttributeScraper> AllWithScrapingServices(IAttributeScrapingServices scrapingServices, string baseUrl = "")
        {
            Guard.VerifyObjectNotNull(scrapingServices, nameof(scrapingServices));
            return new List<AttributeScraper>
            {
                //new
                new AttributeScraper(baseUrl, scrapingServices, AerialJump),
                new AttributeScraper(baseUrl, scrapingServices, AirAcceleration),
                new AttributeScraper(baseUrl, scrapingServices, AirDodge),
                new AttributeScraper(baseUrl, scrapingServices, AirFriction),
                new AttributeScraper(baseUrl, scrapingServices, AirSpeed),
                new AttributeScraper(baseUrl, scrapingServices, Counters),
                new AttributeScraper(baseUrl, scrapingServices, FallSpeed),
                new AttributeScraper(baseUrl, scrapingServices, FullHop),
                new AttributeScraper(baseUrl, scrapingServices, Gravity),
                new AttributeScraper(baseUrl, scrapingServices, JumpSquat),
                new AttributeScraper(baseUrl, scrapingServices, LedgeHop),
                new AttributeScraper(baseUrl, scrapingServices, Reflector),
                new AttributeScraper(baseUrl, scrapingServices, Rolls),
                new AttributeScraper(baseUrl, scrapingServices, RunSpeed),
                new AttributeScraper(baseUrl, scrapingServices, Trip, HardTrip, ScrapingConstants.XPathTableNodeAttributesFirstTable),
                new AttributeScraper(baseUrl, scrapingServices, Trip, SoftTrip, ScrapingConstants.XPathTableNodeAttributesSecondTable),
                new AttributeScraper(baseUrl, scrapingServices, GetUpStand, GetUpStandOnBack, ScrapingConstants.XPathTableNodeAttributesFirstTable),
                new AttributeScraper(baseUrl, scrapingServices, GetUpStand, GetUpStandOnFront, ScrapingConstants.XPathTableNodeAttributesSecondTable),
                new AttributeScraper(baseUrl, scrapingServices, Traction),
                new AttributeScraper(baseUrl, scrapingServices, WalkSpeed),
                new AttributeScraper(baseUrl, scrapingServices, Weight),
                new AttributeScraper(baseUrl, scrapingServices, AerialJump)
            };
        }
    }
}
