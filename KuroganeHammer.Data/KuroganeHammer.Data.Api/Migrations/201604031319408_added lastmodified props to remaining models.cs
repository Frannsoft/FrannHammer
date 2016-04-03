namespace KuroganeHammer.Data.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addedlastmodifiedpropstoremainingmodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CharacterAttributes", "LastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.SmashAttributeTypes", "LastModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SmashAttributeTypes", "LastModified");
            DropColumn("dbo.CharacterAttributes", "LastModified");
        }
    }
}
