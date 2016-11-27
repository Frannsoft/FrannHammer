namespace FrannHammer.Models
{
    public class RangeModel
    {
        public int StartValue { get; set; }
        public RangeConstraint RangeQuantifier { get; set; }
        public int EndValue { get; set; }

        public override string ToString()
        {
            return $"StartValue: {StartValue};RangeQuantifier: {RangeQuantifier};EndValue: {EndValue}";
        }
    }
}
