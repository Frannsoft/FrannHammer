namespace FrannHammer.WebApi.Models
{
    public class CharacterAttributesLink : Link
    {
        public const string Relation = "characterattributes";

        public CharacterAttributesLink(string href, string title = null)
            : base(Relation, href, title)
        { }
    }
}