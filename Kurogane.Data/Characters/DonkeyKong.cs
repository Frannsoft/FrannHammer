
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class DonkeyKong : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat GiantPunch { get; set; }

        [StatProperty]
        public SpecialStat GiantPunchUncharged { get; set; }

        [StatProperty]
        public SpecialStat GiantPunchUnchargedLate { get; set; }

        [StatProperty]
        public SpecialStat GiantPunchFullCharge { get; set; }

        [StatProperty]
        public SpecialStat GiantPunchUnchargedAerial { get; set; }

        [StatProperty]
        public SpecialStat GiantPunchUnchargedAerialLate { get; set; }

        [StatProperty]
        public SpecialStat GiantPunchFullChargeAerial { get; set; }

        [StatProperty]
        public SpecialStat GiantPunchFullChargeAerialLate { get; set; }

        [StatProperty]
        public SpecialStat HeadbuttGround { get; set; }

        [StatProperty]
        public SpecialStat HeadbuttAir { get; set; }

        [StatProperty]
        public SpecialStat SpinningKongHit1Grounded { get; set; }

        [StatProperty]
        public SpecialStat SpinningKongGrounded { get; set; }

        [StatProperty]
        public SpecialStat SpinningKongFinalHitGrounded { get; set; }

        [StatProperty]
        public SpecialStat SpinningKongHit1Aerial { get; set; }

        [StatProperty]
        public SpecialStat SpinningKongHits25Aerial { get; set; }

        [StatProperty]
        public SpecialStat SpinningKongHits68Aerial { get; set; }

        [StatProperty]
        public SpecialStat HandSlap { get; set; }

        [StatProperty]
        public SpecialStat HandSlapHit1Aerial { get; set; }

        [StatProperty]
        public SpecialStat HandSlapHit2Aerial { get; set; }

        #endregion

        public DonkeyKong()
            : base(Characters.DONKEYKONG)
        { }
    }
}
