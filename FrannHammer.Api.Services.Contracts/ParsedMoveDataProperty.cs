using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services.Contracts
{
    /// <summary>
    /// A single column of move data.  E.g., HitboxActive
    /// </summary>
    [DebuggerDisplay("Name = {Name}")]
    public class ParsedMoveDataProperty
    {
        public string Name { get; set; }
        public IList<ParsedMoveAttribute> MoveAttributes { get; }

        public ParsedMoveAttribute this[string attributeName]
        {
            get
            {
                return
                    MoveAttributes.FirstOrDefault(
                        attribute => attribute.Name.Equals(attributeName, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        public ParsedMoveDataProperty()
        {
            MoveAttributes = new List<ParsedMoveAttribute>();
        }

        public void AddParsedMoveAttribute(ParsedMoveAttribute parsedMoveAttribute)
        {
            Guard.VerifyObjectNotNull(parsedMoveAttribute, nameof(parsedMoveAttribute));
            MoveAttributes.Add(parsedMoveAttribute);
        }
    }
}