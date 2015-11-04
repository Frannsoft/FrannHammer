using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [JsonObject]
    [Table("MovementStats")]
    public class MovementStat : Stat
    {
        public string Rank { get; private set; }
        public string Value { get; private set; }

        public MovementStat(string name, int ownerId, string rawName, string value, string rank = "")
            : base(name, ownerId, rawName)
        {
            Value = value;
            Rank = rank;
        }

        public MovementStat()
        { }
    }
}
