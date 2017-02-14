using System;

namespace FrannHammer.Models
{
    [Obsolete("Calculate functionality is Obsolete.")]
    public class ShieldAdvantageProblemData
    {
        public int HitFrame { get; set; }
        public int FirstActionableFrame { get; set; }
        public int ShieldStun { get; set; }
        public ShieldAdvantageModifier ShieldAdvantageModifier { get; set; }
        //public int ShieldHitlag { get; set; }
        public HitlagProblemData HitlagData { get; set; }
        public int Hitlag { get; set; }
    }
}