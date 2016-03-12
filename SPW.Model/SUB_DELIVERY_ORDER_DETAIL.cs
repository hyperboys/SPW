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
    
    public partial class SUB_DELIVERY_ORDER_DETAIL
    {
        public int SUB_DELORDER_DETAIL_ID { get; set; }
        public int SUB_DELORDER_ID { get; set; }
        public string DELORDER_CODE { get; set; }
        public string DELORDER_SUBCODE { get; set; }
        public Nullable<int> DELORDER_SEQ_NO { get; set; }
        public int ORDER_DETAIL_ID { get; set; }
        public Nullable<int> PRODUCT_SEQ { get; set; }
        public int PRODUCT_ID { get; set; }
        public int COLOR_ID { get; set; }
        public int COLOR_TYPE_ID { get; set; }
        public Nullable<int> PRODUCT_QTY { get; set; }
        public Nullable<decimal> PRODUCT_PRICE { get; set; }
        public Nullable<decimal> PRODUCT_WEIGHT { get; set; }
        public string IS_FREE { get; set; }
        public int PRODUCT_SENT_ROUND { get; set; }
        public Nullable<int> PRODUCT_SENT_QTY { get; set; }
        public Nullable<int> PRODUCT_SENT_REMAIN { get; set; }
        public Nullable<decimal> PRODUCT_SENT_PRICE_TOTAL { get; set; }
        public Nullable<decimal> PRODUCT_SENT_WEIGHT_TOTAL { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual COLOR COLOR { get; set; }
        public virtual COLOR_TYPE COLOR_TYPE { get; set; }
        public virtual ORDER_DETAIL ORDER_DETAIL { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
        public virtual SUB_DELIVERY_ORDER SUB_DELIVERY_ORDER { get; set; }
    }
}