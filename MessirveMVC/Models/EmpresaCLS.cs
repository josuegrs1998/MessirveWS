using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessirveMVC.Models
{
    public class EmpresaCLS
    {
        public int IdEmpresa { get; set; }
        public string RUC { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    }
}