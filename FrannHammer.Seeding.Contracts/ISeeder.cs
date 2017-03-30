using FrannHammer.Domain.Contracts;

namespace FrannHammer.Seeding.Contracts
{
    public interface ISeeder
    {
        void SeedCharacterMetadata<T>(T character) where T : ICharacter;
        void SeedAttributeData(IAttribute attribute);
    }
}
