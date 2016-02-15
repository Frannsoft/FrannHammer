﻿using Kurogane.Data.RestApi.Models;
using System.Data.Entity.ModelConfiguration;

namespace Kurogane.Data.RestApi.Configuration
{
    public class MovementStatConfiguration : EntityTypeConfiguration<MovementStat>
    {
        public MovementStatConfiguration()
        {
            ToTable("movements");
            Property(m => m.Name).IsRequired();
            Property(m => m.OwnerId).IsRequired();
            Property(m => m.Value).IsRequired();
        }
    }
}