using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections;

namespace TimeMix
{
    static class OpenCheckIn
    {
        private static DispatcherTimer timer;
        public static DispatcherTimer Timer { get => timer; set => timer = value; }
        public static void Load()
        {
            XElement xElement = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "File/CheckIn.xml");
            Console.WriteLine(xElement);
            
            var times = (from item in xElement.Elements()
                         where item.Name == "Day" && item.Attribute("dayofweek").Value == ((int)DateTime.Now.DayOfWeek).ToString()
                         select item).First().Elements();
            foreach (var item in times)
            {
                Console.WriteLine(item.Value);
            }

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (s, e) => { };
            timer.Start();
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
            if (arg!= null)
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
