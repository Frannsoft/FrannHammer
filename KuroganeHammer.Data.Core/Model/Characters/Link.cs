
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Link : Character
    {
        [StatProperty]
        public SpecialStat HerosBowNoCharge { get; set; }

        [StatProperty]
        public SpecialStat HerosBowFullCharge { get; set; }


        [StatProperty]
        public SpecialStat GaleBoomerang { get; set; }


        [StatProperty]
        public SpecialStat GaleBoomerangLate { get; set; }


        [StatProperty]
        public SpecialStat GaleBoomerangWindboxes { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackNoChargeEarly { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackNoCharge { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackNoChargeLate { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackNoChargeLatest { get; set; }


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
        public SpecialStat SpinAttackAirHit6 { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackAirHit7 { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackAirHit8 { get; set; }


        [StatProperty]
        public SpecialStat SpinAttackAirHit9 { get; set; }


        [StatProperty]
        public SpecialStat Bomb { get; set; }

        public Link()
            : base(Characters.LINK)
        { }
    }
}
