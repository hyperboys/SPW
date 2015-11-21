using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class ORDER_DETAIL
    {
        public ActionEnum Action;
        public string _colorDesc = string.Empty;
        public string ColorDesc
        {
            get { return _colorDesc; }
            set { _colorDesc = value; }
        }

        public string _productName = string.Empty;
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        public int _QTYFree = 0;
        public int QTYFree
        {
            get { return _QTYFree; }
            set { _QTYFree = value; }
        }


        public ORDER_DETAIL GetClone()
        {
            return (ORDER_DETAIL)this.MemberwiseClone();
        }
    }
}
