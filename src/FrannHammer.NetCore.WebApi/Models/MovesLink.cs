namespace FrannHammer.NetCore.WebApi.Models
{
    public class MovesLink : Link
    {
        public const string Relation = "moves";

        public MovesLink(string href)
            : base(Relation, href)
        { }
    }
}