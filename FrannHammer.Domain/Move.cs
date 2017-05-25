using FrannHammer.Domain.Contracts;
using FrannHammer.Domain.PropertyParsers;

namespace FrannHammer.Domain
{
    public class Move : MongoModel, IMove
    {
        [PropertyParser(typeof(HitboxParser))]
        [FriendlyName("hitboxActive")]
        public string HitboxActive { get; set; }

        [PropertyParser(typeof(HitboxParser))]
        [FriendlyName("baseDamage")]
        public string BaseDamage { get; set; }

        [PropertyParser(typeof(HitboxParser))]
        [FriendlyName("angle")]
        public string Angle { get; set; }

        [PropertyParser(typeof(HitboxParser))]
        [FriendlyName("knockbackGrowth")]
        public string KnockbackGrowth { get; set; }

        [PropertyParser(typeof(AutocancelParser))]
        [FriendlyName("autoCancel")]
        public string AutoCancel { get; set; }

        [PropertyParser(typeof(BaseKnockbackParser), "baseKnockback")]
        [PropertyParser(typeof(SetKnockbackParser), "setKnockback")]
        [FriendlyName("baseKnockBackSetKnockback")]
        public string BaseKnockBackSetKnockback { get; set; }

        [PropertyParser(typeof(FirstActionableFrameParser))]
        [FriendlyName("firstActionableFrame")]
        public string FirstActionableFrame { get; set; }

        [PropertyParser(typeof(LandingLagParser))]
        [FriendlyName("landingLag")]
        public string LandingLag { get; set; }

        [FriendlyName("moveType")]
        public string MoveType { get; set; }

        [FriendlyName("ownerId")]
        public string Owner { get; set; }

        [FriendlyName("weightDependent")]
        public bool IsWeightDependent { get; set; }
    }
}
