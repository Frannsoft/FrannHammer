using System;
using FrannHammer.Models;

namespace FrannHammer.Services.MoveSearch
{
    public class HitboxStartupSearchPredicateService : SearchPredicateService
    {
        public HitboxStartupSearchPredicateService()
        {
            RangeMatchProcessingService.ConfigureIsBetweenCheck(
                        (procService, frameRange, numberRange) =>
                            procService.IsBetween(frameRange.StartValue, numberRange) ||
                            procService.IsBetween(frameRange.EndValue, numberRange));
        }

        public Func<Hitbox, bool> GetHitboxStartupPredicate(RangeModel hitboxStartupFrame)
           => h => IsValueInRange(h.Hitbox1, hitboxStartupFrame) ||
               IsValueInRange(h.Hitbox2, hitboxStartupFrame) ||
               IsValueInRange(h.Hitbox3, hitboxStartupFrame) ||
               IsValueInRange(h.Hitbox4, hitboxStartupFrame) ||
               IsValueInRange(h.Hitbox5, hitboxStartupFrame) ||
               IsValueInRange(h.Hitbox6, hitboxStartupFrame);
    }
}
