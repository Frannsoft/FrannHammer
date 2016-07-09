
namespace FrannHammer.WebScraper.Stats
{
    public class GroundStat : MoveStat
    {
        public GroundStat(string name, int ownerId, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth)
            : base(MoveType.Ground, name, ownerId, hitboxActive, firstActionableFrame, baseDamage, angle, baseKnockbackSetKnockback,
                    knockbackGrowth)
        { }

        public GroundStat()
        { }
    }
}
