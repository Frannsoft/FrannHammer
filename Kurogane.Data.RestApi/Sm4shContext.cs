using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core.Model.Stats;
using System.Configuration;
using System.Data.Entity;

namespace Kurogane.Data.RestApi
{
    public class Sm4shContext : DbContext
    {
        public DbSet<CharacterDTO> Characters { get; set; }
        public DbSet<MovementStat> MovementStats { get; set; }
        public DbSet<AerialStat> AerialStats { get; set; }
        public DbSet<GroundStat> GroundStats { get; set; }
        public DbSet<SpecialStat> SpecialStats { get; set; }

        public Sm4shContext()
            : base(ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString)
        { }
    }
}
