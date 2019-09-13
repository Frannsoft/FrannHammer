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

            var movesLink = CreateNameBasedLink<MovesLink>(content.Name, urlHelper, nameof(CharacterController.GetAllMovesForCharacterWhereName));
            var characterAttributesLink = CreateNameBasedLink<CharacterAttributesLink>(content.Name, urlHelper, nameof(CharacterController.GetAttributesForCharacterByName));
            var movementsLink = CreateNameBasedLink<MovementsLink>(content.Name, urlHelper, nameof(CharacterController.GetAllMovementsForCharacterWhereName));
            var selfLink = CreateNameBasedLink<SelfLink>(content.Name, urlHelper, nameof(CharacterController.GetSingleCharacterByName));

            resource.AddLink(selfLink);
            resource.AddLink(movesLink);
            resource.AddLink(characterAttributesLink);
            resource.AddLink(movementsLink);

            return resource;
        }
    }
}