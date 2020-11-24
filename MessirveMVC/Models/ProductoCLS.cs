using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessirveMVC.Models
{
    public class ProductoCLS
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Decripcion { get; set; }
        public bool Activo { get; set; }
        public bool Exento { get; set; }
        public int IdMarca { get; set; }
        public int IdSubCategoria { get; set; }
        public int IdCategoria { get; set; }

    }
}