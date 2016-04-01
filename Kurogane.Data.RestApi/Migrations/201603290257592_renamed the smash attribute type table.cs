namespace Kurogane.Data.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamedthesmashattributetypetable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Sm4shAttributeTypes", newName: "SmashAttributeTypes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SmashAttributeTypes", newName: "Sm4shAttributeTypes");
        }
    }
}
