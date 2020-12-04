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
    public class OrdenProdutoController : Controller
    {

        // GET: Empresa
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
        public ActionResult Lista(int idOrden)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseURL);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session["token"].ToString());

            HttpResponseMessage response = httpClient.GetAsync($"api/OrdenProducto?idOrden={idOrden}").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", "Token");
            }
            else
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<OrdenProductoCLS> cat = JsonConvert.DeserializeObject<List<OrdenProductoCLS>>(data);

                return Json(
                   new
                   {
                       success = true,
                       data = cat,
                       message = "done"
                   },
                   JsonRequestBehavior.AllowGet
                   );
            }

        }

        public ActionResult Guardar(int IdOrdenProducto, int IdOrden, int IdProducto, decimal Precio, decimal Iva, decimal Subtotal, int Cantidad)
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
                OrdenProductoCLS em = new OrdenProductoCLS();
                em.IdOrdenProducto = IdOrdenProducto;
                em.IdOrden = IdOrden;
                em.IdProducto = IdProducto;
                em.Precio = Precio;
                em.Iva = Iva;
                em.Cantidad = Cantidad;
                em.Subtotal = (Cantidad * Precio) + Iva;
                
                
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseURL);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session["token"].ToString());

                string productoJson = JsonConvert.SerializeObject(em);
                HttpContent body = new StringContent(productoJson, Encoding.UTF8, "application/json");

                if (IdOrdenProducto == 0)
                {
                    HttpResponseMessage response = httpClient.PostAsync("api/OrdenProducto", body).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(
                            new
                            {
                                success = true,
                                message = "Se creo una Empresa"
                            }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Token");
                    }
                }
                else
                {

                    HttpResponseMessage response = httpClient.PutAsync($"api/OrdenProducto/{IdOrdenProducto}", body).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(
                            new
                            {
                                success = true,
                                message = "Se edito una Empresa"
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

        public ActionResult eliminar(int IdOrdenProducto)
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

                HttpResponseMessage response = httpClient.DeleteAsync($"api/OrdenProducto/{IdOrdenProducto}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return Json(
                        new
                        {
                            success = true,
                            message = "Se elimino una Empresa"
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