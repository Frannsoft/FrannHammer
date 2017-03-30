using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class Move : IMove
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HitboxActive { get; set; }
        public string FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockbackSetKnockback { get; set; }
        public string LandingLag { get; set; }
        public string AutoCancel { get; set; }
        public string KnockbackGrowth { get; set; }
        public MoveType MoveType { get; set; }
        public int OwnerId { get; set; }
    }
}
