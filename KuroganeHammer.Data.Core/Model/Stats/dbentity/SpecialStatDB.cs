namespace KuroganeHammer.Data.Core.Model.Stats.dbentity
{
    internal class SpecialStatDB : StatDB
    {
        internal string hitboxactive { get; private set; }
        internal string firstactionableframe { get; private set; }
        internal string basedamage { get; private set; }
        internal string angle { get; private set; }
        internal string baseknockbacksetknockback { get; private set; }
        internal string knockbackgrowth { get; private set; }

        internal SpecialStatDB()
        { }

        internal SpecialStatDB(string name, int ownerId, string rawName, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseSetKnockback, string knockbackGrowth)
            : base(name, ownerId, rawName)
        {
            this.hitboxactive = hitboxActive;
            this.firstactionableframe = firstActionableFrame;
            this.basedamage = baseDamage;
            this.angle = angle;
            this.baseknockbacksetknockback = baseSetKnockback;
            this.knockbackgrowth = knockbackGrowth;
        }
    }
}
