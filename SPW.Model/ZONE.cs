//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPW.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ZONE
    {
        public ZONE()
        {
            this.PRODUCT_PRICELIST = new HashSet<PRODUCT_PRICELIST>();
            this.PRODUCT_PROMOTION = new HashSet<PRODUCT_PROMOTION>();
            this.STORE = new HashSet<STORE>();
            this.ZONE_DETAIL = new HashSet<ZONE_DETAIL>();
        }
    
        public int ZONE_ID { get; set; }
        public string ZONE_CODE { get; set; }
        public string ZONE_NAME { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual ICollection<PRODUCT_PRICELIST> PRODUCT_PRICELIST { get; set; }
        public virtual ICollection<PRODUCT_PROMOTION> PRODUCT_PROMOTION { get; set; }
        public virtual ICollection<STORE> STORE { get; set; }
        public virtual ICollection<ZONE_DETAIL> ZONE_DETAIL { get; set; }
    }
}
