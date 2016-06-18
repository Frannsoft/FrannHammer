namespace KuroganeHammer.Data.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class notationoncharattrtypenow : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SmashAttributeTypes", "NotationId", "dbo.Notations");
            DropIndex("dbo.SmashAttributeTypes", new[] { "NotationId" });
            AddColumn("dbo.CharacterAttributeTypes", "NotationId", c => c.Int());
            CreateIndex("dbo.CharacterAttributeTypes", "NotationId");
            AddForeignKey("dbo.CharacterAttributeTypes", "NotationId", "dbo.Notations", "Id");
            DropColumn("dbo.SmashAttributeTypes", "NotationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SmashAttributeTypes", "NotationId", c => c.Int());
            DropForeignKey("dbo.CharacterAttributeTypes", "NotationId", "dbo.Notations");
            DropIndex("dbo.CharacterAttributeTypes", new[] { "NotationId" });
            DropColumn("dbo.CharacterAttributeTypes", "NotationId");
            CreateIndex("dbo.SmashAttributeTypes", "NotationId");
            AddForeignKey("dbo.SmashAttributeTypes", "NotationId", "dbo.Notations", "Id");
        }
    }
}
