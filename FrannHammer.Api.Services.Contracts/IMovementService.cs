using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMovementService : ICrudService<IMovement>
    {
        IEnumerable<IMovement> GetAllWhereCharacterNameIs(string name);
        IEnumerable<IMovement> GetAllWhereCharacterOwnerIdIs(int id);
        IEnumerable<IMovement> GetAllWhere(IFilterResourceQuery query);
    }
}
