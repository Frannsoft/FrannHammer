using System;
using System.Linq;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping
{
    public static class ScrapingXPathConstants
    {
        public const string XPathFrameDataVersion = @"/html/body/div[1]/div/h1|/html/body/div/div/*/h1";
        public const string XPathImageUrl = @"html/body/div[1]/div/img";
        public const string XPathMovementTableCellKeys = @"td[contains(@style, 'font-weight: bold')]";
        public const string XPathTableCellValues = @"following-sibling::td[not(contains(@style, 'font-weight: bold'))]";
        public const string XPathTableNodeMovementStats = @"(//*/table[@id='AutoNumber1'])[1]";
        public const string XPathTableRows = "tbody/tr";
        public const string XPathTableNodeGroundStats = @"(//*/table[@id='AutoNumber1'])[2]";
        public const string XPathTableNodeAerialStats = @"(//*/table[@id='AutoNumber2'])[1]";
        public const string XPathTableCells = "th|td";
        public const string XPathTableNodeSpecialStats = @"(//*/table[@id='AutoNumber3'])[1]";
        public const string XPathTableNodeAttributesWithDescription = @"(//*/table[@id='AutoNumber1'])[2]";
        public const string XPathTableNodeAttributesWithNoDescription = @"(//*/table[@id='AutoNumber1'])[1]";
        public const string XPathTableNodeAttributeHeaders = @"//*[@id='AutoNumber1'][2]/thead/tr";
        public const string XPathTableNodeAttributeHeadersWithNoDescription = @"//*[@id='AutoNumber1'][1]/thead/tr";
    }

    public class CharacterDataScrapingService : ICharacterDataScrapingService
    {
        private readonly IHtmlParser _htmlParser;
        private readonly IImageScrapingService _imageScrapingService;
        private readonly IMovementScrapingService _movementScrapingService;
        private readonly IMoveScrapingService _moveScrapingService;
        private readonly IAttributeScrapingService _attributeScrapingService;

        public CharacterDataScrapingService(IHtmlParser htmlParser, IImageScrapingService imageScrapingService,
            IMovementScrapingService movementScrapingService, IMoveScrapingService moveScrapingService,
            IAttributeScrapingService attributeScrapingService)
        {
            Guard.VerifyObjectNotNull(htmlParser, nameof(htmlParser));
            Guard.VerifyObjectNotNull(imageScrapingService, nameof(imageScrapingService));
            Guard.VerifyObjectNotNull(movementScrapingService, nameof(movementScrapingService));
            Guard.VerifyObjectNotNull(moveScrapingService, nameof(moveScrapingService));
            Guard.VerifyObjectNotNull(attributeScrapingService, nameof(attributeScrapingService));

            _htmlParser = htmlParser;
            _imageScrapingService = imageScrapingService;
            _movementScrapingService = movementScrapingService;
            _moveScrapingService = moveScrapingService;
            _attributeScrapingService = attributeScrapingService;
        }

        public void PopulateCharacterFromWeb<T>(T character)
            where T : WebCharacter
        {
            //call methods off of html parser object for:
            var displayNameHtml = _htmlParser.GetSingle(ScrapingXPathConstants.XPathFrameDataVersion);
            string displayName = GetCharacterDisplayName(displayNameHtml);

            //character details
            string mainImageUrl = _htmlParser.GetAttributeFromSingleNavigable("src", ScrapingXPathConstants.XPathImageUrl);

            //color hex
            string colorHex = _imageScrapingService.GetColorHexValue(mainImageUrl).Result;

            //movements
            var movements = _movementScrapingService.GetMovements(ScrapingXPathConstants.XPathTableNodeMovementStats);

            //moves
            var groundMoves = _moveScrapingService.GetGroundMoves(ScrapingXPathConstants.XPathTableNodeGroundStats);
            var aerialMoves = _moveScrapingService.GetAerialMoves(ScrapingXPathConstants.XPathTableNodeAerialStats);
            var specialMoves = _moveScrapingService.GetSpecialMoves(ScrapingXPathConstants.XPathTableNodeSpecialStats);
            var totalMoves = groundMoves.Concat(aerialMoves).Concat(specialMoves);

            //attributes
            var attributes = _attributeScrapingService.GetAttributes();

            character.DisplayName = displayName;
            character.MainImageUrl = mainImageUrl;
            character.ColorHex = colorHex;
            character.Movements = movements;
            character.Moves = totalMoves;
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
