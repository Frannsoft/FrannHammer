﻿using HtmlAgilityPack;
using Kurogane.Web.Data.Stats;
using System;
using System.Collections.Generic;
using System.IO;

namespace Kurogane.Web.Data
{
    public class Page
    {
        protected string Url { get; private set; }
        private HtmlDocument doc;
        private HtmlWeb web;

        public Page(string url)
        {
            Url = url;
            web = new HtmlWeb();
            doc = web.Load(Url);
        }

        public string GetVersion()
        {
            HtmlNode node = doc.DocumentNode.SelectSingleNode(StatConstants.XPATH_FRAME_DATA_VERSION);

            string[] nameAndVersion = node.InnerText.Split('[');

            string version = string.Empty;

            if (nameAndVersion.Length > 1)
            {
                version = nameAndVersion[1].Replace("]", string.Empty);
            }
            return version; //clean it up
        }

        public Dictionary<string, Stat> GetStats()
        {
            Dictionary<string, Stat> items = new Dictionary<string, Stat>();

            foreach (MovementStat stat in GetStats<MovementStat>(StatConstants.XPATH_TABLE_NODE_MOVEMENT_STATS))
            {
                AddItem(ref items, stat);
                //items.Add(stat.Name, stat);
            }

            foreach (GroundStat stat in GetStats<GroundStat>(StatConstants.XPATH_TABLE_NODE_GROUND_STATS))
            {
                AddItem(ref items, stat);
                //items.Add(stat.Name, stat);
            }

            foreach (AerialStat stat in GetStats<AerialStat>(StatConstants.XPATH_TABLE_NODE_AERIAL_STATS))
            {
                AddItem(ref items, stat);
                //items.Add(stat.Name, stat);
            }

            foreach (SpecialStat stat in GetStats<SpecialStat>(StatConstants.XPATH_TABLE_NODE_SPECIAL_STATS))
            {
                //remove after done writing out class move files
                AddItem(ref items, stat);
                //items.Add(stat.Name, stat);
            }

            return items;
        }

        private void AddItem(ref Dictionary<string, Stat> items, Stat stat)
        {
            try
            {
                items.Add(stat.Name, stat);
            }
            catch (ArgumentException)
            {
                using (TextWriter writer = new StreamWriter(@"E:\char\FULLerrordump.dat", true))
                {
                    writer.WriteLine(Url + " ---- " + stat.Name);
                }
            }
        }

        //internal Dictionary<string, SpecialStat> GetSpecialStats()
        //{
        //    Dictionary<string, SpecialStat> items = new Dictionary<string, SpecialStat>();

        //    foreach (SpecialStat stat in GetStats<SpecialStat>(StatConstants.XPATH_TABLE_NODE_SPECIAL_STATS))
        //    {
        //        try
        //        {
        //            items.Add(stat.Name, stat);
        //        }
        //        catch (ArgumentException)
        //        {
        //            using (TextWriter writer = new StreamWriter(@"E:\char\errordump.dat", true))
        //            {
        //                writer.WriteLine(Url + " ---- " + stat.Name);
        //            }
        //        }
        //    }
        //    return items;
        //}

        private HtmlNodeCollection GetRows(string xpathToTable)
        {
            HtmlNode tableNode = doc.DocumentNode.SelectSingleNode(xpathToTable);
            return tableNode.SelectNodes(StatConstants.XPATH_TABLE_ROWS);
        }

        private string GetStatName(HtmlNode cell)
        {
            string retVal = string.Empty;

            if (!string.IsNullOrEmpty(cell.InnerText))
            {
                retVal = cell.InnerText
                    .Replace(" ", string.Empty)
                    .Replace("/", string.Empty)
                    .Replace("(", string.Empty)
                    .Replace(")", string.Empty)
                    .Replace(",", string.Empty)
                    .Replace("!", string.Empty)
                    .Replace("-", string.Empty)
                    .Replace("&", string.Empty)
                    .Replace("'", string.Empty)
                    .Replace(".", string.Empty)
                    .Replace("͡°͜ʖ͡°", string.Empty)
                    .Replace(":", string.Empty)
                    .Replace("\"", string.Empty)
                    .Trim();
            }

            return retVal;
        }

