using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessirveMVC.Models
{
    public class OrdenCLS
    {
        public int IdOrden { get; set; }
        public int NumeroOrden { get; set; }
        public string Estado { get; set; }
        public decimal Impuesto { get; set; }
        public bool Envio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalOrden { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public Nullable<System.DateTime> FechaEntrega { get; set; }

        public string fecha { get; set; }
        public int IdCupon { get; set; }
        public int IdUsuarioNormal { get; set; }
    }
}