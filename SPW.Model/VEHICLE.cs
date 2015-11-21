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
    
    public partial class VEHICLE
    {
        public VEHICLE()
        {
            this.DELIVERY_INDEX = new HashSet<DELIVERY_INDEX>();
            this.DELIVERY_ORDER = new HashSet<DELIVERY_ORDER>();
            this.STOCK_PRODUCT_WITHDRAW_TRANS = new HashSet<STOCK_PRODUCT_WITHDRAW_TRANS>();
        }
    
        public int VEHICLE_ID { get; set; }
        public string VEHICLE_CODE { get; set; }
        public string VEHICLE_REGNO { get; set; }
        public string VEHICLE_TYPENO { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual ICollection<DELIVERY_INDEX> DELIVERY_INDEX { get; set; }
        public virtual ICollection<DELIVERY_ORDER> DELIVERY_ORDER { get; set; }
        public virtual ICollection<STOCK_PRODUCT_WITHDRAW_TRANS> STOCK_PRODUCT_WITHDRAW_TRANS { get; set; }
    }
}
