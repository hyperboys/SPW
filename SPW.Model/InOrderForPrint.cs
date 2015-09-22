using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public class InOrderForPrint
    {
        public STORE Store { get; set; }
        public List<ORDER_DETAIL> OrderDetails { get; set; }
        public ORDER Order { get; set; }
    }

    public class LineForPrint 
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string line3 { get; set; }
        public string line4 { get; set; }
        public string line5 { get; set; }
    }

    public class ListOfLineForPrint 
    {
        public List<LineForPrint> LineForPrint { get; set; }
    }
}
