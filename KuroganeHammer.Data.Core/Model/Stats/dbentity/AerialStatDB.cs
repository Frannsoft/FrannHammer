namespace KuroganeHammer.Data.Core.Model.Stats.dbentity
{
    internal class AerialStatDB : StatDB
    {
        internal string hitboxactive { get; private set; }
        internal string firstactionableframe { get; private set; }
        internal string basedamage { get; private set; }
        internal string angle { get; private set; }
        internal string baseknockbacksetknockback { get; private set; }
        internal string knockbackgrowth { get; private set; }
        internal string landinglag { get; private set; }
        internal string autocancel { get; private set; }

        internal AerialStatDB()
        { }

        internal AerialStatDB(string name, int ownerId, string rawName, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth, string landingLang, string autoCancel)
            : base(name, ownerId, rawName)
        {
            this.hitboxactive = hitboxActive;
            this.firstactionableframe = firstActionableFrame;
            this.basedamage = baseDamage;
            this.angle = angle;
            this.baseknockbacksetknockback = baseKnockbackSetKnockback;
            this.knockbackgrowth = knockbackGrowth;
            this.landinglag = landingLang;
            this.autocancel = autoCancel;
        }
    }
}
