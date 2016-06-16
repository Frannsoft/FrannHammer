namespace KuroganeHammer.Data.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addcharacterattributetype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CharacterAttributeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CharacterAttributeTypes");
        }
    }
}
