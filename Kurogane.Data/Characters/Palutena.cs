
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Palutena : Character
    {
        [StatProperty]
        public SpecialStat AutoreticleTarget { get; set; }

        [StatProperty]
        public SpecialStat AutoreticleProjectiles { get; set; }


        [StatProperty]
        public SpecialStat ReflectBarrier { get; set; }


        [StatProperty]
        public SpecialStat Warp { get; set; }


        [StatProperty]
        public SpecialStat Counter { get; set; }


        [StatProperty]
        public SpecialStat CounterAttack { get; set; }



        public Palutena()
            : base(Characters.PALUTENA)
        { }
    }
}
