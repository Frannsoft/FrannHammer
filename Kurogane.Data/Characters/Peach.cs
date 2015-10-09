
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Peach : Character
    {
        [StatProperty]
        public SpecialStat Toad { get; set; }

        [StatProperty]
        public SpecialStat ToadAttack { get; set; }


        [StatProperty]
        public SpecialStat ToadAttackHit9 { get; set; }


        [StatProperty]
        public SpecialStat PeachBomberGround { get; set; }


        [StatProperty]
        public SpecialStat PeachBomberAerial { get; set; }


        [StatProperty]
        public SpecialStat PeachBomberHitbox { get; set; }


        [StatProperty]
        public SpecialStat Parasol { get; set; }


        [StatProperty]
        public SpecialStat ParasolFinalHit { get; set; }


        [StatProperty]
        public SpecialStat ParasolOpen { get; set; }


        [StatProperty]
        public SpecialStat Vegetable { get; set; }


        [StatProperty]
        public SpecialStat Float { get; set; }



        public Peach()
            : base(Characters.PEACH)
        { }
    }
}
