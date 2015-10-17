namespace KuroganeHammer.Data.Core.Model.Stats.dbentity
{
    internal class MovementStatDB : StatDB
    {
        internal string rank { get; private set; }
        internal string value { get; private set; }

        internal MovementStatDB()
            : base()
        { }

        internal MovementStatDB(int ownerId, string name, string rank, 
            string value, string rawname)
            : base(name, ownerId, rawname)
        {
            this.rank = rank;
            this.value = value;
        }
    }
}
