using KuroganeHammer.Data.Core;
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KuroganeHammer.WebScraper
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Character
    {
        private Page page;
        private const string URL_BASE = "http://kuroganehammer.com/Smash4/";
        private readonly string urlTail;

        [JsonProperty]
        public string FullUrl
        {
            get { return URL_BASE + urlTail; }
        }

        [JsonProperty]
        public string MainImageUrl { get; private set; }

        [JsonProperty]
        public string ThumbnailUrl { get; set; }

        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public int OwnerId { get; set; }

        public string Description { get; private set; }
        public string FrameDataVersion { get; private set; }
        public string Style { get; private set; }

        [JsonProperty]
        public Dictionary<string, Stat> FrameData { get; private set; }

        public Character(Characters character)
        {
            Name = character.ToString();
            urlTail = CharacterUtility.GetEnumDescription(character);
            OwnerId = (int)character;
            page = new Page(URL_BASE + urlTail, OwnerId);
            GetData();
        }

        public static Character FromId(int id)
        {
            return new Character((Characters)id);
        }

        private void GetData()
        {
            FrameDataVersion = page.GetVersion();
            MainImageUrl = page.GetImageUrl();
            FrameData = page.GetStats();
        }
    }
}
