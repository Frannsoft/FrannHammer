namespace FrannHammer.WebScraping
{
    public static class ScrapingConstants
    {
        public const string XPathFrameDataVersion = @"/html/body/div[1]/div/h1|/html/body/div/div/*/h1";
        public const string XPathImageUrl = @"html/body/div[1]/div/img";
        public const string XPathMovementTableCellKeys = @"td[contains(@style, 'font-weight: bold')]";
        public const string XPathTableCellValues = @"following-sibling::td[not(contains(@style, 'font-weight: bold'))]";
        public const string XPathTableNodeMovementStats = @"(//*/table[@id='AutoNumber1'])[1]";
        public const string XPathTableRows = "tbody/tr";
        public const string XPathTableNodeGroundStats = @"(//*/table[@id='AutoNumber1'])[2]";
        public const string XPathTableNodeGroundStatsAdjusted = @"(//*/table[@id='AutoNumber1'])[3]";
        public const string XPathTableNodeAerialStats = @"(//*/table[@id='AutoNumber2'])[1]";
        public const string XPathTableCells = "th|td";
        public const string XPathThumbnailUrl = "//img[@alt='[charactername]']";
        public const string EveryoneOneElseAttributeKey = "Everyone Else";
        public const string EveryoneKey = "contains(.,'Everyone') or contains(.,'everyone')";
        //public const string XPathEveryoneElseTableRow = "//table/tbody/tr/td[.\"" + EveryoneOneElseAttributeKey + "\"]/parent::tr";
        public const string XPathEveryoneElseTableRow = @"//table/tbody/tr/td[" + EveryoneKey + "]/parent::tr";//\"" + EveryoneOneElseAttributeKey + "\"]/parent::tr";

        public const string XPathTableNodeSpecialStats = @"(//*/table[@id='AutoNumber3'])[1]";
        public const string XPathTableNodeAttributesWithDescription = @"(//*/table[@id='AutoNumber1'])[2]";
        public const string XPathTableNodeAttributesWithNoDescription = @"(//*/table[@id='AutoNumber1'])[1]";
        public const string XPathTableNodeAttributeHeaders = @"//*[@id='AutoNumber1'][2]/thead/tr";
        public const string XPathTableNodeAttributeHeadersWithNoDescription = @"//*[@id='AutoNumber1'][1]/thead/tr";

        public class ExcludedRowHeaders
        {
            public const string Grabs = "Grabs";
            public const string TractorBeams = "Useless Tractor Beams"; //pac-man 
            public const string Throws = "Throws";
            public const string Miscellaneous = "Miscellaneous";
        }

        public class CommonMoveNames
        {
            public const string Grab = "Grab";
            public const string Throw = "Throw";
        }
    }
}