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
    
    public partial class ASSET_TYPE
    {
        public ASSET_TYPE()
        {
            this.AP_VEHICLE_TRANS = new HashSet<AP_VEHICLE_TRANS>();
        }
    
        public int ASSET_TYPE_ID { get; set; }
        public string ASSET_TYPE_NAME { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual ICollection<AP_VEHICLE_TRANS> AP_VEHICLE_TRANS { get; set; }
    }
}