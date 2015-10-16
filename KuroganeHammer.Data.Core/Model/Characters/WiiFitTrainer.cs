
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class WiiFitTrainer : Character
    {
        [StatProperty]
        public SpecialStat SunSalutationWindbox { get; set; }

        [StatProperty]
        public SpecialStat SunSalutation { get; set; }


        [StatProperty]
        public SpecialStat Header { get; set; }


        [StatProperty]
        public SpecialStat HeaderLate { get; set; }


        [StatProperty]
        public SpecialStat HeaderNoInput { get; set; }


        [StatProperty]
        public SpecialStat HeaderNoInputLate { get; set; }


        [StatProperty]
        public SpecialStat HeaderSoccerBall { get; set; }


        [StatProperty]
        public SpecialStat SuperHoop { get; set; }


        [StatProperty]
        public SpecialStat DeepBreathingFastest { get; set; }


        [StatProperty]
        public SpecialStat DeepBreathingSlowest { get; set; }


        [StatProperty]
        public SpecialStat DeepBreathingAerial { get; set; }


        [StatProperty]
        public SpecialStat DeepBreathingFailure { get; set; }




        public WiiFitTrainer()
            : base(Characters.WIIFITRAINER)
        { }
    }
}
