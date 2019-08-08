namespace FrannHammer.NetCore.WebApi.Models
{
    public class CharacterAttributesLink : Link
    {
        public const string Relation = "characterattributes";

        public CharacterAttributesLink(string href)
            : base(Relation, href)
        { }
    }
}