namespace FrannHammer.Domain.Contracts
{
    public interface IMove : IModel, IHaveAnOwner
    {
        string HitboxActive { get; set; }
        string FirstActionableFrame { get; set; }
        string BaseDamage { get; set; }
        string Angle { get; set; }
        string BaseKnockBackSetKnockback { get; set; }
        string LandingLag { get; set; }
        string AutoCancel { get; set; }
        string KnockbackGrowth { get; set; }
        string MoveType { get; set; }
        bool IsWeightDependent { get; set; }
    }
}
