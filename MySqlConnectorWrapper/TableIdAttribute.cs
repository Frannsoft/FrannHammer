using System;

namespace MySqlConnectorWrapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableId : Attribute
    {
        internal string Value { get; private set; }

        public TableId(string value)
        {
            this.Value = value;
        }
    }
}
