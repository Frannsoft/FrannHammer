
using Kurogane.Web.Data.Stats;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kurogane.Web.Data.Characters
{
    public class Character
    {
        private const string URL_BASE = "http://kuroganehammer.com/Smash4/";
        private readonly string urlTail;
        private Page page;

        public string Name { get; private set; }
        public string FrameDataVersion { get; private set; }
        public MovementMoves MovementMoves { get; set; }
        public GroundMoves GroundMoves { get; set; }
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
