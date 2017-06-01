using System.Collections.Generic;

namespace FrannHammer.Api.Services.Contracts
{
    /// <summary>
    /// A fully parsed move data storage container.
    /// </summary>
    public class ParsedMove
    {
        public string MoveName { get; set; }
        public string Owner { get; set; }
        public int OwnerId { get; set; }

        public IList<ParsedMoveDataProperty> MoveData { get; }

        public ParsedMove()
        {
            MoveData = new List<ParsedMoveDataProperty>();
        }
    }

    /// <summary>
    /// A single column of move data.  E.g., HitboxActive
    /// </summary>
    public class ParsedMoveDataProperty
    {
        public string Name { get; set; }
        public IList<ParsedMoveAttribute> Data { get; }

        public ParsedMoveDataProperty()
        {
            Data = new List<ParsedMoveAttribute>();
        }
    }

    /// <summary>
    /// A single field for a parsed move data column. E.g., Hitbox1, Hitbox2, Notes, RawValue, etc.
    /// </summary>
    public class ParsedMoveAttribute
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
