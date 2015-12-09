using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class ORDER
    {
        public ActionEnum Action;
        public string _REG;
        public string REG
        {
            get { return _REG; }
            set { _REG = value; }
        }

        public decimal _WEIGHT;
        public decimal WEIGHT
        {
            get { return _WEIGHT; }
            set { _WEIGHT = value; }
        }
        
    }
}
