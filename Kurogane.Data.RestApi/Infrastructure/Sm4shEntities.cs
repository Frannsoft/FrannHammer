using Kurogane.Data.RestApi.Configuration;
using Kurogane.Data.RestApi.Models;
using System.Data.Entity;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public class Sm4shEntities : DbContext
    {
        public DbSet<MovementStat> MovementStats { get; set; }
        public DbSet<MoveStat> Moves { get; set; }
        public DbSet<CharacterStat> Characters { get; set; }

        public Sm4shEntities()
            : base("Sm4shEntities")
        { }

        public static Sm4shEntities Create()
        {
            return new Sm4shEntities();
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MovementStatConfiguration());
            modelBuilder.Configurations.Add(new MoveStatConfiguration());
            modelBuilder.Configurations.Add(new CharacterStatConfiguration());
        }
    }
}
