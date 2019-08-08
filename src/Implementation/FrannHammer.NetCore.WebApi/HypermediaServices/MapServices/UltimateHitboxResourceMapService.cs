using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.Utility;
using System;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices.MapServices
{
    public class UltimateHitboxActiveResourceMapService
    {
        private readonly TooltipPartParser _tooltipPartParser;

        public UltimateHitboxActiveResourceMapService(TooltipPartParser tooltipPartParser)
        {
            Guard.VerifyObjectNotNull(tooltipPartParser, nameof(tooltipPartParser));
            _tooltipPartParser = tooltipPartParser;
        }

        public ExpandedHitboxResource MapFrom(IMove move)
        {
            Guard.VerifyObjectNotNull(move, nameof(move));

            var hitboxResource = new ExpandedHitboxResource();

            var parts = move.HitboxActive.Split(new[] { "|" }, StringSplitOptions.None);

            hitboxResource.Frames = parts[0].TrimEnd(';');
            string adv = string.Empty;
            if (parts.Count() > 1)
            {
                string tooltipContent = parts[1];
                hitboxResource.Adv = _tooltipPartParser.GetValue("Adv", tooltipContent);
                hitboxResource.SD = _tooltipPartParser.GetValue("SD", tooltipContent);
                hitboxResource.ShieldstunMultiplier = _tooltipPartParser.GetValue("Shieldstun Multiplier", tooltipContent);
                hitboxResource.RehitRate = _tooltipPartParser.GetValue("Rehit Rate", tooltipContent);
                hitboxResource.FacingRestrict = _tooltipPartParser.GetValue("Facing Restrict", tooltipContent);
                hitboxResource.SuperArmor = _tooltipPartParser.GetValue("Super Armor", tooltipContent);
                hitboxResource.HeadMultiplier = _tooltipPartParser.GetValue("Head Multiplier", tooltipContent);
                hitboxResource.Intangible = _tooltipPartParser.GetValue("Intangible", tooltipContent);
                hitboxResource.SetWeight = _tooltipPartParser.GetPhraseOnlyValue("Set Weight", tooltipContent);
                hitboxResource.GroundOnly = _tooltipPartParser.GetPhraseOnlyValue("Ground Only", tooltipContent);
            }

            return hitboxResource;
        }
    }
}
