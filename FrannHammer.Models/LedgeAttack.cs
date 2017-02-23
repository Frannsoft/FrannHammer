namespace FrannHammer.Models
{
    public class LedgeAttack : BaseCharacterAttribute
    {
        public Hitbox Hitbox { get; set; }
        public int HitboxId { get; set; }
        public int FirstActiveFrame { get; set; }
        public double Damage { get; set; }
    }
}