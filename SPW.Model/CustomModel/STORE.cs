using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class STORE
    {
        public ActionEnum Action;
        public decimal _TOTAL;
        public decimal TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
        }

        public decimal _WEIGHT;
        public decimal WEIGHT
        {
            get { return _WEIGHT; }
            set { _WEIGHT = value; }
        }
      
    }
}
