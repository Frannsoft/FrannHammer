
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Providers;

namespace Kurogane.Data.RestApi.DTOs
{
    public class MoveDTO : BaseDTO
    {
        public string HitboxActive { get; set; }
        public int FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockBackSetKnockback { get; set; }
        public string LandingLag { get; set; }
        public string AutoCancel { get; set; }
        public string KnockbackGrowth { get; set; }
        public MoveType Type { get; set; }

        public MoveDTO(MoveStat move, ICharacterStatService characterStatService)
            : base(move, characterStatService)
        {
            Angle = move.Angle;
            AutoCancel = move.AutoCancel;
            BaseDamage = move.BaseDamage;
            BaseKnockBackSetKnockback = move.BaseKnockBackSetKnockback;
            FirstActionableFrame = move.FirstActionableFrame;
            HitboxActive = move.HitboxActive;
            KnockbackGrowth = move.KnockbackGrowth;
            LandingLag = move.LandingLag;
            Type = move.Type;
        }
    }
}