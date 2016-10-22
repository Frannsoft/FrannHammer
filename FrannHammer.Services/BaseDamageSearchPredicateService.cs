using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class BaseDamageSearchPredicateService : SearchPredicateService
    {
        public Func<BaseDamage, bool> GetBaseDamagePredicate(RangeModel hitboxStartupFrame)
           => baseDamage => IsValueInRange(baseDamage.Hitbox1, hitboxStartupFrame) ||
               IsValueInRange(baseDamage.Hitbox2, hitboxStartupFrame) ||
               IsValueInRange(baseDamage.Hitbox3, hitboxStartupFrame) ||
               IsValueInRange(baseDamage.Hitbox4, hitboxStartupFrame) ||
               IsValueInRange(baseDamage.Hitbox5, hitboxStartupFrame) ||
               IsValueInRange(baseDamage.Hitbox6, hitboxStartupFrame);
    }
}
