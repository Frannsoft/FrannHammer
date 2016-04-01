using Kurogane.Data.RestApi.Models;
using System.Data.Entity.ModelConfiguration;

namespace Kurogane.Data.RestApi.Configuration
{
    public class CharacterStatConfiguration : EntityTypeConfiguration<CharacterStat>
    {
        public CharacterStatConfiguration()
        {
            ToTable("characters");
            Property(c => c.Name).IsRequired();
            //Property(c => c.).IsRequired();
        }
    }
}
