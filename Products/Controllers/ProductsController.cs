using Products.Entities;
using Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Products.Controllers
{
    public class ProductsController : ApiController
    {
        public List<Product> products = new List<Product>();

        public ProductsController() { }

        public ProductsController(List<Product> products)
        {
            this.products = products;
        }

        // GET: api/ProductAPI/5
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            using (var db = new ApplicationDbContext())
            {
                products = db.Products.ToList();
                return products;
            }
        }

        // GET: api/Product/5
        public IEnumerable<Product> GetProduct(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                products = db.Products.Where(x=> x.Id == id).ToList();
                return products;
            }
        }

        // POST: api/ProductAPI
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ProductAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProductAPI/5
        public void Delete(int id)
        {
        }
    }
}
