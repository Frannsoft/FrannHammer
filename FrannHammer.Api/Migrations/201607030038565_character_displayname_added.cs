namespace FrannHammer.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class character_displayname_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "DisplayName");
        }
    }
}
