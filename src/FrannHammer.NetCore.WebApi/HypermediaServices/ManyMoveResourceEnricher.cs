using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class ManyMoveResourceEnricher : ObjectContentResponseEnricher<IEnumerable<IMove>, IEnumerable<IMoveResource>>
    {
        private readonly MoveResourceEnricher _singleResourceEnricher;

        public ManyMoveResourceEnricher(MoveResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper, singleResourceEnricher.LinkGenerator, singleResourceEnricher.Context)
        {
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<IMoveResource> Enrich(IEnumerable<IMove> content)
        {
            return content.Select(move => _singleResourceEnricher.Enrich(move));
        }
    }
}