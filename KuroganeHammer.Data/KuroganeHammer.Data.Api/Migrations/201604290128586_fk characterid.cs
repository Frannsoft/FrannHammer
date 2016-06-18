namespace KuroganeHammer.Data.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class fkcharacterid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Angles", "CharacterId", c => c.Int(nullable: false));
            AddColumn("dbo.BaseDamages", "CharacterId", c => c.Int(nullable: false));
            AddColumn("dbo.BaseKnockbackSetKnockbacks", "CharacterId", c => c.Int(nullable: false));
            AddColumn("dbo.Hitboxes", "CharacterId", c => c.Int(nullable: false));
            AddColumn("dbo.KnockbackGrowths", "CharacterId", c => c.Int(nullable: false));
            CreateIndex("dbo.Angles", "CharacterId");
            CreateIndex("dbo.BaseDamages", "CharacterId");
            CreateIndex("dbo.BaseKnockbackSetKnockbacks", "CharacterId");
            CreateIndex("dbo.Hitboxes", "CharacterId");
            CreateIndex("dbo.KnockbackGrowths", "CharacterId");
            AddForeignKey("dbo.Angles", "CharacterId", "dbo.Characters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BaseDamages", "CharacterId", "dbo.Characters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BaseKnockbackSetKnockbacks", "CharacterId", "dbo.Characters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Hitboxes", "CharacterId", "dbo.Characters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.KnockbackGrowths", "CharacterId", "dbo.Characters", "Id", cascadeDelete: true);
            DropColumn("dbo.Angles", "OwnerId");
            DropColumn("dbo.BaseDamages", "OwnerId");
            DropColumn("dbo.BaseKnockbackSetKnockbacks", "OwnerId");
            DropColumn("dbo.Hitboxes", "OwnerId");
            DropColumn("dbo.KnockbackGrowths", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KnockbackGrowths", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.Hitboxes", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.BaseKnockbackSetKnockbacks", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.BaseDamages", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.Angles", "OwnerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.KnockbackGrowths", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.Hitboxes", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.BaseKnockbackSetKnockbacks", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.BaseDamages", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.Angles", "CharacterId", "dbo.Characters");
            DropIndex("dbo.KnockbackGrowths", new[] { "CharacterId" });
            DropIndex("dbo.Hitboxes", new[] { "CharacterId" });
            DropIndex("dbo.BaseKnockbackSetKnockbacks", new[] { "CharacterId" });
            DropIndex("dbo.BaseDamages", new[] { "CharacterId" });
            DropIndex("dbo.Angles", new[] { "CharacterId" });
            DropColumn("dbo.KnockbackGrowths", "CharacterId");
            DropColumn("dbo.Hitboxes", "CharacterId");
            DropColumn("dbo.BaseKnockbackSetKnockbacks", "CharacterId");
            DropColumn("dbo.BaseDamages", "CharacterId");
            DropColumn("dbo.Angles", "CharacterId");
        }
    }
}
