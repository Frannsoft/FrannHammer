using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class ManyCharacterAttributeRowResourceEnricher :
        ObjectContentResponseEnricher<IEnumerable<ICharacterAttributeRow>, IEnumerable<CharacterAttributeRowResource>>
    {
        private readonly CharacterAttributeRowResourceEnricher _singleResourceEnricher;

        public ManyCharacterAttributeRowResourceEnricher(CharacterAttributeRowResourceEnricher singleResourceEnricher)
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper)
        {
            Guard.VerifyObjectNotNull(singleResourceEnricher, nameof(singleResourceEnricher));
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<CharacterAttributeRowResource> Enrich(IEnumerable<ICharacterAttributeRow> content, UrlHelper urlHelper)
        {
            return content.Select(characterAttributeRow => _singleResourceEnricher.Enrich(characterAttributeRow, urlHelper));
        }
    }
}