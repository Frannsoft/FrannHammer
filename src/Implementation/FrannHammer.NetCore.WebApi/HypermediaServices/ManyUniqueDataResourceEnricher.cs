using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class ManyUniqueDataResourceEnricher :
        ObjectContentResponseEnricher<IEnumerable<IUniqueData>, IEnumerable<dynamic>>
    {
        private readonly UniqueDataResourceEnricher _singleResourceEnricher;

        public ManyUniqueDataResourceEnricher(UniqueDataResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper, singleResourceEnricher.LinkGenerator, singleResourceEnricher.Context)
        {
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<dynamic> Enrich(IEnumerable<IUniqueData> content, bool expand = false)
        {
            return content.Select(data => _singleResourceEnricher.Enrich(data));
        }

    }
}