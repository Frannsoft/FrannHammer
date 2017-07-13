using System;
using System.Linq;
using System.Reflection;

namespace FrannHammer.WebScraping.Domain.Contracts
{
    public static class CharacterIds
    {
        public const int Bayonetta = 1;
        public const int Bowser = 2;
        public const int BowserJr = 3;
        public const int CaptainFalcon = 4;
        public const int Charizard = 5;
        public const int Cloud = 6;
        public const int Corrin = 7;
        public const int DarkPit = 8;
        public const int DiddyKong = 9;
        public const int DonkeyKong = 10;
        public const int DrMario = 11;
        public const int DuckHunt = 12;
        public const int Falco = 13;
        public const int Fox = 14;
        public const int Ganondorf = 15;
        public const int Greninja = 16;
        public const int Ike = 17;
        public const int Jigglypuff = 18;
        public const int KingDedede = 19;
        public const int Kirby = 20;
        public const int Link = 21;
        public const int LittleMac = 22;
        public const int Lucario = 23;
        public const int Lucas = 24;
        public const int Lucina = 25;
        public const int Luigi = 26;
        public const int Mario = 27;
        public const int Marth = 28;
        public const int MegaMan = 29;
        public const int MetaKnight = 30;
        public const int Mewtwo = 31;
        public const int MiiBrawler = 32;
        public const int MiiGunner = 33;
        public const int MiiSwordfighter = 34;
        public const int MrGameWatch = 35;
        public const int Ness = 36;
        public const int Olimar = 37;
        public const int Pacman = 38;
        public const int Palutena = 39;
        public const int Peach = 40;
        public const int Pikachu = 41;
        public const int Pit = 42;
        public const int Rob = 43;
        public const int Robin = 44;
        public const int RosalinaLuma = 45;
        public const int Roy = 46;
        public const int Ryu = 47;
        public const int Samus = 48;
        public const int Sheik = 49;
        public const int Shulk = 50;
        public const int Sonic = 51;
        public const int ToonLink = 52;
        public const int Villager = 53;
        public const int Wario = 54;
        public const int WiiFitTrainer = 55;
        public const int Yoshi = 56;
        public const int Zelda = 57;
        public const int ZeroSuitSamus = 58;

        public static int FindByName(string characterName)
        {
            string sanitizedName = characterName
                .Replace(" ", string.Empty)
                .Replace(".", string.Empty)
                .Replace("&", string.Empty)
                .Replace("-", string.Empty);

            var fields = typeof(CharacterIds).GetFields(BindingFlags.Public | BindingFlags.Static);
            var foundConstant = fields.Single(field => field.Name.Equals(sanitizedName, StringComparison.CurrentCultureIgnoreCase));

            return (int)foundConstant.GetValue(null);
        }
    }
}
