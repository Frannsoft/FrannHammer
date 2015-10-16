
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class PacMan : Character
    {
        [StatProperty]
        public SpecialStat BonusFruitThrow { get; set; }

        [StatProperty]
        public SpecialStat BonusFruitCHERRY { get; set; }


        [StatProperty]
        public SpecialStat BonusFruitSTRAWBERRY { get; set; }


        [StatProperty]
        public SpecialStat BonusFruitORANGE { get; set; }


        [StatProperty]
        public SpecialStat BonusFruitAPPLE { get; set; }


        [StatProperty]
        public SpecialStat BonusFruitMELON { get; set; }


        [StatProperty]
        public SpecialStat BonusFruitGALAGA { get; set; }


        [StatProperty]
        public SpecialStat BonusFruitBELL { get; set; }


        [StatProperty]
        public SpecialStat BonusFruitKEY { get; set; }


        [StatProperty]
        public SpecialStat PowerPellet { get; set; }


        [StatProperty]
        public SpecialStat PowerPelletDash { get; set; }


        [StatProperty]
        public SpecialStat PacJump { get; set; }


        [StatProperty]
        public SpecialStat PacJumpAttackEarly { get; set; }


        [StatProperty]
        public SpecialStat PacJumpAttack { get; set; }


        [StatProperty]
        public SpecialStat PacJumpAttackLate { get; set; }


        [StatProperty]
        public SpecialStat FireHydrant { get; set; }


        [StatProperty]
        public SpecialStat FireHydrantAir { get; set; }


        [StatProperty]
        public SpecialStat FireHydrantDropHitbox { get; set; }


        [StatProperty]
        public SpecialStat FireHydrantDamagedHitbox { get; set; }


        [StatProperty]
        public SpecialStat FireHydrantHorizontalWater { get; set; }


        [StatProperty]
        public SpecialStat FireHydrantVerticalWater { get; set; }



        public PacMan()
            : base(Characters.PACMAN)
        { }
    }
}
