using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class PRODUCT
    {
        public ActionEnum Action;

        public int _PRODUCT_QTY;
        public int PRODUCT_QTY
        {
            get { return _PRODUCT_QTY; }
            set { _PRODUCT_QTY = value; }
        }

        public decimal _WEIGHT;
        public decimal WEIGHT
        {
            get { return _WEIGHT; }
            set { _WEIGHT = value; }
        }

        public decimal _PRODUCT_PRICE;
        public decimal PRODUCT_PRICE
        {
            get { return _PRODUCT_PRICE; }
            set { _PRODUCT_PRICE = value; }
        }

        public decimal _PRODUCT_PRICE_TOTAL;
        public decimal PRODUCT_PRICE_TOTAL
        {
            get { return _PRODUCT_PRICE_TOTAL; }
            set { _PRODUCT_PRICE_TOTAL = value; }
        }

        public decimal _PRODUCT_WEIGHT_TOTAL;
         public decimal PRODUCT_WEIGHT_TOTAL
        {
            get { return _PRODUCT_WEIGHT_TOTAL; }
            set { _PRODUCT_WEIGHT_TOTAL = value; }
        }
        
    }
}
