
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
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
