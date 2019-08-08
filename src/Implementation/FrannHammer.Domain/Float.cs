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
}
