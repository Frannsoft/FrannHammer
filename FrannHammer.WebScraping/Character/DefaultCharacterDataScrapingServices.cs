using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Contracts.Images;
using FrannHammer.WebScraping.Contracts.Movements;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping.Character
{
    public class DefaultCharacterDataScrapingServices : ICharacterDataScrapingServices
    {
        private readonly IImageScrapingService _imageScrapingService;
        private readonly IMovementScraper _movementScraper;
        private readonly IEnumerable<IAttributeScraper> _attributeScrapers;
        private readonly ICharacterMoveScraper _characterMoveScraper;
        private readonly IWebServices _webServices;

        public DefaultCharacterDataScrapingServices(IImageScrapingService imageScrapingService, IMovementScraper movementScraper,
            IEnumerable<IAttributeScraper> attributeScrapers, ICharacterMoveScraper characterMoveScraper, IWebServices webServices)
        {
            Guard.VerifyObjectNotNull(imageScrapingService, nameof(imageScrapingService));

            _imageScrapingService = imageScrapingService;
            _movementScraper = movementScraper;
            _attributeScrapers = attributeScrapers;
            _characterMoveScraper = characterMoveScraper;
            _webServices = webServices;
        }

        public void PopulateCharacter(WebCharacter character)
        {
            var htmlParser = _webServices.CreateParserFromSourceUrl(character.SourceUrl);
            string displayNameHtml = htmlParser.GetSingle(ScrapingXPathConstants.XPathFrameDataVersion);

            string displayName = GetCharacterDisplayName(displayNameHtml);

            //character details
            string mainImageUrl = htmlParser.GetAttributeFromSingleNavigable("src", ScrapingXPathConstants.XPathImageUrl);

            //color hex
            string colorHex = _imageScrapingService.GetColorHexValue(mainImageUrl).Result;

            //movements
            var movements = _movementScraper.GetMovementsForCharacter(character);

            //moves
            var moves = _characterMoveScraper.ScrapeMoves(character);

            //attributes
            var attributes = new List<IAttribute>();
            foreach (var attributeScraper in _attributeScrapers)
            {
                attributes.AddRange(attributeScraper.Scrape());
            }

            character.DisplayName = displayName;
            character.MainImageUrl = mainImageUrl;
            character.ColorHex = colorHex;
            character.Movements = movements;
            character.Moves = moves;
            character.Attributes = attributes;
        }

        private static string GetCharacterDisplayName(string rawDisplayNameHtml)
        {
            var node = HtmlNode.CreateNode(rawDisplayNameHtml);
            var characterFriendlyName = node.InnerText.Split(new[] { "'s F" }, StringSplitOptions.None)[0];

            //clean up and remove the "'s"
            string retVal = characterFriendlyName;
            if (characterFriendlyName.EndsWith("'s"))
            {
                retVal = characterFriendlyName.Remove(characterFriendlyName.Length - 2, 2);
            }
            return retVal;
        }
    }
}
