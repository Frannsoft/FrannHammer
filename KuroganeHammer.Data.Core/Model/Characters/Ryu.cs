
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Ryu : Character
    {
        [StatProperty]
        public SpecialStat LightHadouken { get; set; }

        [StatProperty]
        public SpecialStat MediumHadouken { get; set; }


        [StatProperty]
        public SpecialStat HeavyHadouken { get; set; }


        [StatProperty]
        public SpecialStat LightShakunetsuHadouken { get; set; }


        [StatProperty]
        public SpecialStat MediumShakunetsuHadouken { get; set; }


        [StatProperty]
        public SpecialStat HeavyShakunetsuHadouken { get; set; }


        [StatProperty]
        public SpecialStat ShakunetsuHadoukenFinalHit { get; set; }


        [StatProperty]
        public SpecialStat TatsumakiSenpukyakuEarly { get; set; }


        [StatProperty]
        public SpecialStat LightTatsumakiSenpukyakuGrounded { get; set; }


        [StatProperty]
        public SpecialStat MediumTatsumakiSenpukyakuGrounded { get; set; }


        [StatProperty]
        public SpecialStat HeavyTatsumakiSenpukyakuGrounded { get; set; }


        [StatProperty]
        public SpecialStat LightTatsumakiSenpukyakuAerial { get; set; }


        [StatProperty]
        public SpecialStat MediumTatsumakiSenpukyakuAerial { get; set; }


        [StatProperty]
        public SpecialStat HeavyTatsumakiSenpukyakuAerial { get; set; }


        [StatProperty]
        public SpecialStat LightShoryukenEarlyGrounded { get; set; }


        [StatProperty]
        public SpecialStat MediumShoryukenEarlyGrounded { get; set; }


        [StatProperty]
        public SpecialStat HeavyShoryukenEarlyGrounded { get; set; }


        [StatProperty]
        public SpecialStat ShoryukenGrounded { get; set; }


        [StatProperty]
        public SpecialStat ShoryukenLateGrounded { get; set; }


        [StatProperty]
        public SpecialStat LightShoryukenEarlyAerial { get; set; }


        [StatProperty]
        public SpecialStat MediumShoryukenEarlyAerial { get; set; }


        [StatProperty]
        public SpecialStat HeavyShoryukenEarlyAerial { get; set; }


        [StatProperty]
        public SpecialStat ShoryukenAerial { get; set; }


        [StatProperty]
        public SpecialStat ShoryukenLateAerial { get; set; }


        [StatProperty]
        public SpecialStat FocusAttack { get; set; }


        [StatProperty]
        public SpecialStat FocusAttackNoCharge { get; set; }


        [StatProperty]
        public SpecialStat FocusAttackStage2 { get; set; }


        [StatProperty]
        public SpecialStat FocusAttackStage3 { get; set; }



        public Ryu()
            : base(Characters.RYU)
        { }
    }
}
