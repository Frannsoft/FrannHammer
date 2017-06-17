namespace FrannHammer.WebApi.Models
{
    public class CharacterLink : Link
    {
        public const string Relation = "character";

        public CharacterLink(string href, string title = null)
            : base(Relation, href, title)
        { }
    }
}