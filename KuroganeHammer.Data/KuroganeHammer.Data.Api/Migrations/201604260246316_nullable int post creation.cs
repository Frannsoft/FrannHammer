namespace KuroganeHammer.Data.Api.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class nullableintpostcreation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SmashAttributeTypes", "NotationId", "dbo.Notations");
            DropIndex("dbo.SmashAttributeTypes", new[] { "NotationId" });
            AlterColumn("dbo.SmashAttributeTypes", "NotationId", c => c.Int());
            CreateIndex("dbo.SmashAttributeTypes", "NotationId");
            AddForeignKey("dbo.SmashAttributeTypes", "NotationId", "dbo.Notations", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.SmashAttributeTypes", "NotationId", "dbo.Notations");
            DropIndex("dbo.SmashAttributeTypes", new[] { "NotationId" });
            AlterColumn("dbo.SmashAttributeTypes", "NotationId", c => c.Int(nullable: false));
            CreateIndex("dbo.SmashAttributeTypes", "NotationId");
            AddForeignKey("dbo.SmashAttributeTypes", "NotationId", "dbo.Notations", "Id", cascadeDelete: true);
        }
    }
}
