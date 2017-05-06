using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace FrannHammer.Domain.Tests
{
    [TestFixture]
    public class FriendlyNameTests
    {
        private static IEnumerable<Tuple<string, string, Type>> CharacterFriendlyInfo()
        {
            yield return Tuple.Create("displayName", nameof(Character.DisplayName), typeof(Character));
            yield return Tuple.Create("colorTheme", nameof(Character.ColorTheme), typeof(Character));
            yield return Tuple.Create("fullUrl", nameof(Character.FullUrl), typeof(Character));
            yield return Tuple.Create("mainImageUrl", nameof(Character.MainImageUrl), typeof(Character));
            yield return Tuple.Create("name", nameof(Character.Name), typeof(Character));
            yield return Tuple.Create("thumbnailUrl", nameof(Character.ThumbnailUrl), typeof(Character));
        }

        private static IEnumerable<Tuple<string, string, Type>> CharacterAttributeFriendlyInfo()
        {
            yield return Tuple.Create("name", nameof(CharacterAttribute.Name), typeof(CharacterAttribute));
            yield return Tuple.Create("owner", nameof(CharacterAttribute.Owner), typeof(CharacterAttribute));
            yield return Tuple.Create("value", nameof(CharacterAttribute.Value), typeof(CharacterAttribute));
        }

        private static IEnumerable<Tuple<string, string, Type>> MoveFriendlyNameInfo()
        {
            yield return Tuple.Create("angle", nameof(Move.Angle), typeof(Move));
            yield return Tuple.Create("autoCancel", nameof(Move.AutoCancel), typeof(Move));
            yield return Tuple.Create("baseDamage", nameof(Move.BaseDamage), typeof(Move));
            yield return Tuple.Create("baseKnockBackSetKnockback", nameof(Move.BaseKnockBackSetKnockback), typeof(Move));
            yield return Tuple.Create("firstActionableFrame", nameof(Move.FirstActionableFrame), typeof(Move));
            yield return Tuple.Create("hitboxActive", nameof(Move.HitboxActive), typeof(Move));
            yield return Tuple.Create("knockbackGrowth", nameof(Move.KnockbackGrowth), typeof(Move));
            yield return Tuple.Create("moveType", nameof(Move.MoveType), typeof(Move));
            yield return Tuple.Create("landingLag", nameof(Move.LandingLag), typeof(Move));
            yield return Tuple.Create("ownerId", nameof(Move.Owner), typeof(Move));
        }

        private static IEnumerable<Tuple<string, string, Type>> MovementFriendlyNameInfo()
        {
            yield return Tuple.Create("name", nameof(Movement.Name), typeof(Movement));
            yield return Tuple.Create("ownerId", nameof(Movement.OwnerId), typeof(Movement));
            yield return Tuple.Create("value", nameof(Movement.Value), typeof(Movement));
        }

        private static IEnumerable<Tuple<string, string, Type>> CharacterAttributeRowFriendlyNameInfo()
        {
            yield return Tuple.Create("name", nameof(DefaultCharacterAttributeRow.Name), typeof(DefaultCharacterAttributeRow));
            yield return Tuple.Create("characterName", nameof(DefaultCharacterAttributeRow.CharacterName), typeof(DefaultCharacterAttributeRow));
            yield return Tuple.Create("values", nameof(DefaultCharacterAttributeRow.Values), typeof(DefaultCharacterAttributeRow));
        }

        [Test]
        [TestCaseSource(nameof(CharacterFriendlyInfo))]
        [TestCaseSource(nameof(CharacterAttributeFriendlyInfo))]
        [TestCaseSource(nameof(MoveFriendlyNameInfo))]
        [TestCaseSource(nameof(MovementFriendlyNameInfo))]
        [TestCaseSource(nameof(CharacterAttributeRowFriendlyNameInfo))]
        public void FriendlyNameCorrect(Tuple<string, string, Type> info)
        {
            string expectedFriendlyName = info.Item1;
            string propertyName = info.Item2;
            var type = info.Item3;

            var actualFriendlyName =
                type.GetProperty(propertyName)
                    .GetCustomAttribute<FriendlyNameAttribute>()
                    .Name;

            Assert.That(actualFriendlyName, Is.EqualTo(expectedFriendlyName));
        }
    }
}
