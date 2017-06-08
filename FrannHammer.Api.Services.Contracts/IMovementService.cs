using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMovementService : ICrudService<IMovement>
    {
        IEnumerable<IMovement> GetAllWhereCharacterNameIs(string name, string fields = "");
        IEnumerable<IMovement> GetAllWhereCharacterOwnerIdIs(int id, string fields = "");
        IEnumerable<IMovement> GetAllWhere(IFilterResourceQuery query, string fields = "");
    }
}
