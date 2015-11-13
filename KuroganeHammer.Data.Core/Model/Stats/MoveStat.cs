
using System.ComponentModel.DataAnnotations.Schema;
namespace KuroganeHammer.Data.Core.Model.Stats
{
    [Table("Moves")]
    public class MoveStat : Stat
    {
        public string HitboxActive { get; set; }
        public int FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockBackSetKnockback { get; set; }
        public string LandingLag { get; set; }
        public string AutoCancel { get; set; }
        public string KnockbackGrowth { get; set; }
        public MoveType Type { get; set; }
    }

    public enum MoveType
    {
        Aerial,
        Ground,
        Special
    }
}
