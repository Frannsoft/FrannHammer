using HtmlAgilityPack;
using System.Collections.Generic;

namespace KuroganeHammer.WebScraper
{
    public class HomePage
    {
        private string url;

        public HomePage(string url)
        {
            this.url = url;
        }

        public List<Thumbnail> GetThumbnailData()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(".//*[@id='AutoNumber1']/tbody/tr/td/a/img");

            List<Thumbnail> images = new List<Thumbnail>();

            foreach (HtmlNode node in nodes)
            {
                images.Add(new Thumbnail()
                    {
                        Key = node.Attributes["alt"].Value.Replace(" ", string.Empty)
                        .Replace("_", string.Empty)
                        .Replace(".", string.Empty)
                        .Replace("&", string.Empty)
                        .Replace("-", string.Empty)
                        .ToUpper(),
                        Url = url.Replace("/Smash4/", string.Empty) + node.Attributes["src"].Value
                    });
            }

            return images;
        }
    }

    public class Thumbnail
    {
        public string Url { get; set; }
        public string Key { get; set; }

    }
}
