
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Samus : Character
    {
        [StatProperty]
        public SpecialStat ChargeShot { get; set; }

        [StatProperty]
        public SpecialStat ChargeShotFullCharge { get; set; }


        [StatProperty]
        public SpecialStat HomingMissile { get; set; }


        [StatProperty]
        public SpecialStat SuperMissile { get; set; }


        [StatProperty]
        public SpecialStat ScrewAttackGroundHit1 { get; set; }


        [StatProperty]
        public SpecialStat ScrewAttackGroundHits24 { get; set; }


        [StatProperty]
        public SpecialStat ScrewAttackGroundHits510 { get; set; }


        [StatProperty]
        public SpecialStat ScrewAttackGroundHit11 { get; set; }


        [StatProperty]
        public SpecialStat ScrewAttackAirHits13 { get; set; }


        [StatProperty]
        public SpecialStat ScrewAttackAir47 { get; set; }


        [StatProperty]
        public SpecialStat ScrewAttackAir811 { get; set; }


        [StatProperty]
        public SpecialStat ScrewAttackAirHit12 { get; set; }


        [StatProperty]
        public SpecialStat Bomb { get; set; }


        [StatProperty]
        public SpecialStat BombExplosion { get; set; }



        public Samus()
            : base(Characters.SAMUS)
        { }
    }
}
