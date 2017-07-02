using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class ManyCharacterResourceEnricher : ObjectContentResponseEnricher<IEnumerable<ICharacter>, IEnumerable<CharacterResource>>
    {
        private readonly CharacterResourceEnricher _singleResourceEnricher;

        public ManyCharacterResourceEnricher(CharacterResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper)
        {
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<CharacterResource> Enrich(IEnumerable<ICharacter> content, UrlHelper urlHelper)
        {
            return content.Select(character => _singleResourceEnricher.Enrich(character, urlHelper));
        }
    }
}