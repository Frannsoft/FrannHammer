namespace FrannHammer.WebApi.Models
{
    public class SelfLink : Link
    {
        public const string Relation = "self";

        public SelfLink(string href, string title = null)
            : base(Relation, href, title)
        { }
    }
}