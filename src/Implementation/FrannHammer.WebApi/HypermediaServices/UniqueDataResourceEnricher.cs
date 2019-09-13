using System.Web.Http.Routing;
using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class UniqueDataResourceEnricher : ObjectContentResponseEnricher<IUniqueData, UniqueDataResource>
    {
        public UniqueDataResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper)
            : base(linkProvider, entityToDtoMapper)
        { }

        public override UniqueDataResource Enrich(IUniqueData content, UrlHelper urlHelper)
        {
            var resource = EntityToDtoMapper.Map<UniqueDataResource>(content);

            var characterLink = CreateNameBasedLink<CharacterLink>(content.Owner, urlHelper, nameof(CharacterController.GetSingleCharacterByName));
            var selfLink = CreateIdBasedLink<SelfLink>(content.InstanceId, urlHelper, nameof(UniqueDataController.GetAllUniquePropertiesById));

            resource.AddLink(selfLink);
            resource.AddLink(characterLink);

            return resource;
        }
    }
}