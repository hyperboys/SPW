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
    
    public partial class ROLE
    {
        public ROLE()
        {
            this.ROLE_FUNCTION = new HashSet<ROLE_FUNCTION>();
            this.USER = new HashSet<USER>();
        }
    
        public int ROLE_ID { get; set; }
        public string ROLE_CODE { get; set; }
        public string ROLE_NAME { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual ICollection<ROLE_FUNCTION> ROLE_FUNCTION { get; set; }
        public virtual ROLE ROLE1 { get; set; }
        public virtual ROLE ROLE2 { get; set; }
        public virtual ICollection<USER> USER { get; set; }
    }
}
