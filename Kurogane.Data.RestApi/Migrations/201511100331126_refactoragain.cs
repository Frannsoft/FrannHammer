namespace Kurogane.Data.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoragain : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AerialStats", "RawName");
            DropColumn("dbo.GroundStats", "RawName");
            DropColumn("dbo.MovementStats", "RawName");
            DropColumn("dbo.Moves", "RawName");
            DropColumn("dbo.SpecialStats", "RawName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpecialStats", "RawName", c => c.String(nullable: false));
            AddColumn("dbo.Moves", "RawName", c => c.String(nullable: false));
            AddColumn("dbo.MovementStats", "RawName", c => c.String(nullable: false));
            AddColumn("dbo.GroundStats", "RawName", c => c.String(nullable: false));
            AddColumn("dbo.AerialStats", "RawName", c => c.String(nullable: false));
        }
    }
}
