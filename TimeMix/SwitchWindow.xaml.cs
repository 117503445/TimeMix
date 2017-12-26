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
        /// <summary>
        /// 用于动画
        /// </summary>
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
        public SwitchWindow()
        {
            InitializeComponent();
            windows.Clear();
            Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
            Left = SystemParameters.PrimaryScreenWidth - 1;
            timer.Tick += Timer_Tick;
            Loaded += SwitchWindow_Loaded;
        }

        private void SwitchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Public.timeWindow = new TimeWindow();
            Public.classTableWindow = new ClassTableWindow();
            Public.timeTableWindow = new TimeTableWindow();
            Public.editTimeWindow = new EditTimeWindow();
            Public.ScheduleWindow = new ScheduleWindow();
            Public.editTimeWindow = new EditTimeWindow();
            Public.SettingWindow = new SettingWindow();
            windows.Add(new WindowCollection(Public.timeWindow, "Time", Public.switchWindow.ImgTime));
            windows.Add(new WindowCollection(Public.classTableWindow, "ClassTable", Public.switchWindow.ImgClassTable));
            windows.Add(new WindowCollection(Public.timeTableWindow, "TimeTable", Public.switchWindow.ImgTimeTable));
            windows.Add(new WindowCollection(Public.SettingWindow, "Setting", Public.switchWindow.ImgSetting));
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





        }

        private void Timer1000_Tick(object sender, EventArgs e)
        {
            Public.timeTableWindow.ChangeColor();
            Public.timeWindow.ChangeColor();
            Public.classTableWindow.ChangeColor();
            Public.ScheduleWindow.ChangeColor();



            //TbChangeHeTime.Text = "长河时间 " + Public.ChangHeTime().ToString();
            Public.classTableWindow.Topmost = true;
#if !DEBUG
            try
            {
                Core.Update(Public.pathTime, Public.pathClass, Public.ChangHeTime());
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return;
            }
#else
            Core.Update(Public.pathTime, Public.pathClass, Public.ChangHeTime());

#endif
            //timeWindow.Topmost = true;

            Public.timeTableWindow.Changedata(Core.CurrentTimeSection, Core.Progress);
            int week = (int)Public.ChangHeTime().DayOfWeek;
            if (Public.ChangHeTime().CompareTo(Core.LastClassEndTime[week]) > 0 && Settings.Default.isTomorrowClass)
            {
                //明天课表
                Public.classTableWindow.ChangeClass(Core.GetClass((int)Public.ChangHeTime().AddDays(1).DayOfWeek), true);
                Public.classTableWindow.ChangeWeek(Public.ChangHeTime().AddDays(1).DayOfWeek);
            }
            else
            {
                //今天课表
                Public.classTableWindow.ChangeClass(Core.GetClass());
                Public.classTableWindow.ChangeWeek(Public.ChangHeTime().DayOfWeek);

            }

        }


        private void Timer100_Tick(object sender, EventArgs e)
        {
            Public.timeWindow.ChangeTime();
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
        public void SetVisible(int index, bool show)
        {
            string s = "";
            if (show)
            {
                windows[index].window.Show();
                s = "/Resources/Switch/Open/" + windows[index].PicName + ".png";
            }
            else
            {
                windows[index].window.Hide();
                s = "/Resources/Switch/Close/" + windows[index].PicName + ".png";
            }
            windows[index].image.Source = new BitmapImage(new Uri(s, UriKind.RelativeOrAbsolute));
        }

        private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Public.ExitProgram();
        }
    }
}
