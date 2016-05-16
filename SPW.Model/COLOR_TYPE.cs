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
    
    public partial class COLOR_TYPE
    {
        public COLOR_TYPE()
        {
            this.DELIVERY_INDEX_DETAIL = new HashSet<DELIVERY_INDEX_DETAIL>();
            this.DELIVERY_ORDER_DETAIL = new HashSet<DELIVERY_ORDER_DETAIL>();
            this.ORDER_DETAIL = new HashSet<ORDER_DETAIL>();
            this.STOCK_PRODUCT_STOCK = new HashSet<STOCK_PRODUCT_STOCK>();
            this.STOCK_PRODUCT_TRANS = new HashSet<STOCK_PRODUCT_TRANS>();
            this.STOCK_PRODUCT_WITHDRAW_TRANS = new HashSet<STOCK_PRODUCT_WITHDRAW_TRANS>();
            this.SUB_DELIVERY_INDEX_DETAIL = new HashSet<SUB_DELIVERY_INDEX_DETAIL>();
            this.SUB_DELIVERY_ORDER_DETAIL = new HashSet<SUB_DELIVERY_ORDER_DETAIL>();
        }
    
        public int COLOR_TYPE_ID { get; set; }
        public string COLOR_TYPE_SUBNAME { get; set; }
        public string COLOR_TYPE_NAME { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
    
        public virtual ICollection<DELIVERY_INDEX_DETAIL> DELIVERY_INDEX_DETAIL { get; set; }
        public virtual ICollection<DELIVERY_ORDER_DETAIL> DELIVERY_ORDER_DETAIL { get; set; }
        public virtual ICollection<ORDER_DETAIL> ORDER_DETAIL { get; set; }
        public virtual ICollection<STOCK_PRODUCT_STOCK> STOCK_PRODUCT_STOCK { get; set; }
        public virtual ICollection<STOCK_PRODUCT_TRANS> STOCK_PRODUCT_TRANS { get; set; }
        public virtual ICollection<STOCK_PRODUCT_WITHDRAW_TRANS> STOCK_PRODUCT_WITHDRAW_TRANS { get; set; }
        public virtual ICollection<SUB_DELIVERY_INDEX_DETAIL> SUB_DELIVERY_INDEX_DETAIL { get; set; }
        public virtual ICollection<SUB_DELIVERY_ORDER_DETAIL> SUB_DELIVERY_ORDER_DETAIL { get; set; }
    }
}
