namespace KuroganeHammer.Data.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingdatetimetonewmodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CharacterAttributeTypes", "LastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notations", "LastModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notations", "LastModified");
            DropColumn("dbo.CharacterAttributeTypes", "LastModified");
        }
    }
}
