namespace Kurogane.Data.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redoingcharactertabletoomitownerid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.attributes", "LastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.characters", "LastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.movements", "LastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.moves", "LastModified", c => c.DateTime(nullable: false));
            DropColumn("dbo.characters", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.characters", "OwnerId", c => c.Int(nullable: false));
            DropColumn("dbo.moves", "LastModified");
            DropColumn("dbo.movements", "LastModified");
            DropColumn("dbo.characters", "LastModified");
            DropColumn("dbo.attributes", "LastModified");
        }
    }
}
