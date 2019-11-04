using Products.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Products.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VerificationKeyController : Controller
    {
        public ActionResult VerificationForm()
        {
            return PartialView("_VerificationKey");
        }

        [HttpPost]
        public async Task<bool> VerificationKeyValue(VerificationKeyViewModel verificationKeyViewModel)
        {
            var path = Request.Url.GetLeftPart(UriPartial.Authority);
            var route = path + "/api/Keys";
            var json = new JavaScriptSerializer().Serialize(verificationKeyViewModel);
            var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.PostAsync(route, stringContent);
                    var result = await response.Content.ReadAsAsync<bool>();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return default;
                }
            }
        }
    }
}