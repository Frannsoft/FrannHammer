namespace FrannHammer.Domain
{
    public class Pikmin : UniqueData
    {
        public string Attack { get; set; }
        public string Throw { get; set; }
        public string HP { get; set; }
    }

    public class Aura : UniqueData
    {
        public string MinPercentAuraMultiplier { get; set; }
        public string MaxPercentAuraMultiplier { get; set; }
        public string AuraBaselinePercent { get; set; }
        public string AuraCeilingPercent { get; set; }

        public Aura()
        {
            Name = "Aura";
        }
    }
}
