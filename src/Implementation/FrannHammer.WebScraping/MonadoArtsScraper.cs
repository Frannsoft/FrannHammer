using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.UniqueData;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping
{
    public class MonadoArtsScraper : IUniqueDataScraper
    {
        private const string MonadoArtsAttributeTableXPath = @"(//*/table[@id='AutoNumber3'])[2]";
        private const string TimesSymbolEncoded = "&#215;";
        private const string PercentSymbolEncoded = "&#37;";


        private readonly IUniqueDataScrapingServices _uniqueDataScrapingServices;

        public MonadoArtsScraper(IUniqueDataScrapingServices uniqueDataScrapingServices)
        {
            Guard.VerifyObjectNotNull(uniqueDataScrapingServices, nameof(uniqueDataScrapingServices));
            _uniqueDataScrapingServices = uniqueDataScrapingServices;
        }

        public Func<WebCharacter, IEnumerable<object>> Scrape
        {
            get
            {
                return character =>
                {
                    Guard.VerifyObjectNotNull(character, nameof(character));
                    Guard.VerifyStringIsNotNullOrEmpty(character.Name, character.Name);

                    var htmlParser = _uniqueDataScrapingServices.CreateParserFromSourceUrl(character.SourceUrl);

                    string attributeTableHtml = htmlParser.GetSingle(MonadoArtsAttributeTableXPath);

                    var monadoArtsTableRows = HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes(ScrapingConstants.XPathTableRows);

                    if (monadoArtsTableRows == null)
                    {
                        throw new Exception(
                            $"Error getting unique data after attempting to scrape full table using xpath: '{ScrapingConstants.XPathTableRows}'");
                    }

                    if (character.SourceUrl.Contains("Ultimate"))
                    {
                        return monadoArtsTableRows.SelectMany(
                            row => row.SelectNodes(ScrapingConstants.XPathMovementTableCellKeys),
                            (row, statName) => GetUniqueAttributeForUltimate(row, character)).Where(stat => stat != null);
                    }
                    else
                    {
                        return monadoArtsTableRows.SelectMany(
                          row => row.SelectNodes(ScrapingConstants.XPathMovementTableCellKeys),
                          (row, statName) => GetUniqueAttributeForSmash4(row, character)).Where(stat => stat != null);
                    }
                };
            }

        }

        private MonadoArt GetUniqueAttributeForUltimate(HtmlNode row, WebCharacter character)
        {
            var uniqueData = _uniqueDataScrapingServices.Create<MonadoArt>();
            var monadoArtsCells = row.SelectNodes("td");

            string name = CleanupValue(monadoArtsCells[0].InnerText);
            string active = CleanupValue(monadoArtsCells[1].InnerText);
            string cooldown = CleanupValue(monadoArtsCells[2].InnerText);
            string damageTaken = CleanupValue(monadoArtsCells[3].InnerText);
            string damageDealt = CleanupValue(monadoArtsCells[4].InnerText);
            string knockbackTaken = CleanupValue(monadoArtsCells[5].InnerText);
            string knockbackDealt = CleanupValue(monadoArtsCells[6].InnerText);
            string jumpHeight = CleanupValue(monadoArtsCells[7].InnerText);
            string ledgeJumpHeight = CleanupValue(monadoArtsCells[8].InnerText);
            string airSlashHeight = CleanupValue(monadoArtsCells[9].InnerText);
            string initialDashSpeed = CleanupValue(monadoArtsCells[10].InnerText);
            string runSpeed = CleanupValue(monadoArtsCells[11].InnerText);
            string walkSpeed = CleanupValue(monadoArtsCells[12].InnerText);
            string airSpeed = CleanupValue(monadoArtsCells[13].InnerText);
            string gravity = CleanupValue(monadoArtsCells[14].InnerText);
            string fallSpeed = CleanupValue(monadoArtsCells[15].InnerText);
            string shieldHealth = CleanupValue(monadoArtsCells[16].InnerText);
            string shieldRegen = CleanupValue(monadoArtsCells[17].InnerText);

            uniqueData.Name = name;
            uniqueData.Owner = character.Name;
            uniqueData.OwnerId = character.OwnerId;
            uniqueData.Active = active;
            uniqueData.AirSlashHeight = airSlashHeight;
            uniqueData.AirSpeed = airSpeed;
            uniqueData.Cooldown = cooldown;
            uniqueData.DamageDealt = damageDealt;
            uniqueData.DamageTaken = damageTaken;
            uniqueData.FallSpeed = fallSpeed;
            uniqueData.Gravity = gravity;
            uniqueData.InitialDashSpeed = initialDashSpeed;
            uniqueData.JumpHeight = jumpHeight;
            uniqueData.KnockbackDealt = knockbackDealt;
            uniqueData.KnockbackTaken = knockbackTaken;
            uniqueData.LedgeJumpHeight = ledgeJumpHeight;
            uniqueData.RunSpeed = runSpeed;
            uniqueData.ShieldHealth = shieldHealth;
            uniqueData.ShieldRegen = shieldRegen;
            uniqueData.WalkSpeed = walkSpeed;
            uniqueData.Game = Games.Ultimate;

            return uniqueData;
        }

        private MonadoArt GetUniqueAttributeForSmash4(HtmlNode row, WebCharacter character)
        {
            var uniqueData = _uniqueDataScrapingServices.Create<MonadoArt>();
            var monadoArtsCells = row.SelectNodes("td");

            string name = CleanupValue(monadoArtsCells[0].InnerText);
            string damageTaken = CleanupValue(monadoArtsCells[1].InnerText);
            string damageDealt = CleanupValue(monadoArtsCells[2].InnerText);
            string knockbackTaken = CleanupValue(monadoArtsCells[3].InnerText);
            string knockbackDealt = CleanupValue(monadoArtsCells[4].InnerText);
            string jumpHeight = CleanupValue(monadoArtsCells[5].InnerText);
            string walkSpeed = CleanupValue(monadoArtsCells[6].InnerText);
            string airSpeed = CleanupValue(monadoArtsCells[7].InnerText);
            string fallSpeed = CleanupValue(monadoArtsCells[8].InnerText);
            string shieldHealth = CleanupValue(monadoArtsCells[9].InnerText);

            uniqueData.Name = name;
            uniqueData.Owner = character.Name;
            uniqueData.OwnerId = character.OwnerId;
            uniqueData.AirSpeed = airSpeed;
            uniqueData.DamageDealt = damageDealt;
            uniqueData.DamageTaken = damageTaken;
            uniqueData.FallSpeed = fallSpeed;
            uniqueData.JumpHeight = jumpHeight;
            uniqueData.KnockbackDealt = knockbackDealt;
            uniqueData.KnockbackTaken = knockbackTaken;
            uniqueData.ShieldHealth = shieldHealth;
            uniqueData.WalkSpeed = walkSpeed;
            uniqueData.Game = Games.Smash4;

            return uniqueData;
        }

        private static string CleanupValue(string dirtyValue) => dirtyValue.Replace(TimesSymbolEncoded, "x").Replace(PercentSymbolEncoded, "%");

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
