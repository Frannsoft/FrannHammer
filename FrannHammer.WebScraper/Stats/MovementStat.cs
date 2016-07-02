using System.Diagnostics.CodeAnalysis;

namespace FrannHammer.WebScraper.Stats
{
    public class MovementStat : Stat
    {
        public string Value { get; set; }

        public MovementStat(string name, int ownerId, string value)
            : base(name, ownerId)
        {
            value = value.Replace(" ", string.Empty);

            Value = value;
            //double result;
            //if(double.TryParse(value, out result))
            //{
            //    Value = Convert.ToDouble(result);
            //}
        }

        public MovementStat()
        { }

        public override bool Equals(object obj)
        {
            var retVal = false;
            var compMovement = obj as MovementStat;

            if (compMovement != null)
            {
                if (!Value.Equals(compMovement.Value) || !base.Equals(compMovement))
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return new
            {
                Value
            }.GetHashCode() + base.GetHashCode();
        }
    }
}
