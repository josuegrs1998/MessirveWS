using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessirveMVC.Models
{
    public class ClienteCLS
    {
        public int IdUsuarioNormal { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Identificacion { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public int IdLogin { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        

    }
}