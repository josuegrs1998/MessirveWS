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
    
    public partial class CuponDescuento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CuponDescuento()
        {
            this.Ordens = new HashSet<Orden>();
        }
    
        public int IdCupon { get; set; }
        public bool Activo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal MinimoAplicable { get; set; }
        public string Codigo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orden> Ordens { get; set; }
    }
}
