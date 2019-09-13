using System.Web.Http.Routing;
using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class MoveResourceEnricher : ObjectContentResponseEnricher<IMove, MoveResource>
    {
        public MoveResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper)
            : base(linkProvider, entityToDtoMapper)
        { }

        public override MoveResource Enrich(IMove content, UrlHelper urlHelper)
        {
            var resource = EntityToDtoMapper.Map<MoveResource>(content);

            var characterLink = CreateNameBasedLink<CharacterLink>(content.Owner, urlHelper, nameof(CharacterController.GetSingleCharacterByName));
            var selfLink = CreateIdBasedLink<SelfLink>(content.InstanceId, urlHelper, nameof(MoveController.GetMoveById));

            resource.AddLink(selfLink);
            resource.AddLink(characterLink);

            return resource;
        }
    }
}