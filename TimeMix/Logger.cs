using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMix
{
    public static class Logger
    {
        public static void Write(Exception ex) {
            Console.WriteLine(ex.ToString());
        }
    }
}
