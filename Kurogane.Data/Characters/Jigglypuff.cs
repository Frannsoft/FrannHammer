
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Jigglypuff : Character
    {
        [StatProperty]
        public SpecialStat RolloutCharge { get; set; }

        [StatProperty]
        public SpecialStat RolloutGroundRelease { get; set; }


        [StatProperty]
        public SpecialStat RolloutAerialRelease { get; set; }


        [StatProperty]
        public SpecialStat Pound { get; set; }


        [StatProperty]
        public SpecialStat Sing { get; set; }


        [StatProperty]
        public SpecialStat Rest { get; set; }

        public Jigglypuff()
            : base(Characters.JIGGLYPUFF)
        { }
    }
}
