
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
	public class Lucario : Character
    {
        [StatProperty]
        public SpecialStat AuraSphereCharging { get; set; }

        [StatProperty]
        public SpecialStat AuraSphereReleasefromCharge { get; set; }


        [StatProperty]
        public SpecialStat AuraSphereFullCharge { get; set; }


        [StatProperty]
        public SpecialStat ForcePalmCommandGrab { get; set; }


        [StatProperty]
        public SpecialStat ForcePalmProjectile { get; set; }


        [StatProperty]
        public SpecialStat ExtremespeedHitbox { get; set; }


        [StatProperty]
        public SpecialStat Counter { get; set; }


        [StatProperty]
        public SpecialStat CounterAttack { get; set; }




        public Lucario()
            : base(Characters.LUCARIO)
        { }
    }
}
