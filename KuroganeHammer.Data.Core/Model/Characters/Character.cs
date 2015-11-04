using KuroganeHammer.Data.Core.Model.Stats;
using KuroganeHammer.Data.Core.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KuroganeHammer.Data.Core.Model.Characters
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
        public string Name { get; private set; }

        [JsonProperty]
        public int OwnerId { get; set; }

        public string FrameDataVersion { get; private set; }

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

        [Obsolete]
        public string AsJson(StatTypes statType = StatTypes.All)
        {
            string serializedContent = string.Empty;

            switch (statType)
            {
                case StatTypes.All:
                    {
                        serializedContent = JsonConvert.SerializeObject(this);
                        break;
                    }
                case StatTypes.Aerial:
                    {
                        var aerialStats = (from data in FrameData.Values
                                           where data.GetType() == typeof(AerialStat)
                                           select data).ToList();
                        serializedContent = JsonConvert.SerializeObject(aerialStats);
                        break;
                    }
                case StatTypes.Ground:
                    {
                        var groundStats = (from data in FrameData.Values
                                           where data.GetType() == typeof(AerialStat)
                                           select data).ToList();
                        serializedContent = JsonConvert.SerializeObject(groundStats);
                        break;
                    }
                case StatTypes.Movement:
                    {
                        var movementStats = (from data in FrameData.Values
                                             where data.GetType() == typeof(MovementStat)
                                             select data).ToList();
                        serializedContent = JsonConvert.SerializeObject(movementStats);
                        break;
                    }
                case StatTypes.Special:
                    {
                        var specialStats = (from data in FrameData.Values
                                            where data.GetType() == typeof(SpecialStat)
                                            select data).ToList();
                        serializedContent = JsonConvert.SerializeObject(specialStats);
                        break;
                    }
                default:
                    {
                        serializedContent = JsonConvert.SerializeObject(
                            new
                            {
                                Name = Name,
                                FullUrl = FullUrl,
                                OwnerId = OwnerId,
                                FrameDataVersion = FrameDataVersion
                            });
                        break;
                    }
            }
            return serializedContent;
        }

        private void GetData()
        {
            FrameDataVersion = page.GetVersion();
            FrameData = page.GetStats();
        }
    }
}
