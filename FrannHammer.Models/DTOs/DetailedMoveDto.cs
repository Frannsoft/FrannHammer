namespace FrannHammer.Models.DTOs
{
    public class DetailedMoveDto
    {
        public int MoveId { get; set; }
        public string MoveName { get; set; }
        public dynamic Angle { get; set; }
        public dynamic Hitbox { get; set; }
        public dynamic BaseDamage { get; set; }
        public dynamic BaseKnockback { get; set; }
        public dynamic SetKnockback { get; set; }
        public dynamic FirstActionableFrame { get; set; }
        public dynamic Autocancel { get; set; }
        public dynamic LandingLag { get; set; }

        //TODO: No good way to expose this here yet.
        //public dynamic Throws { get; set; }
    }
}
