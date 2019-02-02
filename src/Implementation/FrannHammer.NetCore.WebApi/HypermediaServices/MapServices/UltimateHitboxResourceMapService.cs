using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using System;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices.MapServices
{
    public class UltimateHitboxResourceMapService
    {
        public ExpandedHitboxResource MapFrom(IMove move)
        {
            var hitboxResource = new ExpandedHitboxResource();

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
