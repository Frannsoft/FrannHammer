
namespace Kurogane.Web.Data.Stats
{
    public class GroundStat : Stat
    {
        internal string HitBoxActive { get; private set; }
        internal string FirstActionableFrame { get; private set; }
        internal string BaseDamage { get; private set; }
        internal string Angle { get; private set; }
        internal string BaseKnockbackSetKnockback { get; private set; }
        internal string KnockbackGrowth { get; private set; }

        public GroundStat(string name, string rawName, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseSetKnockback, string knockbackGrowth)
            : base(name, rawName)
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
