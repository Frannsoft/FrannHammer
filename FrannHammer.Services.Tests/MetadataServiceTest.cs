using System.Collections.Generic;
using FrannHammer.Models;
using FrannHammer.Services.Tests.Harnesses;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class MetadataServiceTest : ServiceBaseTest
    {
        public IEnumerable<IMetadataHarness> MetadataTestCases()
        {
            yield return new MetadataHarness<Throw, ThrowDto>("Id,WeightDependent");
            yield return new MetadataHarness<Character, CharacterDto>("Id,Name,DisplayName");
            yield return new MetadataHarness<Notation, Notation>("Id,Name");
            yield return new MetadataHarness<SmashAttributeType, SmashAttributeTypeDto>("Id,Name");
            yield return new MetadataHarness<ThrowType, ThrowTypeDto>("Id,Name");
            yield return new MetadataHarness<Movement, MovementDto>("Name,OwnerId,Value");
            yield return new MetadataHarness<Character, CharacterDto>("Name,Id,Name");
        }

        public IEnumerable<IMoveDataHarness> MoveTestCases()
        {
            yield return new MoveDataHarness<Angle, AngleDto>("Id,MoveName,Hitbox1");
            yield return new MoveDataHarness<BaseDamage, BaseDamageDto>("Id,MoveName,Hitbox1");
            yield return new MoveDataHarness<Hitbox, HitboxDto>("Id,MoveName,Hitbox1");
            yield return new MoveDataHarness<KnockbackGrowth, KnockbackGrowthDto>("Id,MoveName,Hitbox1");
            yield return new MoveDataHarness<BaseKnockback, BaseKnockbackDto>("Id,MoveName,Hitbox2");
            yield return new MoveDataHarness<SetKnockback, SetKnockbackDto>("MoveName,RawValue");
        }

        #region move data tests
        [Test]
        [TestCaseSource(nameof(MoveTestCases))]
        public void ShouldGetAll_Moves(IMoveDataHarness moveDataHarness)
        {
            moveDataHarness.CollectionIsValid(Context);
        }

        [Test]
        [TestCaseSource(nameof(MoveTestCases))]
        public void ShouldGetSingle_Move(IMoveDataHarness moveDataHarness)
        {
            moveDataHarness.SingleIsValidMove(1, Context);
        }

        [Test]
        [TestCaseSource(nameof(MoveTestCases))]
        public void ShouldGetSingleWithFields_Moves(IMoveDataHarness moveDataHarness)
        {
            moveDataHarness.SingleIsValidMoveWithFields(1, Context, moveDataHarness.Fields);
        }

        [Test]
        [TestCaseSource(nameof(MoveTestCases))]
        public void ShouldGetAllWithFields_Moves(IMoveDataHarness moveDataHarness)
        {
            moveDataHarness.CollectionIsValidWithFields(Context, moveDataHarness.Fields);
        }
        #endregion

        #region metadata tests
        [Test]
        [TestCaseSource(nameof(MetadataTestCases))]
        public void ShouldGetAll_Metadata(IMetadataHarness metadataHarness)
        {
            metadataHarness.CollectionIsValid(Context);
        }

        [Test]
        [TestCaseSource(nameof(MetadataTestCases))]
        public void ShouldGetSingle_Metadata(IMetadataHarness metadataHarness)
        {
            metadataHarness.SingleIsValidMeta(1, Context);
        }

        [Test]
        [TestCaseSource(nameof(MetadataTestCases))]
        public void ShouldGetSingleWithFields_Metadata(IMetadataHarness metadataHarness)
        {
            metadataHarness.SingleIsValidMetaWithFields(1, Context, metadataHarness.Fields);
        }

        [Test]
        [TestCaseSource(nameof(MetadataTestCases))]
        public void ShouldGetAllWithFields_Metadata(IMetadataHarness metadataHarness)
        {
            metadataHarness.CollectionIsValidWithFields(Context, metadataHarness.Fields);
        }
        #endregion
    }
}
