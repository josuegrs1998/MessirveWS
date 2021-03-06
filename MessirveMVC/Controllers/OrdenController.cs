﻿using MessirveMVC.Models;
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
    public class OrdenController : Controller
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
        public ActionResult Lista()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseURL);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session["token"].ToString());

            HttpResponseMessage response = httpClient.GetAsync("api/Orden").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", "Token");
            }
            else
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<OrdenCLS> cat = JsonConvert.DeserializeObject<List<OrdenCLS>>(data);
                foreach(OrdenCLS o in cat)
                {
                    o.fecha = o.FechaIngreso.ToString("MM/dd/yyyy");
                }
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

        public ActionResult Guardar(int IdOrden, int NumeroOrden, string Estado, decimal Impuesto, bool Envio, decimal SubTotal, decimal TotalOrden, DateTime FechaIngreso, int idCupon, int IdUsuarioNormal)
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
                OrdenCLS em = new OrdenCLS();
                em.IdOrden = IdOrden;
                em.NumeroOrden = NumeroOrden;
                em.Estado = Estado;
                em.Impuesto = Impuesto;
                em.Envio = Envio;
                em.SubTotal = SubTotal;
                em.TotalOrden = TotalOrden;
                em.FechaIngreso = DateTime.Now;
                em.IdCupon = idCupon;
                em.IdUsuarioNormal = IdUsuarioNormal;
                

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseURL);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session["token"].ToString());

                string productoJson = JsonConvert.SerializeObject(em);
                HttpContent body = new StringContent(productoJson, Encoding.UTF8, "application/json");

                if (IdOrden == 0)
                {
                    HttpResponseMessage response = httpClient.PostAsync("api/Orden", body).Result;
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

                    HttpResponseMessage response = httpClient.PutAsync($"api/Orden/{IdOrden}", body).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(
                            new
                            {
                                success = true,
                                message = "Se edito una Orden"
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

        public ActionResult eliminar(int IdOrden)
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

                HttpResponseMessage response = httpClient.DeleteAsync($"api/Orden/{IdOrden}").Result;

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