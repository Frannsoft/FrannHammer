using Kurogane.Data.RestApi.Models;
using System.Data.Entity.ModelConfiguration;

namespace Kurogane.Data.RestApi.Configuration
{
    public class MoveStatConfiguration : EntityTypeConfiguration<MoveStat>
    {
        public MoveStatConfiguration()
        {
            ToTable("moves");
            Property(m => m.Name).IsRequired();
            Property(m => m.OwnerId).IsRequired();
            Property(m => m.Type).IsRequired();
        }
    }
}
