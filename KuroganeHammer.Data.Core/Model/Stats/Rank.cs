
namespace KuroganeHammer.Data.Core.Model.Stats
{
    public class Rank
    {
        public string Value { get; set; }

        public Rank(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
