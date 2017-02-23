namespace FrannHammer.Models
{
    public class Shield : BaseCharacterAttribute
    {
        public double ShieldHp { get; set; }
        public double ResetHpWhenBroken { get; set; }
        public double DamageMultiplier { get; set; }
        public double HpRegeneratePerFrame { get; set; }
        public double HpDegeneratePerFrame { get; set; }
        public string PowerShieldFrames { get; set; }
    }
}