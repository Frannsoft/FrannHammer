using Kurogane.Data.RestApi.Configuration;
using Kurogane.Data.RestApi.Models;
using System.Data.Entity;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public class Sm4ShEntities : DbContext
    {
        public DbSet<MovementStat> MovementStats { get; set; }
        public DbSet<MoveStat> Moves { get; set; }
        public DbSet<CharacterStat> Characters { get; set; }

        public Sm4ShEntities()
            : base("Sm4shEntities")
        { }

        public static Sm4ShEntities Create()
        {
            return new Sm4ShEntities();
        }

        public virtual void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MovementStatConfiguration());
            modelBuilder.Configurations.Add(new MoveStatConfiguration());
            modelBuilder.Configurations.Add(new CharacterStatConfiguration());
        }
    }
}
