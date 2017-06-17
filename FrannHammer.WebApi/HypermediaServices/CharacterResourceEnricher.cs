using System.Web.Http.Routing;
using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class CharacterResourceEnricher : ObjectContentResponseEnricher<ICharacter, CharacterResource>
    {
        public CharacterResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper)
            : base(linkProvider, entityToDtoMapper)
        { }

        public override CharacterResource Enrich(ICharacter content, UrlHelper urlHelper)
        {
            var resource = EntityToDtoMapper.Map<CharacterResource>(content);

            var movesLink = CreateLink<MovesLink>(resource, urlHelper, nameof(CharacterController.GetAllMovesForCharacterWhereName));
            var characterAttributesLink = CreateLink<CharacterAttributesLink>(resource, urlHelper, nameof(CharacterController.GetAttributesForCharacterByName));
            var movementsLink = CreateLink<MovementsLink>(resource, urlHelper, nameof(CharacterController.GetAllMovementsForCharacterWhereName));

            resource.AddLink(movesLink);
            resource.AddLink(characterAttributesLink);
            resource.AddLink(movementsLink);

            return resource;
        }
    }
}