using Microsoft.AspNet.Identity;
using Products.Models;
using System.Linq;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class PersonController : Controller
    {
        public int? UpdatePerson(PersonViewModel personViewModel) 
        {
            return ModelState.IsValid ? personViewModel.UpdatePerson() : null;
        }

        public int? DeletePerson(PersonViewModel personViewModel)
        {
            if (ModelState.IsValid)
            {
                return personViewModel.DeletePerson();
            }
            return null;
        }

        public bool UserNameVerification(PersonViewModel personViewModel)
        {
            return new ApplicationDbContext().Users.FirstOrDefault(x => x.UserName == personViewModel.UserName) != null;
        }
    }
}