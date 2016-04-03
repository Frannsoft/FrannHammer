
namespace KuroganeHammer.Data.Core
{
    public class AerialStat : MoveStat
    {
        public AerialStat(string name, int ownerId, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth, string landingLag, string autoCancel)
            : base(MoveType.Aerial, name, ownerId, hitboxActive, firstActionableFrame, baseDamage, angle, baseKnockbackSetKnockback,
                    knockbackGrowth, landingLag, autoCancel)
        { }

        public AerialStat()
        { }
    }
}
