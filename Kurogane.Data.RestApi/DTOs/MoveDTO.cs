
using KuroganeHammer.Data.Core.Model.Stats;
namespace Kurogane.Data.RestApi.DTOs
{
    public class MoveDTO
    {
        public int Id { get; set; }
        public string HitboxActive { get; set; }
        public string FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockBackSetKnockback { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string LandingLag { get; set; }
        public string AutoCancel { get; set; }
        public string KnockbackGrowth { get; set; }
        public MoveType Type { get; set; }
    }
}