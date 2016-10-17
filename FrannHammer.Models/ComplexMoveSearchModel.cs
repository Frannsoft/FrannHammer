namespace FrannHammer.Models
{
    public class ComplexMoveSearchModel
    {
        public string Name { get; set; }
        public RangeModel HitboxStartupFrame { get; set; }
        public int HitboxActiveLength { get; set; }
        //public int OwnerId { get; set; }
        public RangeModel HitboxActiveOnFrame { get; set; }
        //public int FirstActionableFrame { get; set; }
        //public int LandingLag { get; set; }
        //public int? AutoCancel { get; set; }
        //public int BaseDamage { get; set; }
        //public int Angle { get; set; }
        //public int BaseKnockbackSetKnockback { get; set; }
        //public int KnockbackGrowth { get; set; }
        //public bool IncludeWeightBasedKnockback { get; set; }
    }
}
