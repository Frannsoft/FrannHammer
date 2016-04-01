namespace Kurogane.Data.RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedcharacterattributetorefsmashattributetypeidinsteadofenum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.attributes", "SmashAttributeTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.attributes", "AttributeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.attributes", "AttributeType", c => c.Int(nullable: false));
            DropColumn("dbo.attributes", "SmashAttributeTypeId");
        }
    }
}
