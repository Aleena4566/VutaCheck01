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
    
    public partial class FoodDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FoodDetail()
        {
            this.FoodIntakeDetails = new HashSet<FoodIntakeDetail>();
        }
    
        public int FoodId { get; set; }
        public string FoodSource { get; set; }
        public Nullable<int> VitaminId { get; set; }
        public bool IsFoodSourceDelete { get; set; }
    
        public virtual VitaminDetail VitaminDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodIntakeDetail> FoodIntakeDetails { get; set; }
    }
}
