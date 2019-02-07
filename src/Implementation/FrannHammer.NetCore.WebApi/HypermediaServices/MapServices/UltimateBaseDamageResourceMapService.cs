using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.Utility;
using System;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices.MapServices
{
    public class UltimateBaseDamageResourceMapService
    {
        public ExpandedBaseDamageResource MapFrom(IMove move)
        {
            Guard.VerifyObjectNotNull(move, nameof(move));
            var baseDamageResource = new ExpandedBaseDamageResource();

            var parts = move.BaseDamage.Split(new[] { "1v1:" }, StringSplitOptions.None);

            baseDamageResource.Normal = parts[0].TrimEnd(';');
            string vs1 = string.Empty;
            if (parts.Count() > 1)
            {
                vs1 = parts[1].Trim();
            }
            baseDamageResource.Vs1 = vs1;

            return baseDamageResource;
        }
    }

    public class DefaultBaseDamageResourceMapService
    {
        public string MapFrom(IMove move)
        {
            Guard.VerifyObjectNotNull(move, nameof(move));

            //return just the first portion of the move data, not the 'extended' data
            var parts = move.BaseDamage.Split(new[] { ';' }, StringSplitOptions.None);
            return parts.FirstOrDefault() ?? string.Empty;
        }
    }
}
