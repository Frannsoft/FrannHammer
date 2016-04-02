using KuroganeHammer.Data.Api.Models;
using System;

namespace KuroganeHammer.Data.Api.Tests
{
    public class TestObjects
    {
        private int _moveCounter;
        private int _movementCounter;
        private int _characterCounter;
        private int _smashAttributeTypeCounter;
        private int _characterAttributeCounter;

        public Move Move()
        {
            var move = new Move
            {
                Angle = "40",
                AutoCancel = "3",
                BaseDamage = "53",
                BaseKnockBackSetKnockback = "60/25",
                FirstActionableFrame = 2,
                HitboxActive = "23-25",
                Id = _moveCounter,
                KnockbackGrowth = "89",
                LandingLag = "11",
                LastModified = DateTime.Now,
                Name = "falco phantasm",
                OwnerId = 1,
                TotalHitboxActiveLength = 30,
                Type = MoveType.Special
            };

            _moveCounter++;

            return move;
        }

        public Movement Movement()
        {
            var movement = new Movement
            {
                Id = _movementCounter,
                LastModified = DateTime.Now,
                Name = "jab 1",
                OwnerId = 1,
                Value = "3 frames"
            };

            _movementCounter++;

            return movement;
        }

        public Character Character()
        {
            var character = new Character
            {
                ColorTheme = "#323423",
                Description = "desc",
                Id = _characterCounter,
                LastModified = DateTime.Now,
                MainImageUrl = "http://img.com/i.png",
                Name = "falco",
                Style = "prefers the air",
                ThumbnailUrl = "http://img.net/ii.png"
            };

            _characterCounter++;

            return character;
        }

        public SmashAttributeType SmashAttributeType()
        {
            var smashAttributeType = new SmashAttributeType
            {
                Id = _smashAttributeTypeCounter,
                Name = "max fall speed",
                LastModified = DateTime.Now
            };

            _smashAttributeTypeCounter++;

            return smashAttributeType;
        }

        public CharacterAttribute CharacterAttribute()
        {
            var characterAttribute = new CharacterAttribute
            {
                Id = _characterAttributeCounter,
                Name = "attribute name",
                OwnerId = 1,
                Rank = "4",
                SmashAttributeType = SmashAttributeType(),
                Value = "5.43"
            };

            _characterAttributeCounter++;

            return characterAttribute;
        }
    }
}
