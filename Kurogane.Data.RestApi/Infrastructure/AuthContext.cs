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
        public DbSet<CharacterAttribute> CharacterAttributes { get; set; }
        public DbSet<SmashAttributeType> SmashAttributes { get; set; }

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
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new MovementStatConfiguration());
            modelBuilder.Configurations.Add(new MoveStatConfiguration());
            modelBuilder.Configurations.Add(new CharacterStatConfiguration());
            modelBuilder.Configurations.Add(new CharacterAttributeConfiguration());
            modelBuilder.Configurations.Add(new SmashAttributeConfiguration());
        }
    }
}
