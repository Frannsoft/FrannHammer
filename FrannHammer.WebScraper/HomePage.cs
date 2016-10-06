using System.Collections.Generic;
using HtmlAgilityPack;

namespace FrannHammer.WebScraper
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

            var thumbnails = new List<Thumbnail>();

            foreach (var node in nodes)
            {
                var thumbnail = new Thumbnail
                {
                    Key = node.Attributes["alt"].Value
                        .Replace(" ", string.Empty)
                        .Replace("_", string.Empty)
                        .Replace(".", string.Empty)
                        .Replace("&", string.Empty)
                        .Replace("-", string.Empty).ToUpper()
                };

                if (node.Attributes["alt"].Value.Equals("Lucas"))
                {
                    thumbnail.Url = node.Attributes["src"].Value;
                }
                else
                {
                    thumbnail.Url = _url.Replace("/Smash4/", string.Empty) + node.Attributes["src"].Value;
                }

                thumbnails.Add(thumbnail);
            }

            return thumbnails;
        }
    }

    public class Thumbnail
    {
        public string Url { get; set; }
        public string Key { get; set; }

    }
}
