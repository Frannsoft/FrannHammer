using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using FrannHammer.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class CharacterAttributeNameResourceEnricher :
        ObjectContentResponseEnricher<ICharacterAttributeName, CharacterAttributeNameResource>
    {
        public CharacterAttributeNameResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper, LinkGenerator linkGenerator, HttpContext context)
            : base(linkProvider, entityToDtoMapper, linkGenerator, context)
        {
        }

        public override CharacterAttributeNameResource Enrich(ICharacterAttributeName content)
        {
            var resource = EntityToDtoMapper.Map<CharacterAttributeNameResource>(content);

            var allAttributesForNameLink = CreateNameBasedLink<CharacterAttributesLink>(content.Name, nameof(CharacterAttributeController.GetAllCharacterAttributesWithName), " CharacterAttribute");

            resource.AddLink(allAttributesForNameLink);

            var relatedLinks = new RelatedLinks();

            return resource;
        }
    }
}