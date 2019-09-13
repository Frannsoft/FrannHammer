using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class ManyUniqueDataResourceEnricher :
        ObjectContentResponseEnricher<IEnumerable<IUniqueData>, IEnumerable<UniqueDataResource>>
    {
        private readonly UniqueDataResourceEnricher _singleResourceEnricher;

        public ManyUniqueDataResourceEnricher(UniqueDataResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper)
        {
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<UniqueDataResource> Enrich(IEnumerable<IUniqueData> content, UrlHelper urlHelper)
        {
            return content.Select(data => _singleResourceEnricher.Enrich(data, urlHelper));
        }

    }
}