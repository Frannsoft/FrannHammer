using System;

namespace KuroganeHammer.Data.Core.Model.Stats.dbentity
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class TableId : Attribute
    {
        internal string Value { get; private set; }

        public TableId(string value)
        {
            this.Value = value;
        }
    }
}
