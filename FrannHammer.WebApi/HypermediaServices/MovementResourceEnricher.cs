using System.Web.Http.Routing;
using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class MovementResourceEnricher : ObjectContentResponseEnricher<IMovement, MovementResource>
    {
        public MovementResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper)
            : base(linkProvider, entityToDtoMapper)
        { }

        public override MovementResource Enrich(IMovement content, UrlHelper urlHelper)
        {
            var resource = EntityToDtoMapper.Map<MovementResource>(content);

            var characterlink = CreateNameBasedLink<CharacterLink>(content.Owner, urlHelper, nameof(CharacterController.GetSingleCharacterByName));
            var selfLink = CreateIdBasedLink<SelfLink>(content.InstanceId, urlHelper, nameof(MovementController.GetMovementById));

            resource.AddLink(selfLink);
            resource.AddLink(characterlink);

            return resource;
        }
    }
}