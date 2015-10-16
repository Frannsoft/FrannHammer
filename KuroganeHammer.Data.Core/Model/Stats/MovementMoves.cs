
namespace KuroganeHammer.Data.Core.Model.Stats
{
    public class MovementMoves
    {
        [StatProperty]
        public MovementStat Weight { get; set; }

        [StatProperty]
        public MovementStat RunSpeed { get; set; }

        [StatProperty]
        public MovementStat WalkSpeed { get; set; }

        [StatProperty]
        public MovementStat AirSpeed { get; set; }

        [StatProperty]
        public MovementStat FallSpeed { get; set; }

        [StatProperty]
        public MovementStat FastFallSpeed { get; set; }

        [StatProperty]
        public MovementStat MaxJumps { get; set; }

        [StatProperty]
        public MovementStat WallJump { get; set; }

        [StatProperty]
        public MovementStat WallCling { get; set; }

        [StatProperty]
        public MovementStat Crawl { get; set; }

        [StatProperty]
        public MovementStat Tether { get; set; }

        [StatProperty]
        public MovementStat Jumpsquat { get; set; }

        [StatProperty]
        public MovementStat SoftLandingLag { get; set; }

        [StatProperty]
        public MovementStat HardLandingLag { get; set; }

        [StatProperty]
        public MovementStat AirAcceleration { get; set; }

        [StatProperty]
        public MovementStat Gravity { get; set; }

        [StatProperty]
        public MovementStat SHAirTime { get; set; }
    }
}
