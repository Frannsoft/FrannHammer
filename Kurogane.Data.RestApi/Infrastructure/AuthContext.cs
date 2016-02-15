using Kurogane.Data.RestApi.Configuration;
using Kurogane.Data.RestApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<MovementStat> MovementStats { get; set; }
        public DbSet<MoveStat> Moves { get; set; }
        public DbSet<CharacterStat> Characters { get; set; }

        public AuthContext()
            : base("AuthContext", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static AuthContext Create()
        {
            return new AuthContext();
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new MovementStatConfiguration());
            modelBuilder.Configurations.Add(new MoveStatConfiguration());
            modelBuilder.Configurations.Add(new CharacterStatConfiguration());
        }
    }
}
