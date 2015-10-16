
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [JsonObject(MemberSerialization.OptOut)]
    public class GroundMoves
    {
        [StatProperty]
        public GroundStat Jab1 { get; set; }
    }
}
