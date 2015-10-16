
using KuroganeHammer.Data.Core.Model.Stats;
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

        public string FrameDataVersion { get; private set; }

        [JsonProperty]
        public MovementMoves MovementMoves { get; set; }

        [JsonProperty]
        public GroundMoves GroundMoves { get; set; }

        [JsonProperty]
        public AerialMoves AerialMoves { get; set; }

        public Character(Characters character)
        {
            Name = character.ToString();
            urlTail = CharacterUtility.GetEnumDescription(character);
            page = new Page(URL_BASE + urlTail);

            GroundMoves = new GroundMoves();
            MovementMoves = new MovementMoves();
            AerialMoves = new AerialMoves();

            GetData();
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
                        serializedContent = JsonConvert.SerializeObject(this.AerialMoves);
                        break;
                    }
                case StatTypes.Ground:
                    {
                        serializedContent = JsonConvert.SerializeObject(this.GroundMoves);
                        break;
                    }
                case StatTypes.Movement:
                    {
                        serializedContent = JsonConvert.SerializeObject(this.MovementMoves);
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

            var characterStatProperties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute<StatProperty>() != null);

            var movementStatProperties = MovementMoves.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute<StatProperty>() != null);

            var groundStatProperties = GroundMoves.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute<StatProperty>() != null);

            var aerialStatProperties = AerialMoves.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute<StatProperty>() != null);

            Dictionary<string, Stat> stats = page.GetStats();

            foreach (PropertyInfo prop in characterStatProperties)
            {
                if (stats.ContainsKey(prop.Name))
                {
                    prop.SetValue(this, stats[prop.Name]);
                }
            }

            foreach (PropertyInfo prop in movementStatProperties)
            {
                if (stats.ContainsKey(prop.Name))
                {
                    prop.SetValue(MovementMoves, stats[prop.Name]);
                }
            }

            foreach (PropertyInfo prop in groundStatProperties)
            {
                if (stats.ContainsKey(prop.Name))
                {
                    prop.SetValue(GroundMoves, stats[prop.Name]);
                }
            }

            foreach (PropertyInfo prop in aerialStatProperties)
            {
                if (stats.ContainsKey(prop.Name))
                {
                    prop.SetValue(AerialMoves, stats[prop.Name]);
                }
            }


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
