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

            var movesLink = CreateLink<MovesLink>(resource, urlHelper, nameof(CharacterController.GetAllMovesForCharacterWhereName));
            var characterAttributesLink = CreateLink<CharacterAttributesLink>(resource, urlHelper, nameof(CharacterController.GetAttributesForCharacterByName));
            var movementsLink = CreateLink<MovementsLink>(resource, urlHelper, nameof(CharacterController.GetAllMovementsForCharacterWhereName));

            resource.AddLink(movesLink);
            resource.AddLink(characterAttributesLink);
            resource.AddLink(movementsLink);

            return resource;
        }

        public MoveResource ConvertToMoveResourceWithHalSupport(IMove entity, UrlHelper urlHelper)
        {
            Guard.VerifyObjectNotNull(entity, nameof(entity));
            Guard.VerifyObjectNotNull(urlHelper, nameof(urlHelper));

            var resource = _entityToDtoMapper.Map<MoveResource>(entity);

            var characterLink = CreateLink<CharacterLink>(resource, urlHelper, nameof(CharacterController.GetSingleCharacterByName));

            resource.AddLink(characterLink);

            return resource;
        }

        private T CreateLink<T>(IHaveAName resource, UrlHelper urlHelper, string routeName)
            where T : Link
        {
            string hypermediaLink = urlHelper.Link(routeName, new { name = resource.Name });

            var link = _linkProvider.CreateLink<T>(hypermediaLink);

            return link;
        }


    }
}