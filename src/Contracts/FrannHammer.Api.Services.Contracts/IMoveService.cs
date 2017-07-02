using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMoveService : ICrudService<IMove>
    {
        IEnumerable<IDictionary<string, string>> GetAllPropertyDataWhereName(string name, string property);
        IDictionary<string, string> GetPropertyDataWhereId(string id, string property);
        IEnumerable<IMove> GetAllThrowsWhereCharacterNameIs(string name);
        IEnumerable<IMove> GetAllWhereCharacterNameIs(string name);
        IEnumerable<IMove> GetAllWhere(IMoveFilterResourceQuery query);
        IEnumerable<ParsedMove> GetAllMovePropertyDataForCharacter(ICharacter character);
        IEnumerable<IMove> GetAllThrowsWhereCharacterOwnerIdIs(int ownerId);
        IEnumerable<IMove> GetAllWhereCharacterOwnerIdIs(int id);
        IEnumerable<IMove> GetAllThrowsForCharacter(ICharacter character);
        IEnumerable<IMove> GetAllMovesForCharacter(ICharacter character);
    }
}
