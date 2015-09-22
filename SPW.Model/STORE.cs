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
    
    public partial class STORE
    {
        public STORE()
        {
            this.ORDER = new HashSet<ORDER>();
        }
    
        public int STORE_ID { get; set; }
        public string STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public string STORE_TEL1 { get; set; }
        public string STORE_TEL2 { get; set; }
        public string STORE_MOBILE { get; set; }
        public string STORE_FAX { get; set; }
        public string STORE_ADDR1 { get; set; }
        public string STORE_STREET { get; set; }
        public string STORE_SUBDISTRICT { get; set; }
        public string STORE_DISTRICT { get; set; }
        public int PROVINCE_ID { get; set; }
        public int SECTOR_ID { get; set; }
        public Nullable<int> ZONE_ID { get; set; }
        public Nullable<int> ROAD_ID { get; set; }
        public string STORE_POSTCODE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual ICollection<ORDER> ORDER { get; set; }
        public virtual PROVINCE PROVINCE { get; set; }
        public virtual ROAD ROAD { get; set; }
        public virtual SECTOR SECTOR { get; set; }
        public virtual ZONE ZONE { get; set; }
    }
}
