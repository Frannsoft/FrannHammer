using System;
using FrannHammer.Models;

namespace FrannHammer.Api.Tests
{
    public class TestObjects
    {
        private int _moveCounter;
        private int _movementCounter;
        private int _characterCounter;
        private int _smashAttributeTypeCounter;
        private int _characterAttributeCounter;
        private int _characterAttributeTypeCounter = 1;
        private int _notationCounter = 1;
        private int _throwCounter;
        private int _angleCounter;
        private int _baseDamagesCounter;
        private int _knockbackGrowthCounter;
        private int _hitboxCounter;

        public Notation Notation()
        {
            var notation = new Notation
            {
                Id = _notationCounter,
                LastModified = DateTime.Now,
                Name = "notation",
            };

            _notationCounter++;

            return notation;
        }

        public MoveDto Move()
        {
            var move = new MoveDto
            {
                Angle = "40",
                AutoCancel = "3",
                BaseDamage = "53",
                BaseKnockBackSetKnockback = "60/25",
                FirstActionableFrame = "2",
                HitboxActive = "23-25",
                Id = ++_moveCounter,
                KnockbackGrowth = "89",
                LandingLag = "11",
                Name = "falco phantasm",
                OwnerId = 1,
                Type = MoveType.Special
            };

            return move;
        }

        internal CharacterAttributeTypeDto CharacterAttributeType()
        {
            var characterAttributeType = new CharacterAttributeTypeDto
            {
                Id = _characterAttributeTypeCounter,
                Name = "attr name",
                NotationId = _notationCounter
                
            };

            _characterAttributeTypeCounter++;

            return characterAttributeType;
        }

        public MovementDto Movement()
        {
            var movement = new MovementDto
            {
                Id = _movementCounter,
                Name = "jab 1",
                OwnerId = 1,
                Value = "3 frames"
            };

            _movementCounter++;

            return movement;
        }

        public CharacterDto Character()
        {
            var character = new CharacterDto
            {
                ColorTheme = "#323423",
                Description = "desc",
                Id = _characterCounter,
                MainImageUrl = "http://img.com/i.png",
                Name = "falco",
                Style = "prefers the air",
                ThumbnailUrl = "http://img.net/ii.png"
            };

            _characterCounter++;

            return character;
        }

        public SmashAttributeTypeDto SmashAttributeType()
        {
            var smashAttributeType = new SmashAttributeTypeDto
            {
                Id = _smashAttributeTypeCounter,
                Name = "max fall speed",
            };

            _smashAttributeTypeCounter++;

            return smashAttributeType;
        }

        public CharacterAttributeDto CharacterAttribute()
        {
            var characterAttribute = new CharacterAttributeDto
            {
                Id = _characterAttributeCounter,
                Name = "attribute name",
                OwnerId = 1,
                Rank = "4",
                Value = "5.43"
            };

            _characterAttributeCounter++;

            return characterAttribute;
        }

        //public CalculatorRequestModel CalcRequest()
        //{
        //    return new CalculatorRequestModel
        //    {
        //        AttackerPercent = 100,
        //        VictimPercent = 100,
        //        BaseDamage = 20,
        //        TargetWeight = 80,
        //        KnockbackGrowth = 10,
        //        BaseKnockbackSetKnockback = 31,
        //        HitlagModifier = 1,
        //        HitFrame = 4,
        //        FirstActiveFrame = 12,
        //        Staleness = 0, 
        //        Modifiers = Modifiers.Standing,
        //    };
        //}

        public AngleDto Angle()
        {
            var move = Move();
            return new AngleDto
            {
                Id = ++_angleCounter,
                MoveId = move.Id,
                Notes = "dummynotes",
                OwnerId = 1,
                RawValue = "40/30/20"
            };
        }

        public ThrowTypeDto ThrowType()
        {
            return new ThrowTypeDto
            {
                Id = ++_throwCounter,
                Name = "ForwardThrow" + Guid.NewGuid().ToString()
            };
        }

        public ThrowDto Throw()
        {
            var move = Move();
            var throwType = ThrowType();

            var newThrow = new ThrowDto
            {
                Id = ++_throwCounter,
                MoveId = move.Id,
                ThrowTypeId = throwType.Id,
                WeightDependent = true,
            };

            return newThrow;
        }

        public BaseDamageDto BaseDamage()
        {
            var move = Move();
            return new BaseDamageDto
            {
                Id = ++_baseDamagesCounter,
                MoveId = move.Id,
                Notes = "dummynotes",
                OwnerId = 1,
                RawValue = "20/30/10"
            };
        }

        public KnockbackGrowthDto KnockbackGrowth()
        {
            var move = Move();
            return new KnockbackGrowthDto
            {
                Id = ++_knockbackGrowthCounter,
                MoveId = move.Id,
                Notes = "dummynotes",
                OwnerId = 1,
                RawValue = "20/34/20",
                Hitbox1 = "20",
                Hitbox2 = "30",
                Hitbox3 = "40",
                Hitbox4 = "20",
                Hitbox5 = "10",
                Hitbox6 = "40"
            };
        }

        public HitboxDto Hitbox()
        {
            var move = Move();
            return new HitboxDto
            {
                Id = ++_hitboxCounter,
                MoveId = move.Id,
                Notes = "dummynotes",
                OwnerId = 1,
                RawValue = "20/34/20",
                Hitbox1 = "2-4",
                Hitbox2 = "5-6",
                Hitbox3 = "9 - 12",
                Hitbox4 = "14 - 15",
                Hitbox5 = "17 - 20",
                Hitbox6 = "20 - 22"
            };
        }
    }
}
