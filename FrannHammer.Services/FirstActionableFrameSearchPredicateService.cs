using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class FirstActionableFrameSearchPredicateService : SearchPredicateService
    {
        public Func<Move, bool> GetFirstActionableFrameSearchPredicate(RangeModel firstActionableFrameRange)
        {
            if(firstActionableFrameRange == null)
            { return null; }

            return move => IsValueInRange(move.FirstActionableFrame, firstActionableFrameRange);
        }
    }
}
