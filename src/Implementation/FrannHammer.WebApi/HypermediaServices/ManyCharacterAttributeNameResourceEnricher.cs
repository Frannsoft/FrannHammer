using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class ManyCharacterAttributeNameResourceEnricher :
        ObjectContentResponseEnricher<IEnumerable<ICharacterAttributeName>, IEnumerable<CharacterAttributeNameResource>>
    {
        private readonly CharacterAttributeNameResourceEnricher _singleResourceEnricher;

        public ManyCharacterAttributeNameResourceEnricher(CharacterAttributeNameResourceEnricher singleResourceEnricher) 
            : base(singleResourceEnricher.LinkProvider, singleResourceEnricher.EntityToDtoMapper)
        {
            Guard.VerifyObjectNotNull(singleResourceEnricher, nameof(singleResourceEnricher));
            _singleResourceEnricher = singleResourceEnricher;
        }

        public override IEnumerable<CharacterAttributeNameResource> Enrich(IEnumerable<ICharacterAttributeName> content, UrlHelper urlHelper)
        {
            return content.Select(characterAttributeRowName => _singleResourceEnricher.Enrich(characterAttributeRowName, urlHelper));
        }
    }
}