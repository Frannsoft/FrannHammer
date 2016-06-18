namespace KuroganeHammer.Data.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addedsmashattributetypeidproptocharacterattributemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CharacterAttributes", "SmashAttributeType_Id", "dbo.SmashAttributeTypes");
            DropIndex("dbo.CharacterAttributes", new[] { "SmashAttributeType_Id" });
            RenameColumn(table: "dbo.CharacterAttributes", name: "SmashAttributeType_Id", newName: "SmashAttributeTypeId");
            AlterColumn("dbo.CharacterAttributes", "SmashAttributeTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.CharacterAttributes", "SmashAttributeTypeId");
            AddForeignKey("dbo.CharacterAttributes", "SmashAttributeTypeId", "dbo.SmashAttributeTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharacterAttributes", "SmashAttributeTypeId", "dbo.SmashAttributeTypes");
            DropIndex("dbo.CharacterAttributes", new[] { "SmashAttributeTypeId" });
            AlterColumn("dbo.CharacterAttributes", "SmashAttributeTypeId", c => c.Int());
            RenameColumn(table: "dbo.CharacterAttributes", name: "SmashAttributeTypeId", newName: "SmashAttributeType_Id");
            CreateIndex("dbo.CharacterAttributes", "SmashAttributeType_Id");
            AddForeignKey("dbo.CharacterAttributes", "SmashAttributeType_Id", "dbo.SmashAttributeTypes", "Id");
        }
    }
}
