
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class ZeroSuitSamus : Character
    {
        [StatProperty]
        public SpecialStat ParalyzerUncharged { get; set; }

        [StatProperty]
        public SpecialStat ParalyzerCharged { get; set; }


        [StatProperty]
        public SpecialStat PlasmaWhipHit1 { get; set; }


        [StatProperty]
        public SpecialStat PlasmaWhip { get; set; }


        [StatProperty]
        public SpecialStat PlasmaWhipFinalHit { get; set; }


        [StatProperty]
        public SpecialStat BoostKickHit1 { get; set; }


        [StatProperty]
        public SpecialStat BoostKickHits25 { get; set; }


        [StatProperty]
        public SpecialStat BoostKickHit6 { get; set; }


        [StatProperty]
        public SpecialStat BoostKickHit7 { get; set; }


        [StatProperty]
        public SpecialStat BoostKickHit8 { get; set; }


        [StatProperty]
        public SpecialStat FlipKick { get; set; }


        [StatProperty]
        public SpecialStat FlipKickAttack { get; set; }


        [StatProperty]
        public SpecialStat FlipKickFootstool { get; set; }




        public ZeroSuitSamus()
            : base(Characters.ZEROSUITSAMUS)
        { }
    }
}
