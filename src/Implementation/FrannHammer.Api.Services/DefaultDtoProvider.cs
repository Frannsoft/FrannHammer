using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services
{
    public class DefaultDtoProvider : IDtoProvider
    {
        public ICharacterDetailsDto CreateCharacterDetailsDto()
        {
            return new CharacterDetailsDto();
        }
    }
}
