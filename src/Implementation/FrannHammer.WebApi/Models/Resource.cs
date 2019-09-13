using System.Collections.Generic;
using FrannHammer.Utility;
using Newtonsoft.Json;

namespace FrannHammer.WebApi.Models
{
    public abstract class Resource
    {
        private readonly List<Link> _links = new List<Link>();

        [JsonProperty(Order = 100)]
        public IEnumerable<Link> Links => _links;

        public void AddLink(Link link)
        {
            Guard.VerifyObjectNotNull(link, nameof(link));
            _links.Add(link);
        }

        public void AddLinks(params Link[] links)
        {
            foreach (var link in links)
            {
                AddLink(link);
            }
        }
    }
}