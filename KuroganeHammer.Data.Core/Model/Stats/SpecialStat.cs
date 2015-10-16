
namespace KuroganeHammer.Data.Core.Model.Stats
{
    public class SpecialStat : Stat
    {
        public string HitboxActive { get; private set; }
        public string FirstActionableFrame { get; private set; }
        public string BaseDamage { get; private set; }
        public string Angle { get; private set; }
        public string BaseKnockbackSetKnockback { get; private set; }
        public string KnockbackGrowth { get; private set; }

        public SpecialStat(string name, string rawName, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseSetKnockback, string knockbackGrowth)
            : base(name, rawName)
        {
            HitboxActive = hitboxActive;
            FirstActionableFrame = firstActionableFrame;
            BaseDamage = baseDamage;
            Angle = angle;
            BaseKnockbackSetKnockback = baseSetKnockback;
            KnockbackGrowth = knockbackGrowth;
        }
    }
}
