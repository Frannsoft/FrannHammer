using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterService : ICrudService<ICharacter>
    {
        ICharacter GetSingleByOwnerId(int id, string fields = "");
        ICharacter GetSingleByName(string name, string fields = "");
    }
}
