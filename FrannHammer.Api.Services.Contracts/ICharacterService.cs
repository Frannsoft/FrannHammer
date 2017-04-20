using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterService
    {
        ICharacter Get(int id, string fields = "");
        IEnumerable<ICharacter> GetAll(string fields = "");
    }
}
