using FrannHammer.Domain.Contracts;
using FrannHammer.Domain.PropertyParsers;
using static FrannHammer.Domain.FriendlyNameMoveCommonConstants;
using static FrannHammer.Domain.FriendlyNameCommonConstants;

namespace FrannHammer.Domain
{
    public class Move : MongoModel, IMove
    {
        [PropertyParser(typeof(HitboxParser))]
        [FriendlyName(HitboxActiveName)]
        public string HitboxActive { get; set; }

        [PropertyParser(typeof(HitboxParser))]
        [FriendlyName(BaseDamageName)]
        public string BaseDamage { get; set; }

        [PropertyParser(typeof(HitboxParser))]
        [FriendlyName(AngleName)]
        public string Angle { get; set; }

        [PropertyParser(typeof(HitboxParser))]
        [FriendlyName(KnockbackGrowthName)]
        public string KnockbackGrowth { get; set; }

        [PropertyParser(typeof(AutocancelParser))]
        [FriendlyName(AutoCancelName)]
        public string AutoCancel { get; set; }

        [PropertyParser(typeof(BaseKnockbackParser), "baseKnockback")]
        [PropertyParser(typeof(SetKnockbackParser), "setKnockback")]
        [FriendlyName(BaseKnockbackSetKnockbackName)]
        public string BaseKnockBackSetKnockback { get; set; }

        [PropertyParser(typeof(FirstActionableFrameParser))]
        [FriendlyName(FirstActionableFrameName)]
        public string FirstActionableFrame { get; set; }

        [PropertyParser(typeof(LandingLagParser))]
        [FriendlyName(LandingLagName)]
        public string LandingLag { get; set; }

        [FriendlyName(MoveTypeName)]
        public string MoveType { get; set; }

        [FriendlyName(OwnerName)]
        public string Owner { get; set; }

        [FriendlyName(IsWeightDependentName)]
        public bool IsWeightDependent { get; set; }

        [FriendlyName(OwnerIdName)]
        public int OwnerId { get; set; }
    }
}
