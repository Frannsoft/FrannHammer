using System.Web.Http.Routing;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public interface IEntityToBusinessTranslationService
    {
        CharacterResource ConvertToCharacterResourceWithHalSupport(ICharacter entity, UrlHelper urlHelper);
    }
}