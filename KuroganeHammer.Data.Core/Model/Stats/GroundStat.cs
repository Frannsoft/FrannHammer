using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [Table("GroundStats")]
    public class GroundStat : Stat
    {
        public string HitBoxActive { get; set; }
        public int FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockbackSetKnockback { get; set; }
        public string KnockbackGrowth { get; set; }

        public GroundStat(string name, int ownerId, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth)
            : base(name, ownerId)
        {
            HitBoxActive = hitboxActive;

            firstActionableFrame = firstActionableFrame.Replace(" ", string.Empty);

            int result = 0;
            if (int.TryParse(firstActionableFrame, out result))
            {
                FirstActionableFrame = Convert.ToInt32(result);
            }
            BaseDamage = baseDamage;
            Angle = angle;
            BaseKnockbackSetKnockback = baseKnockbackSetKnockback;
            KnockbackGrowth = knockbackGrowth;
        }

        public GroundStat()
        { }
    }
}
