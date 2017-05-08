using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class Move : MongoModel, IMove
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
        public string BaseKnockBackSetKnockback { get; set; }

        [FriendlyName("landingLag")]
        public string LandingLag { get; set; }

        [FriendlyName("autoCancel")]
        public string AutoCancel { get; set; }

        [FriendlyName("knockbackGrowth")]
        public string KnockbackGrowth { get; set; }

        [FriendlyName("moveType")]
        public string MoveType { get; set; }

        [FriendlyName("ownerId")]
        public string Owner { get; set; }
    }
}
