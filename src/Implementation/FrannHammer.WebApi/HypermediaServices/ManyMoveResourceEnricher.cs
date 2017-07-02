using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class ManyMoveResourceEnricher : ObjectContentResponseEnricher<IEnumerable<IMove>, IEnumerable<MoveResource>>
    {
        private readonly MoveResourceEnricher _singleResourceEnricher;

        public ManyMoveResourceEnricher(MoveResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper)
        {
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<MoveResource> Enrich(IEnumerable<IMove> content, UrlHelper urlHelper)
        {
            return content.Select(move => _singleResourceEnricher.Enrich(move, urlHelper));
        }
    }
}