
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Robin : Character
    {
        [StatProperty]
        public SpecialStat Thunder { get; set; }

        [StatProperty]
        public SpecialStat Elthunder { get; set; }


        [StatProperty]
        public SpecialStat ArcthunderProjectile { get; set; }


        [StatProperty]
        public SpecialStat ArcthunderHit1 { get; set; }


        [StatProperty]
        public SpecialStat Arcthunder { get; set; }


        [StatProperty]
        public SpecialStat ArcthunderFinalHit { get; set; }


        [StatProperty]
        public SpecialStat Thoron { get; set; }


        [StatProperty]
        public SpecialStat SuperThoron { get; set; }


        [StatProperty]
        public SpecialStat ArcfireProjectile { get; set; }


        [StatProperty]
        public SpecialStat ArcfireHit1 { get; set; }


        [StatProperty]
        public SpecialStat Arcfire { get; set; }


        [StatProperty]
        public SpecialStat ArcfireFinalHit { get; set; }


        [StatProperty]
        public SpecialStat ElwindHit1 { get; set; }


        [StatProperty]
        public SpecialStat ElwindHit1Late { get; set; }


        [StatProperty]
        public SpecialStat ElwindHit2 { get; set; }


        [StatProperty]
        public SpecialStat NosferatuGrab { get; set; }


        [StatProperty]
        public SpecialStat NosferatuAttack { get; set; }



        public Robin()
            : base(Characters.ROBIN)
        { }
    }
}
