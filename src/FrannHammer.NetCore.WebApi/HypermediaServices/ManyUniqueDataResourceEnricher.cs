using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class ManyUniqueDataResourceEnricher :
        ObjectContentResponseEnricher<IEnumerable<IUniqueData>, IEnumerable<UniqueDataResource>>
    {
        private readonly UniqueDataResourceEnricher _singleResourceEnricher;

        public ManyUniqueDataResourceEnricher(UniqueDataResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper, singleResourceEnricher.LinkGenerator, singleResourceEnricher.Context)
        {
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<UniqueDataResource> Enrich(IEnumerable<IUniqueData> content)
        {
            return content.Select(data => _singleResourceEnricher.Enrich(data));
        }

    }
}