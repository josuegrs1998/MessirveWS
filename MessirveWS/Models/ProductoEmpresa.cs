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
    
    public partial class ProductoEmpresa
    {
        public int IdProductoEmpresa { get; set; }
        public int IdEmpresa { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int Descuento { get; set; }
        public decimal PrecioBase { get; set; }
        public System.DateTime FechaIngreso { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
