using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using FrannHammer.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class UniqueDataResourceEnricher : ObjectContentResponseEnricher<IUniqueData, dynamic>
    {
        public UniqueDataResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper, LinkGenerator linkGenerator, HttpContext context)
            : base(linkProvider, entityToDtoMapper, linkGenerator, context)
        { }

        public override dynamic Enrich(IUniqueData content, bool expand = false)
        {
            var resource = EntityToDtoMapper.Map<UniqueDataResource>(content);

            var characterLink = CreateNameBasedLink<CharacterLink>(content.Owner, nameof(CharacterController.GetSingleCharacterByName), "Character");
            var selfLink = CreateIdBasedLink<SelfLink>(content.InstanceId, nameof(UniqueDataController.GetAllUniquePropertiesById), "UniqueData");

            var resd = new ExpandoObject() as IDictionary<string, object>;
            var props = content.GetType().GetProperties().ToList();
            props.ForEach(prop =>
            {
                var value = prop.GetValue(content);
                resd.Add(prop.Name, value);
            });

            var links = new List<Link>();
            links.Add(selfLink);
            links.Add(characterLink);

            resd.Add("Links", links);

            resource.AddLink(selfLink);
            resource.AddLink(characterLink);


            var relatedLinks = new RelatedLinks();

            if (content.OwnerId <= 58)
            {
                relatedLinks.Smash4 = new ExpandoObject();
                relatedLinks.Smash4.Character = characterLink.Href.ReplaceUltimateWithSmash4();
            }

            relatedLinks.Ultimate = new ExpandoObject();
            relatedLinks.Ultimate.Character = characterLink.Href.ReplaceSmash4WithUltimate();

            new RelatedLinkSelfLinkAttacher().AddSelf(relatedLinks, content.Game, selfLink);

            resd.Add("Related", relatedLinks);
            //resource.AddRelated(relatedLinks);

            return resd;
            //return resource;
        }
    }
}