        private List<T> GetStats<T>(string xpathToTable)
            where T : Stat
        {
            List<T> stats = new List<T>();
            var rows = GetRows(xpathToTable);

            if (typeof(T) == typeof(MovementStat))
            {
                foreach (var row in rows)
                {
                    var statNames = row.SelectNodes(StatConstants.XPATH_TABLE_CELLKEYNAMES);

                    foreach (HtmlNode statName in statNames)
                    {
                        T stat = (T)GetStat<T>(statName);
                        if (stat != null)
                        {
                            stats.Add(stat);
                        }
                    }
                }
            }
            else if (typeof(T) == typeof(GroundStat)
                || typeof(T) == typeof(AerialStat)
                || typeof(T) == typeof(SpecialStat))
            {
                foreach (var row in rows)
                {
                    stats.Add((T)GetStat<T>(row.SelectNodes(StatConstants.XPATH_TABLE_CELLS)));
                }
            }

            return stats;

        }

        private Stat GetStat<T>(HtmlNode nameCell)
           where T : Stat
        {

            if (typeof(T) == typeof(MovementStat))
            {
                return GetMovementStat(nameCell);
            }
            else
            {
                throw new Exception("Unable to determine stat type");
            }
        }

        private Stat GetStat<T>(HtmlNodeCollection cells)
            where T : Stat
        {
            if (typeof(T) == typeof(GroundStat))
            {
                return GetGroundStat(cells);
            }
            else if (typeof(T) == typeof(AerialStat))
            {
                return GetAerialStat(cells);
            }
            else if (typeof(T) == typeof(SpecialStat))
            {
                return GetSpecialStat(cells);
            }
            else
            {
                throw new Exception("Unable to determine stat type");
            }
        }

        private MovementStat GetMovementStat(HtmlNode nameCell)
        {
            MovementStat stat = default(MovementStat);

            string rawNameCellText = nameCell.InnerText;
            if (!string.IsNullOrEmpty(rawNameCellText))
            {
                string name = GetStatName(nameCell);
                var valueCell = nameCell.SelectSingleNode(StatConstants.XPATH_TABLE_CELLVALUES);

                string rawValueText = valueCell.InnerText;
                string value = string.Empty;
                string rank = string.Empty;

                if (rawValueText.Contains("["))
                {
                    string[] checkRank = valueCell.InnerText.Split('[');
                    value = checkRank[0];
                    rank = string.Empty;
                    if (checkRank.Length > 1)
                    {
                        rank = checkRank[1]
                            .Replace("]", string.Empty);
                    }
                }
                else
                {
                    value = rawValueText;
                }

                stat = new MovementStat(name, rawNameCellText, value, rank);
            }

            return stat;
        }

        private GroundStat GetGroundStat(HtmlNodeCollection cells)
        {
            GroundStat stat = default(GroundStat);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                string name = GetStatName(cells[0]);
                string rawName = cells[0].InnerText;
                string hitboxActive = cells[1].InnerText;
                string faf = cells[2].InnerText;
                string basedmg = cells[3].InnerText;
                string angle = cells[4].InnerText;
                string bkbwbkb = cells[5].InnerText;
                string kbg = cells[6].InnerText;

                stat = new GroundStat(name, rawName, hitboxActive, faf, basedmg, angle, bkbwbkb, kbg);
            }

            return stat;
        }

        private AerialStat GetAerialStat(HtmlNodeCollection cells)
        {
            AerialStat stat = default(AerialStat);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                string name = GetStatName(cells[0]);
                string rawName = cells[0].InnerText;
                string hitboxActive = cells[1].InnerText;
                string faf = cells[2].InnerText;
                string basedmg = cells[3].InnerText;
                string angle = cells[4].InnerText;
                string bkbwbkb = cells[5].InnerText;
                string kbg = cells[6].InnerText;
                string landingLag = cells[7].InnerText;
                string autocancel = cells[8].InnerText;

                stat = new AerialStat(name, rawName, hitboxActive, faf, basedmg, angle, bkbwbkb,
                    kbg, landingLag, autocancel);
            }

            return stat;
        }

        private SpecialStat GetSpecialStat(HtmlNodeCollection cells)
        {
            SpecialStat stat = default(SpecialStat);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                string name = GetStatName(cells[0]);
                string rawName = cells[0].InnerText;
                string hitboxActive = cells[1].InnerText;
                string faf = cells[2].InnerText;
                string basedmg = cells[3].InnerText;
                string angle = cells[4].InnerText;
                string bkbwbkb = cells[5].InnerText;
                string kbg = cells[6].InnerText;

                stat = new SpecialStat(name, rawName, hitboxActive, faf, basedmg, angle, bkbwbkb,
                    kbg);
            }

            return stat;
        }
    }
}
