
using KuroganeHammer.Data.Core.Model.Stats;
namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class Ike : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat EruptionCharging { get; set; }

        [StatProperty]
        public SpecialStat Eruption { get; set; }

        [StatProperty]
        public SpecialStat EruptionLate { get; set; }

        [StatProperty]
        public SpecialStat EruptionFullyCharged { get; set; }

        [StatProperty]
        public SpecialStat QuickdrawCharge { get; set; }

        [StatProperty]
        public SpecialStat QuickdrawTravel { get; set; }

        [StatProperty]
        public SpecialStat QuickdrawAttack { get; set; }

        [StatProperty]
        public SpecialStat AetherHit1 { get; set; }

        [StatProperty]
        public SpecialStat AetherHit1Late { get; set; }

        [StatProperty]
        public SpecialStat AetherProjectileHit1 { get; set; }

        [StatProperty]
        public SpecialStat AetherProjectileHit2 { get; set; }

        [StatProperty]
        public SpecialStat Aether { get; set; }

        [StatProperty]
        public SpecialStat AetherFalling { get; set; }

        [StatProperty]
        public SpecialStat AetherLanding { get; set; }

        [StatProperty]
        public SpecialStat AetherLandingLate { get; set; }

        [StatProperty]
        public SpecialStat Counter { get; set; }

        [StatProperty]
        public SpecialStat CounterOnHit { get; set; }

        [StatProperty]
        public SpecialStat CounterAttack { get; set; }

        #endregion

        public Ike()
            : base(Characters.IKE)
        { }
    }
}
