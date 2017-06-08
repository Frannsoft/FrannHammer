using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterService : ICrudService<ICharacter>
    {
        ICharacter GetSingleByOwnerId(int id, string fields = "");
        ICharacter GetSingleByName(string name, string fields = "");
        ICharacterDetailsDto GetCharacterDetailsWhereCharacterOwnerIs(string name, string fields = "");
        IEnumerable<IMovement> GetAllMovementsWhereCharacterNameIs(string name, string fields = "");
        IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterNameIs(string name, string fields = "");
        IEnumerable<ParsedMove> GetDetailedMovesWhereCharacterNameIs(string name, string fields = "");
        IEnumerable<IMovement> GetAllMovementsWhereCharacterOwnerIdIs(int id, string fields = "");
        ICharacterDetailsDto GetCharacterDetailsWhereCharacterOwnerIdIs(int id, string fields);
        IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterOwnerIdIs(int id, string fields = "");
        IEnumerable<ParsedMove> GetDetailedMovesWhereCharacterOwnerIdIs(int id, string fields = "");

        IEnumerable<IMove> GetAllThrowsWhereCharacterOwnerIdIs(int id, string fields = "");
        IEnumerable<IMove> GetAllThrowsWhereCharacterNameIs(string name, string fields = "");

        IEnumerable<IMove> GetAllMovesWhereCharacterOwnerIdIs(int id, string fields = "");
        IEnumerable<IMove> GetAllMovesWhereCharacterNameIs(string name, string fields = "");

        IEnumerable<IMove> GetAllMovesForCharacterByNameFilteredBy(IMoveFilterResourceQuery query);
        IEnumerable<IMove> GetAllMovesForCharacterByOwnerIdFilteredBy(IMoveFilterResourceQuery query);

        IEnumerable<IMovement> GetAllMovementsWhereCharacterOwnerIdIsFilteredBy(IMovementFilterResourceQuery query);
        IEnumerable<IMovement> GetAllMovementsWhereCharacterNameIsFilteredBy(IMovementFilterResourceQuery query);
    }
}
