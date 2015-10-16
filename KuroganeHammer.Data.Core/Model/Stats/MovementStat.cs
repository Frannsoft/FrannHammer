
namespace KuroganeHammer.Data.Core.Model.Stats
{
    public class MovementStat : Stat
    {
        internal Rank Rank { get; private set; }
        internal string Value { get; private set; }

        public MovementStat(string name, string rawName, string value, string rank = "")
            : base(name, rawName)
        {
            Value = value;
            Rank = new Rank(rank);
        }
    }
}
