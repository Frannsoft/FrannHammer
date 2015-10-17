namespace KuroganeHammer.Data.Core.Model.Stats.dbentity
{
    public class StatDB
    {
        internal string name { get; set; }
        internal int ownerid { get; set; }
        internal string rawname { get; set; }

        internal StatDB()
        { }

        internal StatDB(string name, int ownerid, string rawname)
        {
            this.name = name;
            this.ownerid = ownerid;
            this.rawname = rawname;
        }
    }
}
