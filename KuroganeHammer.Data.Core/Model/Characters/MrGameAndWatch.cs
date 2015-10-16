
using KuroganeHammer.Data.Core.Model.Stats;
namespace KuroganeHammer.Data.Core.Model.Characters
{
    public class MrGameAndWatch : Character
    {
        [StatProperty]
        public SpecialStat ChefPan { get; set; }

        [StatProperty]
        public SpecialStat ChefSausages { get; set; }


        [StatProperty]
        public SpecialStat Judge1 { get; set; }


        [StatProperty]
        public SpecialStat Judge2 { get; set; }


        [StatProperty]
        public SpecialStat Judge3 { get; set; }


        [StatProperty]
        public SpecialStat Judge4 { get; set; }


        [StatProperty]
        public SpecialStat Judge5 { get; set; }


        [StatProperty]
        public SpecialStat Judge6 { get; set; }


        [StatProperty]
        public SpecialStat Judge7 { get; set; }


        [StatProperty]
        public SpecialStat Judge8 { get; set; }


        [StatProperty]
        public SpecialStat Judge9 { get; set; }


        [StatProperty]
        public SpecialStat FireWindbox { get; set; }


        [StatProperty]
        public SpecialStat Fire { get; set; }


        [StatProperty]
        public SpecialStat OilPanic { get; set; }


        [StatProperty]
        public SpecialStat OilPanicAttack { get; set; }



        public MrGameAndWatch()
            : base(Characters.GAMEANDWATCH)
        { }
    }
}
