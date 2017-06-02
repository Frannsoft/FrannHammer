using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services.Contracts
{
    /// <summary>
    /// A single column of move data.  E.g., HitboxActive
    /// </summary>
    public class ParsedMoveDataProperty
    {
        public string Name { get; set; }
        public IList<ParsedMoveAttribute> Data { get; }

        public ParsedMoveAttribute this[string attributeName]
        {
            get
            {
                return
                    Data.FirstOrDefault(
                        attribute => attribute.Name.Equals(attributeName, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        public ParsedMoveDataProperty()
        {
            Data = new List<ParsedMoveAttribute>();
        }

        public void AddParsedMoveAttribute(ParsedMoveAttribute parsedMoveAttribute)
        {
            Guard.VerifyObjectNotNull(parsedMoveAttribute, nameof(parsedMoveAttribute));
            Data.Add(parsedMoveAttribute);
        }
    }
}