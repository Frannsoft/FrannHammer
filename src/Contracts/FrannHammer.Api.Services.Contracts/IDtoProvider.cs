using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IDtoProvider
    {
        ICharacterDetailsDto CreateCharacterDetailsDto();
    }
}
