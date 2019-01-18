using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.Utility;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class ManyMovementResourceEnricher : ObjectContentResponseEnricher<IEnumerable<IMovement>, IEnumerable<MovementResource>>
    {
        private readonly MovementResourceEnricher _singleResourceEnricher;

        public ManyMovementResourceEnricher(MovementResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper, singleResourceEnricher.LinkGenerator, singleResourceEnricher.Context)
        {
            Guard.VerifyObjectNotNull(singleResourceEnricher, nameof(singleResourceEnricher));
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<MovementResource> Enrich(IEnumerable<IMovement> content)
        {
            return content.Select(movement => _singleResourceEnricher.Enrich(movement));
        }
    }
}