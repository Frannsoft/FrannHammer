using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.Utility;
using System;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices.MapServices
{
    public class UltimateHitboxActiveResourceMapService
    {
        public ExpandedHitboxResource MapFrom(IMove move)
        {
            Guard.VerifyObjectNotNull(move, nameof(move));

            var hitboxResource = new ExpandedHitboxResource();

            var parts = move.HitboxActive.Split(new[] { "Adv:" }, StringSplitOptions.None);

            hitboxResource.Frames = parts[0].TrimEnd(';');
            string adv = string.Empty;
            if (parts.Count() > 1)
            {
                adv = parts[1].Split(new[] { "," }, StringSplitOptions.None).FirstOrDefault() ?? string.Empty;
            }
            hitboxResource.Adv = adv;

            return hitboxResource;
        }
    }

    public class DefaultHitboxActiveResourceMapService
    {
        public string MapFrom(IMove move)
        {
            Guard.VerifyObjectNotNull(move, nameof(move));

            //return just the first portion of the move, not the 'extended' data
            var parts = move.HitboxActive.Split(new[] { ";" }, StringSplitOptions.None);
            return parts.FirstOrDefault() ?? string.Empty;
        }
    }
}
