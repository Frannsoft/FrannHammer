using System.Web.Http.Routing;
using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class CharacterAttributeRowResourceEnricher : ObjectContentResponseEnricher<ICharacterAttributeRow, CharacterAttributeRowResource>
    {
        public CharacterAttributeRowResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper)
            : base(linkProvider, entityToDtoMapper)
        { }

        public override CharacterAttributeRowResource Enrich(ICharacterAttributeRow content, UrlHelper urlHelper)
        {
            var resource = EntityToDtoMapper.Map<CharacterAttributeRowResource>(content);

            var characterlink = CreateNameBasedLink<CharacterLink>(content.Owner, urlHelper, nameof(CharacterController.GetSingleCharacterByName));
            var selfLink = CreateIdBasedLink<SelfLink>(content.InstanceId, urlHelper, nameof(CharacterAttributeController.GetCharacterAttributeById));

            resource.AddLink(selfLink);
            resource.AddLink(characterlink);

            return resource;
        }
    }
}