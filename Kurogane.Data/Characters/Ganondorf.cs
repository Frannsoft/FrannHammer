
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Ganondorf : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat WarlockPunch { get; set; }

        [StatProperty]
        public SpecialStat WarlockPunchAerial { get; set; }

        [StatProperty]
        public SpecialStat WarlockPunchBReversed { get; set; }

        [StatProperty]
        public SpecialStat WarlockPunchBReversedAerial { get; set; }

        [StatProperty]
        public SpecialStat FlameChokeGround { get; set; }

        [StatProperty]
        public SpecialStat FlameChokeAerial { get; set; }

        [StatProperty]
        public SpecialStat FlameChokeGroundAttack { get; set; }

        [StatProperty]
        public SpecialStat FlameChokeAerialAttack { get; set; }

        [StatProperty]
        public SpecialStat DarkDiveCommandGrab { get; set; }

        [StatProperty]
        public SpecialStat DarkDiveLatch { get; set; }

        [StatProperty]
        public SpecialStat DarkDiveAttack { get; set; }

        [StatProperty]
        public SpecialStat WizardsFootGround { get; set; }

        [StatProperty]
        public SpecialStat WizardsFootAerial { get; set; }

        [StatProperty]
        public SpecialStat WizardsFootAerialLate { get; set; }

        [StatProperty]
        public SpecialStat WizardsFootLanding { get; set; }

        #endregion

        public Ganondorf()
            : base(Characters.GANONDORF)
        { }
    }
}
