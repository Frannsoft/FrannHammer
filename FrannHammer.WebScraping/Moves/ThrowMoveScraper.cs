using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping.Moves
{
    public class ThrowMoveScraper : BaseMoveScraper
    {
        public sealed override Func<WebCharacter, IEnumerable<IMove>> Scrape { get; protected set; }

        public ThrowMoveScraper(IMoveScrapingServices scrapingServices)
            : base(scrapingServices)
        {
            Scrape = character =>
            {
                var moveTableRows = GetTableRows(character.SourceUrl, ScrapingConstants.XPathTableNodeGroundStats);
                return moveTableRows.Select(row => GetMove(GetTableCells(row), character.Name));
            };
        }

        protected override IMove GetMove(HtmlNodeCollection cells, string characterName)
        {
            var move = default(IMove);

            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                string name = GetStatName(cells[0]);

                move = ScrapingServices.CreateMove();

                move.Name = name;
                move.MoveType = MoveType.Throw.GetEnumDescription();

                //grab data is different than 'throw' data.  If the cell contains neither of these this scraper
                //will ignore it
                if (name.EndsWith("grab", StringComparison.CurrentCultureIgnoreCase))
                {
                    string hitboxActive = cells[1].InnerText;
                    string faf = cells[2].InnerText;

                    move.HitboxActive = hitboxActive;
                    move.FirstActionableFrame = faf;
                }
                else if (name.EndsWith("throw", StringComparison.CurrentCultureIgnoreCase))
                {
                    bool isWeightDependent = TranslateYesNoToBool(cells[1].InnerText); //throw here because we don't want to store bad data
                    string baseDamage = cells[2].InnerText;
                    string angle = cells[3].InnerText;
                    string baseKnockbackSetKnockback = cells[4].InnerText;
                    string knockbackGrowth = cells[5].InnerText;

                    move.IsWeightDependent = isWeightDependent;
                    move.BaseDamage = baseDamage;
                    move.Angle = angle;
                    move.BaseKnockBackSetKnockback = baseKnockbackSetKnockback;
                    move.KnockbackGrowth = knockbackGrowth;
                }
            }

            return move;
        }

        /// <summary>
        /// Parses a 'yes' or 'no' value to true or false respectively.  Will throw for a value that is not
        /// either 'yes' or 'no'.  This check is case-insensitive.
        /// </summary>
        /// <param name="yesNoValue"></param>
        /// <returns></returns>
        private static bool TranslateYesNoToBool(string yesNoValue)
        {
            if (yesNoValue.Equals("yes", StringComparison.CurrentCultureIgnoreCase))
            { return true; }

            if (yesNoValue.Equals("no", StringComparison.CurrentCultureIgnoreCase))
            { return false; }

            throw new ArgumentException($"Value '{yesNoValue}' must be either 'yes' or 'no'.");
        }
    }
}
