using System.Collections.Generic;

namespace KuroganeHammer.WebScraper
{
    /// <summary>
    /// Holds a List of AttributeValueRows.  This is designed
    /// to hold a page of attributes for all characters (e.g., all air speeds).
    /// </summary>
    public class AttributeValueRowCollection
    {
        public List<AttributeValueRow> AttributeValues { get; private set; }

        public AttributeValueRowCollection(List<AttributeValueRow> values)
        {
            AttributeValues = values;
        }
    }

    /// <summary>
    /// Holds a specific cell value of an attribute (e.g., Rank, 1)
    /// </summary>
    public class AttributeValue
    {
        public readonly string Name;
        public readonly string Value;
        public string AttributeFlag { get; }

        public AttributeValue(string name, string value, string flag)
        {
            Name = name;
            Value = value;
            AttributeFlag = flag;
        }
    }

    /// <summary>
    /// Holds a row of attribute values (e.g., Rank (1), Character (Pikachu), Value (1))
    /// </summary>
    public class AttributeValueRow
    {
        public List<AttributeValue> Values { get; private set; }

        public AttributeValueRow(List<AttributeValue> values)
        {
            Values = values;
        }
    }

}
