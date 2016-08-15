using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class EMP_MEASURE_TRANS
    {
        public ActionEnum Action = ActionEnum.Untouch;

        public string _YEAR_NAME = string.Empty;
        public string YEAR_NAME
        {
            get { return _YEAR_NAME; }
            set { _YEAR_NAME = value; }
        }

        public string _GRADE = string.Empty;
        public string GRADE
        {
            get { return _GRADE; }
            set { _GRADE = value; }
        }

        public decimal _POINT = 0;
        public decimal POINT
        {
            get { return _POINT; }
            set { _POINT = value; }
        }

        public decimal SUM_TARGET = 0;
        public decimal SUM_SCORE = 0;
        public decimal TYPE_PERCEN = 0;
        public decimal WEIGHT_PERCEN = 0;
    }
}
