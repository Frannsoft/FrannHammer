using System.ComponentModel;

namespace FrannHammer.Domain.Contracts
{
    public enum MoveType
    {
        [Description("aerial")]
        Aerial,

        [Description("ground")]
        Ground,

        [Description("special")]
        Special,

        [Description("throw")]
        Throw
    }
}
