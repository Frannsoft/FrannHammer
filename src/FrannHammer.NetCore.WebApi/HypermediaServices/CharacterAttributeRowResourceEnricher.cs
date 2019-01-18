using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using FrannHammer.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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

            var characterlink = CreateNameBasedLink<CharacterLink>(content.Owner, nameof(CharacterController.GetSingleCharacterByName), "Character");
            var selfLink = CreateIdBasedLink<SelfLink>(content.InstanceId, nameof(CharacterAttributeController.GetCharacterAttributeById), "CharacterAttribute");

            resource.AddLink(selfLink);
            resource.AddLink(characterlink);

            return resource;
        }
    }
}