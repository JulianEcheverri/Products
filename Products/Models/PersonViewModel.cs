using Microsoft.AspNet.Identity;
using Products.Entities;
using Products.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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

        [Display(Name= "Date Birth")]
        public DateTime DateBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name= "User name")]
        public string UserName { get; set; }

        [Display(Name= "Role")]
        public int? RoleId { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string RoleName { get; set; }
        public string DateBirthValue { get; set; }
        public string AgeValue { get; set; }
        public string GenderValue { get; set; }
        public string NationalityName { get; set; }

        public void LoadPerson(int id, bool about = false)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.PersonId == id);
                if (user == null) return;
                var userRole = db.UsersRoles.FirstOrDefault(x => x.UserId == user.Id);
                Id = user.Person.Id;
                Document = user.Person.Document;
                Name = user.Person.Name;
                LastName = user.Person.LastName;
                Gender = user.Person.Gender;
                NationalityId = user.Person.NationalityId;
                DateBirth = user.Person.DateBirth;
                Email = user.Email;
                UserName = user.UserName;
                RoleId = userRole != null ? userRole.RoleId : (int?)null;

                if (about)
                {
                     RoleName = userRole != null ? userRole.Roles.Name : string.Empty ;
                     DateBirthValue = user.Person.DateBirth.ToString("yyyy-MM-dd");
                     AgeValue = user.Person.Age.ToString();
                     GenderValue = user.Person.Gender.ToString();
                     NationalityName = user.Person.Nationality.Name;
                }
            }
        }

        public int? UpdatePerson() 
        {
            var edit = default(bool);            
            using (var db = new ApplicationDbContext())
            {
                var personRepeated = db.Persons.FirstOrDefault(x=>x.Document == Document && x.Id != Id);
                if (personRepeated != null) return null;
                var personInBd = db.Persons.FirstOrDefault(x => x.Document == Document);
                var defaultUserRole = new ApplicationDbContext().Roles.FirstOrDefault(x => x.Name == "RegularUser");
                RoleId = RoleId == null ? defaultUserRole != null ? defaultUserRole.Id : RoleId : RoleId;
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
                }
                else
                {
                    personInBd.Name = Name;
                    personInBd.LastName = LastName;
                    personInBd.DateBirth = DateBirth;
                    personInBd.Age = DateTime.Now.Year - DateBirth.Year;
                    personInBd.Gender = Gender;
                    personInBd.NationalityId = NationalityId;
                    personInBd.ModificationDate = DateTime.Now;
                    db.Persons.AddOrUpdate(personInBd);
                    db.SaveChanges();
                    edit = true;
                }
            }
            return UpdateUser(edit) ? Id : -1;
        }

        public bool UpdateUser(bool edit = false)
        {
            if (!edit)
            {
                var user = new ApplicationUser { UserName = UserName, Email = Email, PersonId = Id };
                var manager = new ApplicationUserManager(new IntUserStore(new ApplicationDbContext()));
                var result = manager.Create(user, Password);
                RoleAssignment();
                return result.Succeeded;
            }
            else
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.PersonId == Id);
                    if (user == null) return default;
                    user.Email = Email;
                    user.UserName = UserName;
                    db.Users.AddOrUpdate(user);
                    db.SaveChanges();
                    RoleAssignment();
                }
            }
            return true;
        }

        public bool RoleAssignment()
        {
            using (var db = new ApplicationDbContext())
            {
                if (RoleId == null) return false;
                var user = db.Users.FirstOrDefault(x => x.PersonId == Id);
                if (user == null) return default;
                var userRole = db.UsersRoles.FirstOrDefault(x => x.UserId == user.Id && x.RoleId == RoleId);
                if (userRole != null) return true;

                var userRoles = db.UsersRoles.Where(x => x.UserId == user.Id).ToList();
                foreach (var item in userRoles)
                {
                    db.UsersRoles.Remove(item);
                    db.SaveChanges();
                }

                var newUserRole = new IntUserRole()
                {
                    UserId = user.Id,
                    RoleId = (int)RoleId
                };

                db.UsersRoles.AddOrUpdate(newUserRole);
                db.SaveChanges();
                return true;
            }
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

                var ownUser = HttpContext.Current.User.Identity.GetUserId<int>() == user.Id;
                if (ownUser) return -2;

                var userRoles = db.UsersRoles.Where(x => x.UserId == user.Id).ToList();
                foreach (var item in userRoles)
                {
                    db.UsersRoles.Remove(item);
                    db.SaveChanges();
                }

                db.Users.Remove(user);
                db.Persons.Remove(person);
                db.SaveChanges();
            }
            return Id;
        }
    }
}