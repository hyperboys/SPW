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
    
    public partial class PO_HD_TRANS
    {
        public PO_HD_TRANS()
        {
            this.STOCK_RAW_RECEIVE_TRANS = new HashSet<STOCK_RAW_RECEIVE_TRANS>();
        }
    
        public string PO_BK_NO { get; set; }
        public string PO_RN_NO { get; set; }
        public string PO_YY { get; set; }
        public System.DateTime PO_DATE { get; set; }
        public string PR_BK_NO { get; set; }
        public string PR_RN_NO { get; set; }
        public string PR_YY { get; set; }
        public System.DateTime PR_DATE { get; set; }
        public int VENDOR_ID { get; set; }
        public string VENDOR_CODE { get; set; }
        public string VENDOR_NAME { get; set; }
        public string VENDOR_TEL1 { get; set; }
        public string VENDOR_TEL2 { get; set; }
        public string VENDOR_MOBILE { get; set; }
        public string VENDOR_FAX { get; set; }
        public string VENDOR_EMAIL { get; set; }
        public string VENDOR_CONTACT_PERSON { get; set; }
        public string PO_RECEIVE_PERSON { get; set; }
        public string VENDOR_CREDIT_INTERVAL { get; set; }
        public int VENDOR_CREDIT_VALUE { get; set; }
        public string PO_HD_STATUS { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual ICollection<STOCK_RAW_RECEIVE_TRANS> STOCK_RAW_RECEIVE_TRANS { get; set; }
    }
}
