using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FrannHammer.NetCore.WebApi.Models
{
    public abstract class Resource : IResource
    {
        private readonly List<Link> _links = new List<Link>();

        public Games Game { get; set; }

        public RelatedLinks Related { get; set; }

        [JsonProperty(Order = 100)]
        public IEnumerable<Link> Links => _links;


        public void AddLink(Link link)
        {
            Guard.VerifyObjectNotNull(link, nameof(link));
            _links.Add(link);
        }

        public void AddRelated(RelatedLinks related)
        {
            Related = related;
        }

        public void AddLinks(params Link[] links)
        {
            foreach (var link in links)
            {
                AddLink(link);
            }
        }
    }

    public class RelatedLinks
    {
        public dynamic Smash4 { get; set; }
        public dynamic Ultimate { get; set; }
    }
}