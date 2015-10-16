
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class DrMario : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat Megavitamins { get; set; }

        [StatProperty]
        public SpecialStat MegavitaminsLate { get; set; }

        [StatProperty]
        public SpecialStat SuperSheet { get; set; }

        [StatProperty]
        public SpecialStat SuperJumpPunch { get; set; }

        [StatProperty]
        public SpecialStat SuperJumpPunchLate { get; set; }

        [StatProperty]
        public SpecialStat DrTornadoGround { get; set; }

        [StatProperty]
        public SpecialStat DrTornadoAerial { get; set; }

        #endregion

        public DrMario()
            : base(Characters.DRMARIO)
        { }
    }
}
