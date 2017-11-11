using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimeMix
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public TimeWindow timeWindow;
        public ClassTableWindow classTableWindow;
        public TimeTableWindow timeTableWindow;
        public MainWindow()
        {
            InitializeComponent();

            timeWindow = new TimeWindow();
            timeWindow.Show();

            classTableWindow = new ClassTableWindow();
            classTableWindow.Show();

            timeTableWindow = new TimeTableWindow();
           // timeTableWindow.Show();

            DispatcherTimer timer1000 = new DispatcherTimer();
            {
                timer1000.IsEnabled = true;
                timer1000.Interval = TimeSpan.FromSeconds(1);
                timer1000.Tick += Timer1000_Tick;
            }


            //TimeCore.Core core = new TimeCore.Core(@"C:\User\File\Program\TimeMix\TimeMix\File\Data\Source\时间NEW.txt", @"C:\User\File\Program\TimeMix\TimeMix\File\Data\Source\课表NEW.txt", deltaTime: 0);


          //  Hide();


        }

        private void Timer1000_Tick(object sender, EventArgs e)
        {

            string pathTime = Environment.CurrentDirectory + @"\File\Data\Source\时间NEW.txt";
            string pathClass = Environment.CurrentDirectory + @"\File\Data\Source\课表NEW.txt";
            Core core = new Core(pathTime, pathClass, deltaTime: 0);
            //Console.WriteLine(core.Section.ToString());
            timeWindow.ChangeTime();
            timeWindow.ChangeColor();
            //timeWindow.Topmost = true;

            timeTableWindow.Changedata(core.Section);

            classTableWindow.ChangeColor();
            classTableWindow.ChangeClass(core.TodayClassTable);
         //   classTableWindow.Topmost = true;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //double dWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            //double dHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            //Left = dWidth - 10;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
