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

        //TODO: These three props need to be exposed in the MovesController as well (and metadataservice).
        public dynamic Autocancel { get; set; }
        public dynamic LandingLag { get; set; }
        //public dynamic Throws { get; set; }
    }
}
