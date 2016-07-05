using System;

namespace FrannHammer.Models.DTOs
{
    [Obsolete("No point in using this right now")]
    public class MoveDto
    {
        public string HitboxActive { get; set; }
        public string FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockBackSetKnockback { get; set; }
        public string LandingLag { get; set; }
        public string AutoCancel { get; set; }
        public string KnockbackGrowth { get; set; }
        public string MoveType { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public string CharacterName { get; set; }
        public string ThumbnailUrl { get; set; }

        public MoveDto(Move move, Character character)
        {
            HitboxActive = move.HitboxActive;
            FirstActionableFrame = move.FirstActionableFrame;
            BaseDamage = move.BaseDamage;
            Angle = move.Angle;
            BaseKnockBackSetKnockback = move.BaseKnockBackSetKnockback;
            LandingLag = move.LandingLag;
            AutoCancel = move.AutoCancel;
            KnockbackGrowth = move.KnockbackGrowth;
            MoveType = move.Type.ToString();
            Name = move.Name;
            Id = move.Id;
            LastModified = move.LastModified;
            CharacterName = character.Name;
            ThumbnailUrl = character.ThumbnailUrl;
            OwnerId = character.Id;
        }

        public MoveDto()
        { }
    }
}