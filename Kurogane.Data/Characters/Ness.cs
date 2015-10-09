
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Ness : Character
    {
        [StatProperty]
        public SpecialStat PKTrashNoCharge { get; set; }

        [StatProperty]
        public SpecialStat PKTrashFullyCharged { get; set; }


        [StatProperty]
        public SpecialStat PKFire { get; set; }


        [StatProperty]
        public SpecialStat PKFirePillar { get; set; }


        [StatProperty]
        public SpecialStat PKThunderHead { get; set; }


        [StatProperty]
        public SpecialStat PKThunderTail { get; set; }


        [StatProperty]
        public SpecialStat PKThunder2 { get; set; }


        [StatProperty]
        public SpecialStat PKThunder2Late { get; set; }


        [StatProperty]
        public SpecialStat PSIMagnet { get; set; }



        public Ness()
            : base(Characters.NESS)
        { }
    }
}
