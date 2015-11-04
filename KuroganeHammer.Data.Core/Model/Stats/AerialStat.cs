
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
        public string FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockbackSetKnockback { get; set; }
        public string KnockbackGrowth { get; set; }
        public string LandingLag { get; set; }
        public string Autocancel { get; set; }

        public AerialStat(string name, int ownerId, string rawName, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth, string landingLag, string autoCancel)
            : base(name, ownerId, rawName)
        {
            HitboxActive = hitboxActive;
            FirstActionableFrame = firstActionableFrame;
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
