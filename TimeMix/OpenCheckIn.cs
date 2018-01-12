using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TimeMix
{
    static class OpenCheckIn
    {
        private static DispatcherTimer timer;

        public static DispatcherTimer Timer { get => timer; set => timer = value; }

        public static void Load()
        {





            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (s, e) => { };
            timer.Start();
        }

    }
}
