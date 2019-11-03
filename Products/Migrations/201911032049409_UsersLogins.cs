namespace Products.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersLogins : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        UserLoginId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LoginDate = c.DateTime(nullable: false),
                        LogOutDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserLoginId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logins", "UserId", "dbo.Users");
            DropIndex("dbo.Logins", new[] { "UserId" });
            DropTable("dbo.Logins");
        }
    }
}
