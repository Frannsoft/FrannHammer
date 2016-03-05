using System;

namespace Kurogane.Data.RestApi.Models
{
    public class MovementStat : Stat
    {
        public double Value { get; set; }

        public MovementStat(string name, int ownerId, string value)
            : base(name, ownerId)
        {
            value = value.Replace(" ", string.Empty);

            double result = 0;
            if(double.TryParse(value, out result))
            {
                Value = Convert.ToDouble(result);
            }
        }

        public MovementStat()
        { }

        public override bool Equals(object obj)
        {
            bool retVal = false;
            MovementStat compMovement = obj as MovementStat;

            if (compMovement != null)
            {
                if (this.Value.Equals(compMovement.Value) &&
                    base.Equals(compMovement))
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        public override int GetHashCode()
        {
            return new
            {
                Value
            }.GetHashCode() + base.GetHashCode();
        }
    }
}
