using System;
using System.Linq;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    [TestFixture]
    public class MoveMetadataIntegrityTest : BaseDataIntegrityTest
    {
        [Test]
        [TestCase(typeof(Angle))]
        [TestCase(typeof(BaseDamage))]
        [TestCase(typeof(BaseKnockback))]
        [TestCase(typeof(Hitbox))]
        [TestCase(typeof(KnockbackGrowth))]
        [TestCase(typeof(SetKnockback))]
        public void AllMetadataEntriesOfTypeHaveMoveId(Type moveMetadataType)
        {
            var entries = Context.Set(moveMetadataType).Local.Cast<BaseMeta>().ToList();
            entries.ForEach(e => Assert.That(e.MoveId > 0));
        }

        [Test]
        [TestCase(typeof(Angle))]
        [TestCase(typeof(BaseDamage))]
        [TestCase(typeof(BaseKnockback))]
        [TestCase(typeof(Hitbox))]
        [TestCase(typeof(KnockbackGrowth))]
        [TestCase(typeof(SetKnockback))]
        public void AllMetadataEntriesOfTypeHaveOwnerId(Type moveMetadataType)
        {
            var entries = Context.Set(moveMetadataType).Local.Cast<BaseMeta>().ToList();
            entries.ForEach(e => Assert.That(e.OwnerId > 0));
        }

        [Test]
        [TestCase(typeof(Angle))]
        [TestCase(typeof(BaseDamage))]
        [TestCase(typeof(BaseKnockback))]
        [TestCase(typeof(Hitbox))]
        [TestCase(typeof(KnockbackGrowth))]
        [TestCase(typeof(SetKnockback))]
        public void AllMetadataEntriesOfTypeHaveRawValue(Type moveMetadataType)
        {
            var entries = Context.Set(moveMetadataType).Local.Cast<BaseMeta>().ToList();
            entries.ForEach(e => Assert.That(!string.IsNullOrEmpty(e.RawValue)));
        }
    }
}
