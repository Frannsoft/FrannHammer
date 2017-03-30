using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using HtmlAgilityPack;
using System.Linq;

namespace FrannHammer.WebScraping
{
    public class MoveScrapingService : IMoveScrapingService
    {
        private readonly IHtmlParser _htmlParser;
        private readonly IMoveProvider _moveProvider;

        public MoveScrapingService(IHtmlParser htmlParser, IMoveProvider moveProvider)
        {
            Guard.VerifyObjectNotNull(htmlParser, nameof(htmlParser));
            Guard.VerifyObjectNotNull(moveProvider, nameof(moveProvider));

            _htmlParser = htmlParser;
            _moveProvider = moveProvider;
        }

        public IEnumerable<IMove> GetGroundMoves(string xpath)
        {
            var moveTableRows = GetTableRows(xpath);
            return moveTableRows.Select(row => GetGroundMove(GetTableCells(row)));
        }

        public IEnumerable<IMove> GetAerialMoves(string xpath)
        {
            var moveTableRows = GetTableRows(xpath);
            return moveTableRows.Select(row => GetAerialMove(GetTableCells(row)));
        }

        public IEnumerable<IMove> GetSpecialMoves(string xpath)
        {
            var moveTableRows = GetTableRows(xpath);
            return moveTableRows.Select(row => GetSpecialMove(GetTableCells(row)));
        }

        private HtmlNodeCollection GetTableCells(HtmlNode row) => row.SelectNodes(ScrapingXPathConstants.XPathTableCells);

        private HtmlNodeCollection GetTableRows(string xpath)
        {
            //get moves table html
            var movesTableHtml = _htmlParser.GetSingle(xpath);

            var moveTableRows = HtmlNode.CreateNode(movesTableHtml)?.SelectNodes(ScrapingXPathConstants.XPathTableRows);

            if (moveTableRows == null)
            { throw new Exception($"Error getting move table data after attempting to scrape full table using xpath: '{xpath};"); }

            return moveTableRows;
        }

        private IMove GetGroundMove(HtmlNodeCollection cells)
        {
            var move = default(IMove);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                var name = GetStatName(cells[0]);
                var hitboxActive = cells[1].InnerText;
                var faf = cells[2].InnerText;
                var basedmg = cells[3].InnerText;
                var angle = cells[4].InnerText;
                var bkbwbkb = cells[5].InnerText;
                var kbg = cells[6].InnerText;

                move = _moveProvider.Create();

                move.Name = name;
                move.Angle = angle;
                move.BaseDamage = basedmg;
                move.BaseKnockbackSetKnockback = bkbwbkb;
                move.FirstActionableFrame = faf;
                move.HitboxActive = hitboxActive;
                move.KnockbackGrowth = kbg;
                move.MoveType = MoveType.Ground;
            }

            return move;
        }

        private IMove GetAerialMove(HtmlNodeCollection cells)
        {
            var move = default(IMove);

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

                move = _moveProvider.Create();

                move.Name = name;
                move.Angle = angle;
                move.BaseDamage = basedmg;
                move.BaseKnockbackSetKnockback = bkbwbkb;
                move.FirstActionableFrame = faf;
                move.HitboxActive = hitboxActive;
                move.KnockbackGrowth = kbg;
                move.LandingLag = landingLag;
                move.AutoCancel = autocancel;
                move.MoveType = MoveType.Aerial;
            }

            return move;
        }

        private IMove GetSpecialMove(HtmlNodeCollection cells)
        {
            var move = default(IMove);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                var name = GetStatName(cells[0]);
                var hitboxActive = cells[1].InnerText;
                var faf = cells[2].InnerText;
                var basedmg = cells[3].InnerText;
                var angle = cells[4].InnerText;
                var bkbwbkb = cells[5].InnerText;
                var kbg = cells[6].InnerText;

                move = _moveProvider.Create();

                move.Name = name;
                move.Angle = angle;
                move.BaseDamage = basedmg;
                move.BaseKnockbackSetKnockback = bkbwbkb;
                move.FirstActionableFrame = faf;
                move.HitboxActive = hitboxActive;
                move.KnockbackGrowth = kbg;
                move.MoveType = MoveType.Special;
            }

            return move;
        }

        private static string GetStatName(HtmlNode cell)
        {
            var retVal = string.Empty;

            if (!string.IsNullOrEmpty(cell.InnerText))
            {
                retVal = cell.InnerText.Trim();
            }

            return retVal;
        }
    }
}
