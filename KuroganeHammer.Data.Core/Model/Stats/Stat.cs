using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Stat
    {
        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public int OwnerId { get; private set; }

        internal string RawName { get; private set; }

        public Stat(string name, int ownerId, string rawName)
        {
            Name = name;
            OwnerId = ownerId;
            RawName = rawName;
        }
    }
}
