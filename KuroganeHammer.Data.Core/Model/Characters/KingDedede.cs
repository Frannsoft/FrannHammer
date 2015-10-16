
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class KingDedede : Character
    {
        [StatProperty]
        public SpecialStat Inhale { get; set; }

        [StatProperty]
        public SpecialStat InhaleSpit { get; set; }


        [StatProperty]
        public SpecialStat GordoThrowHammer { get; set; }


        [StatProperty]
        public SpecialStat GordoThrowGordoEarly { get; set; }


        [StatProperty]
        public SpecialStat GordoThrowGordo { get; set; }


        [StatProperty]
        public SpecialStat GordoThrowGordoLate { get; set; }


        [StatProperty]
        public SpecialStat GordoThrowGordoLatest { get; set; }


        [StatProperty]
        public SpecialStat GordoThrowWall { get; set; }


        [StatProperty]
        public SpecialStat SuperDededeJump { get; set; }


        [StatProperty]
        public SpecialStat SuperDededeJumpLanding { get; set; }


        [StatProperty]
        public SpecialStat SuperDededeJumpStars { get; set; }


        [StatProperty]
        public SpecialStat JetHammerGroundUncharged { get; set; }


        [StatProperty]
        public SpecialStat JetHammerGroundFullyCharged { get; set; }


        [StatProperty]
        public SpecialStat JetHammerAirUncharged { get; set; }


        [StatProperty]
        public SpecialStat JetHammerAirFullyCharged { get; set; }




        public KingDedede()
            : base(Characters.DEDEDE)
        { }
    }
}
