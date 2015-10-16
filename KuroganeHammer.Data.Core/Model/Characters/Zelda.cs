
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Zelda : Character
    {
        [StatProperty]
        public SpecialStat NayrusLove { get; set; }

        [StatProperty]
        public SpecialStat NayrusLoveFinalHit { get; set; }


        [StatProperty]
        public SpecialStat DinsFire { get; set; }


        [StatProperty]
        public SpecialStat DinsFireSourspot { get; set; }


        [StatProperty]
        public SpecialStat FaroresWindHit1 { get; set; }


        [StatProperty]
        public SpecialStat FaroresWindHit2 { get; set; }


        [StatProperty]
        public SpecialStat FaroresWindHit1Aerial { get; set; }


        [StatProperty]
        public SpecialStat FaroresWindHit2Aerial { get; set; }


        [StatProperty]
        public SpecialStat PhantomSlash { get; set; }



        public Zelda()
            : base(Characters.ZELDA)
        { }
    }
}
