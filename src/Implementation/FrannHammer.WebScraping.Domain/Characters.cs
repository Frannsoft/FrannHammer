using System.Collections.Generic;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public static class Characters
    {
        public static WebCharacter Greninja => new Greninja();
        public static WebCharacter Cloud => new Cloud();
        public static WebCharacter Yoshi => new Yoshi();
        public static WebCharacter DarkPit => new DarkPit();
        public static WebCharacter Bowser => new Bowser();
        public static WebCharacter DonkeyKong => new DonkeyKong();

        public static IEnumerable<WebCharacter> All => new List<WebCharacter>
        {
            new Bayonetta(),
            new Bowser(),
            new BowserJr(),
            new CaptainFalcon(),
            new Charizard(),
            new Cloud(),
            new Corrin(),
            new DarkPit(),
            new DiddyKong(),
            new DonkeyKong(),
            new DrMario(),
            new DuckHunt(),
            new Falco(),
            new Fox(),
            new Ganondorf(),
            new Greninja(),
            new Ike(),
            new Jigglypuff(),
            new KingDedede(),
            new Kirby(),
            new Link(),
            new LittleMac(),
            new Lucario(),
            new Lucas(),
            new Lucina(),
            new Luigi(),
            new Mario(),
            new Marth(),
            new MegaMan(),
            new MetaKnight(),
            new Mewtwo(),
            new MiiSwordfighter(),
            new MiiBrawler(),
            new MiiGunner(),
            new MrGameAndWatch(),
            new Ness(),
            new Olimar(),
            new PacMan(),
            new Palutena(),
            new Peach(),
            new Pikachu(),
            new Pit(),
            new Rob(),
            new Robin(),
            new RosalinaAndLuma(),
            new Roy(),
            new Ryu(),
            new Samus(),
            new Sheik(),
            new Shulk(),
            new Sonic(),
            new ToonLink(),
            new Villager(),
            new Wario(),
            new WiiFitTrainer(),
            new Yoshi(),
            new Zelda(),
            new ZeroSuitSamus()
        };
    }
}
