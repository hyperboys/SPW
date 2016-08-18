using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class EMPLOYEE
    {
        public ActionEnum Action;
        public EMPLOYEE_HIST EMPLOYEE_HIST { get; set; }
    }
}
