using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class ManyMovementResourceEnricher : ObjectContentResponseEnricher<IEnumerable<IMovement>, IEnumerable<MovementResource>>
    {
        private readonly MovementResourceEnricher _singleResourceEnricher;

        public ManyMovementResourceEnricher(MovementResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper)
        {
            Guard.VerifyObjectNotNull(singleResourceEnricher, nameof(singleResourceEnricher));
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<MovementResource> Enrich(IEnumerable<IMovement> content, UrlHelper urlHelper)
        {
            return content.Select(movement => _singleResourceEnricher.Enrich(movement, urlHelper));
        }
    }
}