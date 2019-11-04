using Products.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Products.Entities
{
    [Table("ProductsReserve")]
    public class ProductReserve
    {
        [Key]
        [Column("ProductReserveId")]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}