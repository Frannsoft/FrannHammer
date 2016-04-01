using System.Data.Entity.ModelConfiguration;
using Kurogane.Data.RestApi.Models;

namespace Kurogane.Data.RestApi.Configuration
{
    public class SmashAttributeConfiguration : EntityTypeConfiguration<SmashAttributeType>
    {
        public SmashAttributeConfiguration()
        {
            ToTable("SmashAttributeTypes");
            Property(a => a.Name).IsRequired();
        }
    }
}