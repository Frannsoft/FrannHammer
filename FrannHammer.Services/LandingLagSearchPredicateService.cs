using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class LandingLagSearchPredicateService : SearchPredicateService
    {
        public Func<LandingLag, bool> GetLandingLagSearchPredicate(RangeModel landingLagFrameRange)
            => landingLag => IsValueInRange(landingLag.Frames.ToString(), landingLagFrameRange);
    }
}
