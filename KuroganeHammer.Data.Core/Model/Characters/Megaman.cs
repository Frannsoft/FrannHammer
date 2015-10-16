﻿
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Megaman : Character
    {
        [StatProperty]
        public SpecialStat MetalBlade { get; set; }

        [StatProperty]
        public SpecialStat MetalBladeThrownBack { get; set; }


        [StatProperty]
        public SpecialStat CrashBomberBomb { get; set; }


        [StatProperty]
        public SpecialStat CrashBomberExplosion { get; set; }


        [StatProperty]
        public SpecialStat CrashBomberExplosionFinalHit { get; set; }


        [StatProperty]
        public SpecialStat RushCoil { get; set; }


        [StatProperty]
        public SpecialStat LeafShield { get; set; }


        [StatProperty]
        public SpecialStat LeafShieldThrown { get; set; }



        public Megaman()
            : base(Characters.MEGAMAN)
        { }
    }
}
