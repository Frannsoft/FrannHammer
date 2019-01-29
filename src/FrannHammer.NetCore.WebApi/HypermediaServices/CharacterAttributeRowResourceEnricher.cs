using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using FrannHammer.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Dynamic;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class CharacterAttributeRowResourceEnricher : ObjectContentResponseEnricher<ICharacterAttributeRow, CharacterAttributeRowResource>
    {
        public CharacterAttributeRowResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper, LinkGenerator linkGenerator, HttpContext context)
            : base(linkProvider, entityToDtoMapper, linkGenerator, context)
        { }

        public override CharacterAttributeRowResource Enrich(ICharacterAttributeRow content)
        {
            var resource = EntityToDtoMapper.Map<CharacterAttributeRowResource>(content);

            var characterLink = CreateNameBasedLink<CharacterLink>(content.Owner, nameof(CharacterController.GetSingleCharacterByName), "Character");
            var selfLink = CreateIdBasedLink<SelfLink>(content.InstanceId, nameof(CharacterAttributeController.GetCharacterAttributeById), "CharacterAttribute");

            resource.AddLink(selfLink);
            resource.AddLink(characterLink);

            var relatedLinks = new RelatedLinks();

            if (content.OwnerId <= 58)
            {
                relatedLinks.Smash4 = new ExpandoObject();
                relatedLinks.Smash4.Self = selfLink.Href.ReplaceUltimateWithSmash4();
                relatedLinks.Smash4.Character = characterLink.Href.ReplaceUltimateWithSmash4();
            }
            else
            {
                relatedLinks.Ultimate = new ExpandoObject();
                relatedLinks.Ultimate.Self = selfLink.Href.ReplaceSmash4WithUltimate();
                relatedLinks.Ultimate.Character = characterLink.Href.ReplaceSmash4WithUltimate();
            }

            resource.AddRelated(relatedLinks);

            return resource;
        }
    }
}