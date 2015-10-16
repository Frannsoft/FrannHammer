﻿
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Rob : Character
    {
        [StatProperty]
        public SpecialStat RoboBeamNoCharge { get; set; }

        [StatProperty]
        public SpecialStat RoboBeamHalfCharge { get; set; }


        [StatProperty]
        public SpecialStat RoboBeamSuperCharge { get; set; }


        [StatProperty]
        public SpecialStat ArmRotorNoMashing { get; set; }


        [StatProperty]
        public SpecialStat ArmRotorMaxMashing { get; set; }


        [StatProperty]
        public SpecialStat ArmRotorFinalHit { get; set; }


        [StatProperty]
        public SpecialStat RoboBurner { get; set; }


        [StatProperty]
        public SpecialStat GyroCharging { get; set; }


        [StatProperty]
        public SpecialStat Gyro { get; set; }


        [StatProperty]
        public SpecialStat GyroFailure { get; set; }



        public Rob()
            : base(Characters.ROB)
        { }
    }
}
