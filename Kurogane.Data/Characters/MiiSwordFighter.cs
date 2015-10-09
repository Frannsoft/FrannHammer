
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class MiiSwordFighter : Character
    {
        [StatProperty]
        public SpecialStat Neutral1GaleStrikeEarly { get; set; }

        [StatProperty]
        public SpecialStat Neutral1GaleStrike { get; set; }


        [StatProperty]
        public SpecialStat Neutral1GaleStrikeLate { get; set; }


        [StatProperty]
        public SpecialStat Neutral1GaleStrikeWindbox { get; set; }


        [StatProperty]
        public SpecialStat Neutral2ShurikenofLightClose { get; set; }


        [StatProperty]
        public SpecialStat Neutral2ShurikenofLight { get; set; }


        [StatProperty]
        public SpecialStat Neutral3BladeFlurryNoChargeHits14 { get; set; }


        [StatProperty]
        public SpecialStat Neutral3BladeFlurryNoChargeHit5 { get; set; }


        [StatProperty]
        public SpecialStat Neutral3BladeFlurryNoChargeFinalHit { get; set; }


        [StatProperty]
        public SpecialStat Neutral3BladeFlurryFullChargeHits14 { get; set; }


        [StatProperty]
        public SpecialStat Neutral3BladeFlurryFullChargeHit5 { get; set; }


        [StatProperty]
        public SpecialStat Neutral3BladeFlurryFullChargeFinalHit { get; set; }


        [StatProperty]
        public SpecialStat Side1AirborneAssaultNoCharge { get; set; }


        [StatProperty]
        public SpecialStat Side1AirborneAssaultFullCharge { get; set; }


        [StatProperty]
        public SpecialStat Side2SurgingSlashNoCharge { get; set; }


        [StatProperty]
        public SpecialStat Side2SurgingSlashFullCharge { get; set; }


        [StatProperty]
        public SpecialStat Side3Chakram { get; set; }


        [StatProperty]
        public SpecialStat Up1StoneScabbardHit1 { get; set; }


        [StatProperty]
        public SpecialStat Up1StoneScabbardHit2 { get; set; }


        [StatProperty]
        public SpecialStat Up1StoneScabbardLanding { get; set; }


        [StatProperty]
        public SpecialStat Up2SkywardSlashDashHit1 { get; set; }


        [StatProperty]
        public SpecialStat Up2SkywardSlashDashHit26 { get; set; }


        [StatProperty]
        public SpecialStat Up2SkywardSlashDashHit7 { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinNoChargeEarly { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinNoCharge { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinNoChargeLate { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinNoChargeLatest { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinAirHit1 { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinAirHit2 { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinAirHit3 { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinAirHit4 { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinAirHit5 { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinAirHit6 { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinAirHit7 { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinAirHit8 { get; set; }


        [StatProperty]
        public SpecialStat Up3HerosSpinAirHit9 { get; set; }


        [StatProperty]
        public SpecialStat Down1BladeCounter { get; set; }


        [StatProperty]
        public SpecialStat Down1BladeCounterOnHit { get; set; }


        [StatProperty]
        public SpecialStat Down1BladeCounterAttack { get; set; }


        [StatProperty]
        public SpecialStat Down2ReversalSlash { get; set; }


        [StatProperty]
        public SpecialStat Down3PowerThrustEarlyGround { get; set; }


        [StatProperty]
        public SpecialStat Down3PowerThrustGround { get; set; }


        [StatProperty]
        public SpecialStat Down3PowerThrustLateGround { get; set; }


        [StatProperty]
        public SpecialStat Down3PowerThrustEarlyAir { get; set; }


        [StatProperty]
        public SpecialStat Down3PowerThrustAir { get; set; }


        [StatProperty]
        public SpecialStat Down3PowerThrustLateAir { get; set; }


        [StatProperty]
        public SpecialStat Down3PowerThrustAirLanding { get; set; }



        public MiiSwordFighter()
            : base(Characters.MIISWORDFIGHTER)
        { }
    }
}
