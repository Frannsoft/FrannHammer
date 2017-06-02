using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;

namespace FrannHammer.WebApi.Specs.Characters
{
    public static class CharacterDetailsDtoAssertions
    {
        public static void AssertIsValid(this ICharacter character)
        {
            Assert.That(character.Name, Is.Not.Empty, $"{nameof(character.Name)}");
        }

        public static void AssertIsValid(this IEnumerable<IMovement> movements)
        {
            movements.ToList().ForEach(movement =>
            {
                Assert.That(movement.Owner, Is.Not.Empty, $"{nameof(movement.Owner)}");
            });
        }

        public static void AssertIsValid(this IEnumerable<ICharacterAttributeRow> attributeRows)
        {
            attributeRows.ToList().ForEach(row =>
            {
                Assert.That(row.Owner, Is.Not.Empty, $"{nameof(row.Owner)}");
            });
        }
    }
}