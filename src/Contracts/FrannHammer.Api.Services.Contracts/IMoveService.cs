using FrannHammer.Domain.Contracts;
using System.Collections.Generic;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMoveService : ICrudService<IMove>
    {
        IEnumerable<IMove> GetAllThrowsWhereCharacterNameIs(string name);
        IEnumerable<IMove> GetAllWhereCharacterNameIs(string name);
        IEnumerable<IMove> GetAllThrowsWhereCharacterOwnerIdIs(int ownerId);
        IEnumerable<IMove> GetAllWhereCharacterOwnerIdIs(int id);
        IEnumerable<IMove> GetAllThrowsForCharacter(ICharacter character);
        IEnumerable<IMove> GetAllMovesForCharacter(ICharacter character);
    }
}
