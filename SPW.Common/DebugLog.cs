using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Common
{
    public static class DebugLog
    {
        public static void WriteLog(string logMessage)
        {
            //using (StreamWriter w = File.AppendText("log.txt"))
            //{
            //    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
            //        DateTime.Now.ToLongDateString());
            //    w.Write("  :{0}", logMessage);
            //}
        }
    }
}
