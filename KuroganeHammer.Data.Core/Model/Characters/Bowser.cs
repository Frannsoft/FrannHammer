
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Bowser : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat FireBreath { get; set; }

        [StatProperty]
        public SpecialStat FireBreathFlinchless { get; set; }

        [StatProperty]
        public SpecialStat FlyingSlamCommandGrabGround { get; set; }

        [StatProperty]
        public SpecialStat FlyingSlamCommandGrabAerial { get; set; }

        [StatProperty]
        public SpecialStat FlyingSlamHitbox { get; set; }

        [StatProperty]
        public SpecialStat WhirlingFortressGround { get; set; }

        [StatProperty]
        public SpecialStat WhirlingFortressFinalHitGround { get; set; }

        [StatProperty]
        public SpecialStat WhirlingFortressAerialHit1 { get; set; }

        [StatProperty]
        public SpecialStat WhirlingFortressAerialHits26 { get; set; }

        [StatProperty]
        public SpecialStat WhirlingFortressAerialHits711 { get; set; }

        [StatProperty]
        public SpecialStat BowserBombHit1 { get; set; }

        [StatProperty]
        public SpecialStat BowserBombHit2 { get; set; }

        [StatProperty]
        public SpecialStat BowserBombLanding { get; set; }

        [StatProperty]
        public SpecialStat BowserBombAerial { get; set; }

        #endregion

        public Bowser()
            : base(Characters.BOWSER)
        { }


    }
}
