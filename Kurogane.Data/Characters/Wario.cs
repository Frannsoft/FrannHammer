
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Wario : Character
    {
        [StatProperty]
        public SpecialStat Chomp { get; set; }

        [StatProperty]
        public SpecialStat ChompEatingExplosive { get; set; }


        [StatProperty]
        public SpecialStat ChompFood { get; set; }


        [StatProperty]
        public SpecialStat WarioBike { get; set; }


        [StatProperty]
        public SpecialStat CorkscrewHit1 { get; set; }


        [StatProperty]
        public SpecialStat Corkscrew { get; set; }


        [StatProperty]
        public SpecialStat WarioWaftNoCharge { get; set; }


        [StatProperty]
        public SpecialStat WarioWaft20Seconds { get; set; }


        [StatProperty]
        public SpecialStat WarioWaft55Seconds { get; set; }


        [StatProperty]
        public SpecialStat WarioWaft110Seconds { get; set; }


        [StatProperty]
        public SpecialStat WarioWaft110SecondsLate { get; set; }



        public Wario()
            : base(Characters.WARIO)
        { }
    }
}
