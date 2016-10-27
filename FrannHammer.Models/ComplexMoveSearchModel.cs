namespace FrannHammer.Models
{
    public class ComplexMoveSearchModel
    {
        public string Name { get; set; }
        public RangeModel HitboxStartupFrame { get; set; }
        public int HitboxActiveLength { get; set; }
        public string CharacterName { get; set; }
        public RangeModel HitboxActiveOnFrame { get; set; }
        public RangeModel FirstActionableFrame { get; set; }

        public RangeModel LandingLag { get; set; }
        public RangeModel AutoCancel { get; set; }
        public RangeModel BaseDamage { get; set; }
        public RangeModel Angle { get; set; }
        public RangeModel BaseKnockback { get; set; }
        public RangeModel SetKnockback { get; set; }
        public RangeModel KnockbackGrowth { get; set; }
    }
}
