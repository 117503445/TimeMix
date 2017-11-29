using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
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

            Public.timeWindow = new TimeWindow();
            //  timeWindow.Show();

            Public.classTableWindow = new ClassTableWindow();
            // classTableWindow.Show();

            Public.timeTableWindow = new TimeTableWindow();
            // timeTableWindow.Show();
            Public.switchWindow = new SwitchWindow(this);
            Public.switchWindow.Show();
            Hide();
            DispatcherTimer timer10000 = new DispatcherTimer();
            {
                timer10000.IsEnabled = true;
                timer10000.Interval = TimeSpan.FromSeconds(10);
                timer10000.Tick += Timer10000_Tick;
            }

            DispatcherTimer timer1000 = new DispatcherTimer();
            {
                timer1000.IsEnabled = true;
                timer1000.Interval = TimeSpan.FromSeconds(1);
                timer1000.Tick += Timer1000_Tick;
            }
            DispatcherTimer timer100 = new DispatcherTimer();
            {
                timer100.IsEnabled = true;
                timer100.Interval = TimeSpan.FromMilliseconds(100);
                timer100.Tick += Timer100_Tick;
            }
            DirectoryInfo dir = new DirectoryInfo(pathData);
            if (dir.Exists)
            {
                FileInfo[] fiList = dir.GetFiles();
                DirectoryInfo[] dList = dir.GetDirectories();
                List<string> timeList = new List<string>();
                List<string> classList = new List<string>();
                foreach (var item in fiList)
                {
                    if (item.Name.Length >= 2 && item.Name.Substring(0, 2) == "课表")
                    {
                        classList.Add(item.Name);
                    }
                }

                foreach (var item in dList)
                {
                    if (item.Name.Length >= 2 && item.Name.Substring(0, 2) == "时间")
                    {
                        timeList.Add(item.Name);
                    }
                }
                CboTime.ItemsSource = timeList;
                CboClass.ItemsSource = classList;

            }
            //TimeCore.Core core = new TimeCore.Core(@"C:\User\File\Program\TimeMix\TimeMix\File\Data\Source\时间NEW.txt", @"C:\User\File\Program\TimeMix\TimeMix\File\Data\Source\课表NEW.txt", deltaTime: 0);


            CboTime.SelectedItem = Settings.Default.nameTime;
            CboClass.SelectedItem = Settings.Default.nameClass;
            if (CboTime.SelectedItem == null)
            {
                CboTime.SelectedIndex = 0;
            }
            if (CboClass.SelectedItem == null)
            {
                CboClass.SelectedIndex = 0;
            }
            Tbdpi.Text = Settings.Default.dpi.ToString();
            TbDeltaTime.Text = Settings.Default.deltaTime.ToString();

            ChkTomorrowClass.IsChecked = Settings.Default.isTomorrowClass;

        }

        private void Timer10000_Tick(object sender, EventArgs e)
        {
            Public.timeTableWindow.ChangeColor();
            Public.timeWindow.ChangeColor();
            Public.classTableWindow.ChangeColor();
        }

        private void Timer100_Tick(object sender, EventArgs e)
        {
            Public.timeWindow.ChangeTime();
        }

        /// <summary>
        /// Environment.CurrentDirectory + @"\File\Data"
        /// </summary>
        readonly string pathData = Environment.CurrentDirectory + @"\File\Data";

        private void Timer1000_Tick(object sender, EventArgs e)
        {

            Public.classTableWindow.Topmost = true;

            string pathTime = pathData + "/" + CboTime.SelectedItem.ToString() + "/";
            string pathClass = pathData + "/" + CboClass.SelectedItem.ToString();

#if !DEBUG
            try
            {
                Core.Update(pathTime, pathClass, Public.ChangHetime());
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return;
            }
#else
            Core.Update(pathTime, pathClass, Public.ChangHetime());
#endif
            //timeWindow.Topmost = true;

            Public.timeTableWindow.Changedata(Core.CurrentTimeSection, Core.Progress);
            if (Public.ChangHetime().CompareTo(Core.LastClassEndTime[1]) > 0 && Settings.Default.isTomorrowClass)
            {
                //明天课表
                Public.classTableWindow.ChangeClass(Core.GetClass((int)Public.ChangHetime().AddDays(1).DayOfWeek));
            }
            else
            {
                //今天课表
                Public.classTableWindow.ChangeClass(Core.GetClass());
            }

        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Public.ExitProgram();


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            Public.switchWindow.SetVisible(3);
            e.Cancel = true;
        }

        private void CboTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.nameTime = CboTime.SelectedItem.ToString();
            Settings.Default.Save();
        }

        private void CboClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.nameClass = CboClass.SelectedItem.ToString();
            Settings.Default.Save();
        }

        private void Tbdpi_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Settings.Default.dpi = double.Parse(Tbdpi.Text.ToString());
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void TbDeltaTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Settings.Default.deltaTime = int.Parse(TbDeltaTime.Text.ToString());
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void ChkTomorrowClass_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.isTomorrowClass = (bool)ChkTomorrowClass.IsChecked;
        }
    }
}
