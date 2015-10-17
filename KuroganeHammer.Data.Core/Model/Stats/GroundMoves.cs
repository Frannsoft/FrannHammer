
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [JsonObject(MemberSerialization.OptOut)]
    public class GroundMoves : BaseMoves
    {
        [StatProperty]
        public GroundStat Jab1 { get; set; }

        [StatProperty]
        public GroundStat Jab2 { get; set; }

        [StatProperty]
        public GroundStat DashAttack { get; set; }

        [StatProperty]
        public GroundStat DashAttackLate { get; set; }

        [StatProperty]
        public GroundStat Ftilt { get; set; }

        [StatProperty]
        public GroundStat UtiltHit1 { get; set; }

        [StatProperty]
        public GroundStat UtiltHit2 { get; set; }

        [StatProperty]
        public GroundStat Dtilt { get; set; }

        [StatProperty]
        public GroundStat FsmashFlatGround { get; set; }

        [StatProperty]
        public GroundStat FsmashOffLedge { get; set; }

        [StatProperty]
        public GroundStat UsmashHit1 { get; set; }

        [StatProperty]
        public GroundStat UsmashHit25 { get; set; }

        [StatProperty]
        public GroundStat UsmashHit6 { get; set; }

        [StatProperty]
        public GroundStat UsmashHit { get; set; }


    }
}
