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

        private IEnumerable<PropertyInfo> characterStatProperties
        {
            get { return GetCharacterProperties(this.GetType()); }
        }

        //private IEnumerable<PropertyInfo> movementStatProperties
        //{
        //    get { return GetCharacterProperties(MovementMoves.GetType()); }
        //}

        //private IEnumerable<PropertyInfo> groundStatProperties
        //{
        //    get { return GetCharacterProperties(GroundMoves.GetType()); }
        //}

        //private IEnumerable<PropertyInfo> aerialStatProperties
        //{
        //    get { return GetCharacterProperties(AerialMoves.GetType()); }
        //}

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

        //[JsonProperty]
        //public MovementMoves MovementMoves { get; set; }

        //[JsonProperty]
        //public GroundMoves GroundMoves { get; set; }

        //[JsonProperty]
        //public AerialMoves AerialMoves { get; set; }

        public Character(Characters character)
        {
            Name = character.ToString();
            urlTail = CharacterUtility.GetEnumDescription(character);
            OwnerId = (int)character;
            page = new Page(URL_BASE + urlTail, OwnerId);


            //GroundMoves = new GroundMoves();
            //MovementMoves = new MovementMoves();
            //AerialMoves = new AerialMoves();

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
            //Sm4shDB db = new Sm4shDB();
            //db.Save()
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

        private IEnumerable<PropertyInfo> GetCharacterProperties(Type type)
        {
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute<StatProperty>() != null);

            return props;
        }

        internal void GetData()
        {
            FrameDataVersion = page.GetVersion();

            FrameData = page.GetStats();


            //foreach (PropertyInfo prop in characterStatProperties)
            //{
            //    if (FrameData.ContainsKey(prop.Name))
            //    {
            //        prop.SetValue(this, FrameData[prop.Name]);
            //    }
            //}

            //foreach (PropertyInfo prop in movementStatProperties)
            //{
            //    if (FrameData.ContainsKey(prop.Name))
            //    {
            //        prop.SetValue(MovementMoves, stats[prop.Name]);
            //    }
            //}

            //foreach (PropertyInfo prop in groundStatProperties)
            //{
            //    if (stats.ContainsKey(prop.Name))
            //    {
            //        prop.SetValue(GroundMoves, stats[prop.Name]);
            //    }
            //}

            //foreach (PropertyInfo prop in aerialStatProperties)
            //{
            //    if (stats.ContainsKey(prop.Name))
            //    {
            //        prop.SetValue(AerialMoves, stats[prop.Name]);
            //    }
            //}


            ////for writing character move names only
            //var moves = page.GetSpecialStats();
            //foreach (SpecialStat stat in moves.Values)
            //{
            //    WritePropertyToFIle(stat.Name);
            //}

        }

        //private void WritePropertyToFIle(string moveName)
        //{
        //    if (!File.Exists(@"E:\char\" + Name + ".dat"))
        //    {
        //        File.Create(@"E:\char\" + Name + ".dat").Close();
        //        File.WriteAllText(@"E:\char\" + Name + ".dat",
        //            "[StatProperty]\n" +
        //            "public SpecialStat " + moveName + " { get; set;}\n\n");
        //    }
        //    else
        //    {
        //        using (StreamWriter writer = File.AppendText(@"E:\char\" + Name + ".dat"))
        //        {
        //            writer.WriteLine(
        //                "[StatProperty]\n" +
        //                "public SpecialStat " + moveName + " { get; set;}\n\n");
        //        }
        //    }
        //}


    }
}
