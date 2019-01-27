using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using System;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices.MapServices
{
    public class UltimateBaseDamageResourceMapService
    {
        public BaseDamageResource MapFrom(IMove move)
        {
            var baseDamageResource = new BaseDamageResource();

            var parts = move.BaseDamage.Split(new[] { "1v1:" }, StringSplitOptions.None);

            baseDamageResource.Normal = parts[0];
            if (parts.Count() > 1)
            {
                baseDamageResource.Vs1 = parts[1].Trim();
            }

            return baseDamageResource;
        }
    }

    public class UltimateHitboxResourceMapService
    {
        public HitboxResource MapFrom(IMove move)
        {
            var hitboxResource = new HitboxResource();

            var parts = move.HitboxActive.Split(new[] { "Adv:" }, StringSplitOptions.None);

            hitboxResource.Frames = parts[0];
            if (parts.Count() > 1)
            {
                hitboxResource.Adv = parts[1].Trim();
            }

            return hitboxResource;
        }
    }
}
