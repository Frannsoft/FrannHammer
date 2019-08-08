using FrannHammer.Domain.Contracts;
using System.Collections.Generic;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMovementService : ICrudService<IMovement>
    {
        IEnumerable<IMovement> GetAllWhereCharacterNameIs(string name);
        IEnumerable<IMovement> GetAllWhereCharacterOwnerIdIs(int id);
    }
}
