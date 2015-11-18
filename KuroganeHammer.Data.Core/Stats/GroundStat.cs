using System.ComponentModel.DataAnnotations.Schema;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [Table("GroundStats")]
    public class GroundStat : MoveStat
    {
        public GroundStat(string name, int ownerId, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth)
            : base(name, ownerId, hitboxActive, firstActionableFrame, baseDamage, angle, baseKnockbackSetKnockback,
                    knockbackGrowth)
        { }

        public GroundStat()
        { }
    }
}
