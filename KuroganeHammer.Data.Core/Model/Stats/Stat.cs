
using Newtonsoft.Json;
using System;
namespace KuroganeHammer.Data.Core.Model.Stats
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Stat
    {
        [JsonProperty]
        public string Name { get; private set; }

        internal string RawName { get; private set; }

        public Stat(string name, string rawName)
        {
            Name = name;
            RawName = rawName;
        }
    }
}
