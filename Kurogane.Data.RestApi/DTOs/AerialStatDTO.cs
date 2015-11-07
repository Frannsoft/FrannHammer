using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kurogane.Data.RestApi.DTOs
{
    public class AerialStatDTO : BaseDTO
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string RawName { get; set; }
        public string HitboxActive { get; set; }
        public string LandingLag { get; set; }
        public string Angle { get; set; }
        public string KnockbackGrowth { get; set; }
        public string FirstActionableFrame { get; set; }
        public string AutoCancel { get; set; }
        public string BaseDamage { get; set; }
        public string BaseKnockbackSetKnockback { get; set; }
    }
}
