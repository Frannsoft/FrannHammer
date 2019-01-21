using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.WebScraping.Moves
{
    public class SpecialMoveScraper : BaseMoveScraper
    {
        public sealed override Func<WebCharacter, IEnumerable<IMove>> Scrape { get; protected set; }

        public SpecialMoveScraper(IMoveScrapingServices scrapingServices)
            : base(scrapingServices)
        {
            Scrape = character =>
            {
                var moveTableRows = GetTableRows(character.SourceUrl, ScrapingConstants.XPathTableNodeSpecialStats);
                return moveTableRows.Select(row => GetMove(GetTableCells(row), character));
            };
        }

        protected override IMove GetMove(HtmlNodeCollection cells, WebCharacter character, string sourceUrl = "")
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
                move.MoveType = MoveType.Special.GetEnumDescription();
                move.Owner = character.Name;
                move.OwnerId = character.OwnerId;
                move.Game = character.SourceUrl.Contains("Ultimate") ? Games.Ultimate : Games.Smash4;
            }

            return move;
        }
    }
}