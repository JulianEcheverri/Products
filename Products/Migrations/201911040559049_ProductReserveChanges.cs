namespace Products.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductReserveChanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductsReserve", "AmountReserve");
            DropColumn("dbo.ProductsReserve", "ModificationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductsReserve", "ModificationDate", c => c.DateTime());
            AddColumn("dbo.ProductsReserve", "AmountReserve", c => c.Int(nullable: false));
        }
    }
}
