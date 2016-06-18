namespace KuroganeHammer.Data.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class charattrtypeidfk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CharacterAttributes", "CharacterAttributeTypeId", c => c.Int());
            CreateIndex("dbo.CharacterAttributes", "CharacterAttributeTypeId");
            AddForeignKey("dbo.CharacterAttributes", "CharacterAttributeTypeId", "dbo.CharacterAttributeTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharacterAttributes", "CharacterAttributeTypeId", "dbo.CharacterAttributeTypes");
            DropIndex("dbo.CharacterAttributes", new[] { "CharacterAttributeTypeId" });
            DropColumn("dbo.CharacterAttributes", "CharacterAttributeTypeId");
        }
    }
}
