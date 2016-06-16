using System;

namespace KuroganeHammer.Data.Core.Models
{
    public abstract class BaseMeta
    {
        public int Id { get; set; }
        public Character Character { get; set; }
        public int CharacterId { get; set; }
        public Move Move { get; set; }
        public int MoveId { get; set; }
        public string Notes { get; set; }
        public DateTime LastModified { get; set; }
    }

    public abstract class BaseMoveHitboxMeta : BaseMeta
    {
        public string Hitbox1 { get; set; }
        public string Hitbox2 { get; set; }
        public string Hitbox3 { get; set; }
        public string Hitbox4 { get; set; }
        public string Hitbox5 { get; set; }
    }

    public class Hitbox : BaseMoveHitboxMeta
    { }

    public class BaseDamage : BaseMoveHitboxMeta
    { }

    public class Angle : BaseMoveHitboxMeta
    { }

    public class BaseKnockbackSetKnockback : BaseMoveHitboxMeta
    { }

    public class KnockbackGrowth : BaseMoveHitboxMeta
    { }

    public class LandingLag : BaseMeta
    {
        public int Frames { get; set; }
    }

    public class Autocancel : BaseMeta
    {
        public string Cancel1 { get; set; }
        public string Cancel2 { get; set; }
    }
}