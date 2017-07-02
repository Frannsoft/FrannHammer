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

        public ICharacter GetSingleByOwnerId(int id)
        {
            return Repository.GetSingleWhere(c => c.OwnerId == id);
        }

        public ICharacter GetSingleByName(string name)
        {
            var character = Repository.GetSingleWhere(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (character == null)
            {
                throw new ResourceNotFoundException($"No character with name '{name}' found.");
            }
            return character;
        }

        public ICharacterDetailsDto GetCharacterDetailsWhereCharacterOwnerIs(string name)
        {
            var character = GetSingleWhere(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            //get movement, attributes and put into aggregate dto 
            var dto = _dtoProvider.CreateCharacterDetailsDto();

            var movements = _movementService.GetAllWhereCharacterNameIs(name);
            var attributeRows = _attributeRowService.GetAllWhereCharacterNameIs(name);

            dto.Metadata = character;
            dto.Movements = movements;
            dto.AttributeRows = attributeRows;

            return dto;
        }

        public ICharacterDetailsDto GetCharacterDetailsWhereCharacterOwnerIdIs(int id)
        {
            var character = GetSingleWhere(c => c.OwnerId == id);

            var dto = _dtoProvider.CreateCharacterDetailsDto();

            var movements = _movementService.GetAllWhereCharacterOwnerIdIs(id);
            var attributeRows = _attributeRowService.GetAllWhereCharacterOwnerIdIs(id);

            dto.Metadata = character;
            dto.Movements = movements;
            dto.AttributeRows = attributeRows;

            return dto;
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

        public IEnumerable<ParsedMove> GetDetailedMovesWhereCharacterNameIs(string name)
        {
            var foundCharacter = GetSingleByName(name);
            return GetDetailedMovesCore(foundCharacter);
        }

        public IEnumerable<ParsedMove> GetDetailedMovesWhereCharacterOwnerIdIs(int id)
        {
            var foundCharacter = GetSingleByOwnerId(id);
            return GetDetailedMovesCore(foundCharacter);
        }

        private IEnumerable<ParsedMove> GetDetailedMovesCore(ICharacter character)
        {
            return _moveService.GetAllMovePropertyDataForCharacter(character);
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

        public IEnumerable<IMove> GetAllMovesForCharacterByNameFilteredBy(IMoveFilterResourceQuery query)
        {
            return _moveService.GetAllWhere(query);
        }

        public IEnumerable<IMove> GetAllMovesForCharacterByOwnerIdFilteredBy(IMoveFilterResourceQuery query)
        {
            return _moveService.GetAllWhere(query);
        }

        public IEnumerable<IMovement> GetAllMovementsWhereCharacterOwnerIdIsFilteredBy(IMovementFilterResourceQuery query)
        {
            return _movementService.GetAllWhere(query);
        }

        public IEnumerable<IMovement> GetAllMovementsWhereCharacterNameIsFilteredBy(IMovementFilterResourceQuery query)
        {
            return _movementService.GetAllWhere(query);
        }
    }
}
