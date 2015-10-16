
using KuroganeHammer.Data.Core.Model.Stats;
namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class DiddyKong : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat PeanutPopgunPeanut { get; set; }

        [StatProperty]
        public SpecialStat PeanutPopgunExplosion { get; set; }

        [StatProperty]
        public SpecialStat MonkeyFlipGrab { get; set; }

        [StatProperty]
        public SpecialStat MonkeyFlipGroundThrow { get; set; }

        [StatProperty]
        public SpecialStat MonkeyFlipAerialThrow { get; set; }

        [StatProperty]
        public SpecialStat MonkeyFlipAttackInput { get; set; }

        [StatProperty]
        public SpecialStat MonkeyFlipAttackInputLate { get; set; }

        [StatProperty]
        public SpecialStat RocketbarrelBoostLaunch { get; set; }

        [StatProperty]
        public SpecialStat RocketbarrelBoostTravel { get; set; }

        [StatProperty]
        public SpecialStat RocketbarrelBoostTravelLate { get; set; }

        [StatProperty]
        public SpecialStat BananaPeel { get; set; }

        [StatProperty]
        public SpecialStat BananaPeelFailed { get; set; }

        #endregion

        public DiddyKong()
            : base(Characters.DIDDYKONG)
        { }
    }
}
