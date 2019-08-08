using System.Web.Http.Routing;
using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class CharacterAttributeNameResourceEnricher :
        ObjectContentResponseEnricher<ICharacterAttributeName, CharacterAttributeNameResource>
    {
        public CharacterAttributeNameResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper)
            : base(linkProvider, entityToDtoMapper)
        {
        }

        public override CharacterAttributeNameResource Enrich(ICharacterAttributeName content, UrlHelper urlHelper)
        {
            var resource = EntityToDtoMapper.Map<CharacterAttributeNameResource>(content);

            var allAttributesForNameLink = CreateNameBasedLink<CharacterAttributesLink>(content.Name, urlHelper, nameof(CharacterAttributeController.GetAllCharacterAttributesWithName));

            resource.AddLink(allAttributesForNameLink);

            return resource;
        }
    }
}