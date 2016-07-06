namespace FrannHammer.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class throws_last_modified : DbMigration
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
