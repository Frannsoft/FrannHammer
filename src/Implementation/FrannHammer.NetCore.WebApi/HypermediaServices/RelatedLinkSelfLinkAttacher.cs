using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.Utility;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class RelatedLinkSelfLinkAttacher
    {
        public void AddSelf(RelatedLinks existingRelatedLinks, Games game, SelfLink selfLink)
        {
            Guard.VerifyObjectNotNull(selfLink, nameof(selfLink));
            Guard.VerifyObjectNotNull(existingRelatedLinks, nameof(existingRelatedLinks));

            if (game == Games.Ultimate)
            {
                existingRelatedLinks.Ultimate.Self = selfLink.Href.ReplaceSmash4WithUltimate();
            }
            else
            {
                existingRelatedLinks.Smash4.Self = selfLink.Href.ReplaceUltimateWithSmash4();

            }
        }
    }
}