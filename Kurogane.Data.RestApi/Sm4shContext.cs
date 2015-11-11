using KuroganeHammer.Data.Core.Model.Stats;
using System.Configuration;
using System.Data.Entity;

namespace Kurogane.Data.RestApi
{
    public class Sm4shContext : DbContext
    {
        public DbSet<CharacterStat> Characters { get; set; }
        public DbSet<MovementStat> MovementStats { get; set; }
        public DbSet<MoveStat> Moves { get; set; }

        public Sm4shContext()
            : base(ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString)
        { }
    }
}
