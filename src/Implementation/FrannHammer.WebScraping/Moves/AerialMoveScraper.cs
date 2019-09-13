using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.WebScraping.Moves
{
    public class AerialMoveScraper : BaseMoveScraper
    {
        public sealed override Func<WebCharacter, IEnumerable<IMove>> Scrape { get; protected set; }

        public AerialMoveScraper(IMoveScrapingServices scrapingServices)
            : base(scrapingServices)
        {
            Scrape = character =>
            {
                var moveTableRows = GetTableRows(character.SourceUrl, ScrapingConstants.XPathTableNodeAerialStats);
                return moveTableRows.Select(row => GetMove(GetTableCells(row), character));
            };
        }

        protected override IMove GetMove(HtmlNodeCollection cells, WebCharacter character)
        {
            var move = default(IMove);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                string name = GetStatName(cells[0]);
                string faf = cells[2].InnerText;
                string angle = cells[4].InnerText;
                string bkbwbkb = cells[5].InnerText;
                string kbg = cells[6].InnerText;

                string landingLag = cells[7].InnerText;

                var autocancel = new CommonAutocancelParameterResolver().Resolve(cells[8]);

                move = ScrapingServices.CreateMove();

                move = new CommonMoveParameterResolver(character.Game).Resolve(cells, move);

                move.Name = name;
                move.Angle = angle;
                move.BaseKnockBackSetKnockback = bkbwbkb;
                move.FirstActionableFrame = faf;
                move.KnockbackGrowth = kbg;
                move.LandingLag = landingLag;
                move.AutoCancel = autocancel;
                move.MoveType = MoveType.Aerial.GetEnumDescription();
                move.Owner = character.Name;
                move.OwnerId = character.OwnerId;
            }

            return move;
        }
    }
}