namespace Products.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VerificationKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VerificationKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false),
                        VerificationName = c.String(nullable: false),
                        Verification = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VerificationKeys");
        }
    }
}
