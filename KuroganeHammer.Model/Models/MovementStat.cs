using System;

namespace KuroganeHammer.Model
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
    }
}
