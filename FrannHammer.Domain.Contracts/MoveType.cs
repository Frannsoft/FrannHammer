using System.ComponentModel;

namespace FrannHammer.Domain.Contracts
{
    public enum MoveType
    {
        [Description("Aerial")]
        Aerial,

        [Description("Ground")]
        Ground,

        [Description("Special")]
        Special
    }
}
