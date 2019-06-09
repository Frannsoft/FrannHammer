namespace FrannHammer.Domain
{
    public class Float : UniqueData
    {
        public string DurationInFrames { get; set; }
        public string DurationInSeconds { get; set; }

        public Float()
        {
            Name = "Float";
        }
    }

    public class Vegetable : UniqueData
    {
        public string Chance { get; set; }
        public string DamageDealt { get; set; }
    }
}
