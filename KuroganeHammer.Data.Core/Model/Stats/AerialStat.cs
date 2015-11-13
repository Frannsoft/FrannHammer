
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [JsonObject]
    [Serializable]
    [Table("AerialStats")]
    public class AerialStat : Stat
    {
        public string HitboxActive { get; set; }
        public int FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockbackSetKnockback { get; set; }
        public string KnockbackGrowth { get; set; }
        public string LandingLag { get; set; }
        public string Autocancel { get; set; }

        public AerialStat(string name, int ownerId, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth, string landingLag, string autoCancel)
            : base(name, ownerId)
        {
            HitboxActive = hitboxActive;
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
            LandingLag = landingLag;
            Autocancel = autoCancel;
        }

        public AerialStat()
        { }
    }
}
