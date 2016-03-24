namespace Kurogane.Data.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcolorthemehexstringtocharacter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.characters", "ColorTheme", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.characters", "ColorTheme");
        }
    }
}
