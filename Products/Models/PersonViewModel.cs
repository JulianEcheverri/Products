using Microsoft.AspNet.Identity;
using Products.Entities;
using Products.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Products.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(12)]
        public string Document { get; set; }
        
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-ZÑÁÉÍÓÚñáéíóú\s]{1,20}$", ErrorMessage = "The {0} must be at least 20 characters long")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Last name")]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-ZÑÁÉÍÓÚñáéíóú\s]{1,20}$", ErrorMessage = "The {0} must be at least 20 characters long")]
        public string LastName { get; set; }

        [Range(0, 90)] 
        public int Age { get; set; }
        public EGender Gender { get; set; }
        [Display(Name= "Nationality")]
        public int NationalityId { get; set; }

        public DateTime DateBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name= "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public List<Person> Persons { get; set; }

        public void LoadPersons() 
        {
            using (var db = new ApplicationDbContext())
            {
                Persons = db.Persons.Include("Nationality").ToList();
            }
        }

        public int? UpdatePerson() 
        {
            using (var db = new ApplicationDbContext())
            {
                var personRepeated = db.Persons.FirstOrDefault(x=>x.Document == Document && x.Id != Id);
                if (personRepeated != null) return null;
                var personInBd = db.Persons.FirstOrDefault(x => x.Document == Document);
                if (personInBd == null)
                {
                    var userNameRepeated = db.Users.FirstOrDefault(x => x.UserName == UserName);
                    if (userNameRepeated != null) return default(int);

                    var person = new Person()
                    {
                        Id = Id,
                        Document = Document,
                        Name = Name,
                        LastName = LastName,
                        DateBirth = DateBirth,
                        Age = DateTime.Now.Year - DateBirth.Year,
                        Gender = Gender,
                        NationalityId = NationalityId,
                        CreationDate = DateTime.Now
                    };
                    db.Persons.AddOrUpdate(person);
                    db.SaveChanges();
                    Id = person.Id;
                    return UserCreation() ? Id : -1;
                }
                else
                {
                    personInBd.Name = Name;
                    personInBd.LastName = LastName;
                    personInBd.Age = Age;
                    personInBd.Gender = Gender;
                    personInBd.NationalityId = NationalityId;
                    personInBd.ModificationDate = DateTime.Now;
                    db.Persons.AddOrUpdate(personInBd);
                    db.SaveChanges();
                }
            }
            return Id;
        }

        public bool UserCreation()
        {
            var user = new ApplicationUser { UserName = UserName, Email = Email, PersonId = Id };
            var manager = new ApplicationUserManager(new IntUserStore(new ApplicationDbContext()));
            var result = manager.Create(user, Password);
            return result.Succeeded;
        }

        public int? DeletePerson() 
        {
            using (var db = new ApplicationDbContext())
            {
                var person = db.Persons.FirstOrDefault(x => x.Id == Id);
                if (person == null) return null;

                var user = db.Users.FirstOrDefault(x => x.PersonId == person.Id);
                if (user == null) return null;

                var products = db.Products.Where(x => x.CreatorUserId == user.Id).ToList();
                if (products.Any() || products.Count() > default(int)) return -1;

                db.Users.Remove(user);
                db.Persons.Remove(person);
                db.SaveChanges();
            }
            return null;
        }
    }
}