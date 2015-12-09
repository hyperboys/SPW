using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class DELIVERY_ORDER_DETAIL
    {
        public string _colorDesc = string.Empty;
        public string ColorDesc
        {
            get { return _colorDesc; }
            set { _colorDesc = value; }
        }
        public string ORDER_CODE { get; set; }
    }
}
