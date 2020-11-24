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
    public class SubCategoriaController : Controller
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
        private string baseURL = "https://localhost:44331/";
        public ActionResult Lista()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseURL);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session["token"].ToString());

            HttpResponseMessage response = httpClient.GetAsync("api/SubCategoria").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", "Token");
            }
            else
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<SubCategoriaCLS> cat = JsonConvert.DeserializeObject<List<SubCategoriaCLS>>(data);

                return Json(
                   new
                   {
                       success = true,
                       data = cat,
                       message = "donde"
                   },
                   JsonRequestBehavior.AllowGet
                   );
            }

        }

        public ActionResult Guardar(int IdSubCategoria, string Nombre, string Descripcion)
        {
            if (!usuarioAutenticado())
            {
                return Json(
                          new
                          {
                              success = false,
                              message = "Usuario no autenticado"
                          }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                SubCategoriaCLS cat = new SubCategoriaCLS();
                cat.IdSubCategoria = IdSubCategoria;
                cat.Nombre = Nombre;
                cat.Descripcion = Descripcion;

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseURL);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session["token"].ToString());

                string productoJson = JsonConvert.SerializeObject(cat);
                HttpContent body = new StringContent(productoJson, Encoding.UTF8, "application/json");

                if (IdSubCategoria == 0)
                {
                    HttpResponseMessage response = httpClient.PostAsync("api/SubCategoria", body).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(
                            new
                            {
                                success = true,
                                message = "Se creo una categoria"
                            }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Token");
                    }
                }
                else
                {
                    HttpResponseMessage response = httpClient.PutAsync($"api/SubCategoria/{IdSubCategoria}", body).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(
                            new
                            {
                                success = true,
                                message = "Se edito una categoia"
                            }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Token");
                    }
                }
                throw new Exception("Error al guardar");
            }
            catch (Exception ex)
            {
                return Json(
                          new
                          {
                              success = false,
                              message = ex.InnerException
                          }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult eliminar(int IdSubCategoria)
        {
            if (!usuarioAutenticado())
            {
                return Json(
                          new
                          {
                              success = false,
                              message = "Usuario no autenticado"
                          }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseURL);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session["token"].ToString());

                HttpResponseMessage response = httpClient.DeleteAsync($"api/SubCategoria/{IdSubCategoria}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return Json(
                        new
                        {
                            success = true,
                            message = "Se elimino una subcategoria"
                        }, JsonRequestBehavior.AllowGet);
                }
                throw new Exception("Error al eliminar");
            }
            catch (Exception ex)
            {
                return Json(
                        new
                        {
                            success = false,
                            message = ex.InnerException
                        }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}