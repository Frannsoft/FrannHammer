using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterAttributeService
    {
        ICharacterAttributeRow Get(int id, string fields = "");
        IEnumerable<ICharacterAttributeRow> GetAll(string fields = "");
        ICharacterAttributeRow Add(ICharacterAttributeRow attribute);
        void AddMany(IEnumerable<ICharacterAttributeRow> attributes);
    }
}
