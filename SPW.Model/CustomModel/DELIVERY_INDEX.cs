using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
     [Serializable]
    public partial class DELIVERY_INDEX
    {
         public decimal TOTAL_WEIGHT
         {
             get
             {
                 return (decimal)this.DELIVERY_INDEX_DETAIL.Sum(x => x.PRODUCT_WEIGHT_TOTAL);
             }           
         }
         public decimal TOTAL_PRICE 
         {
             get
             {
                 return (decimal)this.DELIVERY_INDEX_DETAIL.Sum(x => x.PRODUCT_PRICE_TOTAL);
             }
         }

         public bool ISDELETE
         {
             get
             {
                 return !(this.DELIVERY_ORDER.All(x => x.DELORDER_STEP == "50"));
             }
         }
    }
}
