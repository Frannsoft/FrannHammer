using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KuroganeHammer.Data.Core;

namespace KuroganeHammer.WebScraper
{
    public class Page
    {
        protected string Url { get; }
        private readonly HtmlDocument _doc;
        private readonly int _ownerId;
        private readonly string _urlTail;

        public Page(string url, int ownerId)
        {
            Url = url;
            _urlTail = GetUrlTail();
            var web = new HtmlWeb();
            _doc = web.Load(Url);
            _ownerId = ownerId;
        }

        public Page(string url)
        {
            Url = url;
            _urlTail = GetUrlTail();
            var web = new HtmlWeb();
            _doc = web.Load(Url);
        }

        public string GetVersion()
        {
            var node = _doc.DocumentNode.SelectSingleNode(StatConstants.XpathFrameDataVersion);

            var nameAndVersion = node.InnerText.Split('[');

            var version = string.Empty;

            if (nameAndVersion.Length > 1)
            {
                version = nameAndVersion[1].Replace("]", string.Empty);
            }
            return version; //clean it up
        }

        public string GetImageUrl()
        {
            var node = _doc.DocumentNode.SelectSingleNode(StatConstants.XpathImageUrl);
            return node.Attributes["src"].Value;
        }

        public Dictionary<string, Stat> GetStats()
        {
            var items = new Dictionary<string, Stat>();

            foreach (var stat in GetStats<MovementStat>(StatConstants.XpathTableNodeMovementStats))
            {
                AddItem(ref items, stat);
            }

            foreach (var stat in GetStats<GroundStat>(StatConstants.XpathTableNodeGroundStats))
            {
                AddItem(ref items, stat);
            }

            foreach (var stat in GetStats<AerialStat>(StatConstants.XpathTableNodeAerialStats))
            {
                AddItem(ref items, stat);
            }

            foreach (var stat in GetStats<SpecialStat>(StatConstants.XpathTableNodeSpecialStats))
            {
                //remove after done writing out class move files
                AddItem(ref items, stat);
            }

            return items;
        }

        /// <summary>
        /// Get the character attributes.
        /// </summary>
        /// <returns></returns>
        public AttributeValueRowCollection GetAttributes()
        {
            var rows = GetRows(StatConstants.XpathTableNodeAttributesWithDescription) ??
                       GetRows(StatConstants.XpathTableNodeAttributesWithNoDescription);

            var headers = GetHeaders();
            var attributesRows = new List<AttributeValueRow>();

            foreach (var t in rows)
            {
                var cells = t.SelectNodes(StatConstants.XpathTableCells);

                var attributes = cells.Select((t1, k) =>
                    new AttributeValue(headers[k], t1.InnerText, _urlTail))
                    .ToList();
                attributesRows.Add(new AttributeValueRow(attributes));
            }

            var attrValueCollection = new AttributeValueRowCollection(attributesRows);

            return attrValueCollection;
        }

