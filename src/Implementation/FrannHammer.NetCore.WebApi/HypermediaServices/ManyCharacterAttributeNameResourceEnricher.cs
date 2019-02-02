using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.Utility;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class ManyCharacterAttributeNameResourceEnricher :
        ObjectContentResponseEnricher<IEnumerable<ICharacterAttributeName>, IEnumerable<CharacterAttributeNameResource>>
    {
        private readonly CharacterAttributeNameResourceEnricher _singleResourceEnricher;

        public ManyCharacterAttributeNameResourceEnricher(CharacterAttributeNameResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper, singleResourceEnricher.LinkGenerator, singleResourceEnricher.Context)
        {
            Guard.VerifyObjectNotNull(singleResourceEnricher, nameof(singleResourceEnricher));
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<CharacterAttributeNameResource> Enrich(IEnumerable<ICharacterAttributeName> content, bool expand = false)
        {
            return content.Select(characterAttributeRowName => _singleResourceEnricher.Enrich(characterAttributeRowName));
        }
    }
}