﻿using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [JsonObject]
    public class GroundStat : Stat
    {
        internal string HitBoxActive { get; private set; }
        internal string FirstActionableFrame { get; private set; }
        internal string BaseDamage { get; private set; }
        internal string Angle { get; private set; }
        internal string BaseKnockbackSetKnockback { get; private set; }
        internal string KnockbackGrowth { get; private set; }

        public GroundStat(string name, int ownerId, string rawName, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseSetKnockback, string knockbackGrowth)
            : base(name, ownerId, rawName)
        {
            HitBoxActive = hitboxActive;
            FirstActionableFrame = firstActionableFrame;
            BaseDamage = baseDamage;
            Angle = angle;
            BaseKnockbackSetKnockback = baseSetKnockback;
            KnockbackGrowth = knockbackGrowth;
        }
    }
}
