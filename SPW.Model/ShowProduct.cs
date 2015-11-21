using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public class ShowProduct
    {
        public int PRODUCT_ID { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public decimal PRICE { get; set; }
        public int CATEGORY_ID { get; set; }
    }
}
