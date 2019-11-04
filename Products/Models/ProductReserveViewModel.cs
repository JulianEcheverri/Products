using Products.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Products.Models
{
    public class ProductReserveViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; }

        public List<Product> Products { get; set; }

        public int? ReserveProduct()
        {
            using (var db = new ApplicationDbContext())
            {
                var product = db.Products.FirstOrDefault(x => x.Id == ProductId);
                if (product == null) return null;

                if (product.Amount <= 0) return -1;

                var productReserve = db.ProductsReserve.FirstOrDefault(x => x.ProductId == product.Id && x.UserId == UserId);
                if (productReserve != null) return -2;

                var newproductReserve = new ProductReserve()
                {
                    Id = default,
                    ProductId = ProductId,
                    UserId = UserId,
                    CreationDate = DateTime.Now
                };
                db.ProductsReserve.AddOrUpdate(newproductReserve);
                db.SaveChanges();
                Amount = product.Amount - 1;
                UpdateProductAmount();
                return Amount;
            }
        }

        public bool UpdateProductAmount()
        {
            using (var db = new ApplicationDbContext())
            {
                var product = db.Products.FirstOrDefault(x => x.Id == ProductId);
                if (product == null) return default;
                product.Amount = Amount;
                product.ModificationDate = DateTime.Now;
                db.Products.AddOrUpdate(product);
                db.SaveChanges();
            }
            return true;
        }

        public bool UpdateProductListAmount()
        {
            using (var db = new ApplicationDbContext())
            {
                if (Products == null) return default;
                if (Products.Any() || Products != null)
                {
                    foreach (var item in Products)
                    {
                        ProductId = item.Id;
                        Amount = item.Amount;
                        UpdateProductAmount();
                    }
                }
            }
            return true;
        }
    }
}