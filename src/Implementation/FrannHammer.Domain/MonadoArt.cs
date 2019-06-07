namespace FrannHammer.Domain
{
    public class MonadoArt : UniqueData
    {
        public string Active { get; set; }
        public string Cooldown { get; set; }
        public string DamageTaken { get; set; }
        public string DamageDealt { get; set; }
        public string KnockbackTaken { get; set; }
        public string KnockbackDealt { get; set; }
        public string JumpHeight { get; set; }
        public string LedgeJumpHeight { get; set; }
        public string AirSlashHeight { get; set; }
        public string InitialDashSpeed { get; set; }
        public string RunSpeed { get; set; }
        public string WalkSpeed { get; set; }
        public string AirSpeed { get; set; }
        public string Gravity { get; set; }
        public string FallSpeed { get; set; }
        public string ShieldHealth { get; set; }
        public string ShieldRegen { get; set; }
    }
}
