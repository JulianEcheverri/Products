using Microsoft.AspNet.Identity;
using Products.Models;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class ProductController : Controller
    {
        [Authorize(Roles = "RegularUser")]
        public ActionResult Products()
        {
            using (var db = new ApplicationDbContext())
            {
                var products = db.Products.ToList();
                return View(products);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductsAdmin()
        {
            using (var db = new ApplicationDbContext())
            {
                var products = db.Products.ToList();
                return View(products);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductsReserveAdmin()
        {
            using (var db = new ApplicationDbContext())
            {
                var products = db.Products.ToList();
                return View(products);
            }
        }

        [Authorize(Roles = "RegularUser, Admin")]
        public ActionResult ProductsReserve()
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = User.Identity.GetUserId<int>();
                var productsReserve = db.ProductsReserve.Include("Product").Where(x => x.UserId == userId).ToList();
                return View(productsReserve);
            }
        }

        [HttpPost]
        public int? ReserveProduct(ProductReserveViewModel productReserveViewModel)
        {
            productReserveViewModel.UserId = User.Identity.GetUserId<int>();
            return productReserveViewModel.ReserveProduct();
        }

        public bool UpdateAmount(ProductReserveViewModel productReserveViewModel)
        {
            return productReserveViewModel.UpdateProductListAmount();
        }
    }
}