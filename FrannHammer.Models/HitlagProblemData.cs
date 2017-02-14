using System;

namespace FrannHammer.Models
{
    [Obsolete("Calculate functionality is Obsolete.")]
    public class HitlagProblemData
    {
        public double Damage { get; set; }
        public double HitlagMultiplier { get; set; }
        public ElectricModifier ElectricModifier { get; set; }
        public CrouchingModifier CrouchingModifier { get; set; }
    }
}