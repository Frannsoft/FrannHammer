using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class ManyMoveResourceEnricher : ObjectContentResponseEnricher<IEnumerable<IMove>, IEnumerable<MoveResource>>
    {
        public ManyMoveResourceEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper)
            : base(linkProvider, entityToDtoMapper)
        { }

        public override IEnumerable<MoveResource> Enrich(IEnumerable<IMove> content, UrlHelper urlHelper)
        {
            var resources = EntityToDtoMapper.Map<IEnumerable<MoveResource>>(content).ToList();

            foreach (var resource in resources)
            {
                string hypermediaLink = urlHelper.Link(nameof(CharacterController.GetSingleByName),
                    new { name = resource.Owner });

                var characterLink = LinkProvider.CreateLink<CharacterLink>(hypermediaLink);
                resource.AddLink(characterLink);
            }

            return resources;
        }
    }
}