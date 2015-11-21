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
    
    public partial class PRODUCT
    {
        public PRODUCT()
        {
            this.DELIVERY_INDEX_DETAIL = new HashSet<DELIVERY_INDEX_DETAIL>();
            this.DELIVERY_ORDER_DETAIL = new HashSet<DELIVERY_ORDER_DETAIL>();
            this.ORDER_DETAIL = new HashSet<ORDER_DETAIL>();
            this.PRODUCT_PRICELIST = new HashSet<PRODUCT_PRICELIST>();
            this.PRODUCT_PROMOTION = new HashSet<PRODUCT_PROMOTION>();
            this.STOCK_PRODUCT_STOCK = new HashSet<STOCK_PRODUCT_STOCK>();
            this.STOCK_PRODUCT_TRANS = new HashSet<STOCK_PRODUCT_TRANS>();
            this.STOCK_PRODUCT_WITHDRAW_TRANS = new HashSet<STOCK_PRODUCT_WITHDRAW_TRANS>();
        }
    
        public int PRODUCT_ID { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string PRODUCT_SIZE { get; set; }
        public string PRODUCT_IMAGE_PATH { get; set; }
        public int CATEGORY_ID { get; set; }
        public Nullable<int> PRODUCT_TYPE_CODE { get; set; }
        public string PRODUCT_PACKING_DESC { get; set; }
        public Nullable<int> PRODUCT_PACKING_QTY { get; set; }
        public string PRODUCT_PACKING_PER_UDESC { get; set; }
        public string PRODUCT_PACKING_PER_PDESC { get; set; }
        public Nullable<decimal> PRODUCT_WEIGHT { get; set; }
        public string PRODUCT_WEIGHT_DEFINE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
        public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
        public Nullable<bool> SYE_DEL { get; set; }
        public int STOCK_TYPE_ID { get; set; }
    
        public virtual CATEGORY CATEGORY { get; set; }
        public virtual ICollection<DELIVERY_INDEX_DETAIL> DELIVERY_INDEX_DETAIL { get; set; }
        public virtual ICollection<DELIVERY_ORDER_DETAIL> DELIVERY_ORDER_DETAIL { get; set; }
        public virtual ICollection<ORDER_DETAIL> ORDER_DETAIL { get; set; }
        public virtual ICollection<PRODUCT_PRICELIST> PRODUCT_PRICELIST { get; set; }
        public virtual ICollection<PRODUCT_PROMOTION> PRODUCT_PROMOTION { get; set; }
        public virtual STOCK_TYPE STOCK_TYPE { get; set; }
        public virtual ICollection<STOCK_PRODUCT_STOCK> STOCK_PRODUCT_STOCK { get; set; }
        public virtual ICollection<STOCK_PRODUCT_TRANS> STOCK_PRODUCT_TRANS { get; set; }
        public virtual ICollection<STOCK_PRODUCT_WITHDRAW_TRANS> STOCK_PRODUCT_WITHDRAW_TRANS { get; set; }
    }
}
