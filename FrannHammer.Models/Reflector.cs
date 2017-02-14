namespace FrannHammer.Models
{
    public class Reflector : BaseCharacterAttribute
    {
        public string ReflectsOnFrames { get; set; }
        public Hitbox Hitbox { get; set; }
        public int HitboxId { get; set; }
        public double DamageMultiplier { get; set; }
        public double SpeedMultiplier { get; set; }
        public double BreakThreshold { get; set; }
    }
}