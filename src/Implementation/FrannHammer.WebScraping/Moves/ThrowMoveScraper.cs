using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;
using System;
using System.Linq;

namespace FrannHammer.WebScraping.Moves
{
    public class ThrowMoveScraper : GroundMoveScraper
    {
        public ThrowMoveScraper(IMoveScrapingServices scrapingServices)
            : base(scrapingServices)
        {
            Scrape = character =>
            {
                var moveTableRows = GetTableRows(character.SourceUrl, ScrapingConstants.XPathTableNodeGroundStats);

                //filter out null results since they are not throw data.
                var throws = moveTableRows.Select(row => GetMove(GetTableCells(row), character)).Where(move => move != null);
                return throws;
            };
        }

        protected override IMove GetMove(HtmlNodeCollection cells, WebCharacter character)
        {
            if (!string.IsNullOrEmpty(cells[0].InnerText) && cells.Count > 1)
            {
                string name = GetStatName(cells[0]);

                IMove move = ScrapingServices.CreateMove();

                move.Name = name;
                move.Owner = character.Name;
                move.OwnerId = character.OwnerId;
                move.MoveType = MoveType.Throw.GetEnumDescription();

                if (name.IndexOf(ScrapingConstants.CommonMoveNames.Throw, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (character.SourceUrl.Contains("Smash4")) //this value isn't present in Ultimate values on kh site yet.
                    {
                        bool isWeightDependent = TranslateYesNoToBool(cells[1].InnerText);
                        move.IsWeightDependent = isWeightDependent;

                        string baseDamage = new Smash4BaseDamageResolver().GetRawValue(cells[2]);
                        move.BaseDamage = baseDamage;

                        string angle = cells[3].InnerText;
                        string baseKnockbackSetKnockback = cells[4].InnerText;
                        string knockbackGrowth = cells[5].InnerText;

                        move.Angle = angle;
                        move.BaseKnockBackSetKnockback = baseKnockbackSetKnockback;
                        move.KnockbackGrowth = knockbackGrowth;
                    }
                    else if (character.SourceUrl.Contains("Ultimate"))
                    {
                        string baseDamage = new UltimateBaseDamageResolver().GetRawValue(cells[1]);
                        move.BaseDamage = baseDamage;

                        string angle = cells[2].InnerText;
                        string baseKnockbackSetKnockback = cells[3].InnerText;
                        string knockbackGrowth = cells[4].InnerText;

                        move.Angle = angle;
                        move.BaseKnockBackSetKnockback = baseKnockbackSetKnockback;
                        move.KnockbackGrowth = knockbackGrowth;

                        move.Game = character.Game;
                    }

                    return move;
                }
                else
                {
                    return default(IMove);
                }
            }
            else
            {
                return default(IMove);
            }
        }

        /// <summary>
        /// Parses a 'yes' or 'no' value to true or false respectively.  Will throw for a value that is not
        /// either 'yes' or 'no'.  This check is case-insensitive.
        /// </summary>
        /// <param name="yesNoValue"></param>
        /// <returns></returns>
        private static bool TranslateYesNoToBool(string yesNoValue)
        {
            if (yesNoValue.IndexOf("yes", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
            { return true; }

            if (yesNoValue.IndexOf("no", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
            { return false; }

            throw new ArgumentException($"Value '{yesNoValue}' must be either 'yes' or 'no'.");
        }
    }
}
