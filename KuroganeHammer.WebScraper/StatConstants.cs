
namespace KuroganeHammer.WebScraper
{
    internal class StatConstants
    {
        #region xpath
        internal const string XpathImageUrl = @"html/body/div[1]/div/img";
        internal const string XpathFrameDataVersion = @"/html/body/div[1]/div/h1|/html/body/div/div/*/h1";
        internal const string XpathTableNodeMovementStats = @"(//*/table[@id='AutoNumber1'])[1]";

        /// <summary>
        /// When there is no table above the main one that describes the stat we want, 
        /// but there is only one table with the values we need.
        /// </summary>
        internal const string XpathTableNodeAttributesWithNoDescription = @"(//*/table[@id='AutoNumber1'])[1]";

        /// <summary>
        /// When there is a table above the main one that describes the stat we want the second one that 
        /// actually has the values
        /// </summary>
        internal const string XpathTableNodeAttributesWithDescription = @"(//*/table[@id='AutoNumber1'])[2]";

        /// <summary>
        /// There is a description table on the page above the main table.
        /// </summary>
        internal const string XpathTableNodeAttributeHeaders = @"//*[@id='AutoNumber1'][2]/thead/tr";

        /// <summary>
        /// There is no description table on the page above the main table.
        /// </summary>
        internal const string XpathTableNodeAttributeHeadersWithNoDescription = @"//*[@id='AutoNumber1'][1]/thead/tr";
        internal const string XpathTableNodeGroundStats = @"(//*/table[@id='AutoNumber1'])[2]";
        internal const string XpathTableNodeGroundStatsAdjusted = @"(//*/table[@id='AutoNumber1'])[3]";
        internal const string XpathTableNodeAerialStats = @"(//*/table[@id='AutoNumber2'])[1]";
        internal const string XpathTableNodeSpecialStats = @"(//*/table[@id='AutoNumber3'])[1]";
        internal const string XpathTableRows = "tbody/tr";
        internal const string XpathTableCells = "th|td";
        internal const string XpathTableCellkeynames = "td[contains(@style, 'font-weight: bold')]"; //th[contains(@style, 'font-weight: bold')]|
        internal const string XpathTableCellvalues = @"following-sibling::td[not(contains(@style, 'font-weight: bold'))]";
        #endregion
    }
}
