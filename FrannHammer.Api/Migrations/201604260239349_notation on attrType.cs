using System.Data.Entity.Migrations;

namespace FrannHammer.Api.Migrations
{
    public partial class notationonattrType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SmashAttributeTypes", "NotationId", c => c.Int(nullable: true));
            CreateIndex("dbo.SmashAttributeTypes", "NotationId");
            AddForeignKey("dbo.SmashAttributeTypes", "NotationId", "dbo.Notations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SmashAttributeTypes", "NotationId", "dbo.Notations");
            DropIndex("dbo.SmashAttributeTypes", new[] { "NotationId" });
            DropColumn("dbo.SmashAttributeTypes", "NotationId");
        }
    }
}
