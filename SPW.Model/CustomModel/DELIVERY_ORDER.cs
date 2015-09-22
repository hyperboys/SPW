using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class DELIVERY_ORDER
    {
        private STORE _store;
        public STORE STORE
        {
            get { return _store; }
            set { _store = value; }
        }
    }
}
