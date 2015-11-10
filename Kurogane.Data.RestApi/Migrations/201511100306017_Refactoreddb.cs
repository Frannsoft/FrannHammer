namespace Kurogane.Data.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refactoreddb : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Roster", newName: "Characters");
            CreateTable(
                "dbo.Moves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HitboxActive = c.String(),
                        FirstActionableFrame = c.String(),
                        BaseDamage = c.String(),
                        Angle = c.String(),
                        BaseKnockBackSetKnockback = c.String(),
                        LandingLag = c.String(),
                        AutoCancel = c.String(),
                        KnockbackGrowth = c.String(),
                        Type = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        OwnerId = c.Int(nullable: false),
                        RawName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Characters", "Style", c => c.String());
            AddColumn("dbo.Characters", "ImageUrl", c => c.String());
            AddColumn("dbo.Characters", "Description", c => c.String());
            DropColumn("dbo.Characters", "FullUrl");
            DropColumn("dbo.Characters", "FrameDataVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Characters", "FrameDataVersion", c => c.String());
            AddColumn("dbo.Characters", "FullUrl", c => c.String());
            DropColumn("dbo.Characters", "Description");
            DropColumn("dbo.Characters", "ImageUrl");
            DropColumn("dbo.Characters", "Style");
            DropTable("dbo.Moves");
            RenameTable(name: "dbo.Characters", newName: "Roster");
        }
    }
}
