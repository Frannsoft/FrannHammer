using System.Data.Entity.ModelConfiguration;
using Kurogane.Data.RestApi.Models;

namespace Kurogane.Data.RestApi.Configuration
{
    public class CharacterAttributeConfiguration : EntityTypeConfiguration<CharacterAttribute>
    {
        public CharacterAttributeConfiguration()
        {
            ToTable("attributes");
            Property(a => a.Name).IsRequired();
            Property(a => a.Value).IsRequired();
            Property(a => a.AttributeType).IsRequired();
        }
    }
}