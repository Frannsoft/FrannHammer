using System;

namespace FrannHammer.Models
{
    [Obsolete("Calculate functionality is Obsolete.")]
    public class PikminGrabDurationProblemData
    {
        public int TargetPercent { get; set; }
        public PikminGrabControllerInput Input { get; set; }
    }
}