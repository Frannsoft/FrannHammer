using FrannHammer.Utility;

namespace FrannHammer.WebApi.Models
{
    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Title { get; set; }

        protected Link(string relation, string href, string title = null)
        {
            Guard.VerifyStringIsNotNullOrEmpty(relation, nameof(relation));
            Guard.VerifyStringIsNotNullOrEmpty(href, nameof(href));

            Rel = relation;
            Href = href;
            Title = title;
        }

        public Link()
        { }
    }
}