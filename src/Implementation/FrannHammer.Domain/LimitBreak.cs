namespace FrannHammer.Domain
{
    public class LimitBreak : UniqueData
    {
        public string FramesToCharge { get; set; }
        public string PercentDealtToCharge { get; set; }
        public string RunSpeed { get; set; }
        public string WalkSpeed { get; set; }
        public string Gravity { get; set; }
        public string SHAirTime { get; set; }
        public string GainedPerFrame { get; set; }
        public string PercentTakenToCharge { get; set; }
        public string AirSpeed { get; set; }
        public string FallSpeed { get; set; }
        public string AirAcceleration { get; set; }
        public string FHAirTime { get; set; }

        public LimitBreak()
        {
            Name = "Limit Break";
        }
    }
}
