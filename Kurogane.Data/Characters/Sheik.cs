
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Sheik : Character
    {
        [StatProperty]
        public SpecialStat NeedleStorm { get; set; }

        [StatProperty]
        public SpecialStat NeedleStorm6NeedlesThrownOn { get; set; }


        [StatProperty]
        public SpecialStat NeedleStorm15NeedlesThrownOn { get; set; }


        [StatProperty]
        public SpecialStat NeedleStormHitboxes { get; set; }


        [StatProperty]
        public SpecialStat NeedleStormHitboxesLate { get; set; }


        [StatProperty]
        public SpecialStat BurstGrenadeThrown { get; set; }


        [StatProperty]
        public SpecialStat BurstGrenadeDetonation { get; set; }


        [StatProperty]
        public SpecialStat BurstGrenadeExplosion { get; set; }


        [StatProperty]
        public SpecialStat Vanish { get; set; }


        [StatProperty]
        public SpecialStat VanishReappear { get; set; }


        [StatProperty]
        public SpecialStat BouncingFishEarliest { get; set; }


        [StatProperty]
        public SpecialStat BouncingFishLatest { get; set; }




        public Sheik()
            : base(Characters.SHEIK)
        { }
    }
}
