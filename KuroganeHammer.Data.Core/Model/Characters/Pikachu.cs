using KuroganeHammer.Data.Core.Model.Characters;
using KuroganeHammer.Data.Core.Model.Stats;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class Pikachu : Character
    {
        [StatProperty]
        public SpecialStat ThunderJoltAir { get; set; }

        [StatProperty]
        public SpecialStat ThunderJoltArcEarly { get; set; }


        [StatProperty]
        public SpecialStat ThunderJoltArc { get; set; }


        [StatProperty]
        public SpecialStat ThunderJoltArcLate { get; set; }


        [StatProperty]
        public SpecialStat SkullBash { get; set; }


        [StatProperty]
        public SpecialStat QuickAttackHit1 { get; set; }


        [StatProperty]
        public SpecialStat QuickAttackHit2 { get; set; }


        [StatProperty]
        public SpecialStat ThunderCloud { get; set; }


        [StatProperty]
        public SpecialStat ThunderProjectile { get; set; }


        [StatProperty]
        public SpecialStat ThunderContact { get; set; }



        public Pikachu() 
            : base(Characters.PIKACHU)
        { }
    }
}
