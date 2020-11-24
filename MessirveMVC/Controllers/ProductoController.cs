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
    public class ProductoController : Controller
    {
        // GET: Producto

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

            HttpResponseMessage response = httpClient.GetAsync("api/Producto").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", "Token");
            }
            else
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<ProductoCLS> cat = JsonConvert.DeserializeObject<List<ProductoCLS>>(data);

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

        public ActionResult Guardar(int IdProducto, string Nombre, string Codigo, string Decripcion,  bool Activo, bool Exento, int IdMarca, int IdSubCategoria, int IdCategoria)
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
                ProductoCLS p = new ProductoCLS();
                p.IdProducto = IdProducto;
                p.Nombre = Nombre;
                p.Codigo = Codigo;
                p.Decripcion = Decripcion;
                p.Activo = Activo;
                p.Exento = Exento;
                p.IdMarca = IdMarca;
                p.IdSubCategoria = IdSubCategoria;
                p.IdCategoria = IdCategoria;

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseURL);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session["token"].ToString());

                string productoJson = JsonConvert.SerializeObject(p);
                HttpContent body = new StringContent(productoJson, Encoding.UTF8, "application/json");

                if (IdProducto == 0)
                {
                    HttpResponseMessage response = httpClient.PostAsync("api/Producto", body).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(
                            new
                            {
                                success = true,
                                message = "Se creo un producto"
                            }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Token");
                    }
                }
                else
                {
                    HttpResponseMessage response = httpClient.PutAsync($"api/Producto/{IdProducto}", body).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(
                            new
                            {
                                success = true,
                                message = "Se edito un producto"
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

        public ActionResult eliminar(int idProducto)
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

                HttpResponseMessage response = httpClient.DeleteAsync($"api/Producto/{idProducto}").Result;

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