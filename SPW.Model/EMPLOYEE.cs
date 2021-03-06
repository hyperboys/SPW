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
    
    public partial class EMPLOYEE
    {
        public EMPLOYEE()
        {
            this.USER = new HashSet<USER>();
            this.ZONE_DETAIL = new HashSet<ZONE_DETAIL>();
        }
    
        public int EMPLOYEE_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string EMPLOYEE_NAME { get; set; }
        public string EMPLOYEE_SURNAME { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual DEPARTMENT DEPARTMENT { get; set; }
        public virtual ICollection<USER> USER { get; set; }
        public virtual ICollection<ZONE_DETAIL> ZONE_DETAIL { get; set; }
    }
}
