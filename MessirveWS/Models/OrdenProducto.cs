//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MessirveWS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrdenProducto
    {
        public int IdOrdenProducto { get; set; }
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<decimal> Iva { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public int Cantidad { get; set; }
    
        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }
    }
}