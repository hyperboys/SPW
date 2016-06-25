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
    
    public partial class VENDOR
    {
        public VENDOR()
        {
            this.AP_VEHICLE_TRANS = new HashSet<AP_VEHICLE_TRANS>();
            this.PO_HD_TRANS = new HashSet<PO_HD_TRANS>();
            this.PR_HD_TRANS = new HashSet<PR_HD_TRANS>();
            this.RAW_PACK_PRICE_HIST = new HashSet<RAW_PACK_PRICE_HIST>();
            this.VENDOR_DEAL_DISCOUNT = new HashSet<VENDOR_DEAL_DISCOUNT>();
        }
    
        public int VENDOR_ID { get; set; }
        public string VENDOR_CODE { get; set; }
        public string VENDOR_NAME { get; set; }
        public string VENDOR_TEL1 { get; set; }
        public string VENDOR_TEL2 { get; set; }
        public string VENDOR_MOBILE { get; set; }
        public string VENDOR_FAX { get; set; }
        public string VENDOR_ADDR1 { get; set; }
        public string VENDOR_STREET { get; set; }
        public string VENDOR_SUBDISTRICT { get; set; }
        public string VENDOR_DISTRICT { get; set; }
        public int PROVINCE_ID { get; set; }
        public Nullable<int> ROAD_ID { get; set; }
        public string VENDOR_POSTCODE { get; set; }
        public string VENDOR_EMAIL { get; set; }
        public string VENDOR_CONTACT_PERSON { get; set; }
        public string VENDOR_CREDIT_INTERVAL { get; set; }
        public int VENDOR_CREDIT_VALUE { get; set; }
        public string VAT_TYPE { get; set; }
        public decimal VAT_RATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual ICollection<AP_VEHICLE_TRANS> AP_VEHICLE_TRANS { get; set; }
        public virtual ICollection<PO_HD_TRANS> PO_HD_TRANS { get; set; }
        public virtual ICollection<PR_HD_TRANS> PR_HD_TRANS { get; set; }
        public virtual PROVINCE PROVINCE { get; set; }
        public virtual ICollection<RAW_PACK_PRICE_HIST> RAW_PACK_PRICE_HIST { get; set; }
        public virtual ROAD ROAD { get; set; }
        public virtual ICollection<VENDOR_DEAL_DISCOUNT> VENDOR_DEAL_DISCOUNT { get; set; }
    }
}
