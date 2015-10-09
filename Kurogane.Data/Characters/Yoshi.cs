
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Yoshi : Character
    {
        [StatProperty]
        public SpecialStat EggLay { get; set; }

        [StatProperty]
        public SpecialStat EggRoll { get; set; }


        [StatProperty]
        public SpecialStat EggThrowContact { get; set; }


        [StatProperty]
        public SpecialStat EggThrowExplosion { get; set; }


        [StatProperty]
        public SpecialStat YoshiBombHit1 { get; set; }


        [StatProperty]
        public SpecialStat YoshiBombHit2 { get; set; }


        [StatProperty]
        public SpecialStat YoshiBombAerial { get; set; }


        [StatProperty]
        public SpecialStat YoshiBombStars { get; set; }




        public Yoshi()
            : base(Characters.YOSHI)
        { }
    }
}
