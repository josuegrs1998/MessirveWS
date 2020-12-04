using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessirveMVC.Models
{
    public class ProductoEmpresaCLS
    {
        public int IdProductoEmpresa { get; set; }
        public int IdEmpresa { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int Descuento { get; set; }
        public decimal PrecioBase { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public EmpresaCLS Empresa { get; set; }

        public ProductoCLS Producto { get; set; }
    }
}