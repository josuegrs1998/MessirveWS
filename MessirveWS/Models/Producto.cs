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
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.OrdenProductoes = new HashSet<OrdenProducto>();
            this.ProductoEmpresas = new HashSet<ProductoEmpresa>();
        }
    
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Decripcion { get; set; }
        public bool Activo { get; set; }
        public bool Exento { get; set; }
        public Nullable<int> IdMarca { get; set; }
        public Nullable<int> IdSubCategoria { get; set; }
        public Nullable<int> IdCategoria { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        public virtual SubCategoria SubCategoria1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenProducto> OrdenProductoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductoEmpresa> ProductoEmpresas { get; set; }
    }
}