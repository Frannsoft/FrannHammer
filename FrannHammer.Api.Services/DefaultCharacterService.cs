using System;
using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterService : BaseApiService<ICharacter>, ICharacterService
    {
        private readonly ICharacterAttributeRowService _attributeRowService;
        private readonly IMoveService _moveService;
        private readonly IMovementService _movementService;
        private readonly IDtoProvider _dtoProvider;

        public DefaultCharacterService(IRepository<ICharacter> repository, IDtoProvider dtoProvider,
                                IMovementService movementService, ICharacterAttributeRowService attributeRowService,
                                IMoveService moveService)
            : base(repository)
        {
            Guard.VerifyObjectNotNull(dtoProvider, nameof(dtoProvider));
            Guard.VerifyObjectNotNull(attributeRowService, nameof(attributeRowService));
            Guard.VerifyObjectNotNull(moveService, nameof(moveService));
            Guard.VerifyObjectNotNull(movementService, nameof(movementService));

            _dtoProvider = dtoProvider;
            _attributeRowService = attributeRowService;
            _moveService = moveService;
            _movementService = movementService;
        }

        public ICharacter GetSingleByOwnerId(int id, string fields = "")
        {
            return Repository.GetSingleWhere(c => c.OwnerId == id);
        }

        public ICharacter GetSingleByName(string name, string fields = "")
        {
            return Repository.GetSingleWhere(c => c.Name == name);
        }

        public ICharacterDetailsDto GetCharacterDetailsWhereCharacterOwnerIs(string name, string fields = "")
        {
            var character = GetSingleWhere(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            //get movement, attributes and put into aggregate dto 
            var dto = _dtoProvider.CreateCharacterDetailsDto();

            var movements = _movementService.GetAllWhereCharacterNameIs(name, fields);
            var attributeRows = _attributeRowService.GetAllWhereCharacterNameIs(name, fields);

            dto.Metadata = character;
            dto.Movements = movements;
            dto.AttributeRows = attributeRows;

            return dto;
        }

        public ICharacterDetailsDto GetCharacterDetailsWhereCharacterOwnerIdIs(int id, string fields)
        {
            var character = GetSingleWhere(c => c.OwnerId == id);

            var dto = _dtoProvider.CreateCharacterDetailsDto();

            var movements = _movementService.GetAllWhereCharacterOwnerIdIs(id, fields);
            var attributeRows = _attributeRowService.GetAllWhereCharacterOwnerIdIs(id, fields);

            dto.Metadata = character;
            dto.Movements = movements;
            dto.AttributeRows = attributeRows;

            return dto;
        }

        public IEnumerable<IMovement> GetAllMovementsWhereCharacterNameIs(string name, string fields = "")
        {
            return _movementService.GetAllWhereCharacterNameIs(name, fields);
        }

        public IEnumerable<IMovement> GetAllMovementsWhereCharacterOwnerIdIs(int id, string fields = "")
        {
            return _movementService.GetAllWhereCharacterOwnerIdIs(id, fields);
        }

        public IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterNameIs(string name, string fields = "")
        {
            return _attributeRowService.GetAllWhereCharacterNameIs(name, fields);
        }

        public IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterOwnerIdIs(int id, string fields = "")
        {
            return _attributeRowService.GetAllWhereCharacterOwnerIdIs(id, fields);
        }

        public IEnumerable<ParsedMove> GetDetailedMovesWhereCharacterNameIs(string name, string fields = "")
        {
            var foundCharacter = GetSingleByName(name, fields);
            return GetDetailedMovesCore(foundCharacter);
        }

        public IEnumerable<ParsedMove> GetDetailedMovesWhereCharacterOwnerIdIs(int id, string fields = "")
        {
            var foundCharacter = GetSingleByOwnerId(id, fields);
            return GetDetailedMovesCore(foundCharacter);
        }

        private IEnumerable<ParsedMove> GetDetailedMovesCore(ICharacter character)
        {
            return _moveService.GetAllMovePropertyDataForCharacter(character);
        }

        public IEnumerable<IMove> GetAllThrowsWhereCharacterNameIs(string name, string fields = "")
        {
            var foundCharacter = GetSingleByName(name, fields);
            return _moveService.GetAllThrowsForCharacter(foundCharacter, fields);
        }

        public IEnumerable<IMove> GetAllMovesWhereCharacterNameIs(string name, string fields = "")
        {
            var foundCharacter = GetSingleByName(name, fields);
            return _moveService.GetAllMovesForCharacter(foundCharacter, fields);
        }

        public IEnumerable<IMove> GetAllThrowsWhereCharacterOwnerIdIs(int id, string fields = "")
        {
            var foundCharacter = GetSingleByOwnerId(id, fields);
            return _moveService.GetAllThrowsForCharacter(foundCharacter, fields);
        }

        public IEnumerable<IMove> GetAllMovesWhereCharacterOwnerIdIs(int id, string fields = "")
        {
            var foundCharacter = GetSingleByOwnerId(id, fields);
            return _moveService.GetAllMovesForCharacter(foundCharacter, fields);
        }

        public IEnumerable<IMove> GetAllMovesForCharacterByNameFilteredBy(IMoveFilterResourceQuery query)
        {
            return _moveService.GetAllWhere(query);
        }

        public IEnumerable<IMove> GetAllMovesForCharacterByOwnerIdFilteredBy(IMoveFilterResourceQuery query)
        {
            return _moveService.GetAllWhere(query);
        }
    }
}
