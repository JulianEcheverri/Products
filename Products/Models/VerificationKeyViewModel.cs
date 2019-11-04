using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Products.Models
{
    public class VerificationKeyViewModel
    {
        [Required]
        public string Key { get; set; }
        [Display(Name = "Verification Name"), Required]
        public string VerificationName { get; set; }
    }
}