namespace FrannHammer.WebApi.Models
{
    public class MovementsLink : Link
    {
        public const string Relation = "movements";

        public MovementsLink(string href)
            : base(Relation, href)
        { }
    }
}