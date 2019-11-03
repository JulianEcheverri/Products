using Products.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Entities
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        [Column("PersonId")] 
        public int Id { get; set; }
        [Required, StringLength(12), Index("IX_Person", IsUnique = true)]
        public string Document { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime DateBirth { get; set; }
        public EGender Gender { get; set; }
        public int NationalityId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public virtual Nationality Nationality { get; set; }
    }
}