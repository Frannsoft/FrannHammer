using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterService : ICrudService<ICharacter>
    {
        ICharacter GetSingleByOwnerId(int id);
        ICharacter GetSingleByName(string name);
        ICharacterDetailsDto GetCharacterDetailsWhereCharacterOwnerIs(string name);
        IEnumerable<IMovement> GetAllMovementsWhereCharacterNameIs(string name);
        IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterNameIs(string name);
        IEnumerable<ParsedMove> GetDetailedMovesWhereCharacterNameIs(string name);
        IEnumerable<IMovement> GetAllMovementsWhereCharacterOwnerIdIs(int id);
        ICharacterDetailsDto GetCharacterDetailsWhereCharacterOwnerIdIs(int id);
        IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterOwnerIdIs(int id);
        IEnumerable<ParsedMove> GetDetailedMovesWhereCharacterOwnerIdIs(int id);
        IEnumerable<IMove> GetAllThrowsWhereCharacterOwnerIdIs(int id);
        IEnumerable<IMove> GetAllThrowsWhereCharacterNameIs(string name);
        IEnumerable<IMove> GetAllMovesWhereCharacterOwnerIdIs(int id);
        IEnumerable<IMove> GetAllMovesWhereCharacterNameIs(string name);
        IEnumerable<IMove> GetAllMovesForCharacterByNameFilteredBy(IMoveFilterResourceQuery query);
        IEnumerable<IMove> GetAllMovesForCharacterByOwnerIdFilteredBy(IMoveFilterResourceQuery query);
        IEnumerable<IMovement> GetAllMovementsWhereCharacterOwnerIdIsFilteredBy(IMovementFilterResourceQuery query);
        IEnumerable<IMovement> GetAllMovementsWhereCharacterNameIsFilteredBy(IMovementFilterResourceQuery query);
        IEnumerable<IUniqueData> GetUniquePropertiesWhereCharacterOwnerIdIs(int id);
        IEnumerable<IUniqueData> GetUniquePropertiesWhereCharacterNameIs(string name);
        IEnumerable<ICharacterAttributeRow> GetAttributesOfNameWhereCharacterOwnerIdIs(string name, int id);
        IEnumerable<ICharacterAttributeRow> GetAttributesOfNameWhereCharacterNameIs(string name, string attributeName);
    }
}
