﻿using Kurogane.Web.Data.Stats;

namespace Kurogane.Web.Data.Characters
{
    public class RosalinaAndLuma : Character
    {
        [StatProperty]
        public SpecialStat LumaShotNoCharge { get; set; }

        [StatProperty]
        public SpecialStat LumaShotFullChargeRelease { get; set; }


        [StatProperty]
        public SpecialStat LumaShotWandWave { get; set; }


        [StatProperty]
        public SpecialStat StarBits { get; set; }


        [StatProperty]
        public SpecialStat LaunchStar { get; set; }


        [StatProperty]
        public SpecialStat GravitationalPull { get; set; }




        public RosalinaAndLuma()
            : base(Characters.ROSALINAANDLUMA)
        { }
    }
}
