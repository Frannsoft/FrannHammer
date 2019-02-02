namespace FrannHammer.NetCore.WebApi.Models
{
    public class CharacterLink : Link
    {
        public const string Relation = "character";

        public CharacterLink(string href)
            : base(Relation, href)
        { }
    }
}