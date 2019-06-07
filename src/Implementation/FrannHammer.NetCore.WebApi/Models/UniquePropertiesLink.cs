namespace FrannHammer.NetCore.WebApi.Models
{
    public class UniquePropertiesLink : Link
    {
        public const string Relation = "uniqueproperties";

        public UniquePropertiesLink(string href)
            : base(Relation, href)
        { }
    }
}