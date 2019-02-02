using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.NetCore.WebApi.Models
{
    public interface IResource
    {
        Games Game { get; set; }
        IEnumerable<Link> Links { get; }
        RelatedLinks Related { get; set; }

        void AddLink(Link link);
        void AddLinks(params Link[] links);
        void AddRelated(RelatedLinks related);
    }
}