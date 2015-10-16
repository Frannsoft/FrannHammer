
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class LittleMac : Character
    {
        [StatProperty]
        public SpecialStat KOPunch { get; set; }

        [StatProperty]
        public SpecialStat KOPunchAerial { get; set; }


        [StatProperty]
        public SpecialStat StraightLungeNoCharge { get; set; }


        [StatProperty]
        public SpecialStat StraightLungeNoChargeLate { get; set; }


        [StatProperty]
        public SpecialStat StraightLungeFullCharge { get; set; }


        [StatProperty]
        public SpecialStat StraightLungeFullChargeLate { get; set; }


        [StatProperty]
        public SpecialStat StraightLungeNoChargeAerial { get; set; }


        [StatProperty]
        public SpecialStat StraightLungeNoChargeLateAerial { get; set; }


        [StatProperty]
        public SpecialStat StraightLungeFullChargeAerial { get; set; }


        [StatProperty]
        public SpecialStat JoltHaymakerWindbox { get; set; }


        [StatProperty]
        public SpecialStat JoltHaymakerEarliest { get; set; }


        [StatProperty]
        public SpecialStat JoltHaymakerLatest { get; set; }


        [StatProperty]
        public SpecialStat RisingUppercutGrounded { get; set; }


        [StatProperty]
        public SpecialStat RisingUppercut { get; set; }


        [StatProperty]
        public SpecialStat RisingUppercutFinalHit { get; set; }


        [StatProperty]
        public SpecialStat SlipCounter { get; set; }


        [StatProperty]
        public SpecialStat SlipCounterAttack { get; set; }



        public LittleMac()
            : base(Characters.LITTLEMAC)
        { }
    }
}
