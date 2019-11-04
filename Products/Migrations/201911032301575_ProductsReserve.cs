namespace Products.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsReserve : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductsReserve",
                c => new
                    {
                        ProductReserveId = c.Int(nullable: false, identity: true),
                        AmountReserve = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductReserveId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Products", "ModificationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsReserve", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProductsReserve", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductsReserve", new[] { "UserId" });
            DropIndex("dbo.ProductsReserve", new[] { "ProductId" });
            AlterColumn("dbo.Products", "ModificationDate", c => c.DateTime(nullable: false));
            DropTable("dbo.ProductsReserve");
        }
    }
}
