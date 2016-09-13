namespace FrannHammer.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThrowDt2 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Throws", "LastModified", c => c.DateTime(nullable: false));
            //DropColumn("dbo.Throws", "LastEdited");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Throws", "LastEdited", c => c.DateTime(nullable: false));
            //DropColumn("dbo.Throws", "LastModified");
        }
    }
}
