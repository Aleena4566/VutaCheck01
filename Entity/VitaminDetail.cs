//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VutaCheck01.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class VitaminDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VitaminDetail()
        {
            this.FluidVitaminDetails = new HashSet<FluidVitaminDetail>();
            this.FoodDetails = new HashSet<FoodDetail>();
        }
    
        public int VitaminId { get; set; }
        public string Vitamin { get; set; }
        public string VitaminDescription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FluidVitaminDetail> FluidVitaminDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodDetail> FoodDetails { get; set; }
    }
}
