
using KuroganeHammer.Data.Core.Model.Stats;
namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class Greninja : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat WaterShurikenUncharged { get; set; }

        [StatProperty]
        public SpecialStat WaterShurikenFullCharge { get; set; }

        [StatProperty]
        public SpecialStat WaterShurikenFullChargeFinalHit { get; set; }

        [StatProperty]
        public SpecialStat ShadowSneakCharge { get; set; }

        [StatProperty]
        public SpecialStat ShadowSneakAttack { get; set; }

        [StatProperty]
        public SpecialStat HydroPump { get; set; }

        [StatProperty]
        public SpecialStat Substitute { get; set; }

        [StatProperty]
        public SpecialStat SubstituteAttackNeutralSide { get; set; }

        [StatProperty]
        public SpecialStat SubstituteAttackAttackUp { get; set; }

        [StatProperty]
        public SpecialStat SubstituteAttackUpDiagonal { get; set; }

        [StatProperty]
        public SpecialStat SubstituteAttackDown { get; set; }

        [StatProperty]
        public SpecialStat SubstituteAttackDownDiagonal { get; set; }

        #endregion

        public Greninja()
            : base(Characters.GRENINJA)
        { }
    }
}
