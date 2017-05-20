namespace FrannHammer.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class faftable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FirstActionableFrames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastModified = c.DateTime(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Frame = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FirstActionableFrames");
        }
    }
}
