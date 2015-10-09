
using System;
namespace Kurogane.Web.Data.Stats
{
    public class Stat
    {
        internal string Name { get; private set; }
        internal string RawName { get; private set; }

        public Stat(string name, string rawName)
        {
            Name = name;
            RawName = rawName;
        }
    }
}
