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
    
    public partial class PRODUCT_PRICELIST
    {
        public int PRODUCT_PRICELIST_ID { get; set; }
        public int PRODUCT_ID { get; set; }
        public string PRODUCT_CODE { get; set; }
        public int ZONE_ID { get; set; }
        public Nullable<decimal> PRODUCT_PRICE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual PRODUCT PRODUCT { get; set; }
        public virtual ZONE ZONE { get; set; }
    }
}
