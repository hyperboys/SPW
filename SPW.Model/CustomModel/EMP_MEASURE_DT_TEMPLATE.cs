using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class EMP_MEASURE_DT_TEMPLATE
    {
        public ActionEnum Action = ActionEnum.Untouch;

        public string _SKILL_TYPE_NAME = string.Empty;
        public string SKILL_TYPE_NAME
        {
            get { return _SKILL_TYPE_NAME; }
            set { _SKILL_TYPE_NAME = value; }
        }

        public string _SKILL_NAME = string.Empty;
        public string SKILL_NAME
        {
            get { return _SKILL_NAME; }
            set { _SKILL_NAME = value; }
        }
    }
}
