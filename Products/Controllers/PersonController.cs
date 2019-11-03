using Microsoft.AspNet.Identity;
using Products.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult Persons()
        {
            var users = new ApplicationDbContext().Users.Include("Person").Include("Person.Nationality").ToList();
            return View(users);
        }

        public ActionResult CreatePerson() 
        {
          
            return PartialView("_CreatePerson");
        }

        public ActionResult EditPerson(int personid)
        {
            var personViewModel = new PersonViewModel();
            personViewModel.LoadPerson(personid);
            ModelState.Clear();
            return PartialView("_EditPerson", personViewModel);
        }

        public int? UpdatePerson(PersonViewModel personViewModel) 
        {
            return ModelState.IsValid ? personViewModel.UpdatePerson() : null;
        }

        public int? DeletePerson(PersonViewModel personViewModel) => personViewModel.DeletePerson();

        public bool UserNameVerification(PersonViewModel personViewModel)
        {
            return new ApplicationDbContext().Users.FirstOrDefault(x => x.UserName == personViewModel.UserName) != null;
        }
    }
}