using System;
using System.Collections.Generic;
using FrannHammer.Utility;
using System.Linq;

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
        public IList<ParsedMoveDataProperty> MoveProperties { get; }

        public ParsedMoveDataProperty this[string propertyName]
        {
            get
            {
                return
                    MoveProperties.FirstOrDefault(
                        moveProperty =>
                            moveProperty.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        public ParsedMove()
        {
            MoveProperties = new List<ParsedMoveDataProperty>();
        }

        public void AddParsedMoveDataProperty(ParsedMoveDataProperty parsedMoveDataProperty)
        {
            Guard.VerifyObjectNotNull(parsedMoveDataProperty, nameof(parsedMoveDataProperty));
            MoveProperties.Add(parsedMoveDataProperty);
        }
    }
}
