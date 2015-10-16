
using KuroganeHammer.Data.Core.Model.Stats;
namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class Charizard : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat Flamethrower { get; set; }

        [StatProperty]
        public SpecialStat FlareBlitzHit1 { get; set; }

        [StatProperty]
        public SpecialStat FlareBlitzHit2 { get; set; }

        [StatProperty]
        public SpecialStat FlyHit1 { get; set; }

        [StatProperty]
        public SpecialStat Fly { get; set; }

        [StatProperty]
        public SpecialStat RockSmashHit1 { get; set; }

        [StatProperty]
        public SpecialStat RockSmashHit2 { get; set; }

        [StatProperty]
        public SpecialStat RockSmashResidue { get; set; }

        #endregion

        public Charizard()
            : base(Characters.CHARIZARD)
        { }
    }
}
