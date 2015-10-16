
using KuroganeHammer.Data.Core.Model.Stats;
namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class MetaKnight : Character
    {
        [StatProperty]
        public SpecialStat MachTornado { get; set; }

        [StatProperty]
        public SpecialStat MachTornadoFinalHit { get; set; }


        [StatProperty]
        public SpecialStat DrillRush { get; set; }


        [StatProperty]
        public SpecialStat DrillRushFinalHit { get; set; }


        [StatProperty]
        public SpecialStat ShuttleLoopGroundedHit1 { get; set; }


        [StatProperty]
        public SpecialStat ShuttleLoopGroundedHit1Late { get; set; }


        [StatProperty]
        public SpecialStat ShuttleLoopGroundedHit2 { get; set; }


        [StatProperty]
        public SpecialStat ShuttleLoopAerialHit1 { get; set; }


        [StatProperty]
        public SpecialStat ShuttleLoopAerialHit2 { get; set; }


        [StatProperty]
        public SpecialStat DimensionalCape { get; set; }


        [StatProperty]
        public SpecialStat DimensionalCapeAttack { get; set; }



        public MetaKnight()
            : base(Characters.METAKNIGHT)
        { }
    }
}
