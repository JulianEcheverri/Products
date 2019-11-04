using Products.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Column("ProductId")] 
        public int Id { get; set; }
        [Required, StringLength(100), Index("IX_Product", IsUnique = true)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int CreatorUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public ApplicationUser CreatorUser { get; set; }
    }
}