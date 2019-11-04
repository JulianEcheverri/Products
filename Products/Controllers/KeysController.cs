using Products.Entities;
using Products.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;

namespace Products.Controllers
{
    public class KeysController : ApiController
    {
        public List<VerificationKey> keys = new List<VerificationKey>();
        public KeysController() { }
        public KeysController(List<VerificationKey> keys)
        {
            this.keys = keys;
        }

        //[HttpGet]
        //public IEnumerable<VerificationKey> Get()
        //{
        //    using (var db = new ApplicationDbContext())
        //    {
        //        keys = db.VerificationKeys.ToList();
        //        return keys;
        //    }
        //}

        //[HttpGet]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpPost]
        public bool Post([FromBody]VerificationKey verificationName)
        {
            using (var db = new ApplicationDbContext())
            {
                try
                {                    
                    if (verificationName == null) return default;
                    var verificationObject = db.VerificationKeys.FirstOrDefault(x => x.VerificationName == verificationName.VerificationName);
                    if (verificationObject == null) return default;
                    verificationObject.Key = verificationName.Key;
                    verificationObject.Verification = verificationObject.VerificationName + verificationName.Key;
                    db.VerificationKeys.AddOrUpdate(verificationObject);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return default;
                }

            }
        }

        //// PUT: api/Keys/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Keys/5
        //public void Delete(int id)
        //{
        //}
    }
}
