using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.Utility;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class ManyCharacterAttributeRowResourceEnricher :
        ObjectContentResponseEnricher<IEnumerable<ICharacterAttributeRow>, IEnumerable<CharacterAttributeRowResource>>
    {
        private readonly CharacterAttributeRowResourceEnricher _singleResourceEnricher;

        public ManyCharacterAttributeRowResourceEnricher(CharacterAttributeRowResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper, singleResourceEnricher.LinkGenerator, singleResourceEnricher.Context)
        {
            Guard.VerifyObjectNotNull(singleResourceEnricher, nameof(singleResourceEnricher));
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<CharacterAttributeRowResource> Enrich(IEnumerable<ICharacterAttributeRow> content, bool expand = false)
        {
            return content.Select(characterAttributeRow => _singleResourceEnricher.Enrich(characterAttributeRow));
        }
    }
}