using KuroganeHammer.Model;
using System.Data.Entity.ModelConfiguration;

namespace KuroganeHammer.Data.Configuration
{
    public class CharacterStatConfiguration : EntityTypeConfiguration<CharacterStat>
    {
        public CharacterStatConfiguration()
        {
            ToTable("characters");
            Property(c => c.Name).IsRequired();
            Property(c => c.OwnerId).IsRequired();
        }
    }
}
