namespace FrannHammer.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class fafsupport2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FirstActionableFrames", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.FirstActionableFrames", "Notes", c => c.String());
            AddColumn("dbo.FirstActionableFrames", "RawValue", c => c.String());
            AlterColumn("dbo.FirstActionableFrames", "Frame", c => c.String());
            CreateIndex("dbo.FirstActionableFrames", "OwnerId");
            CreateIndex("dbo.FirstActionableFrames", "MoveId");
            AddForeignKey("dbo.FirstActionableFrames", "MoveId", "dbo.Moves", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FirstActionableFrames", "OwnerId", "dbo.Characters", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FirstActionableFrames", "OwnerId", "dbo.Characters");
            DropForeignKey("dbo.FirstActionableFrames", "MoveId", "dbo.Moves");
            DropIndex("dbo.FirstActionableFrames", new[] { "MoveId" });
            DropIndex("dbo.FirstActionableFrames", new[] { "OwnerId" });
            AlterColumn("dbo.FirstActionableFrames", "Frame", c => c.Int(nullable: false));
            DropColumn("dbo.FirstActionableFrames", "RawValue");
            DropColumn("dbo.FirstActionableFrames", "Notes");
            DropColumn("dbo.FirstActionableFrames", "OwnerId");
        }
    }
}
