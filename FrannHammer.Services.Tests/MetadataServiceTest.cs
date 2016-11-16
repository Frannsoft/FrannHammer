﻿using System.Collections.Generic;
using FrannHammer.Models;
using FrannHammer.Services.Exceptions;
using FrannHammer.Services.Tests.Harnesses;
using NUnit.Framework;
using StackExchange.Redis;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class MetadataServiceTest : ServiceBaseTest
    {
        private static IEnumerable<IMetadataHarness> MetadataTestCases()
        {
            yield return new MetadataHarness<Throw, ThrowDto>($"{Id},{WeightDependent}");
            yield return new MetadataHarness<Character, CharacterDto>($"{Id},{Name},{DisplayName}");
            yield return new MetadataHarness<Notation, NotationDto>($"{Id},{Name}");
            yield return new MetadataHarness<SmashAttributeType, SmashAttributeTypeDto>($"{Id},{Name}");
            yield return new MetadataHarness<ThrowType, ThrowTypeDto>($"{Id},{Name}");
            yield return new MetadataHarness<Movement, MovementDto>($"{Name},{OwnerId},{Value}");
            yield return new MetadataHarness<Character, CharacterDto>($"{Name},{Id},{Name}");
        }

        private static IEnumerable<IMoveDataHarness> MoveTestCases()
        {
            yield return new MoveDataHarness<Angle, AngleDto>($"{Id},{MoveName},{Hitbox1}");
            yield return new MoveDataHarness<BaseDamage, BaseDamageDto>($"{Id},{MoveName},{Hitbox1}");
            yield return new MoveDataHarness<Hitbox, HitboxDto>($"{Id},{MoveName},{Hitbox1}");
            yield return new MoveDataHarness<KnockbackGrowth, KnockbackGrowthDto>($"{Id},{MoveName},{Hitbox1}");
        }

        private static IEnumerable<IMoveDataHarness> BaseKnockBackMoveTestCases()
        {
            yield return new MoveDataHarness<BaseKnockback, BaseKnockbackDto>($"{Id},{MoveName},{Hitbox2}");
        }

        private static IEnumerable<IMoveDataHarness> SetKnockBackMoveTestCases()
        {
            yield return new MoveDataHarness<SetKnockback, SetKnockbackDto>($"{MoveName},{RawValue}");
        }

        #region move data tests
        [Test]
        [TestCaseSource(nameof(MoveTestCases))]
        public void ShouldGetAll_Moves(IMoveDataHarness moveDataHarness)
        {
            moveDataHarness.CollectionIsValid(Context);
        }

        [Test]
        [TestCaseSource(nameof(BaseKnockBackMoveTestCases))]
        public void ShouldGetAllBaseKnockback_Moves(IMoveDataHarness moveDataHarness)
        {
            moveDataHarness.SingleIsValidMove(1, Context);
        }

        [Test]
        [TestCaseSource(nameof(SetKnockBackMoveTestCases))]
        public void ShouldGetAllSetKnockback_Moves(IMoveDataHarness moveDataHarness)
        {
            //TODO: I hate magic numbers.  Id = 4 is the first entry for set knockback in the db
            moveDataHarness.SingleIsValidMove(4, Context);
        }

        [Test]
        [TestCaseSource(nameof(MoveTestCases))]
        public void ShouldGetSingle_Move(IMoveDataHarness moveDataHarness)
        {
            moveDataHarness.SingleIsValidMove(1, Context);
        }

        [Test]
        [TestCaseSource(nameof(BaseKnockBackMoveTestCases))]
        public void ShouldGetSingleWithFieldsBaseKnockback_Moves(IMoveDataHarness moveDataHarness)
        {
            moveDataHarness.SingleIsValidMove(1, Context);
        }

        [Test]
        [TestCaseSource(nameof(SetKnockBackMoveTestCases))]
        public void ShouldGetSingleWithFieldsSetKnockback_Moves(IMoveDataHarness moveDataHarness)
        {
            //TODO: I hate magic numbers.  Id = 4 is the first entry for set knockback in the db
            moveDataHarness.SingleIsValidMove(4, Context);
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

        [Test]
        public void ThrowsForNonExistentIdOnGetSingle()
        {
            Assert.Throws<EntityNotFoundException>(() =>
            {
                new MetadataService(Context, ResultValidationService).Get<Character, CharacterDto>(200);
            });
        }

        [Test]
        public void ThrowsForNonExistentIdOnGetAll()
        {
            Assert.Throws<EntityNotFoundException>(() =>
            {
                new MetadataService(Context, ResultValidationService).GetAll<Character, CharacterDto>(c => c.Id == 200);
            });
        }

        [Test]
        public void ThrowsForNonExistentIdOnGetAllForOwnerId()
        {
            Assert.Throws<EntityNotFoundException>(() =>
            {
                new MetadataService(Context, ResultValidationService).GetAllForOwnerId<Move, Angle, AngleDto>(200);
            });
        }
    }
}
