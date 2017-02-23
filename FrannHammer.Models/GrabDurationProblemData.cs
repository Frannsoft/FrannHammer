using System;

namespace FrannHammer.Models
{
    [Obsolete("Calculate functionality is Obsolete.")]
    public class GrabDurationProblemData
    {
        public int TargetPercent { get; set; }
        public ControllerInput Input { get; set; }
    }
}