using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessirveMVC.Models
{
    public class OrdenProductoCLS
    {
        public int IdOrdenProducto { get; set; }
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<decimal> Iva { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public int Cantidad { get; set; }
        public ProductoCLS Producto { get; set; }
        public OrdenCLS Orden { get; set; }
    
    }
}