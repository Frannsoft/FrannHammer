using FrannHammer.Utility;

namespace FrannHammer.NetCore.WebApi.Models
{
    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }

        protected Link(string relation, string href)
        {
            Guard.VerifyStringIsNotNullOrEmpty(relation, nameof(relation));
            Guard.VerifyStringIsNotNullOrEmpty(href, nameof(href));

            Rel = relation;
            Href = href;
        }

        public Link()
        { }
    }
}