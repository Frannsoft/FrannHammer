
namespace Kurogane.Web.Data.Stats
{
    internal class StatConstants
    {
        #region xpath

        internal const string XPATH_FRAME_DATA_VERSION = @"/html/body/div[1]/div/h1|/html/body/div/div/*/h1";
        internal const string XPATH_TABLE_NODE_MOVEMENT_STATS = @"(//*/table[@id='AutoNumber1'])[1]";
        internal const string XPATH_TABLE_NODE_GROUND_STATS = @"(//*/table[@id='AutoNumber1'])[2]";
        internal const string XPATH_TABLE_NODE_AERIAL_STATS = @"(//*/table[@id='AutoNumber2'])[1]";
        internal const string XPATH_TABLE_NODE_SPECIAL_STATS = @"(//*/table[@id='AutoNumber3'])[1]";
        internal const string XPATH_TABLE_ROWS = "tbody/tr";
        internal const string XPATH_TABLE_CELLS = "th|td";
        internal const string XPATH_TABLE_CELLKEYNAMES = "td[contains(@style, 'font-weight: bold')]"; //th[contains(@style, 'font-weight: bold')]|
        internal const string XPATH_TABLE_CELLVALUES =  @"following-sibling::td[not(contains(@style, 'font-weight: bold'))]";
        #endregion
    }
}
