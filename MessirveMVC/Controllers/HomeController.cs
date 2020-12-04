using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessirveMVC.Controllers
{
    public class HomeController : Controller
    {
        private bool usuarioAutenticado()
        {
            return HttpContext.Session["token"] != null;
        }
        public ActionResult Index()
        {
            if (!usuarioAutenticado())
            {
                return RedirectToAction("Index", "Token");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}