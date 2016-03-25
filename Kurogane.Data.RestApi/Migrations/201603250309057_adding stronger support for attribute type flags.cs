namespace Kurogane.Data.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingstrongersupportforattributetypeflags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.attributes", "Rank", c => c.String());
            AddColumn("dbo.attributes", "OwnerId", c => c.Int(nullable: false));
            AlterColumn("dbo.attributes", "AttributeType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.attributes", "AttributeType", c => c.String(nullable: false));
            DropColumn("dbo.attributes", "OwnerId");
            DropColumn("dbo.attributes", "Rank");
        }
    }
}
