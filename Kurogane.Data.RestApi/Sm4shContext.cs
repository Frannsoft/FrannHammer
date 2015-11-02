using Kurogane.Data.RestApi.Models;
using System.Configuration;
using System.Data.Entity;

namespace Kurogane.Data.RestApi
{
    public class Sm4shContext : DbContext
    {
        public DbSet<CharacterModel> Characters { get; set; }
        public DbSet<MovementStatModel> MovementStats { get; set; }

        public Sm4shContext()
            : base(ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString)
        { }
    }
}
