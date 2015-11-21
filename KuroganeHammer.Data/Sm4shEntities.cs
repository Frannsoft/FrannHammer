using KuroganeHammer.Data.Configuration;
using KuroganeHammer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuroganeHammer.Data
{
    public class Sm4shEntities : DbContext
    {
        public DbSet<MovementStat> MovementStats { get; set; }
        public DbSet<MoveStat> Moves { get; set; }
        public DbSet<CharacterStat> Characters { get; set; }

        public Sm4shEntities() 
            : base("Sm4shEntities")
        { }

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
