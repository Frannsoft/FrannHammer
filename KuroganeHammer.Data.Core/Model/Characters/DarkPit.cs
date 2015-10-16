﻿
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class DarkPit : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat SilverBowNoCharge { get; set; }

        [StatProperty]
        public SpecialStat SilverBowNoChargeAerial { get; set; }

        [StatProperty]
        public SpecialStat SilverBowFullCharge { get; set; }

        [StatProperty]
        public SpecialStat ElectroshockArmGround { get; set; }

        [StatProperty]
        public SpecialStat ElectroshockArmGroundAttack { get; set; }

        [StatProperty]
        public SpecialStat ElectroshockArmAerial { get; set; }

        [StatProperty]
        public SpecialStat ElectroshockArmAerialAttack { get; set; }

        [StatProperty]
        public SpecialStat PowerofFlight { get; set; }

        [StatProperty]
        public SpecialStat GuardianOrbitarsMinimumDuration { get; set; }

        [StatProperty]
        public SpecialStat GuardianOrbitarsMaximumDuration { get; set; }

        #endregion

        public DarkPit()
            : base(Characters.DARKPIT)
        { }
    }
}
