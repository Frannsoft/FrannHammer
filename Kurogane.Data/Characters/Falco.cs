﻿
using Kurogane.Web.Data.Stats;
namespace Kurogane.Web.Data.Characters
{
    public class Falco : Character
    {
        #region special moves

        [StatProperty]
        public SpecialStat Blaster { get; set; }

        [StatProperty]
        public SpecialStat BlasterAerial { get; set; }

        [StatProperty]
        public SpecialStat FalcoPhantasmGround { get; set; }

        [StatProperty]
        public SpecialStat FalcoPhantasmAerial { get; set; }

        [StatProperty]
        public SpecialStat FireBirdHits17 { get; set; }

        [StatProperty]
        public SpecialStat FireBirdHit8 { get; set; }

        [StatProperty]
        public SpecialStat FireBirdHits914 { get; set; }

        [StatProperty]
        public SpecialStat FireBirdHit15 { get; set; }

        [StatProperty]
        public SpecialStat Reflector { get; set; }

        #endregion

        public Falco()
            : base(Characters.FALCO)
        { }
    }
}
