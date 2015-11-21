
namespace KuroganeHammer.Model
{
    public class SpecialStat : MoveStat
    {
        public SpecialStat(string name, int ownerId, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth)
            : base(name, ownerId, hitboxActive, firstActionableFrame, baseDamage, angle, baseKnockbackSetKnockback,
                    knockbackGrowth)
        { }

        public SpecialStat()
        { }
    }
}
