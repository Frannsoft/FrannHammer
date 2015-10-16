
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Roy : Character
    {
        [StatProperty]
        public SpecialStat FlareBladeUncharged { get; set; }

        [StatProperty]
        public SpecialStat FlareBladeCharged { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance2Up { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance2 { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance3Up { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance3UpLate { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance3 { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance3Down { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance4Up { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance4 { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance4DownHits14 { get; set; }


        [StatProperty]
        public SpecialStat DoubleEdgeDance4DownHit5 { get; set; }


        [StatProperty]
        public SpecialStat BlazerHit1 { get; set; }


        [StatProperty]
        public SpecialStat Blazer { get; set; }


        [StatProperty]
        public SpecialStat BlazerFinalHit { get; set; }


        [StatProperty]
        public SpecialStat BlazerHit1Aerial { get; set; }


        [StatProperty]
        public SpecialStat BlazerAerial { get; set; }


        [StatProperty]
        public SpecialStat BlazerFinalHitAerial { get; set; }


        [StatProperty]
        public SpecialStat Counter { get; set; }


        [StatProperty]
        public SpecialStat CounterOnHit { get; set; }


        [StatProperty]
        public SpecialStat CounterAttack { get; set; }



        public Roy()
            : base(Characters.ROY)
        { }
    }
}
