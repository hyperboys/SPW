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
    
    public partial class STOCK_RAW_RECEIVE_TRANS
    {
        public int TRANS_ID { get; set; }
        public int RAW_ID { get; set; }
        public System.DateTime TRANS_DATE { get; set; }
        public string TRANS_TYPE { get; set; }
        public string PO_BK_NO { get; set; }
        public string PO_RN_NO { get; set; }
        public string PO_YY { get; set; }
        public int VENDOR_ID { get; set; }
        public Nullable<int> STOCK_BEFORE { get; set; }
        public Nullable<int> TRANS_QTY { get; set; }
        public Nullable<int> STOCK_AFTER { get; set; }
        public Nullable<int> APPROVE_EMPLOYEE_ID { get; set; }
        public Nullable<System.TimeSpan> SYS_TIME { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual PO_HD_TRANS PO_HD_TRANS { get; set; }
        public virtual RAW_PRODUCT RAW_PRODUCT { get; set; }
    }
}
