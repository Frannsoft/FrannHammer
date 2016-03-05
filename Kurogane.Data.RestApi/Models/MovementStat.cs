using System;
using System.Diagnostics.CodeAnalysis;

namespace Kurogane.Data.RestApi.Models
{
    public class MovementStat : Stat
    {
        public double Value { get; set; }

        public MovementStat(string name, int ownerId, string value)
            : base(name, ownerId)
        {
            value = value.Replace(" ", string.Empty);

            double result;
            if(double.TryParse(value, out result))
            {
                Value = Convert.ToDouble(result);
            }
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
