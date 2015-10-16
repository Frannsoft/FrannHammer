using KuroganeHammer.Data.Core.Model.Stats;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class Villager : Character
    {
        [StatProperty]
        public SpecialStat Pocket { get; set; }

        [StatProperty]
        public SpecialStat LloidRocket { get; set; }

        [StatProperty]
        public SpecialStat LloidRocketLate { get; set; }

        [StatProperty]
        public SpecialStat LloidRocketRide { get; set; }

        [StatProperty]
        public SpecialStat LloidRocketRideLate { get; set; }

        [StatProperty]
        public SpecialStat BalloonTrip { get; set; }

        [StatProperty]
        public SpecialStat TimberPlanting { get; set; }

        [StatProperty]
        public SpecialStat TimberWateringCan { get; set; }

        [StatProperty]
        public SpecialStat TimberAxe { get; set; }

        [StatProperty]
        public SpecialStat TimberChop { get; set; }

        [StatProperty]
        public SpecialStat TimberGrowing { get; set; }

        [StatProperty]
        public SpecialStat TimberGrowingLate { get; set; }

        [StatProperty]
        public SpecialStat TimberFalling { get; set; }


        public Villager()
            : base(Characters.VILLAGER)
        { }
    }
}
