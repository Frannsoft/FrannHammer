using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;

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
                return moveTableRows.Select(row => GetMove(GetTableCells(row), character.Name));
            };
        }

        protected override HtmlNodeCollection GetTableRows(string sourceUrl, string xpath)
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

                return tableRows;
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

                return tableRows;
            }
        }

        protected override IMove GetMove(HtmlNodeCollection cells, string characterName)
        {
            var move = default(IMove);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                string name = GetStatName(cells[0]);
                string hitboxActive = cells[1].InnerText;
                string faf = cells[2].InnerText;
                string basedmg = cells[3].InnerText;
                string angle = cells[4].InnerText;
                string bkbwbkb = cells[5].InnerText;
                string kbg = cells[6].InnerText;

                move = ScrapingServices.CreateMove();

                move.Name = name;
                move.Angle = angle;
                move.BaseDamage = basedmg;
                move.BaseKnockBackSetKnockback = bkbwbkb;
                move.FirstActionableFrame = faf;
                move.HitboxActive = hitboxActive;
                move.KnockbackGrowth = kbg;
                move.MoveType = MoveType.Ground.GetEnumDescription();
                move.Owner = characterName;
            }

            return move;
        }
    }
}
