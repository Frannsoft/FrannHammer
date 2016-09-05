using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FrannHammer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FrannHammer.Services
{
    public interface IApplicationDbContext : IDisposable
    {
        IDbSet<Character> Characters { get; set; }
        IDbSet<CharacterAttribute> CharacterAttributes { get; set; }
        IDbSet<Move> Moves { get; set; }
        IDbSet<Movement> Movements { get; set; }
        IDbSet<SmashAttributeType> SmashAttributeTypes { get; set; }
        IDbSet<Throw> Throws { get; set; }
        IDbSet<ThrowType> ThrowTypes { get; set; }
        IDbSet<Notation> Notations { get; set; }
        IDbSet<CharacterAttributeType> CharacterAttributeTypes { get; set; }
        IDbSet<Hitbox> Hitbox { get; set; }
        IDbSet<BaseDamage> BaseDamage { get; set; }
        IDbSet<Angle> Angle { get; set; }
        IDbSet<BaseKnockback> BaseKnockback { get; set; }
        IDbSet<SetKnockback> SetKnockback { get; set; }
        IDbSet<KnockbackGrowth> KnockbackGrowth { get; set; }
        IDbSet<LandingLag> LandingLag { get; set; }
        IDbSet<Autocancel> Autocancel { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }
        bool RequireUniqueEmail { get; set; }

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        DbSet Set(Type entityType);

        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }

        public ApplicationDbContext(DbConnection existingConnection)
            : base(existingConnection, true)
        { }

        protected ApplicationDbContext(string connectionString)
            : base(connectionString)
        { }

        public static ApplicationDbContext Create(DbConnection connect = null)
        {
            var context = connect == null ? new ApplicationDbContext() :
             new ApplicationDbContext(connect);
            return context;
        }

        public virtual IDbSet<Character> Characters { get; set; }
        public virtual IDbSet<CharacterAttribute> CharacterAttributes { get; set; }
        public virtual IDbSet<Move> Moves { get; set; }
        public virtual IDbSet<Movement> Movements { get; set; }
        public virtual IDbSet<Throw> Throws { get; set; }
        public virtual IDbSet<ThrowType> ThrowTypes { get; set; }
        public virtual IDbSet<SmashAttributeType> SmashAttributeTypes { get; set; }
        public virtual IDbSet<Notation> Notations { get; set; }
        public virtual IDbSet<CharacterAttributeType> CharacterAttributeTypes { get; set; }
        public virtual IDbSet<Hitbox> Hitbox { get; set; }
        public virtual IDbSet<BaseDamage> BaseDamage { get; set; }
        public virtual IDbSet<Angle> Angle { get; set; }
        public virtual IDbSet<BaseKnockback> BaseKnockback { get; set; }
        public virtual IDbSet<SetKnockback> SetKnockback { get; set; }
        public virtual IDbSet<KnockbackGrowth> KnockbackGrowth { get; set; }
        public virtual IDbSet<LandingLag> LandingLag { get; set; }
        public virtual IDbSet<Autocancel> Autocancel { get; set; }
    }
}