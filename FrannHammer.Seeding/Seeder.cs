using System;
using FrannHammer.Domain.Contracts;
using FrannHammer.Seeding.Contracts;

namespace FrannHammer.Seeding
{
    public class Seeder : ISeeder
    {
        public void SeedCharacterMetadata<T>(T character) where T : ICharacter
        {
            throw new NotImplementedException();
        }

        public void SeedAttributeData(IAttribute attribute)
        {
            throw new NotImplementedException();
        }
    }
}
