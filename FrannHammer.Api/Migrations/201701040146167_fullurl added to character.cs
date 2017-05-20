namespace FrannHammer.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class fullurladdedtocharacter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "FullUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "FullUrl");
        }
    }
}
