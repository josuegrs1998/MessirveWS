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
    public class ClienteController : Controller
    {
        private bool usuarioAutenticado()
        {
            return HttpContext.Session["token"] != null;
        }
        public ActionResult Index()
        {
            /*if (!usuarioAutenticado())
            {
                return RedirectToAction("Index", "Token");
            }*/
            return View();
        }
        private string baseURL = "https://localhost:44331/";
        public ActionResult Lista()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseURL);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session["token"].ToString());

            HttpResponseMessage response = httpClient.GetAsync("api/Cliente").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", "Token");
            }
            else
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<ClienteCLS> cliente = JsonConvert.DeserializeObject<List<ClienteCLS>>(data);

                return Json(
                   new
                   {
                       success = true,
                       data = cliente,
                       message = "donde"
                   },
                   JsonRequestBehavior.AllowGet
                   );
            }

        }

        public ActionResult Guardar( string Nombre, string Apellido, string Telefono, string Identificacion, string Correo, string direccion, string username, string password)
        {
          
            try
            {
                int IdUsuarioNormal = 0;
                ClienteCLS c = new ClienteCLS();
  
               
                /*Crear Cliente*/
          
                c.IdUsuarioNormal = IdUsuarioNormal;
                c.Nombre = Nombre;
                c.Apellido = Apellido;
                c.Telefono = Telefono;
                c.Identificacion = Identificacion;
                c.Correo = Correo;
                c.Activo = true;
                DateTime fecha = new DateTime(2010, 10, 10);
                c.FechaNacimiento = fecha;
                c.Direccion = direccion;
                c.IdLogin = 3;
                c.Password = password;
                c.Username = username;

                //Validar que el nombre de usuario no exista en la bd
         
                HttpClient httpClientC = new HttpClient();
                httpClientC.BaseAddress = new Uri(baseURL);
                httpClientC.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseC = httpClientC.GetAsync($"api/Cliente?Username={c.Username}").Result;
                string dataC = responseC.Content.ReadAsStringAsync().Result;
                
                if (dataC == "[]")
                {
                    //CrearCliente
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(baseURL);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string clienteJson = JsonConvert.SerializeObject(c);
                    HttpContent body = new StringContent(clienteJson, Encoding.UTF8, "application/json");

                    if (IdUsuarioNormal == 0)
                    {
                        HttpResponseMessage response = httpClient.PostAsync("api/Cliente", body).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            TempData["shortMessage"] = "Usuario creado, use sus credenciales para ingresar";
                            ViewBag.Message = TempData["shortMessage"].ToString();
                            return RedirectToAction("Index", "Token");
                           
                        }
                        else
                        {
                            return RedirectToAction("Index", "Token");
                        }
                    }

                    throw new Exception("Error al guardar");
                }
                else
                {
                    return ViewBag.Message("Nombre de usuario no disponible");

                }

                //Fin Validacion

            }
            catch (Exception ex)
            {
                TempData["shortMessage"] = "Nombre de usuario no disponible";
                ViewBag.Message = TempData["shortMessage"].ToString();
                return RedirectToAction("Index", "Cliente");
                
                //return View(ViewBag.Message="Nombre de usuario no disponible");

            }
        }

        public ActionResult eliminar(int IdUsuarioNormal)
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

                HttpResponseMessage response = httpClient.DeleteAsync($"api/Cliente/{IdUsuarioNormal}").Result;

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