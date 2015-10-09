
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Shulk : Character
    {
        [StatProperty]
        public SpecialStat MonadoArtsDuration { get; set; }

        [StatProperty]
        public SpecialStat MonadoArtsCooldown { get; set; }


        [StatProperty]
        public SpecialStat Backslash { get; set; }


        [StatProperty]
        public SpecialStat AirSlashHit1Early { get; set; }


        [StatProperty]
        public SpecialStat AirSlashHit1 { get; set; }


        [StatProperty]
        public SpecialStat AirSlashHit1Late { get; set; }


        [StatProperty]
        public SpecialStat AirSlashHit2 { get; set; }


        [StatProperty]
        public SpecialStat Vision { get; set; }


        [StatProperty]
        public SpecialStat VisionDepreciated { get; set; }


        [StatProperty]
        public SpecialStat VisionAttack { get; set; }


        [StatProperty]
        public SpecialStat VisionForwardAttack { get; set; }



        public Shulk()
            : base(Characters.SHULK)
        { }
    }
}
