namespace FrannHammer.Domain
{
    public class OneWingedAngel : UniqueData
    {
        public string RunSpeed { get; set; }
        public string WalkSpeed { get; set; }
        public string AirSpeed { get; set; }
        public string FallSpeed { get; set; }
        public string FastFallSpeed { get; set; }
        public string Gravity { get; set; }
        public string SHAirTime { get; set; }
        public string RunAcceleration { get; set; }
        public string WalkAcceleration { get; set; }
        public string AirSpeedFriction { get; set; }
        public string DamageDealt { get; set; }
        public string MaxJumps { get; set; }
        public string AirAcceleration { get; set; }
        public string FHAirTime { get; set; }

        public OneWingedAngel()
        {
            Name = "One Winged Angel";
        }
    }
}
