namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public static class UrlReplacementExtensions
    {
        public static string ReplaceSmash4WithUltimate(this string url) => url.Replace("smash4", "ultimate");
        public static string ReplaceUltimateWithSmash4(this string url) => url.Replace("ultimate", "smash4");
    }
}
