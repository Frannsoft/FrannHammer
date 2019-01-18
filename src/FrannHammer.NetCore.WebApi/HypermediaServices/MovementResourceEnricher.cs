using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using FrannHammer.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class MovementResourceEnricher : ObjectContentResponseEnricher<IMovement, MovementResource>
    {
        public MovementResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper, LinkGenerator linkGenerator, HttpContext context)
            : base(linkProvider, entityToDtoMapper, linkGenerator, context)
        { }

        public override MovementResource Enrich(IMovement content)
        {
            var resource = EntityToDtoMapper.Map<MovementResource>(content);

            var characterlink = CreateNameBasedLink<CharacterLink>(content.Owner, nameof(CharacterController.GetSingleCharacterByName), "Character");
            var selfLink = CreateIdBasedLink<SelfLink>(content.InstanceId, nameof(MovementController.GetMovementById), "Movement");

            resource.AddLink(selfLink);
            resource.AddLink(characterlink);

            return resource;
        }
    }
}