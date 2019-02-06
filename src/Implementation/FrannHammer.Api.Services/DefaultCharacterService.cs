using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using System;
using System.Collections.Generic;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterService : BaseApiService<ICharacter>, ICharacterService
    {
        private readonly ICharacterAttributeRowService _attributeRowService;
        private readonly IMoveService _moveService;
        private readonly IMovementService _movementService;
        private readonly IUniqueDataService _uniqueDataService;

        public DefaultCharacterService(IRepository<ICharacter> repository,
                                IMovementService movementService, ICharacterAttributeRowService attributeRowService,
                                IMoveService moveService, IUniqueDataService uniqueDataService, IGameParameterParserService gameParameterParserService)
            : base(repository, gameParameterParserService)
        {
            Guard.VerifyObjectNotNull(attributeRowService, nameof(attributeRowService));
            Guard.VerifyObjectNotNull(moveService, nameof(moveService));
            Guard.VerifyObjectNotNull(movementService, nameof(movementService));
            Guard.VerifyObjectNotNull(uniqueDataService, nameof(uniqueDataService));

            _attributeRowService = attributeRowService;
            _moveService = moveService;
            _movementService = movementService;
            _uniqueDataService = uniqueDataService;
        }

        public ICharacter GetSingleByOwnerId(int id)
        {
            return GetSingleWhere(c => c.OwnerId == id && WhereGameIs()(c));
        }

        public ICharacter GetSingleByName(string name)
        {
            var character = GetSingleWhere(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) && WhereGameIs()(c));
            return character;
        }

        public IEnumerable<IMovement> GetAllMovementsWhereCharacterNameIs(string name)
        {
            return _movementService.GetAllWhereCharacterNameIs(name);
        }

        public IEnumerable<IMovement> GetAllMovementsWhereCharacterOwnerIdIs(int id)
        {
            return _movementService.GetAllWhereCharacterOwnerIdIs(id);
        }

        public IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterNameIs(string name)
        {
            return _attributeRowService.GetAllWhereCharacterNameIs(name);
        }

        public IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterOwnerIdIs(int id)
        {
            return _attributeRowService.GetAllWhereCharacterOwnerIdIs(id);
        }

        public IEnumerable<ICharacterAttributeRow> GetAttributesOfNameWhereCharacterOwnerIdIs(string name, int id)
        {
            return _attributeRowService.GetSingleWithNameAndMatchingCharacterOwnerId(name, id);
        }

        public IEnumerable<ICharacterAttributeRow> GetAttributesOfNameWhereCharacterNameIs(string name, string attributeName)
        {
            return _attributeRowService.GetAllWithNameAndMatchingCharacterOwner(attributeName, name);
        }

        public IEnumerable<IMove> GetAllThrowsWhereCharacterNameIs(string name)
        {
            var foundCharacter = GetSingleByName(name);
            return _moveService.GetAllThrowsForCharacter(foundCharacter);
        }

        public IEnumerable<IMove> GetAllMovesWhereCharacterNameIs(string name)
        {
            var foundCharacter = GetSingleByName(name);
            return _moveService.GetAllMovesForCharacter(foundCharacter);
        }

        public IEnumerable<IMove> GetAllThrowsWhereCharacterOwnerIdIs(int id)
        {
            var foundCharacter = GetSingleByOwnerId(id);
            return _moveService.GetAllThrowsForCharacter(foundCharacter);
        }

        public IEnumerable<IMove> GetAllMovesWhereCharacterOwnerIdIs(int id)
        {
            var foundCharacter = GetSingleByOwnerId(id);
            return _moveService.GetAllMovesForCharacter(foundCharacter);
        }

        public IEnumerable<IUniqueData> GetUniquePropertiesWhereCharacterOwnerIdIs(int id)
        {
            var uniqueProperties = _uniqueDataService.GetAllWhere(u => u.OwnerId == id);
            return uniqueProperties;
        }

        public IEnumerable<IUniqueData> GetUniquePropertiesWhereCharacterNameIs(string name)
        {
            var uniqueProperties = _uniqueDataService.GetAllWhere(u => u.Owner.Equals(name, StringComparison.OrdinalIgnoreCase));
            return uniqueProperties;
        }
    }
}
