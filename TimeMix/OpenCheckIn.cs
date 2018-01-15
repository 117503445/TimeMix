using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections;
using System.Diagnostics;

namespace TimeMix
{
    static class OpenCheckIn
    {
        static List<DateTime>[] list = new List<DateTime>[7];
        private static DispatcherTimer timer;
        public static DispatcherTimer Timer { get => timer; set => timer = value; }
        public static void Load()
        {
            XElement xElement = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "File/CheckIn.xml");
            //Console.WriteLine(xElement);

            var times = (from item in xElement.Elements()
                         where item.Name == "Day" && item.Attribute("dayofweek").Value == ((int)DateTime.Now.DayOfWeek).ToString()
                         select item).First().Elements();
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = new List<DateTime>();
            }
            foreach (var day in xElement.Elements())
            {
                foreach (var item in day.Elements())
                {
                    list[int.Parse(day.Attribute("dayofweek").Value)].Add
                        (Convert.ToDateTime(item.Value));
                }
            }
            //foreach (var item in list)
            //{
            //    foreach (var i in item)
            //    {
            //        Console.WriteLine(i);
            //    }
            //}
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (s, e) => { };
            timer.Start();
        }
        public static void Check()
        {
            //Console.WriteLine("---Check---");
            foreach (var item in list[(int)DateTime.Now.DayOfWeek])
            {
                TimeSpan span = (item - DateTime.Now);
                if (span < TimeSpan.FromSeconds(1) && span > TimeSpan.FromSeconds(-1))
                {
                    Process p = new Process();
                    p.StartInfo.FileName = TimeMix.Properties.Settings.Default.CheckPath;
                    p.Start();
                }
                //Console.WriteLine(item);
            }

           // Console.WriteLine("---end---");

        }

    }
    public class CheckInCollection
    {
        DayOfWeek dayOfWeek;
        IEnumerable<DateTime> dateTimes;
        public DayOfWeek DayOfWeek { get => dayOfWeek; set => dayOfWeek = value; }

        public CheckInCollection(DayOfWeek dayOfWeek, IEnumerable<DateTime> dateTimes)
        {
            this.dayOfWeek = dayOfWeek;
            this.dateTimes = dateTimes;
        }

        public bool Exist(DateTime value)
        {
            var arg = from item in dateTimes where item.TimeOfDay == value.TimeOfDay select item.TimeOfDay;
            if (arg != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
