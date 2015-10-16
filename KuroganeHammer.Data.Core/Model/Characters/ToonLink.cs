
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class ToonLink : Character
    {
        [StatProperty]
        public SpecialStat HerosBowNoCharge { get; set; }

        [StatProperty]
        public SpecialStat HerosBowFullCharge { get; set; }


        [StatProperty]
        public SpecialStat Boomerang { get; set; }


        [StatProperty]
        public SpecialStat BoomerangLate { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackNoCharge { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackNoChargeFinalHit { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackAirHit1 { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackAirHit2 { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackAirHit3 { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackAirHit4 { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackAirHit5 { get; set; }


        [StatProperty]
        public SpecialStat Bomb { get; set; }



        public ToonLink()
            : base(Characters.TOONLINK)
        { }
    }
}
