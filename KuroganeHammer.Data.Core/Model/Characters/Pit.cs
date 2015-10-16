
using KuroganeHammer.Data.Core.Model.Stats;
namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class Pit : Character
    {
        [StatProperty]
        public SpecialStat PalutenasBowNoCharge { get; set; }

        [StatProperty]
        public SpecialStat PalutenasBowNoChargeAerial { get; set; }


        [StatProperty]
        public SpecialStat PalutenasBowFullCharge { get; set; }


        [StatProperty]
        public SpecialStat UpperdashArmGround { get; set; }


        [StatProperty]
        public SpecialStat UpperdashArmGroundAttack { get; set; }


        [StatProperty]
        public SpecialStat UpperdashArmAerial { get; set; }


        [StatProperty]
        public SpecialStat UpperdashArmAerialAttack { get; set; }


        [StatProperty]
        public SpecialStat PowerofFlight { get; set; }


        [StatProperty]
        public SpecialStat GuardianOrbitarsMinimumDuration { get; set; }


        [StatProperty]
        public SpecialStat GuardianOrbitarsMaxDuration { get; set; }




        public Pit()
            : base(Characters.PIT)
        { }
    }
}
