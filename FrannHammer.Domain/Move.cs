using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class Move : IMove
    {
        [FriendlyName("hitboxActive")]
        public string HitboxActive { get; set; }

        [FriendlyName("firstActionableFrame")]
        public string FirstActionableFrame { get; set; }

        [FriendlyName("baseDamage")]
        public string BaseDamage { get; set; }

        [FriendlyName("angle")]
        public string Angle { get; set; }

        [FriendlyName("baseKnockBackSetKnockback")]
        public string BaseKnockbackSetKnockback { get; set; }

        [FriendlyName("landingLag")]
        public string LandingLag { get; set; }

        [FriendlyName("autoCancel")]
        public string AutoCancel { get; set; }

        [FriendlyName("knockbackGrowth")]
        public string KnockbackGrowth { get; set; }

        [FriendlyName("moveType")]
        public MoveType MoveType { get; set; }

        [FriendlyName("ownerId")]
        public int OwnerId { get; set; }

        [FriendlyName("_id")]
        public int Id { get; set; }

        [FriendlyName("name")]
        public string Name { get; set; }
    }
}
