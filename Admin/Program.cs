using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.NetTimeClient client = new Server.NetTimeClient();
            Console.WriteLine(client.Login("admin", "2018")); 
            client.SetTime(DateTime.Now.AddDays(1));
            Console.WriteLine(client.GetTime()); 
            Console.Read();
 
        }

    }
}
