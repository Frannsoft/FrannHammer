
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
	[JsonObject]
	public class CaptainFalcon : Character
    {
        #region special moves
        //TODO: remove special moves
        [StatProperty]
        public SpecialStat FalconPunch { get; set; }

        [StatProperty]
        public SpecialStat FalconPunchAerial { get; set; }

        [StatProperty]
        public SpecialStat FalconPunchBReversed { get; set; }

        [StatProperty]
        public SpecialStat FalconPunchBReversedAerial { get; set; }

        [StatProperty]
        public SpecialStat RaptorBoostGround { get; set; }

        [StatProperty]
        public SpecialStat RaptorBoostAerial { get; set; }

        [StatProperty]
        public SpecialStat RaptorBoostAttackGround { get; set; }

        [StatProperty]
        public SpecialStat RaptorBoostAttackAerial { get; set; }

        [StatProperty]
        public SpecialStat FalconDiveCommandGrab { get; set; }

        [StatProperty]
        public SpecialStat FalconDiveLatch { get; set; }

        [StatProperty]
        public SpecialStat FalconDiveAttack { get; set; }

        [StatProperty]
        public SpecialStat FalconKickEarlyGround { get; set; }

        [StatProperty]
        public SpecialStat FalconKickGround { get; set; }

        [StatProperty]
        public SpecialStat FalconKickLateGround { get; set; }

        [StatProperty]
        public SpecialStat FalconKickEarlyAerial { get; set; }

        [StatProperty]
        public SpecialStat FalconKickAerial { get; set; }

        [StatProperty]
        public SpecialStat FalconKickLateAerial { get; set; }

        [StatProperty]
        public SpecialStat FalconKickLanding { get; set; }

        #endregion
        public CaptainFalcon()
            : base(Characters.CAPTAINFALCON)
        { }
    }
}
