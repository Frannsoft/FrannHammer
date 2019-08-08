using FrannHammer.Domain.Contracts;
using System.Collections.Generic;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterService : ICrudService<ICharacter>
    {
        ICharacter GetSingleByOwnerId(int id);
        ICharacter GetSingleByName(string name);
        IEnumerable<IMovement> GetAllMovementsWhereCharacterNameIs(string name);
        IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterNameIs(string name);
        IEnumerable<IMovement> GetAllMovementsWhereCharacterOwnerIdIs(int id);
        IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterOwnerIdIs(int id);
        IEnumerable<IMove> GetAllThrowsWhereCharacterOwnerIdIs(int id);
        IEnumerable<IMove> GetAllThrowsWhereCharacterNameIs(string name);
        IEnumerable<IMove> GetAllMovesWhereCharacterOwnerIdIs(int id);
        IEnumerable<IMove> GetAllMovesWhereCharacterNameIs(string name);
        IEnumerable<IUniqueData> GetUniquePropertiesWhereCharacterOwnerIdIs(int id);
        IEnumerable<IUniqueData> GetUniquePropertiesWhereCharacterNameIs(string name);
        IEnumerable<ICharacterAttributeRow> GetAttributesOfNameWhereCharacterOwnerIdIs(string name, int id);
        IEnumerable<ICharacterAttributeRow> GetAttributesOfNameWhereCharacterNameIs(string name, string attributeName);
    }
}
