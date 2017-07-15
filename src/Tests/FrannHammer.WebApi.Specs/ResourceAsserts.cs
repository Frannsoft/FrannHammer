using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Models;
using NUnit.Framework;
using static FrannHammer.WebApi.Specs.ExpectedLinkRelConstants;

namespace FrannHammer.WebApi.Specs
{
    public static class ResourceAsserts
    {
        public static void AssertCharacterAttributeRowIsValid(CharacterAttributeRowResource characterAttributeRow)
        {
            Assert.That(characterAttributeRow, Is.Not.Null, $"{nameof(characterAttributeRow)}");
            Assert.That(characterAttributeRow.InstanceId, Is.Not.Null, $"{nameof(characterAttributeRow.InstanceId)}");
            Assert.That(characterAttributeRow.Name, Is.Not.Null, $"{nameof(characterAttributeRow.Name)}");
            Assert.That(characterAttributeRow.Owner, Is.Not.Null, $"{nameof(characterAttributeRow.Owner)}");

            var characterLink = characterAttributeRow.Links.FirstOrDefault(l => l.Rel.Equals(CharacterLinkName));
            AssertSelfLinkIsPresent(characterAttributeRow);
            Assert.That(characterLink, Is.Not.Null, $"Unable to find '{CharacterLinkName}' link.");

            var attributeValues = characterAttributeRow.Values.ToList();
            attributeValues.ForEach(value =>
            {
                Assert.That(value.Name, Is.Not.Null, $"{nameof(IAttribute.Name)}");
                Assert.That(value.Owner, Is.Not.Null, $"{nameof(IAttribute.Owner)}");
                Assert.That(value.Value, Is.Not.Null, $"{nameof(IAttribute.Value)}");
            });
        }

        public static void AssertCharacterAttributeIsValid(CharacterAttributeNameResource characterAttributeName)
        {
            Assert.That(characterAttributeName.Name, Is.Not.Null, $"{nameof(characterAttributeName.Name)}");

            var allAttributesByNameLink = characterAttributeName.Links.FirstOrDefault(l => l.Rel.Equals(CharacterAttributeLinkName));
            Assert.That(allAttributesByNameLink, Is.Not.Null, $"Unable to find '{CharacterAttributeLinkName}' link.");

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(allAttributesByNameLink.Href, Contains.Substring(characterAttributeName.Name));
        }

        public static void AssertCharacterIsValid(CharacterResource characterResource)
        {
            Assert.That(characterResource, Is.Not.Null, $"{nameof(characterResource)}");
            Assert.That(characterResource.ThumbnailUrl, Is.Not.Null, $"{nameof(characterResource.ThumbnailUrl)}");
            Assert.That(characterResource.DisplayName, Is.Not.Null, $"{nameof(characterResource.DisplayName)}");
            Assert.That(characterResource.ColorTheme, Is.Not.Null, $"{nameof(characterResource.ColorTheme)}");
            Assert.That(characterResource.FullUrl, Is.Not.Null, $"{nameof(characterResource.FullUrl)}");
            Assert.That(characterResource.InstanceId, Is.Not.Null, $"{nameof(characterResource.InstanceId)}");
            Assert.That(characterResource.MainImageUrl, Is.Not.Null, $"{nameof(characterResource.MainImageUrl)}");
            Assert.That(characterResource.Name, Is.Not.Null, $"{nameof(characterResource.Name)}");
            AssertCharacterResourceHalLinksArePresent(characterResource);
        }

        private static void AssertCharacterResourceHalLinksArePresent(CharacterResource resource)
        {
            var moveLink = resource.Links.FirstOrDefault(l => l.Rel.Equals(MoveLinkName));
            var characterAttributesLink = resource.Links.FirstOrDefault(l => l.Rel.Equals(CharacterAttributeLinkName));
            var movementsLink = resource.Links.FirstOrDefault(l => l.Rel.Equals(MovementLinkName));
            AssertSelfLinkIsPresent(resource);
            Assert.That(moveLink, Is.Not.Null, $"Unable to find '{MoveLinkName}' link.");
            Assert.That(characterAttributesLink, Is.Not.Null, $"Unable to find '{CharacterAttributeLinkName}' link.");
            Assert.That(movementsLink, Is.Not.Null, $"Unable to find '{MovementLinkName}' link.");
        }

