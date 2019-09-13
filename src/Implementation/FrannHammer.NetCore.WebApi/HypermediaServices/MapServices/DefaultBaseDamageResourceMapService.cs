using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using System;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices.MapServices
{
    public class DefaultBaseDamageResourceMapService
    {
        public string MapFrom(IMove move)
        {
            Guard.VerifyObjectNotNull(move, nameof(move));

            //return just the first portion of the move data, not the 'extended' data
            var parts = move.BaseDamage.Split(new[] { '|' }, StringSplitOptions.None);
            return parts.FirstOrDefault() ?? string.Empty;
        }
    }
}
