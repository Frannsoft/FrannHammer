namespace KuroganeHammer.Data.Core.Model.Stats.dbentity
{
    [TableId("roster")]
    internal class CharacterDB
    {
        internal string name { get; private set; }
        internal int id { get; private set; }
        internal string kurourl { get; private set; }

        internal CharacterDB()
        { }

        internal CharacterDB(string name, int id, string kurourl)
        {
            this.name = name;
            this.id = id;
            this.kurourl = kurourl;
        }
    }
}