        public static void AssertAllMovesAreValid(IList<MoveResource> moves)
        {
            moves.Where(m => m.MoveType == MoveType.Ground.ToString()).ToList().ForEach(AssertGroundMoveIsValid);
            moves.Where(m => m.MoveType == MoveType.Aerial.ToString()).ToList().ForEach(AssertAerialMoveIsValid);
            moves.Where(m => m.MoveType == MoveType.Special.ToString()).ToList().ForEach(AssertSpecialMoveIsValid);
        }

        public static void AssertGroundMoveIsValid(MoveResource move)
        {
            Assert.That(move, Is.Not.Null, $"{nameof(move)}");
            Assert.That(move.Angle, Is.Not.Null, $"{nameof(move.Angle)}");
            Assert.That(move.BaseDamage, Is.Not.Null, $"{nameof(move.BaseDamage)}");
            Assert.That(move.BaseKnockBackSetKnockback, Is.Not.Null, $"{nameof(move.BaseKnockBackSetKnockback)}");
            Assert.That(move.FirstActionableFrame, Is.Not.Null, $"{nameof(move.FirstActionableFrame)}");
            Assert.That(move.HitboxActive, Is.Not.Null, $"{nameof(move.HitboxActive)}");
            Assert.That(move.InstanceId, Is.Not.Null, $"{nameof(move.InstanceId)}");
            Assert.That(move.KnockbackGrowth, Is.Not.Null, $"{nameof(move.KnockbackGrowth)}");
            Assert.That(move.Name, Is.Not.Null, $"{nameof(move.Name)}");
            Assert.That(move.Owner, Is.Not.Null, $"{nameof(move.Owner)}");
            AssertSelfLinkIsPresent(move);
            AssertResourceContainsCorrectOwnerLink(move.Links.FirstOrDefault(l => l.Rel.Equals(CharacterLinkName)), move.Owner);
        }

        public static void AssertAerialMoveIsValid(MoveResource move)
        {
            Assert.That(move, Is.Not.Null, $"{nameof(move)}");
            Assert.That(move.Angle, Is.Not.Null, $"{nameof(move.Angle)}");
            Assert.That(move.BaseDamage, Is.Not.Null, $"{nameof(move.BaseDamage)}");
            Assert.That(move.BaseKnockBackSetKnockback, Is.Not.Null, $"{nameof(move.BaseKnockBackSetKnockback)}");
            Assert.That(move.FirstActionableFrame, Is.Not.Null, $"{nameof(move.FirstActionableFrame)}");
            Assert.That(move.HitboxActive, Is.Not.Null, $"{nameof(move.HitboxActive)}");
            Assert.That(move.InstanceId, Is.Not.Null, $"{nameof(move.InstanceId)}");
            Assert.That(move.KnockbackGrowth, Is.Not.Null, $"{nameof(move.KnockbackGrowth)}");
            Assert.That(move.Name, Is.Not.Null, $"{nameof(move.Name)}");
            Assert.That(move.Owner, Is.Not.Null, $"{nameof(move.Owner)}");
            Assert.That(move.LandingLag, Is.Not.Null, $"{nameof(move.LandingLag)}");
            Assert.That(move.AutoCancel, Is.Not.Null, $"{nameof(move.AutoCancel)}");
            AssertSelfLinkIsPresent(move);
            AssertResourceContainsCorrectOwnerLink(move.Links.FirstOrDefault(l => l.Rel.Equals(CharacterLinkName)), move.Owner);
        }

        public static void AssertSpecialMoveIsValid(MoveResource move)
        {
            Assert.That(move, Is.Not.Null, $"{nameof(move)}");
            Assert.That(move.Angle, Is.Not.Null, $"{nameof(move.Angle)}");
            Assert.That(move.BaseDamage, Is.Not.Null, $"{nameof(move.BaseDamage)}");
            Assert.That(move.BaseKnockBackSetKnockback, Is.Not.Null, $"{nameof(move.BaseKnockBackSetKnockback)}");
            Assert.That(move.FirstActionableFrame, Is.Not.Null, $"{nameof(move.FirstActionableFrame)}");
            Assert.That(move.HitboxActive, Is.Not.Null, $"{nameof(move.HitboxActive)}");
            Assert.That(move.InstanceId, Is.Not.Null, $"{nameof(move.InstanceId)}");
            Assert.That(move.KnockbackGrowth, Is.Not.Null, $"{nameof(move.KnockbackGrowth)}");
            Assert.That(move.Name, Is.Not.Null, $"{nameof(move.Name)}");
            Assert.That(move.Owner, Is.Not.Null, $"{nameof(move.Owner)}");
            AssertSelfLinkIsPresent(move);
            AssertResourceContainsCorrectOwnerLink(move.Links.FirstOrDefault(l => l.Rel.Equals(CharacterLinkName)), move.Owner);
        }

