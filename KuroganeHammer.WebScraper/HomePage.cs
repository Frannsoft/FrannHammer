using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace KuroganeHammer.WebScraper
{
    public class HomePage
    {
        private readonly string _url;

        public HomePage(string url)
        {
            _url = url;
        }

        public List<Thumbnail> GetThumbnailData()
        {
            var web = new HtmlWeb();
            var doc = web.Load(_url);
            var nodes = doc.DocumentNode.SelectNodes(".//*[@id='AutoNumber1']/tbody/tr/td/a/img");

            return nodes.Select(node => new Thumbnail()
            {
                Key = node.Attributes["alt"].Value
                .Replace(" ", string.Empty)
                .Replace("_", string.Empty)
                .Replace(".", string.Empty)
                .Replace("&", string.Empty)
                .Replace("-", string.Empty).ToUpper(),
                Url = _url.Replace("/Smash4/", string.Empty) + node.Attributes["src"]
                .Value
            }).ToList();
        }
    }

    public class Thumbnail
    {
        public string Url { get; set; }
        public string Key { get; set; }

    }
}
