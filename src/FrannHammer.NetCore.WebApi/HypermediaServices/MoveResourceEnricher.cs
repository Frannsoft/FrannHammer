using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using FrannHammer.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class MoveResourceEnricher : ObjectContentResponseEnricher<IMove, MoveResource>
    {
        public MoveResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper, LinkGenerator linkGenerator, HttpContext context)
            : base(linkProvider, entityToDtoMapper, linkGenerator, context)
        { }

        public override MoveResource Enrich(IMove content)
        {
            var resource = EntityToDtoMapper.Map<MoveResource>(content);

            var characterLink = CreateNameBasedLink<CharacterLink>(content.Owner, nameof(CharacterController.GetSingleCharacterByName), "Character");
            var selfLink = CreateIdBasedLink<SelfLink>(content.InstanceId, nameof(MoveController.GetMoveById), "Move");

            resource.AddLink(selfLink);
            resource.AddLink(characterLink);

            return resource;
        }
    }
}