        private string GetUrlTail()
        {
            var tail = Url.Split('/').Last(); //get the key off of the url tail
            return tail;
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

        private HtmlNodeCollection GetRows(string xpathToTable)
        {
            var tableNode = _doc.DocumentNode.SelectSingleNode(xpathToTable);
            return tableNode?.SelectNodes(StatConstants.XpathTableRows);
        }

        /// <summary>
        /// Designed to get the headers on tables where necessary.  Right now, it's only
        /// necessary when getting character attributes.
        /// </summary>
        /// <returns></returns>
        private List<string> GetHeaders()
        {
            var rawHeaders = _doc.DocumentNode.SelectNodes(StatConstants.XpathTableNodeAttributeHeaders) ??
                             _doc.DocumentNode.SelectNodes(StatConstants.XpathTableNodeAttributeHeadersWithNoDescription);
            var headers = (from row in rawHeaders
                           from cell in row.SelectNodes(StatConstants.XpathTableCells)
                           select cell.InnerText).ToList();

            return headers;
        }

        private string GetStatName(HtmlNode cell)
        {
            var retVal = string.Empty;

            if (!string.IsNullOrEmpty(cell.InnerText))
            {
                retVal = cell.InnerText
                    //.Replace(" ", string.Empty)
                    //.Replace("/", string.Empty)
                    //.Replace("(", string.Empty)
                    //.Replace(")", string.Empty)
                    //.Replace(",", string.Empty)
                    //.Replace("!", string.Empty)
                    //.Replace("-", string.Empty)
                    //.Replace("&", string.Empty)
                    //.Replace("'", string.Empty)
                    //.Replace(".", string.Empty)
                    //.Replace("͡°͜ʖ͡°", string.Empty)
                    //.Replace(":", string.Empty)
                    //.Replace("\"", string.Empty)
                    .Trim();
            }

            return retVal;
        }

        private List<T> GetStats<T>(string xpathToTable)
            where T : Stat
        {
            var stats = new List<T>();
            var rows = GetRows(xpathToTable);

            if (typeof(T) == typeof(MovementStat))
            {
                stats.AddRange(rows.SelectMany(row => row.SelectNodes(StatConstants.XpathTableCellkeynames),
                    (row, statName) => (T)GetStat<T>(statName)).Where(stat => stat != null));
            }
            else if (typeof(T) == typeof(GroundStat)
                || typeof(T) == typeof(AerialStat)
                || typeof(T) == typeof(SpecialStat))
            {
                stats.AddRange(rows.Select(row => (T)GetStat<T>(row.SelectNodes(StatConstants.XpathTableCells))));
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
            var stat = default(MovementStat);

            var rawNameCellText = nameCell.InnerText;
            if (!string.IsNullOrEmpty(rawNameCellText))
            {
                var name = GetStatName(nameCell);
                var valueCell = nameCell.SelectSingleNode(StatConstants.XpathTableCellvalues);

                var rawValueText = valueCell.InnerText;//.Replace(" frames", string.Empty);
                string value;

                if (rawValueText.Contains("["))
                {
                    var checkRank = valueCell.InnerText.Split('[');
                    value = checkRank[0];
                }
                else
                {
                    value = rawValueText;
                }

                stat = new MovementStat(name, _ownerId, value);
            }

            return stat;
        }

        private GroundStat GetGroundStat(HtmlNodeCollection cells)
        {
            var stat = default(GroundStat);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                var name = GetStatName(cells[0]);
                var hitboxActive = cells[1].InnerText;
                var faf = cells[2].InnerText;
                var basedmg = cells[3].InnerText;
                var angle = cells[4].InnerText;
                var bkbwbkb = cells[5].InnerText;
                var kbg = cells[6].InnerText;

                stat = new GroundStat(name, _ownerId, hitboxActive, faf, basedmg, angle, bkbwbkb, kbg);
            }

            return stat;
        }

        private AerialStat GetAerialStat(HtmlNodeCollection cells)
        {
            var stat = default(AerialStat);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                var name = GetStatName(cells[0]);
                var hitboxActive = cells[1].InnerText;
                var faf = cells[2].InnerText;
                var basedmg = cells[3].InnerText;
                var angle = cells[4].InnerText;
                var bkbwbkb = cells[5].InnerText;
                var kbg = cells[6].InnerText;
                var landingLag = cells[7].InnerText;
                var autocancel = cells[8].InnerText;

                stat = new AerialStat(name, _ownerId, hitboxActive, faf, basedmg, angle, bkbwbkb,
                    kbg, landingLag, autocancel);
            }

            return stat;
        }

        private SpecialStat GetSpecialStat(HtmlNodeCollection cells)
        {
            var stat = default(SpecialStat);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                var name = GetStatName(cells[0]);
                var hitboxActive = cells[1].InnerText;
                var faf = cells[2].InnerText;
                var basedmg = cells[3].InnerText;
                var angle = cells[4].InnerText;
                var bkbwbkb = cells[5].InnerText;
                var kbg = cells[6].InnerText;

                stat = new SpecialStat(name, _ownerId, hitboxActive, faf, basedmg, angle, bkbwbkb,
                    kbg);
            }

            return stat;
        }
    }
}
