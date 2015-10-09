
using System;
using System.ComponentModel;
using System.Reflection;

namespace Kurogane.Web.Data.Characters
{
    public class CharacterUtility
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = 
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes<DescriptionAttribute>(false);

            if(attributes != null &&
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

    public enum Characters
    {
        [Description("Pikachu")]
        PIKACHU,

        [Description("Falco")]
        FALCO,

        [Description("Donkey%20Kong")]
        DONKEYKONG,

        [Description("Bowser")]
        BOWSER,

        [Description("Bowser%20Jr")]
        BOWSERJR,

        [Description("Captain%20Falcon")]
        CAPTAINFALCON,

        [Description("Charizard")]
        CHARIZARD,

        [Description("Dark%20Pit")]
        DARKPIT,

        [Description("Diddy%20Kong")]
        DIDDYKONG,

        [Description("Dr.%20Mario/")]
        DRMARIO,

        [Description("Duck%20Hunt")]
        DUCKHUNT,

        [Description("Fox")]
        FOX,

        [Description("Ganondorf")]
        GANONDORF,

        [Description("Greninja")]
        GRENINJA,

        [Description("Ike")]
        IKE,

        [Description("Jigglypuff")]
        JIGGLYPUFF,

        [Description("King%20Dedede")]
        DEDEDE,

        [Description("Link")]
        LINK,

        [Description("Little%20Mac")]
        LITTLEMAC,

        [Description("Lucario")]
        LUCARIO,

        [Description("Lucas")]
        LUCAS,

        [Description("Lucina")]
        LUCINA,

        [Description("Luigi")]
        LUIGI,

        [Description("Mario")]
        MARIO,

        [Description("Marth")]
        MARTH,

        [Description("Mega%20Man")]
        MEGAMAN,

        [Description("Meta%20Knight")]
        METAKNIGHT,

        [Description("Mewtwo")]
        MEWTWO,

        [Description("Mii%20Swordfighter")]
        MIISWORDFIGHTER,

        [Description("Game%20And%20Watch")]
        GAMEANDWATCH,

        [Description("Ness")]
        NESS,

        [Description("Olimar")]
        OLIMAR,

        [Description("PAC-MAN")]
        PACMAN,

        [Description("Palutena")]
        PALUTENA,

        [Description("Peach")]
        PEACH,

        [Description("Pit")]
        PIT,

        [Description("R.O.B/")]
        ROB,

        [Description("Robin")]
        ROBIN,

        [Description("Rosalina%20And%20Luma")]
        ROSALINAANDLUMA,

        [Description("Roy")]
        ROY,

        [Description("Ryu")]
        RYU,

        [Description("Samus")]
        SAMUS,

        [Description("Sheik")]
        SHEIK,

        [Description("Shulk")]
        SHULK,

        [Description("Sonic")]
        SONIC,

        [Description("Toon%20Link")]
        TOONLINK,

        [Description("Wario")]
        WARIO,

        [Description("Wii%20Fit%20Trainer")]
        WIIFITRAINER,

        [Description("Yoshi")]
        YOSHI,

        [Description("Zelda")]
        ZELDA,

        [Description("Zero%20Suit%20Samus")]
        ZEROSUITSAMUS
    }
}
