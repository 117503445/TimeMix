using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimeMix
{
    /// <summary>
    /// SwitchWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SwitchWindow : Window
    {
        MainWindow mainWindow;
        DispatcherTimer timer = new DispatcherTimer
        {
            IsEnabled = true,
            Interval = TimeSpan.FromSeconds(3)
        };
        class WindowCollection
        {
            /// <summary>
            /// 窗体
            /// </summary>
            public Window window;
            /// <summary>
            /// 图片资源名称
            /// </summary>
            public string PicName;
            /// <summary>
            /// 对应图片
            /// </summary>
            public Image image;

            public WindowCollection(Window window, string picName, Image image)
            {
                this.window = window;
                PicName = picName;
                this.image = image;
            }
        }
        List<WindowCollection> windows = new List<WindowCollection>();
        public SwitchWindow(MainWindow window)
        {
            InitializeComponent();

            windows.Clear();






            mainWindow = window;
            Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
            Left = SystemParameters.PrimaryScreenWidth - 1;
            timer.Tick += Timer_Tick;


            Loaded += SwitchWindow_Loaded;

        }

        private void SwitchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            windows.Add(new WindowCollection(Public.timeWindow, "Time", Public.switchWindow.ImgTime));
            windows.Add(new WindowCollection(Public.classTableWindow, "ClassTable", Public.switchWindow.ImgClassTable));
            windows.Add(new WindowCollection(Public.timeTableWindow, "TimeTable", Public.switchWindow.ImgTimeTable));
            windows.Add(new WindowCollection(Public.switchWindow.mainWindow, "Setting", Public.switchWindow.ImgSetting));
            windows.Add(new WindowCollection(Public.ScheduleWindow, "Schedule", Public.switchWindow.ImgSchedule));

            if (Settings.Default.isTimeWindowShowed)
            {
                SetVisible(0, true);
            }
            Public.timeWindow.Left = Settings.Default.pTimeWindow.X;
            Public.timeWindow.Top = Settings.Default.pTimeWindow.Y;
            if (Settings.Default.isClassTableWindowShowed)
            {
                SetVisible(1, true);
            }
            Public.classTableWindow.Left = Settings.Default.pClassTableWindow.X;
            Public.classTableWindow.Top = Settings.Default.pClassTableWindow.Y;
            if (Settings.Default.isTimeTableWindowShowed)
            {
                SetVisible(2, true);
            }
            Public.timeTableWindow.Left = Settings.Default.pTimeTableWindow.X;
            Public.timeTableWindow.Top = Settings.Default.pTimeTableWindow.Y;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //3s后归位
            timer.Stop();
            Left = SystemParameters.PrimaryScreenWidth - 1;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Public.ExitProgram();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            timer.Stop();
            Left = SystemParameters.PrimaryScreenWidth - Width;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            timer.Start();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in windows)
            {
                if ((Image)sender == item.image)
                {
                    string s = "";
                    if (item.window.IsVisible)
                    {
                        item.window.Hide();
                        s = "/Resources/Switch/Close/" + item.PicName + ".png";
                    }
                    else
                    {
                        item.window.Show();
                        s = "/Resources/Switch/Open/" + item.PicName + ".png";
                    }
                    item.image.Source = new BitmapImage(new Uri(s, UriKind.RelativeOrAbsolute));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="show">是否展示</param>
        public void SetVisible(int index,bool show)
        {
            string s = "";
            if (show)
            {
                windows[index].window.Hide();
                s = "/Resources/Switch/Close/" + windows[index].PicName + ".png";
            }
            else
            {
                windows[index].window.Show();
                s = "/Resources/Switch/Open/" + windows[index].PicName + ".png";
            }
            windows[index].image.Source = new BitmapImage(new Uri(s, UriKind.RelativeOrAbsolute));
            return;
        }

        private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Public.ExitProgram();
        }
    }
}
