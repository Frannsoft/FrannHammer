
using KuroganeHammer.Data.Core.Model.Stats;
namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class DuckHunt : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat TrickShot { get; set; }

        [StatProperty]
        public SpecialStat TrickShotReticleHit { get; set; }

        [StatProperty]
        public SpecialStat TrickShotExplosion { get; set; }

        [StatProperty]
        public SpecialStat ClayShootingPidgeon { get; set; }

        [StatProperty]
        public SpecialStat ClayShootingReticleHits13 { get; set; }

        [StatProperty]
        public SpecialStat ClayShootingReticleHit4 { get; set; }

        [StatProperty]
        public SpecialStat DuckJump { get; set; }

        [StatProperty]
        public SpecialStat WildGunman { get; set; }

        [StatProperty]
        public SpecialStat WildGunmanSombrero { get; set; }

        [StatProperty]
        public SpecialStat WildGunmanBrownCoatHat { get; set; }

        [StatProperty]
        public SpecialStat WildGunmanBlackCoatHat { get; set; }

        [StatProperty]
        public SpecialStat WildGunmanWhiteShirtBrownHat { get; set; }

        [StatProperty]
        public SpecialStat WildGunmanBlackVestBrownHat { get; set; }

        #endregion

        public DuckHunt()
            : base(Characters.DUCKHUNT)
        { }
    }
}
