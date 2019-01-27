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

            //new hateoas stuff
            var relatedLinks = new RelatedLinks();

            if (content.OwnerId <= 58)
            {
                relatedLinks.Smash4 = new ExpandoObject();
                relatedLinks.Smash4.Self = selfLink.Href.ReplaceUltimateWithSmash4();
                relatedLinks.Smash4.Moves = movesLink.Href.ReplaceUltimateWithSmash4();
                relatedLinks.Smash4.Movements = movementsLink.Href.ReplaceUltimateWithSmash4();
                relatedLinks.Smash4.Attributes = characterAttributesLink.Href.ReplaceUltimateWithSmash4();
            }

            relatedLinks.Ultimate = new ExpandoObject();
            relatedLinks.Ultimate.Self = selfLink.Href.ReplaceSmash4WithUltimate();
            relatedLinks.Ultimate.Moves = movesLink.Href.ReplaceSmash4WithUltimate();
            relatedLinks.Ultimate.Movements = movementsLink.Href.ReplaceSmash4WithUltimate();
            relatedLinks.Ultimate.Attributes = characterAttributesLink.Href.ReplaceSmash4WithUltimate();

            resource.AddRelated(relatedLinks);

            return resource;
        }
    }
}