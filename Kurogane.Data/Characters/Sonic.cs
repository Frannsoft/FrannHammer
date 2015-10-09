
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Sonic : Character
    {
        [StatProperty]
        public SpecialStat HomingAttackEarliest { get; set; }

        [StatProperty]
        public SpecialStat HomingAttackNoInput { get; set; }


        [StatProperty]
        public SpecialStat SpringJumpGrounded { get; set; }


        [StatProperty]
        public SpecialStat SpringJumpAerial { get; set; }


        [StatProperty]
        public SpecialStat SpinDash { get; set; }


        [StatProperty]
        public SpecialStat SpinChargeRelease { get; set; }



        public Sonic()
            : base(Characters.SONIC)
        { }
    }
}
