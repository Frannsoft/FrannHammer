using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using System;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices.MapServices
{
    public class UltimateBaseDamageResourceMapService
    {
        public ExpandedBaseDamageResource MapFrom(IMove move)
        {
            var baseDamageResource = new ExpandedBaseDamageResource();

            var parts = move.BaseDamage.Split(new[] { "1v1:" }, StringSplitOptions.None);

            baseDamageResource.Normal = parts[0];
            if (parts.Count() > 1)
            {
                baseDamageResource.Vs1 = parts[1].Trim();
            }

            return baseDamageResource;
        }
    }
}
