using System;

namespace FrannHammer.Models
{
    [Obsolete("Calculate functionality is Obsolete.")]
    public class SmashChargeProblemData
    {
        public int Damage { get; set; }
        public int HeldFrames { get; set; }
        public SmashChargeModifier SmashChargeModifier { get; set; } = SmashChargeModifier.Default;
    }
}