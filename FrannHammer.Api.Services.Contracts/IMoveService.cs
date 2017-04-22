using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMoveService
    {
        IMove Get(int id, string fields = "");
        IEnumerable<IMove> GetAll(string fields = "");
    }
}
