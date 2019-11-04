using System.ComponentModel.DataAnnotations;

namespace Products.Entities
{
    public class VerificationKey
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string VerificationName { get; set; }
        [Required]
        public string Verification { get; set; }
    }
}