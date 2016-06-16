namespace KuroganeHammer.Data.Api.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addingtablesformovesdata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Angles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.Autocancels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Cancel1 = c.String(),
                        Cancel2 = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.BaseDamages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.BaseKnockbackSetKnockbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.Hitboxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.KnockbackGrowths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.LandingLags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Frames = c.Int(nullable: false),
                        Notes = c.Double(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .Index(t => t.MoveId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LandingLags", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.KnockbackGrowths", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.Hitboxes", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.BaseKnockbackSetKnockbacks", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.BaseDamages", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.Autocancels", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.Angles", "MoveId", "dbo.Moves");
            DropIndex("dbo.LandingLags", new[] { "MoveId" });
            DropIndex("dbo.KnockbackGrowths", new[] { "MoveId" });
            DropIndex("dbo.Hitboxes", new[] { "MoveId" });
            DropIndex("dbo.BaseKnockbackSetKnockbacks", new[] { "MoveId" });
            DropIndex("dbo.BaseDamages", new[] { "MoveId" });
            DropIndex("dbo.Autocancels", new[] { "MoveId" });
            DropIndex("dbo.Angles", new[] { "MoveId" });
            DropTable("dbo.LandingLags");
            DropTable("dbo.KnockbackGrowths");
            DropTable("dbo.Hitboxes");
            DropTable("dbo.BaseKnockbackSetKnockbacks");
            DropTable("dbo.BaseDamages");
            DropTable("dbo.Autocancels");
            DropTable("dbo.Angles");
        }
    }
}
