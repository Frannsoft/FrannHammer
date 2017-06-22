namespace FrannHammer.WebApi.Models
{
    public class MovesLink : Link
    {
        public const string Relation = "moves";

        public MovesLink(string href, string title = null)
            : base(Relation, href, title)
        { }
    }
}