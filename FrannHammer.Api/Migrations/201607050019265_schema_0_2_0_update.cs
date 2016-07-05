namespace FrannHammer.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schema_0_2_0_update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Angles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Hitbox6 = c.String(),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        RawValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.Moves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HitboxActive = c.String(),
                        FirstActionableFrame = c.String(),
                        BaseDamage = c.String(),
                        Angle = c.String(),
                        BaseKnockBackSetKnockback = c.String(),
                        LandingLag = c.String(),
                        AutoCancel = c.String(),
                        KnockbackGrowth = c.String(),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        OwnerId = c.Int(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Style = c.String(),
                        MainImageUrl = c.String(),
                        ThumbnailUrl = c.String(),
                        Description = c.String(),
                        ColorTheme = c.String(),
                        Name = c.String(),
                        DisplayName = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Autocancels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cancel1 = c.String(),
                        Cancel2 = c.String(),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        RawValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.BaseDamages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Hitbox6 = c.String(),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        RawValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.BaseKnockbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Hitbox6 = c.String(),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        RawValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.CharacterAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        Rank = c.String(),
                        Value = c.String(),
                        Name = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        SmashAttributeTypeId = c.Int(nullable: false),
                        CharacterAttributeTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CharacterAttributeTypes", t => t.CharacterAttributeTypeId)
                .ForeignKey("dbo.SmashAttributeTypes", t => t.SmashAttributeTypeId, cascadeDelete: true)
                .Index(t => t.SmashAttributeTypeId)
                .Index(t => t.CharacterAttributeTypeId);
            
            CreateTable(
                "dbo.CharacterAttributeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NotationId = c.Int(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notations", t => t.NotationId, cascadeDelete: true)
                .Index(t => t.NotationId);
            
            CreateTable(
                "dbo.Notations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SmashAttributeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hitboxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Hitbox6 = c.String(),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        RawValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.KnockbackGrowths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Hitbox6 = c.String(),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        RawValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.LandingLags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Frames = c.Int(nullable: false),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        RawValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.Movements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OwnerId = c.Int(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SetKnockbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hitbox1 = c.String(),
                        Hitbox2 = c.String(),
                        Hitbox3 = c.String(),
                        Hitbox4 = c.String(),
                        Hitbox5 = c.String(),
                        Hitbox6 = c.String(),
                        OwnerId = c.Int(nullable: false),
                        MoveId = c.Int(nullable: false),
                        Notes = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        RawValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.MoveId);
            
            CreateTable(
                "dbo.Throws",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MoveId = c.Int(nullable: false),
                        ThrowTypeId = c.Int(nullable: false),
                        WeightDependent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moves", t => t.MoveId, cascadeDelete: true)
                .ForeignKey("dbo.ThrowTypes", t => t.ThrowTypeId, cascadeDelete: true)
                .Index(t => t.MoveId)
                .Index(t => t.ThrowTypeId);
            
            CreateTable(
                "dbo.ThrowTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Throws", "ThrowTypeId", "dbo.ThrowTypes");
            DropForeignKey("dbo.Throws", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.SetKnockbacks", "OwnerId", "dbo.Characters");
            DropForeignKey("dbo.SetKnockbacks", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LandingLags", "OwnerId", "dbo.Characters");
            DropForeignKey("dbo.LandingLags", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.KnockbackGrowths", "OwnerId", "dbo.Characters");
            DropForeignKey("dbo.KnockbackGrowths", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.Hitboxes", "OwnerId", "dbo.Characters");
            DropForeignKey("dbo.Hitboxes", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.CharacterAttributes", "SmashAttributeTypeId", "dbo.SmashAttributeTypes");
            DropForeignKey("dbo.CharacterAttributes", "CharacterAttributeTypeId", "dbo.CharacterAttributeTypes");
            DropForeignKey("dbo.CharacterAttributeTypes", "NotationId", "dbo.Notations");
            DropForeignKey("dbo.BaseKnockbacks", "OwnerId", "dbo.Characters");
            DropForeignKey("dbo.BaseKnockbacks", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.BaseDamages", "OwnerId", "dbo.Characters");
            DropForeignKey("dbo.BaseDamages", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.Autocancels", "OwnerId", "dbo.Characters");
            DropForeignKey("dbo.Autocancels", "MoveId", "dbo.Moves");
            DropForeignKey("dbo.Angles", "OwnerId", "dbo.Characters");
            DropForeignKey("dbo.Angles", "MoveId", "dbo.Moves");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Throws", new[] { "ThrowTypeId" });
            DropIndex("dbo.Throws", new[] { "MoveId" });
            DropIndex("dbo.SetKnockbacks", new[] { "MoveId" });
            DropIndex("dbo.SetKnockbacks", new[] { "OwnerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LandingLags", new[] { "MoveId" });
            DropIndex("dbo.LandingLags", new[] { "OwnerId" });
            DropIndex("dbo.KnockbackGrowths", new[] { "MoveId" });
            DropIndex("dbo.KnockbackGrowths", new[] { "OwnerId" });
            DropIndex("dbo.Hitboxes", new[] { "MoveId" });
            DropIndex("dbo.Hitboxes", new[] { "OwnerId" });
            DropIndex("dbo.CharacterAttributeTypes", new[] { "NotationId" });
            DropIndex("dbo.CharacterAttributes", new[] { "CharacterAttributeTypeId" });
            DropIndex("dbo.CharacterAttributes", new[] { "SmashAttributeTypeId" });
            DropIndex("dbo.BaseKnockbacks", new[] { "MoveId" });
            DropIndex("dbo.BaseKnockbacks", new[] { "OwnerId" });
            DropIndex("dbo.BaseDamages", new[] { "MoveId" });
            DropIndex("dbo.BaseDamages", new[] { "OwnerId" });
            DropIndex("dbo.Autocancels", new[] { "MoveId" });
            DropIndex("dbo.Autocancels", new[] { "OwnerId" });
            DropIndex("dbo.Angles", new[] { "MoveId" });
            DropIndex("dbo.Angles", new[] { "OwnerId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ThrowTypes");
            DropTable("dbo.Throws");
            DropTable("dbo.SetKnockbacks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Movements");
            DropTable("dbo.LandingLags");
            DropTable("dbo.KnockbackGrowths");
            DropTable("dbo.Hitboxes");
            DropTable("dbo.SmashAttributeTypes");
            DropTable("dbo.Notations");
            DropTable("dbo.CharacterAttributeTypes");
            DropTable("dbo.CharacterAttributes");
            DropTable("dbo.BaseKnockbacks");
            DropTable("dbo.BaseDamages");
            DropTable("dbo.Autocancels");
            DropTable("dbo.Characters");
            DropTable("dbo.Moves");
            DropTable("dbo.Angles");
        }
    }
}
