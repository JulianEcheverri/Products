namespace Products.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataBaseMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nationalities",
                c => new
                    {
                        NationalityId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.NationalityId)
                .Index(t => t.Name, unique: true, name: "IX_Nationality");
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Document = c.String(nullable: false, maxLength: 12),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        DateBirth = c.DateTime(nullable: false),
                        Gender = c.Byte(nullable: false),
                        NationalityId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Nationalities", t => t.NationalityId)
                .Index(t => t.Document, unique: true, name: "IX_Person")
                .Index(t => t.NationalityId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        Amount = c.Int(nullable: false),
                        CreatorUserId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Users", t => t.CreatorUserId)
                .Index(t => t.Name, unique: true, name: "IX_Product")
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
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
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.Name, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UsersClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UsersLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UsersRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CreatorUserId", "dbo.Users");
            DropForeignKey("dbo.UsersRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UsersRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.UsersLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Persons", "NationalityId", "dbo.Nationalities");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UsersRoles", new[] { "RoleId" });
            DropIndex("dbo.UsersRoles", new[] { "UserId" });
            DropIndex("dbo.UsersLogins", new[] { "UserId" });
            DropIndex("dbo.UsersClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "PersonId" });
            DropIndex("dbo.Products", new[] { "CreatorUserId" });
            DropIndex("dbo.Products", "IX_Product");
            DropIndex("dbo.Persons", new[] { "NationalityId" });
            DropIndex("dbo.Persons", "IX_Person");
            DropIndex("dbo.Nationalities", "IX_Nationality");
            DropTable("dbo.Roles");
            DropTable("dbo.UsersRoles");
            DropTable("dbo.UsersLogins");
            DropTable("dbo.UsersClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Persons");
            DropTable("dbo.Nationalities");
        }
    }
}
