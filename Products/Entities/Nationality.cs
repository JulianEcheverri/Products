using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Entities
{
    [Table("Nationalities")]
    public class Nationality
    {
        [Key]
        [Column("NationalityId")] 
        public int Id { get; set; }
        [Required, StringLength(100), Index("IX_Nationality", IsUnique = true)]
        public string Name { get; set; }
    }
}