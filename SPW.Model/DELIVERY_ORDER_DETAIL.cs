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
    
    public partial class DELIVERY_ORDER_DETAIL
    {
        public int DELORDER_DETAIL_ID { get; set; }
        public int DELORDER_ID { get; set; }
        public Nullable<int> PRODUCT_SEQ { get; set; }
        public int PRODUCT_ID { get; set; }
        public Nullable<int> PRODUCT_QTY { get; set; }
        public Nullable<int> PRODUCT_SEND_QTY { get; set; }
        public Nullable<decimal> PRODUCT_PRICE { get; set; }
        public Nullable<decimal> PRODUCT_TOTAL { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
        public string COLOR_CODE { get; set; }
    
        public virtual DELIVERY_ORDER DELIVERY_ORDER { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
