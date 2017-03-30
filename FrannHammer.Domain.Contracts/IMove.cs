namespace FrannHammer.Domain.Contracts
{
    public interface IMove : IModel
    {
        string HitboxActive { get; set; }
        string FirstActionableFrame { get; set; }
        string BaseDamage { get; set; }
        string Angle { get; set; }
        string BaseKnockbackSetKnockback { get; set; }
        string LandingLag { get; set; }
        string AutoCancel { get; set; }
        string KnockbackGrowth { get; set; }
        MoveType MoveType { get; set; }
        int OwnerId { get; set; }
    }
}
