
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
        [DataMember(Name="HitboxActive")]
        public string HitboxActive { get; private set; }
        public string FirstActionableFrame { get; private set; }
        public string BaseDamage { get; private set; }
        public string Angle { get; private set; }
        public string BaseKnockbackSetKnockback { get; private set; }
        public string KnockbackGrowth { get; private set; }
        public string LandingLag { get; private set; }
        public string Autocancel { get; private set; }

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
