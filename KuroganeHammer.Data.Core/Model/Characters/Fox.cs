
using KuroganeHammer.Data.Core.Model.Stats;
namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class Fox : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat Blaster { get; set; }

        [StatProperty]
        public SpecialStat FoxIllusionGrounded { get; set; }

        [StatProperty]
        public SpecialStat FoxIllusionAerial { get; set; }

        [StatProperty]
        public SpecialStat FireFoxHits17 { get; set; }

        [StatProperty]
        public SpecialStat FireFoxHit8 { get; set; }

        [StatProperty]
        public SpecialStat FireFoxHit8Late { get; set; }

        [StatProperty]
        public SpecialStat Reflector { get; set; }

        #endregion

        public Fox()
            : base(Characters.FOX)
        { }
    }
}
