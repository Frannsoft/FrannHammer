using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using FrannHammer.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Dynamic;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class MoveResourceEnricher : ObjectContentResponseEnricher<IMove, IMoveResource>
    {
        public MoveResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper, LinkGenerator linkGenerator, HttpContext context)
            : base(linkProvider, entityToDtoMapper, linkGenerator, context)
        { }

        public override IMoveResource Enrich(IMove content, bool expand = false)
        {
            var resource = default(IMoveResource);

            if (expand)
            {
                resource = EntityToDtoMapper.Map<ExpandedMoveResource>(content);
            }
            else
            {
                resource = EntityToDtoMapper.Map<MoveResource>(content);
            }

            var characterLink = CreateNameBasedLink<CharacterLink>(content.Owner, nameof(CharacterController.GetSingleCharacterByName), "Character");
            var selfLink = CreateIdBasedLink<SelfLink>(content.InstanceId, nameof(MoveController.GetMoveById), "Move");

            resource.AddLink(selfLink);
            resource.AddLink(characterLink);

            //new hateoas stuff 
            var relatedLinks = new RelatedLinks();

            if (content.OwnerId <= 58)
            {
                relatedLinks.Smash4 = new ExpandoObject();
                relatedLinks.Smash4.Character = characterLink.Href.ReplaceUltimateWithSmash4();
            }

            relatedLinks.Ultimate = new ExpandoObject();
            relatedLinks.Ultimate.Character = characterLink.Href.ReplaceSmash4WithUltimate();

            new RelatedLinkSelfLinkAttacher().AddSelf(relatedLinks, content.Game, selfLink);

            resource.AddRelated(relatedLinks);

            return resource;
        }
    }
}