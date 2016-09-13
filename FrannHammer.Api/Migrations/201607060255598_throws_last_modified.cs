namespace FrannHammer.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThrowsLastModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThrowTypes", "LastModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThrowTypes", "LastModified");
        }
    }
}
