
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class BowserJr : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat ClownCannonNoCharge { get; set; }

        [StatProperty]
        public SpecialStat ClownCannonNoChargeLate { get; set; }

        [StatProperty]
        public SpecialStat ClownCannonFullCharge { get; set; }

        [StatProperty]
        public SpecialStat ClownCannonFullChargeLate { get; set; }

        [StatProperty]
        public SpecialStat ClownKartDashGroundDash { get; set; }

        [StatProperty]
        public SpecialStat ClownKartDashSpinout { get; set; }

        [StatProperty]
        public SpecialStat AbandonShipInitial { get; set; }

        [StatProperty]
        public SpecialStat AbandonShipExplosion { get; set; }

        [StatProperty]
        public SpecialStat AbandonShipHammer { get; set; }

        [StatProperty]
        public SpecialStat MechakoopaProjectile { get; set; }

        [StatProperty]
        public SpecialStat MechakoopaExplosion { get; set; }

        #endregion

        public BowserJr()
            : base(Characters.BOWSERJR)
        { }
    }
}
