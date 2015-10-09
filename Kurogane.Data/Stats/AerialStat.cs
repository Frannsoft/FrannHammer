﻿
namespace Kurogane.Web.Data.Stats
{
    public class AerialStat : Stat
    {
        public string HitboxActive { get; private set; }
        public string FirstActionableFrame { get; private set; }
        public string BaseDamage { get; private set; }
        public string Angle { get; private set; }
        public string BaseKnockbackSetKnockback { get; private set; }
        public string KnockbackGrowth { get; private set; }
        public string LandingLag { get; private set; }
        public string Autocancel { get; private set; }

        public AerialStat(string name, string rawName, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth, string landingLang, string autoCancel)
            : base(name, rawName)
        {
            HitboxActive = hitboxActive;
            FirstActionableFrame = firstActionableFrame;
            BaseDamage = baseDamage;
            Angle = angle;
            BaseKnockbackSetKnockback = baseKnockbackSetKnockback;
            KnockbackGrowth = knockbackGrowth;
            LandingLag = landingLang;
            Autocancel = autoCancel;
        }
    }
}
