using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMoveService : ICrudService<IMove>
    {
        IEnumerable<IDictionary<string, string>> GetAllPropertyDataWhereName(string name, string property,
            string fields = "");

        IDictionary<string, string> GetPropertyDataWhereId(string id, string property, string fields = "");
    }
}
