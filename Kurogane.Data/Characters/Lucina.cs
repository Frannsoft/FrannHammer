
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Lucina : Character
    {
        [StatProperty]
        public SpecialStat ShieldbreakerNoCharge { get; set; }

        [StatProperty]
        public SpecialStat ShieldbreakerFullCharge { get; set; }


        [StatProperty]
        public SpecialStat DancingBlade { get; set; }


        [StatProperty]
        public SpecialStat DancingBlade2Up { get; set; }


        [StatProperty]
        public SpecialStat DancingBlade2 { get; set; }


        [StatProperty]
        public SpecialStat DancingBlade3Up { get; set; }


        [StatProperty]
        public SpecialStat DancingBlade3 { get; set; }


        [StatProperty]
        public SpecialStat DancingBlade3Down { get; set; }


        [StatProperty]
        public SpecialStat DancingBlade4Up { get; set; }


        [StatProperty]
        public SpecialStat DancingBlade4 { get; set; }


        [StatProperty]
        public SpecialStat DancingBlade4DownHits14 { get; set; }


        [StatProperty]
        public SpecialStat DancingBlade4DownHit5 { get; set; }


        [StatProperty]
        public SpecialStat DolphinSlashEarly { get; set; }


        [StatProperty]
        public SpecialStat DolphinSlash { get; set; }


        [StatProperty]
        public SpecialStat DolphinSlashLate { get; set; }


        [StatProperty]
        public SpecialStat Counter { get; set; }


        [StatProperty]
        public SpecialStat CounterOnHit { get; set; }


        [StatProperty]
        public SpecialStat CounterAttack { get; set; }




        public Lucina()
            : base(Characters.LUCINA)
        { }
    }
}
