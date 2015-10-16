
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
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
