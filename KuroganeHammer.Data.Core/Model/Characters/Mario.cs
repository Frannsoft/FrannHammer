﻿
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Mario : Character
    {
        [StatProperty]
        public SpecialStat Fireball { get; set; }

        [StatProperty]
        public SpecialStat FireballLate { get; set; }


        [StatProperty]
        public SpecialStat Cape { get; set; }


        [StatProperty]
        public SpecialStat SuperJumpPunchHit1 { get; set; }


        [StatProperty]
        public SpecialStat SuperJumpPunchHits24 { get; set; }


        [StatProperty]
        public SpecialStat SuperJumpPunchHit5 { get; set; }


        [StatProperty]
        public SpecialStat SuperJumpPunchHit6 { get; set; }


        [StatProperty]
        public SpecialStat FLUDD { get; set; }


        [StatProperty]
        public SpecialStat FLUDDAttack { get; set; }




        public Mario()
            : base(Characters.MARIO)
        { }
    }
}
