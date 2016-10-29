namespace FrannHammer.Models
{
    /// <summary>
    /// The model used for forming a search request through all available moves in the database.  Properties not 
    /// set will not be included in the search.
    /// 
    /// Not all properties on the <see cref="RangeModel"/> need to be set in every case.
    /// 
    /// For example, an <see cref="RangeQuantifier.EqualTo"/> based <see cref="RangeModel"/>
    /// does not need both the Start and End values filled in (just the Start).
    /// </summary>
    public class ComplexMoveSearchModel
    {
        public string Name { get; set; }
        public string CharacterName { get; set; }
        public RangeModel HitboxActiveLength { get; set; }
        public RangeModel HitboxStartupFrame { get; set; }
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
