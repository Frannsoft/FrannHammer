using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping.Moves
{
    public class SpecialMoveScraper : BaseMoveScraper
    {
        public sealed override Func<string, IEnumerable<IMove>> Scrape { get; protected set; }

        public SpecialMoveScraper(IMoveScrapingServices scrapingServices)
            : base(scrapingServices)
        {
            Scrape = url =>
            {
                var moveTableRows = GetTableRows(url, ScrapingXPathConstants.XPathTableNodeSpecialStats);
                return moveTableRows.Select(row => GetMove(GetTableCells(row)));
            };
        }

        protected override IMove GetMove(HtmlNodeCollection cells)
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
                move.MoveType = MoveType.Special;
            }

            return move;
        }
    }
}