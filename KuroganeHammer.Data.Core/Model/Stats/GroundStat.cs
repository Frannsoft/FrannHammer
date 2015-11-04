using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [Table("GroundStats")]
    public class GroundStat : Stat
    {
        public string HitBoxActive { get; private set; }
        public string FirstActionableFrame { get; private set; }
        public string BaseDamage { get; private set; }
        public string Angle { get; private set; }
        public string BaseKnockbackSetKnockback { get; private set; }
        public string KnockbackGrowth { get; private set; }

        public GroundStat(string name, int ownerId, string rawName, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth)
            : base(name, ownerId, rawName)
        {
            HitBoxActive = hitboxActive;
            FirstActionableFrame = firstActionableFrame;
            BaseDamage = baseDamage;
            Angle = angle;
            BaseKnockbackSetKnockback = baseKnockbackSetKnockback;
            KnockbackGrowth = knockbackGrowth;
        }

        public GroundStat()
        { }
    }
}
