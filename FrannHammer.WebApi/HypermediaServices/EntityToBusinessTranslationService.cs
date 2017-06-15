using System.Web.Http.Routing;
using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebApi.Controllers;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public class EntityToBusinessTranslationService : IEntityToBusinessTranslationService
    {
        private readonly ILinkProvider _linkProvider;
        private readonly IMapper _entityToDtoMapper;

        public EntityToBusinessTranslationService(ILinkProvider linkProvider, IMapper entityToDtoMapper)
        {
            Guard.VerifyObjectNotNull(linkProvider, nameof(linkProvider));
            Guard.VerifyObjectNotNull(entityToDtoMapper, nameof(entityToDtoMapper));

            _linkProvider = linkProvider;
            _entityToDtoMapper = entityToDtoMapper;
        }

        public CharacterResource ConvertToCharacterResourceWithHalSupport(ICharacter entity, UrlHelper urlHelper)
        {
            Guard.VerifyObjectNotNull(entity, nameof(entity));
            Guard.VerifyObjectNotNull(urlHelper, nameof(urlHelper));

            var resource = _entityToDtoMapper.Map<CharacterResource>(entity);

            string link = urlHelper.Link(nameof(CharacterController.GetAllMovesForCharacterWhereName),
                new { name = resource.Name });

            var movesLink = _linkProvider.CreateLink<MovesLink>(link);
            resource.AddLink(movesLink);

            return resource;
        }
    }
}