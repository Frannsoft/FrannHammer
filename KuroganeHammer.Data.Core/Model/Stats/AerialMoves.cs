
namespace KuroganeHammer.Data.Core.Model.Stats
{
    public class AerialMoves
    {
        [StatProperty]
        public AerialStat NairHit1 { get; set; }

        [StatProperty]
        public AerialStat NairHit2 { get; set; }

        [StatProperty]
        public AerialStat Fair { get; set; }

        [StatProperty]
        public AerialStat Bair { get; set; }

        [StatProperty]
        public AerialStat Uair { get; set; }

        [StatProperty]
        public AerialStat UairLate { get; set; }

        [StatProperty]
        public AerialStat Dair { get; set; }

        [StatProperty]
        public AerialStat DairLate { get; set; }

        [StatProperty]
        public AerialStat DairLanding { get; set; }

        [StatProperty]
        public AerialStat DairLandingWindbox { get; set; }

        [StatProperty]
        public AerialStat Zair { get; set; }
    }
}
