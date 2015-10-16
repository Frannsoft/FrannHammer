
using System;
namespace KuroganeHammer.Data.Core.Model.Stats
{
    public class Stat
    {
        public string Name { get; private set; }
        internal string RawName { get; private set; }

        public Stat(string name, string rawName)
        {
            Name = name;
            RawName = rawName;
        }
    }
}
