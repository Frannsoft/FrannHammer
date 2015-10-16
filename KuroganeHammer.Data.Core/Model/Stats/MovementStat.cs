
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [JsonObject]
    public class MovementStat : Stat
    {
        public Rank Rank { get; private set; }
        public string Value { get; private set; }

        public MovementStat(string name, string rawName, string value, string rank = "")
            : base(name, rawName)
        {
            Value = value;
            Rank = new Rank(rank);
        }
    }
}
