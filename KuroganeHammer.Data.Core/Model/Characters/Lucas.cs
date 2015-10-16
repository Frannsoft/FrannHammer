﻿
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Lucas : Character
    {
        [StatProperty]
        public SpecialStat PKFreeze { get; set; }

        [StatProperty]
        public SpecialStat PKFire { get; set; }


        [StatProperty]
        public SpecialStat PKFireContact { get; set; }


        [StatProperty]
        public SpecialStat PKThunder { get; set; }


        [StatProperty]
        public SpecialStat PKThunder2Hit1 { get; set; }


        [StatProperty]
        public SpecialStat PKThunder2Hits26 { get; set; }


        [StatProperty]
        public SpecialStat PKThunder2Hits711 { get; set; }


        [StatProperty]
        public SpecialStat PKThunder2Hit12 { get; set; }


        [StatProperty]
        public SpecialStat PSIMagnet { get; set; }




        public Lucas()
            : base(Characters.LUCAS)
        { }
    }
}
