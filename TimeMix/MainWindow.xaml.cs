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
        public MainWindow()
        {
            InitializeComponent();

            TimeWindow timeWindow = new TimeWindow();
            //timeWindow.Show();

            ClassTableWindow classTableWindow = new ClassTableWindow();
            //  classTableWindow.Show();

            TimeTableWindow timeTableWindow = new TimeTableWindow();
            timeTableWindow.Show();
            DispatcherTimer timer = new DispatcherTimer();
            {
                timer.IsEnabled = true;
                timer.Interval = TimeSpan.FromSeconds(1);
               // timer.Tick += Timer_Tick;
            }

            //TimeCore.Core core = new TimeCore.Core(@"C:\User\File\Program\TimeMix\TimeMix\File\Data\Source\时间NEW.txt", @"C:\User\File\Program\TimeMix\TimeMix\File\Data\Source\课表NEW.txt", deltaTime: 0);


            Hide();


        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            string pathTime = Environment.CurrentDirectory+@"\File\Data\Source\时间NEW.txt";
            string pathClass = Environment.CurrentDirectory + @"\File\Data\Source\课表NEW.txt";
            TimeCore.Core core = new TimeCore.Core(pathTime,pathClass, deltaTime: 0);
            Console.WriteLine(core.Section.ToString());
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
    }
}
