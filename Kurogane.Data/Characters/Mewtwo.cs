
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Mewtwo : Character
    {
        [StatProperty]
        public SpecialStat ShadowBall { get; set; }

        [StatProperty]
        public SpecialStat Confusion { get; set; }


        [StatProperty]
        public SpecialStat Teleport { get; set; }


        [StatProperty]
        public SpecialStat Disable { get; set; }



        public Mewtwo()
            : base(Characters.MEWTWO)
        { }
    }
}