        public static void AssertThrowMoveIsValid(MoveResource move)
        {
            Assert.That(move, Is.Not.Null, $"{nameof(move)}");
            Assert.That(move.Angle, Is.Not.Null, $"{nameof(move.Angle)}");
            Assert.That(move.BaseDamage, Is.Not.Null, $"{nameof(move.BaseDamage)}");
            Assert.That(move.BaseKnockBackSetKnockback, Is.Not.Null, $"{nameof(move.BaseKnockBackSetKnockback)}");
            Assert.That(move.InstanceId, Is.Not.Null, $"{nameof(move.InstanceId)}");
            Assert.That(move.KnockbackGrowth, Is.Not.Null, $"{nameof(move.KnockbackGrowth)}");
            Assert.That(move.Name, Is.Not.Null, $"{nameof(move.Name)}");
            Assert.That(move.Owner, Is.Not.Null, $"{nameof(move.Owner)}");
            AssertSelfLinkIsPresent(move);
            AssertResourceContainsCorrectOwnerLink(move.Links.FirstOrDefault(l => l.Rel.Equals(CharacterLinkName)), move.Owner);
        }

        public static void AssertGrabMoveIsValid(MoveResource move)
        {
            Assert.That(move, Is.Not.Null, $"{nameof(move)}");
            Assert.That(move.Owner, Is.Not.Null, $"{nameof(move.Owner)}");
            Assert.That(move.Name, Is.Not.Null, $"{nameof(move.Name)}");
            Assert.That(move.InstanceId, Is.Not.Null, $"{nameof(move.InstanceId)}");
            Assert.That(move.FirstActionableFrame, Is.Not.Null, $"{nameof(move.FirstActionableFrame)}");
            AssertSelfLinkIsPresent(move);
            AssertResourceContainsCorrectOwnerLink(move.Links.FirstOrDefault(l => l.Rel.Equals(CharacterLinkName)), move.Owner);
        }

        private static void AssertSelfLinkIsPresent(Resource resource)
        {
            var selfLink = resource.Links.FirstOrDefault(l => l.Rel.Equals(SelfLinkName));

            Assert.That(selfLink, Is.Not.Null, $"Unable to find '{SelfLinkName}' link.");
        }

        private static void AssertResourceContainsCorrectOwnerLink(Link link, string owner)
        {


            Assert.That(link, Is.Not.Null);
            Assert.That(link.Rel, Is.EqualTo(CharacterLinkName));
            StringAssert.Contains(owner, link.Href.Replace("%20", " "));
        }

        public static void AssertMovementIsValid(MovementResource movement)
        {
            Assert.That(movement, Is.Not.Null, $"{nameof(movement)}");
            Assert.That(movement.Name, Is.Not.Null, $"{nameof(movement.Name)}");
            Assert.That(movement.Owner, Is.Not.Null, $"{nameof(movement.Owner)}");
            Assert.That(movement.Value, Is.Not.Null, $"{nameof(movement.Value)}");
            Assert.That(movement.InstanceId, Is.Not.Null, $"{nameof(movement.InstanceId)}");
            Assert.That(movement.Links.Any(l => l.Rel.Equals(CharacterLinkName)), $"Unable to find '{CharacterLinkName}' link.");
        }

        public static void AssertUniqueDataIsValid(UniqueDataResource uniqueData)
        {
            Assert.That(uniqueData, Is.Not.Null, $"{nameof(uniqueData)}");
            Assert.That(uniqueData.Name, Is.Not.Null, $"{nameof(uniqueData.Name)}");
            Assert.That(uniqueData.Owner, Is.Not.Null, $"{nameof(uniqueData.Owner)}");
            Assert.That(uniqueData.Value, Is.Not.Null, $"{nameof(uniqueData.Value)}");
            Assert.That(uniqueData.InstanceId, Is.Not.Null, $"{nameof(uniqueData.InstanceId)}");
            Assert.That(uniqueData.Links.Any(l => l.Rel.Equals(CharacterLinkName)), $"Unable to find '{CharacterLinkName}' link.");
            Assert.That(uniqueData.Links.Any(l => l.Rel.Equals(SelfLinkName)), $"Unable to find '{SelfLinkName}' link.");
        }
    }
}
