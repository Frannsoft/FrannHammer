using System;

namespace FrannHammer.WebScraper.Stats
{
    public class MoveStat : Stat
    {
        public string HitboxActive { get; set; }
        public string FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockBackSetKnockback { get; set; }
        public string LandingLag { get; set; }
        public string AutoCancel { get; set; }
        public string KnockbackGrowth { get; set; }
        public MoveType Type { get; set; }

        public MoveStat(MoveType type, string name, int ownerId, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth, string landingLag = "", string autoCancel = "")
            : base(name, ownerId)
        {
            HitboxActive = hitboxActive;
            //TotalHitboxActiveLength = DetermineHitBoxActiveLength(hitboxActive);
            //firstActionableFrame = firstActionableFrame.Replace(" ", string.Empty);

            //int result;
            //if (int.TryParse(firstActionableFrame, out result))
            //{
            //    FirstActionableFrame = Convert.ToInt32(result);
            //}
            FirstActionableFrame = firstActionableFrame;
            BaseDamage = baseDamage;
            Angle = angle;
            BaseKnockBackSetKnockback = baseKnockbackSetKnockback;
            KnockbackGrowth = knockbackGrowth;
            LandingLag = landingLag;
            AutoCancel = autoCancel;
            Type = type;
        }

        public MoveStat()
        { }
    }



}
