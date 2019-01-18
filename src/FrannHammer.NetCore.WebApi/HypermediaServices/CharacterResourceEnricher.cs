using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using FrannHammer.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class CharacterResourceEnricher : ObjectContentResponseEnricher<ICharacter, CharacterResource>
    {
        public CharacterResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper, LinkGenerator linkGenerator, HttpContext context)
            : base(linkProvider, entityToDtoMapper, linkGenerator, context)
        { }

        public override CharacterResource Enrich(ICharacter content)
        {
            var resource = EntityToDtoMapper.Map<CharacterResource>(content);

            var movesLink = CreateNameBasedLink<MovesLink>(content.Name, nameof(CharacterController.GetAllMovesForCharacterWhereName), "Character");
            var characterAttributesLink = CreateNameBasedLink<CharacterAttributesLink>(content.Name, nameof(CharacterController.GetAttributesForCharacterByName), "Character");
            var movementsLink = CreateNameBasedLink<MovementsLink>(content.Name, nameof(CharacterController.GetAllMovementsForCharacterWhereName), "Character");
            var selfLink = CreateNameBasedLink<SelfLink>(content.Name, nameof(CharacterController.GetSingleCharacterByName), "Character");

            resource.AddLink(selfLink);
            resource.AddLink(movesLink);
            resource.AddLink(characterAttributesLink);
            resource.AddLink(movementsLink);

            return resource;
        }
    }
}