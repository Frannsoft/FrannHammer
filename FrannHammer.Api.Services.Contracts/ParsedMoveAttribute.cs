using System.Diagnostics;

namespace FrannHammer.Api.Services.Contracts
{
    /// <summary>
    /// A single field for a parsed move data column. E.g., Hitbox1, Hitbox2, Notes, RawValue, etc.
    /// </summary>
    [DebuggerDisplay("Name = {Name}")]
    public class ParsedMoveAttribute
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}