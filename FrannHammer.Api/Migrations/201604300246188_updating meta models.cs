using System.Data.Entity.Migrations;

namespace FrannHammer.Api.Migrations
{
    public partial class updatingmetamodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Autocancels", "CharacterId", c => c.Int(nullable: false));
            AddColumn("dbo.Autocancels", "Notes", c => c.String());
            AddColumn("dbo.LandingLags", "CharacterId", c => c.Int(nullable: false));
            AlterColumn("dbo.LandingLags", "Notes", c => c.String());
            CreateIndex("dbo.Autocancels", "CharacterId");
            CreateIndex("dbo.LandingLags", "CharacterId");
            AddForeignKey("dbo.Autocancels", "CharacterId", "dbo.Characters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LandingLags", "CharacterId", "dbo.Characters", "Id", cascadeDelete: true);
            DropColumn("dbo.Autocancels", "OwnerId");
            DropColumn("dbo.LandingLags", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LandingLags", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.Autocancels", "OwnerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.LandingLags", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.Autocancels", "CharacterId", "dbo.Characters");
            DropIndex("dbo.LandingLags", new[] { "CharacterId" });
            DropIndex("dbo.Autocancels", new[] { "CharacterId" });
            AlterColumn("dbo.LandingLags", "Notes", c => c.Double(nullable: false));
            DropColumn("dbo.LandingLags", "CharacterId");
            DropColumn("dbo.Autocancels", "Notes");
            DropColumn("dbo.Autocancels", "CharacterId");
        }
    }
}
