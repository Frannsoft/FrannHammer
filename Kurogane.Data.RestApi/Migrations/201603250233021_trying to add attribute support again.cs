namespace Kurogane.Data.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tryingtoaddattributesupportagain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.attributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Value = c.String(nullable: false),
                        AttributeType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.attributes");
        }
    }
}
