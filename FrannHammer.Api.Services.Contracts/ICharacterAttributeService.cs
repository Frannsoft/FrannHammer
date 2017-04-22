using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterAttributeService
    {
        IAttribute Get(int id, string fields = "");
        IEnumerable<IAttribute> GetAll(string fields = "");
        IAttribute Add(IAttribute attribute);
        void AddMany(IEnumerable<IAttribute> attributes);
    }
}
