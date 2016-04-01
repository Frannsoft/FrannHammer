namespace Kurogane.Data.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsmashattributetypetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sm4shAttributeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sm4shAttributeTypes");
        }
    }
}
