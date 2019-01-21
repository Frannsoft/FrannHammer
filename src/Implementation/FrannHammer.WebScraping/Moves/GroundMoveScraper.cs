using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.WebScraping.Moves
{
    public class GroundMoveScraper : BaseMoveScraper
    {
        public sealed override Func<WebCharacter, IEnumerable<IMove>> Scrape { get; protected set; }

        public GroundMoveScraper(IMoveScrapingServices scrapingServices)
            : base(scrapingServices)
        {
            Scrape = character =>
            {
                var moveTableRows = GetTableRows(character.SourceUrl, ScrapingConstants.XPathTableNodeGroundStats);

                //filter out null results.  The below scraping logic returns null for values that do not meet the criteria
                //for a ground move.
                return moveTableRows.Select(row => GetMove(GetTableCells(row), character, character.SourceUrl)).Where(move => move != null);
            };
        }

        protected override IEnumerable<HtmlNode> GetTableRows(string sourceUrl, string xpath)
        {
            const string exceptionMessageBase = "Error getting move table data after attempting to scrape full table using xpath: ";
            var htmlParser = ScrapingServices.CreateParserFromSourceUrl(sourceUrl);

            //account for extra info tables having the same id
            if (htmlParser.GetCollection(ScrapingConstants.XPathTableNodeGroundStats + "//thead/tr/*").Count() > 4)
            {
                string movesTableHtml = htmlParser.GetSingle(xpath);

                var movesTableHtmlNode = HtmlNode.CreateNode(movesTableHtml);

                //get row data
                var tableRows = movesTableHtmlNode.SelectNodes(ScrapingConstants.XPathTableRows);


                if (tableRows == null)
                {
                    throw new Exception(
                        $"{exceptionMessageBase}'{ScrapingConstants.XPathTableRows};");
                }

                return FilterHeadersFromFoundTableRows(tableRows);
            }
            else
            {
                string movesTableHtml = htmlParser.GetSingle(ScrapingConstants.XPathTableNodeGroundStatsAdjusted);

                var movesTableHtmlNode = HtmlNode.CreateNode(movesTableHtml);

                var tableRows = movesTableHtmlNode.SelectNodes(ScrapingConstants.XPathTableRows);

                if (tableRows == null)
                {
                    throw new Exception(
                        $"{exceptionMessageBase}'{ScrapingConstants.XPathTableRows};");
                }

                return FilterHeadersFromFoundTableRows(tableRows);
            }
        }

        private static IEnumerable<HtmlNode> FilterHeadersFromFoundTableRows(IEnumerable<HtmlNode> tableRows)
        {
            return tableRows.Where(node =>
            {
                var firstHeaderNode = node.SelectSingleNode("th");

                return
                    !firstHeaderNode.InnerText.Equals(ScrapingConstants.ExcludedRowHeaders.Grabs,
                        StringComparison.OrdinalIgnoreCase) &&
                    !firstHeaderNode.InnerText.Equals(ScrapingConstants.ExcludedRowHeaders.Throws,
                        StringComparison.OrdinalIgnoreCase) &&
                    !firstHeaderNode.InnerText.Equals(ScrapingConstants.ExcludedRowHeaders.Miscellaneous,
                        StringComparison.OrdinalIgnoreCase);
            });
        }

        protected override IMove GetMove(HtmlNodeCollection cells, WebCharacter character, string sourceUrl)
        {
            var move = default(IMove);

            string name = GetStatName(cells[0]);

            //a throw move is not a ground move
            if (name.IndexOf(ScrapingConstants.CommonMoveNames.Throw, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return default(IMove);
            }

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                move = ScrapingServices.CreateMove();

                string hitboxActive = cells[1].InnerText;
                string faf = cells[2].InnerText;

                if (sourceUrl.Contains("Smash4"))
                {
                    string basedmg = cells[3].InnerText;
                    move.BaseDamage = basedmg;
                }
                else if (sourceUrl.Contains("Ultimate"))
                {
                    string basedmg = string.Empty;
                    var baseDmgCell = cells[3];//<div class="tooltip">11<span class="tooltiptext">1v1: 13.2</span></div>
                    var normalBaseDamageNode = baseDmgCell.SelectSingleNode("./div[@class = 'tooltip']");
                    if (normalBaseDamageNode != null)
                    {
                        string normalBaseDamage = normalBaseDamageNode.FirstChild.InnerText;

                        string oneVoneBaseDamage = normalBaseDamageNode.LastChild.InnerText;
                        basedmg = normalBaseDamage + oneVoneBaseDamage;
                    }
                    else
                    {
                        basedmg = cells[3].InnerText;
                    }
                    move.Game = Games.Ultimate;
                    move.BaseDamage = basedmg;
                }

                string angle = cells[4].InnerText;
                string bkbwbkb = cells[5].InnerText;
                string kbg = cells[6].InnerText;

                move.Name = name;

                move.Angle = angle;
                move.BaseKnockBackSetKnockback = bkbwbkb;
                move.FirstActionableFrame = faf;
                move.HitboxActive = hitboxActive;
                move.KnockbackGrowth = kbg;
                move.MoveType = MoveType.Ground.GetEnumDescription();
                move.Owner = character.Name;
                move.OwnerId = character.OwnerId;
            }

            return move;
        }
    }
}
