using Products.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Entities
{
    [Table("Logins")]
    public class UserLogin
    {
        [Key, Column("UserLoginId")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogOutDate { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}