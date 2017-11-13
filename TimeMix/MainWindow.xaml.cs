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
        public ManageWindow manageWindow;
        public MainWindow()
        {
            InitializeComponent();

            timeWindow = new TimeWindow();
            //  timeWindow.Show();

            classTableWindow = new ClassTableWindow();
            // classTableWindow.Show();

            timeTableWindow = new TimeTableWindow();
            // timeTableWindow.Show();

            manageWindow = new ManageWindow(this);
            manageWindow.Show();

            manageWindow.SetVisible += SetVisible;


            Hide();


            DispatcherTimer timer1000 = new DispatcherTimer();
            {
                timer1000.IsEnabled = true;
                timer1000.Interval = TimeSpan.FromSeconds(1);
                timer1000.Tick += Timer1000_Tick;
            }
            DispatcherTimer timer500 = new DispatcherTimer();
            {
               timer500. IsEnabled = true;
                timer500.Interval = TimeSpan.FromMilliseconds(500);
                timer500.Tick += Timer500_Tick;
            }
            System.IO.DirectoryInfo dir = new DirectoryInfo(pathSource);
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

            Tbdpi.Text = Settings.Default.dpi.ToString();
            TbDeltaTime.Text = Settings.Default.deltaTime.ToString();
        }

        private void Timer500_Tick(object sender, EventArgs e)
        {
            string pathTime = pathSource + @"\时间NEW.txt";
            string pathClass = pathSource + @"\课表NEW.txt";
            Core core = new Core(pathTime, pathClass, Public.ChangHetime());
            timeWindow.ChangeTime();

        }

        /// <summary>
        /// 
        /// </summary>
        string pathSource = Environment.CurrentDirectory + @"\File\Data\Source";

        private void Timer1000_Tick(object sender, EventArgs e)
        {

            string pathTime = pathSource + @"\时间NEW.txt";
            string pathClass = pathSource + @"\课表NEW.txt";
            Core core = new Core(pathTime, pathClass, Public.ChangHetime());
            //Console.WriteLine(core.Section.ToString());

            timeWindow.ChangeColor();
            //timeWindow.Topmost = true;

            timeTableWindow.Changedata(core.Section);
            timeTableWindow.ChangeColor();

            classTableWindow.ChangeColor();
            classTableWindow.ChangeClass(core.TodayClassTable);
            classTableWindow.Topmost = true;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            manageWindow.ChkClassTableWindow.IsChecked = classTableWindow.IsVisible;
            manageWindow.ChkTimeWindow.IsChecked = timeWindow.IsVisible;
            manageWindow.ChkMainWindow.IsChecked = IsVisible;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void SetVisibleTimeWindow(bool IsVisible)
        {
            if (IsVisible)
            {
                timeWindow.Show();
            }
            else
            {
                timeWindow.Hide();
            }

        }

        private void SetVisibleClassTableWindow(bool IsVisible)
        {
            if (IsVisible)
            {
                classTableWindow.Show();
            }
            else
            {
                classTableWindow.Hide();
            }
        }
        private void SetVisibleMainWindow(bool IsVisible)
        {
            if (IsVisible)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
        private void SetVisible(Window window, bool isVisible)
        {
            if (isVisible)
            {
                window.Show();
            }
            else
            {
                window.Hide();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();

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
