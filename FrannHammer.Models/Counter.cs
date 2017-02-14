namespace FrannHammer.Models
{
    public class Counter : BaseCharacterAttribute
    {
        public string Frames { get; set; }
        public string Intangible { get; set; }
        public int FirstActiveFrame { get; set; }
        public double DamageMultiplier { get; set; }
        public BaseKnockback BaseKnockback { get; set; }
        public SetKnockback SetKnockback { get; set; }
        public int BaseKnockbackSetKnockbackId { get; set; }
        public KnockbackGrowth KnockbackGrowth { get; set; }
        public int KnockbackGrowthId { get; set; }
    }
}