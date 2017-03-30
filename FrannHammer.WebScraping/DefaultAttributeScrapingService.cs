using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping
{
    public class DefaultAttributeScrapingService : IAttributeScrapingService
    {
        private readonly IHtmlParserProvider _htmlParserProvider;
        private readonly IAttributeProvider _attributeProvider;
        private readonly IPageDownloader _pageDownloader;
        private readonly IWebClientProvider _webClientProvider;

        public DefaultAttributeScrapingService(IHtmlParserProvider htmlParserProvider, IAttributeProvider attributeProvider, IPageDownloader pageDownloader, IWebClientProvider webClientProvider)
        {
            Guard.VerifyObjectNotNull(htmlParserProvider, nameof(htmlParserProvider));
            Guard.VerifyObjectNotNull(attributeProvider, nameof(attributeProvider));
            Guard.VerifyObjectNotNull(pageDownloader, nameof(pageDownloader));
            Guard.VerifyObjectNotNull(webClientProvider, nameof(webClientProvider));

            _htmlParserProvider = htmlParserProvider;
            _attributeProvider = attributeProvider;
            _pageDownloader = pageDownloader;
            _webClientProvider = webClientProvider;
        }

        public IEnumerable<IAttribute> GetAttributes()
        {
            var attributeValues = new List<IAttribute>();

            const string baseSmashUrl = "http://kuroganehammer.com/Smash4/";
            const string baseAttributePageUrl = baseSmashUrl + "Attributes";
            string baseAttributePageHtml = _pageDownloader.DownloadPageSource(new Uri(baseAttributePageUrl), _webClientProvider);

            var basePageHtmlParser = _htmlParserProvider.Create(baseAttributePageHtml);

            foreach (var attributePage in GetDefaultAttributeMetadata(basePageHtmlParser, baseAttributePageUrl))
            {
                string url = baseSmashUrl + attributePage.Key;

                string attributePageSource = _pageDownloader.DownloadPageSource(new Uri(url), _webClientProvider);
                var htmlParser = _htmlParserProvider.Create(attributePageSource);

                string attributeTableHtml = htmlParser.GetSingle(ScrapingXPathConstants.XPathTableNodeAttributesWithDescription) ?? 
                                            htmlParser.GetSingle(ScrapingXPathConstants.XPathTableNodeAttributesWithNoDescription);

                var attributeTableRows =
                    HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes(ScrapingXPathConstants.XPathTableRows);

                if (attributeTableRows == null)
                { throw new Exception("Error getting attribute table data after attempting to scrape full table"); }

                //TODO - create list of exclusions like before since not all of these are the same
                var headers = GetHeaders(htmlParser);

                foreach (var row in attributeTableRows)
                {
                    var cells = row.SelectNodes(ScrapingXPathConstants.XPathTableCells);

                    var attributeValue = default(IAttribute);

                    for (int i = 0; i < cells.Count; i++)
                    {
                        attributeValue = _attributeProvider.CreateAttribute();
                        attributeValue.Name = headers[i];
                        attributeValue.Value = cells[i].InnerText;
                        attributeValue.AttributeFlag = attributePage.Key;
                    }

                    attributeValues.Add(attributeValue);
                }
            }

            return attributeValues;
        }

        private static IDictionary<string, string> GetDefaultAttributeMetadata(IHtmlParser htmlParser, string sourceUrl)
        {
            var nodes = htmlParser.GetCollection(".//*[@id='AutoNumber1']/tbody/tr/td/a/img");

            var data = new Dictionary<string, string>();

            foreach (var node in nodes)
            {
                var htmlNode = HtmlNode.CreateNode(node);
                string sanitizedName = htmlNode.Attributes["alt"].Value
                    .Replace(" ", string.Empty)
                    .Replace("_", string.Empty)
                    .Replace(".", string.Empty)
                    .Replace("&", string.Empty)
                    .Replace("-", string.Empty).ToUpper();

                string url = string.Empty;

                if (htmlNode.Attributes["alt"].Value.Equals("Lucas"))
                {
                    url = htmlNode.Attributes["src"].Value;
                }
                else
                {
                    url = sourceUrl.Replace("/Smash4/", string.Empty) + htmlNode.Attributes["src"].Value;
                }

                data.Add(sanitizedName, url);
            }

            return data;
        }

        private static IList<string> GetHeaders(IHtmlParser htmlParser)
        {
            var rawHeaders = htmlParser.GetCollection(ScrapingXPathConstants.XPathTableNodeAttributeHeaders) ??
                            htmlParser.GetCollection(ScrapingXPathConstants.XPathTableNodeAttributeHeadersWithNoDescription);
            var headers = (from row in rawHeaders
                           from cell in HtmlNode.CreateNode(row).SelectNodes(ScrapingXPathConstants.XPathTableCells)
                           select cell.InnerText).ToList();

            return headers;
        }
    }
}
