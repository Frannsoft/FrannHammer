
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Luigi : Character
    {
        [StatProperty]
        public SpecialStat Fireball { get; set; }

        [StatProperty]
        public SpecialStat FireballLate { get; set; }


        [StatProperty]
        public SpecialStat GreenMissileNoCharge { get; set; }


        [StatProperty]
        public SpecialStat GreenMissile { get; set; }


        [StatProperty]
        public SpecialStat GreenMissileMisfire { get; set; }


        [StatProperty]
        public SpecialStat SuperJumpPunchSweetspotGround { get; set; }


        [StatProperty]
        public SpecialStat SuperJumpPunchSweetspotAerial { get; set; }


        [StatProperty]
        public SpecialStat SuperJumpPunch { get; set; }


        [StatProperty]
        public SpecialStat LuigiCycloneGround { get; set; }


        [StatProperty]
        public SpecialStat LuigiCycloneAerial { get; set; }


        [StatProperty]
        public SpecialStat LuigiCycloneFinalHit { get; set; }



        public Luigi()
            : base(Characters.LUIGI)
        { }
    }
}
