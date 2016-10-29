using System;
using FrannHammer.Core;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class HitboxActiveLengthSearchPredicateService : SearchPredicateService
    {
        public Func<Hitbox, bool> GetHitboxActiveLengthPredicate(RangeModel hitboxActiveLengthFrameRange)
        {
            if (hitboxActiveLengthFrameRange == null)
            { return null; }

            return h => IsValueInRange(h.Hitbox1, hitboxActiveLengthFrameRange) ||
                 IsValueInRange(h.Hitbox2, hitboxActiveLengthFrameRange) ||
                 IsValueInRange(h.Hitbox3, hitboxActiveLengthFrameRange) ||
                 IsValueInRange(h.Hitbox4, hitboxActiveLengthFrameRange) ||
                 IsValueInRange(h.Hitbox5, hitboxActiveLengthFrameRange) ||
                 IsValueInRange(h.Hitbox6, hitboxActiveLengthFrameRange);
        }

        protected internal override bool IsValueInRange(string raw, RangeModel frameRange)
        {
            Guard.VerifyObjectNotNull(frameRange, nameof(frameRange));

            var dataParsingService = new DataParsingService();

            var dbNumberRanges = dataParsingService.Parse(raw);

            bool retVal = false;
            if (dbNumberRanges.Count > 0)
            {
                foreach (var dbNumberRange in dbNumberRanges)
                {
                    int lengthOfHitbox = GetNumberRangeDifference(dbNumberRange);
                    retVal = RangeMatchProcessingService.Check(frameRange, new NumberRange(lengthOfHitbox));
                }
            }
            return retVal;
        }

        private int GetNumberRangeDifference(NumberRange dbNumberRange)
        {
            if (dbNumberRange.End.HasValue)
            {
                return dbNumberRange.End.Value - dbNumberRange.Start;
            }
            else
            {
                return 0; //no range
            }
        }
    }
}
