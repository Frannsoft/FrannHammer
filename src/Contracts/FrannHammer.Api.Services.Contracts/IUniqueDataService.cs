using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IUniqueDataService : ICrudService<IUniqueData>
    {
        IEnumerable<IUniqueData> GetAllWhereOwnerId(int id);
    }
}
