using System;

namespace KuroganeHammer.Data.Api.Models
{
    public abstract class BaseMeta
    {
        public int Id { get; set; }
        public Character Character { get; set; }
        public int CharacterId { get; set; }
        public Move Move { get; set; }
        public int MoveId { get; set; }
        public string Hitbox1 { get; set; }
        public string Hitbox2 { get; set; }
        public string Hitbox3 { get; set; }
        public string Hitbox4 { get; set; }
        public string Hitbox5 { get; set; }
        public string Notes { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class Hitbox : BaseMeta
    { }

    public class BaseDamage : BaseMeta
    { }

    public class Angle : BaseMeta
    { }

    public class BaseKnockbackSetKnockback : BaseMeta
    { }

    public class KnockbackGrowth : BaseMeta
    { }

    public class LandingLag
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public Move Move { get; set; }
        public int MoveId { get; set; }
        public int Frames { get; set; }
        public double Notes { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class Autocancel
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public Move Move { get; set; }
        public int MoveId { get; set; }
        public string Cancel1 { get; set; }
        public string Cancel2 { get; set; }
        public DateTime LastModified { get; set; }
    }
}