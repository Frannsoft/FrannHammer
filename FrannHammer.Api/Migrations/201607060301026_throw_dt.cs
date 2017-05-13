namespace FrannHammer.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ThrowDt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Throws", "LastModified", c => c.DateTime(nullable: false));
            //DropColumn("dbo.Throws", "LastModified");
        }

        public override void Down()
        {
            DropColumn("dbo.Throws", "LastModified");
        }
    }
}
