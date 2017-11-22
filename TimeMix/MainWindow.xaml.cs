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
        public TimeWindow timeWindow;
        public ClassTableWindow classTableWindow;
        public TimeTableWindow timeTableWindow;

        public SwitchWindow switchWindow;
        public MainWindow()
        {
            InitializeComponent();

            timeWindow = new TimeWindow();
            //  timeWindow.Show();

            classTableWindow = new ClassTableWindow();
            // classTableWindow.Show();

            timeTableWindow = new TimeTableWindow();
            // timeTableWindow.Show();

            switchWindow = new SwitchWindow(this);
            switchWindow.Show();

            Hide();

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
            DirectoryInfo dir = new DirectoryInfo(pathSource);
            if (dir.Exists)
            {
                FileInfo[] fiList = dir.GetFiles();
                List<string> timeList = new List<string>();
                List<string> classList = new List<string>();
                foreach (var item in fiList)
                {
                    if (item.Name.Substring(0, 2) == "时间")
                    {
                        timeList.Add(item.Name);
                    }
                    if (item.Name.Substring(0, 2) == "课表")
                    {
                        classList.Add(item.Name);
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
        }

        private void Timer100_Tick(object sender, EventArgs e)
        {
            timeWindow.ChangeTime();
        }

        /// <summary>
        /// 
        /// </summary>
        string pathSource = Environment.CurrentDirectory + @"\File\Data\Source";

        private void Timer1000_Tick(object sender, EventArgs e)
        {
            timeTableWindow.ChangeColor();
            timeWindow.ChangeColor();
            classTableWindow.ChangeColor();
            classTableWindow.Topmost = true;
            try
            {
                string pathTime = pathSource + @"\" + CboTime.SelectedItem.ToString();
                string pathClass = pathSource + @"\" + CboClass.SelectedItem.ToString();
                Core.Update(pathTime, pathClass, Public.ChangHetime());
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return;
            }
            //Console.WriteLine(core.Section.ToString());
            //timeWindow.Topmost = true;
            timeTableWindow.Changedata(Core.Section);
            classTableWindow.ChangeClass(Core.TodayClassTable);
        }

        private void Window_Activated(object sender, EventArgs e)
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            switchWindow.SetVisible(3);
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


    }
}
