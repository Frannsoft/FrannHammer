using System;
using KuroganeHammer.Data.Api.Models;

namespace KuroganeHammer.Data.Api.DTOs
{
    public class MoveDto
    {
        public string HitboxActive { get; set; }
        public int TotalHitboxActiveLength { get; set; }
        public int FirstActionableFrame { get; set; }
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
            TotalHitboxActiveLength = move.TotalHitboxActiveLength;
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
        }

        public MoveDto()
        { }
    }
}