using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TimeMix
{
    public static class Logger
    {
        /// <summary>
        /// Environment.CurrentDirectory + @"\File\Log\Log.txt";
        /// </summary>
        static string pathLog = AppDomain.CurrentDomain.BaseDirectory + @"\File\Log\Log.txt";
        public static void Write(Exception ex)
        {
            string str = DateTime.Now.ToString() + ";" + "ERROR" + ";" + ex.Message + "\r\n";
            File.AppendAllText(pathLog, str);
            Console.WriteLine(ex.ToString());
        }
        public static void Write(string s)
        {
            string str = DateTime.Now.ToString() + ";" + "INFO" + ";" + s + "\r\n";
            File.AppendAllText(pathLog, str);
        }
    }
}
