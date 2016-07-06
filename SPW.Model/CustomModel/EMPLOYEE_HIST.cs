using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class EMPLOYEE_HIST
    {
        public string _positionName = string.Empty;
        public string POSITION_NAME
        {
            get { return _positionName; }
            set { _positionName = value; }
        }
    }
}
