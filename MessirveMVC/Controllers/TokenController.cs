using MessirveMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MessirveMVC.Controllers
{
    public class TokenController : Controller
    {
        private string baseUrl = "https://localhost:44331";
        // GET: Token
        public ActionResult Index()
        {
            if (HttpContext.Session["token"] == null)
            {
                ViewBag.Message = "";
            }
            else
            {
                return RedirectToAction("Index", "Home");
               
            }
            Usuario model = new Usuario();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(Usuario user)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            string stringData = JsonConvert.SerializeObject(user);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("api/Token", contentData).Result;
            string stringJWT = response.Content.ReadAsStringAsync().Result;
            Token token = JsonConvert.DeserializeObject<Token>(stringJWT);
            if (token != null)
            {
                HttpContext.Session.Add("token", token.AccessToken);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Credenciales incorrectas";
            }

            return View("Index");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("token");

            ViewBag.Message = "Usuario salio de la sesion";
            return View("Index");

        }
    }
}