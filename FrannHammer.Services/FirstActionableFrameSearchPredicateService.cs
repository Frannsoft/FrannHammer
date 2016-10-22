using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class FirstActionableFrameSearchPredicateService : SearchPredicateService
    {
        public Func<Move, bool> GetFirstActionableFrameSearchPredicate(RangeModel firstActionableFrameRange)
           => move => IsValueInRange(move.FirstActionableFrame, firstActionableFrameRange);
    }
}
