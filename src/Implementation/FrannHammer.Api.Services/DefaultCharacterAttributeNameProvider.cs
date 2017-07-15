using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterAttributeNameProvider : ICharacterAttributeNameProvider
    {
        public ICharacterAttributeName Create(string name)
        {
            return new CharacterAttributeName
            {
                Name = name
            };
        }
    }
}
