using Kurogane.Data.RestApi.Models;
using KuroganeHammer.Data.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KuroganeHammer.WebScraper
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Character
    {
        private readonly Page _page;
        private const string UrlBase = "http://kuroganehammer.com/Smash4/";
        private readonly string _urlTail;

        [JsonProperty]
        public string FullUrl => UrlBase + _urlTail;

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

        public Character(Characters character, string description, string style)
        {
            Description = description;
            Style = style;
            Name = character.ToString();
            _urlTail = CharacterUtility.GetEnumDescription(character);
            OwnerId = (int)character;
            _page = new Page(UrlBase + _urlTail, OwnerId);
            GetData();
        }

        private void GetData()
        {
            FrameDataVersion = _page.GetVersion();
            MainImageUrl = _page.GetImageUrl();
            FrameData = _page.GetStats();
        }
    }
}
