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
            DateTime date = DateTime.Now;
            string path = @"C:\inetpub\wwwroot\SPW\Log";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string logName = path + "log" + date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + ".txt";
            if (!File.Exists(logName))
            {
                StreamWriter sw = File.CreateText(logName);
                sw.Close();
                sw.Dispose();
            }
            using (StreamWriter w = File.AppendText(logName))
            {
                Write(logMessage, w);
            }

            using (StreamReader r = File.OpenText(logName))
            {
                DumpLog(r);
            }
        }

        private static void Write(string logMessage, TextWriter w)
        {
            w.WriteLine("[" + DateTime.Now.ToString() + ":" + DateTime.Now.ToLongTimeString() + "] : " + logMessage);
        }

        private static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}
