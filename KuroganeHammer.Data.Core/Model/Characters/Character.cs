using KuroganeHammer.Data.Core.D;
using KuroganeHammer.Data.Core.Model.Stats;
using KuroganeHammer.Data.Core.Model.Stats.dbentity;
using KuroganeHammer.Data.Core.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KuroganeHammer.Data.Core.Model.Characters
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Character
    {
        private const string URL_BASE = "http://kuroganehammer.com/Smash4/";
        private readonly string urlTail;

        [JsonProperty]
        private string fullurl
        {
            get { return URL_BASE + urlTail; }
        }
        private Page page;

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

        /// <summary>
        /// Actually saves the values to the Sm4sh database.
        /// </summary>
        public void SaveCharacterToDatabase()
        {
            var movementStats = ConvertStats<MovementStat, MovementStatDB>(FrameData);
            var grountStats = ConvertStats<GroundStat, GroundStatDB>(FrameData);
            var aerialStats = ConvertStats<AerialStat, AerialStatDB>(FrameData);
            var specialStats = ConvertStats<SpecialStat, SpecialStatDB>(FrameData);
            CharacterDB charData = new CharacterDB(Name, OwnerId, fullurl);

            using (Sm4shDB db = new Sm4shDB())
            {
                //db.Save<MovementStatDB>(movementStats);
                db.Save<CharacterDB>(charData);
            }
        }

        private List<TOut> ConvertStats<Tin, TOut>(Dictionary<string, Stat> items)
            where Tin : Stat
            where TOut : StatDB
        {
            var convertedItemsList = (from item in items.Values
                                      where item.GetType() == typeof(Tin)
                                      select EntityBusinessConverter<Tin>.ConvertTo<TOut>((Tin)item))
                       .ToList();

            return convertedItemsList;
        }

        public string AsJson<T>(StatTypes statType = StatTypes.All)
            where T : Character
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
                        //serializedContent = JsonConvert.SerializeObject(this.GroundMoves);
                        break;
                    }
                case StatTypes.Movement:
                    {
                        //serializedContent = JsonConvert.SerializeObject(this.MovementMoves);
                        break;
                    }
                case StatTypes.Special:
                    {
                        var convertChar = Convert.ChangeType(this, typeof(T));
                        serializedContent = JsonConvert.SerializeObject(convertChar);
                        break;
                    }
            }
            return serializedContent;
        }

        internal void GetData()
        {
            FrameDataVersion = page.GetVersion();
            FrameData = page.GetStats();
        }
    }
}
