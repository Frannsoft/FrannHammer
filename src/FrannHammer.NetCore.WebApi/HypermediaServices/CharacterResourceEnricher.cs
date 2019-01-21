using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using FrannHammer.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Dynamic;

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


            var relatedLinks = new RelatedLinks();

            if (content.OwnerId <= 58)
            {
                relatedLinks.Smash4 = new ExpandoObject();
                relatedLinks.Smash4.Self = selfLink.Href.Replace("ultimate", "smash4");
                relatedLinks.Smash4.Moves = movesLink.Href.Replace("ultimate", "smash4");
                relatedLinks.Smash4.Movements = movementsLink.Href.Replace("ultimate", "smash4");
                relatedLinks.Smash4.Attributes = characterAttributesLink.Href.Replace("ultimate", "smash4");
            }

            relatedLinks.Ultimate = new ExpandoObject();
            relatedLinks.Ultimate.Self = selfLink.Href.Replace("smash4", "ultimate");
            relatedLinks.Ultimate.Moves = movesLink.Href.Replace("smash4", "ultimate");
            relatedLinks.Ultimate.Movements = movementsLink.Href.Replace("smash4", "ultimate");
            relatedLinks.Ultimate.Attributes = characterAttributesLink.Href.Replace("smash4", "ultimate");

            resource.AddRelated(relatedLinks);

            return resource;
        }
    }
}