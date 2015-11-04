
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject]
    public class CharacterUtility
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes<DescriptionAttribute>(false);

            if (attributes != null &&
                attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }

    [Flags]
    public enum Characters
    {
        [Description("Pikachu")]
        PIKACHU = 1,

        [Description("Falco")]
        FALCO = 2,

        [Description("Donkey%20Kong")]
        DONKEYKONG = 3,

        [Description("Bowser")]
        BOWSER = 4,

        [Description("Bowser%20Jr")]
        BOWSERJR = 5,

        [Description("Captain%20Falcon")]
        CAPTAINFALCON = 6,

        [Description("Charizard")]
        CHARIZARD = 7,

        [Description("Dark%20Pit")]
        DARKPIT = 8,

        [Description("Diddy%20Kong")]
        DIDDYKONG = 9,

        [Description("Dr.%20Mario/")]
        DRMARIO = 10,

        [Description("Duck%20Hunt")]
        DUCKHUNT = 11,

        [Description("Fox")]
        FOX = 12,

        [Description("Ganondorf")]
        GANONDORF = 13,

        [Description("Greninja")]
        GRENINJA = 14,

        [Description("Ike")]
        IKE = 15,

        [Description("Jigglypuff")]
        JIGGLYPUFF = 16,

        [Description("King%20Dedede")]
        DEDEDE = 17,

        [Description("Link")]
        LINK = 18,

        [Description("Little%20Mac")]
        LITTLEMAC = 19,

        [Description("Lucario")]
        LUCARIO = 20,

        [Description("Lucas")]
        LUCAS = 21,

        [Description("Lucina")]
        LUCINA = 22,

        [Description("Luigi")]
        LUIGI = 23,

        [Description("Mario")]
        MARIO = 24,

        [Description("Marth")]
        MARTH = 25,

        [Description("Mega%20Man")]
        MEGAMAN = 26,

        [Description("Meta%20Knight")]
        METAKNIGHT = 27,

        [Description("Mewtwo")]
        MEWTWO = 28,

        [Description("Mii%20Swordfighter")]
        MIISWORDFIGHTER = 29,

        [Description("Game%20And%20Watch")]
        GAMEANDWATCH = 30,

        [Description("Ness")]
        NESS = 31,

        [Description("Olimar")]
        OLIMAR = 32,

        [Description("PAC-MAN")]
        PACMAN = 33,

        [Description("Palutena")]
        PALUTENA = 34,

        [Description("Peach")]
        PEACH = 35,

        [Description("Pit")]
        PIT = 36,

        [Description("R.O.B/")]
        ROB = 37,

        [Description("Robin")]
        ROBIN = 38,

        [Description("Rosalina%20And%20Luma")]
        ROSALINAANDLUMA = 39,

        [Description("Roy")]
        ROY = 40,

        [Description("Ryu")]
        RYU = 41,

        [Description("Samus")]
        SAMUS = 42,

        [Description("Sheik")]
        SHEIK = 43,

        [Description("Shulk")]
        SHULK = 44,

        [Description("Sonic")]
        SONIC = 45,

        [Description("Toon%20Link")]
        TOONLINK = 46,

        [Description("Villager")]
        VILLAGER = 47,

        [Description("Wario")]
        WARIO = 48,

        [Description("Wii%20Fit%20Trainer")]
        WIIFITRAINER = 49,

        [Description("Yoshi")]
        YOSHI = 50,

        [Description("Zelda")]
        ZELDA = 51,

        [Description("Zero%20Suit%20Samus")]
        ZEROSUITSAMUS = 52,

        [Description("Mii%20Brawler")]
        MIIBRAWLER = 53,

        [Description("Mii%20Gunner")]
        MIIGUNNER = 54,

        [Description("Kirby")]
        KIRBY = 55
    }
}
