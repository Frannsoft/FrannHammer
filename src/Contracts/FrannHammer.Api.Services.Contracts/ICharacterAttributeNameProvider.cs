using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterAttributeNameProvider
    {
        ICharacterAttributeName Create(string name);
    }
}
