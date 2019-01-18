using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class ManyCharacterResourceEnricher : ObjectContentResponseEnricher<IEnumerable<ICharacter>, IEnumerable<CharacterResource>>
    {
        private readonly CharacterResourceEnricher _singleResourceEnricher;

        public ManyCharacterResourceEnricher(CharacterResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper, singleResourceEnricher.LinkGenerator, singleResourceEnricher.Context)
        {
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<CharacterResource> Enrich(IEnumerable<ICharacter> content)
        {
            return content.Select(character => _singleResourceEnricher.Enrich(character));
        }
    }
}