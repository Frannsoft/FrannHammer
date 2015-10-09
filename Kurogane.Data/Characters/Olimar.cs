
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Olimar : Character
    {
        [StatProperty]
        public SpecialStat PikminPluck { get; set; }

        [StatProperty]
        public SpecialStat PikminPluck3Pikmin { get; set; }


        [StatProperty]
        public SpecialStat PikminPluckAir { get; set; }


        [StatProperty]
        public SpecialStat PikminToss { get; set; }


        [StatProperty]
        public SpecialStat WingedPikmin { get; set; }


        [StatProperty]
        public SpecialStat PikminOrder { get; set; }



        public Olimar()
            : base(Characters.OLIMAR)
        { }
    }
}
