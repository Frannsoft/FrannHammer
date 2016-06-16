namespace KuroganeHammer.Data.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class notation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NotationType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notations");
        }
    }
}
