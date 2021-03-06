﻿using System;
using System.IO;
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
using System.Runtime.Serialization.Formatters.Binary;
using Settings = TimeMix.Properties.Settings;
using User.SoftWare;

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

        public List<Windows> windows = new List<Windows>();
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
            Public.SettingWindow = new SettingWindow();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "windows.dat"))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream fileStream = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "windows.dat"))
                {
                    windows = binaryFormatter.Deserialize(fileStream) as List<Windows>;
                }
                windows[0].BindObject(Public.timeWindow, Public.switchWindow.ImgTime);
                windows[1].BindObject(Public.classTableWindow, Public.switchWindow.ImgClassTable);
                windows[2].BindObject(Public.timeTableWindow, Public.switchWindow.ImgTimeTable);
                windows[3].BindObject(Public.SettingWindow, Public.switchWindow.ImgSetting);
                windows[4].BindObject(Public.ScheduleWindow, Public.switchWindow.ImgSchedule);

            }
            else//创建新的
            {
                windows.Add(new Windows(Public.timeWindow, "Time", Public.switchWindow.ImgTime));
                windows.Add(new Windows(Public.classTableWindow, "ClassTable", Public.switchWindow.ImgClassTable));
                windows.Add(new Windows(Public.timeTableWindow, "TimeTable", Public.switchWindow.ImgTimeTable));
                windows.Add(new Windows(Public.SettingWindow, "Setting", Public.switchWindow.ImgSetting));
                windows.Add(new Windows(Public.ScheduleWindow, "Schedule", Public.switchWindow.ImgSchedule));
            }
            for (int i = 0; i < 5; i++)
            {
                SetVisible(i, windows[i].Showed);
            }
            foreach (var item in windows)
            {
                item.DataToWindow();
            }

            DispatcherTimer timer1000 = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer1000.Tick += Timer1000_Tick;
            timer1000.Start();
            DispatcherTimer timer10000 = new DispatcherTimer
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(10)
            };
            timer10000.Tick += ((s, eventargs) =>
            {
                foreach (var item in windows)
                {
                    if (item.Window is FunctionWindow window)
                    {
                        if (window.IsVisible)
                        {
                            window.ChangeColor();
                        }
                    }
                }

            });

        }

        private void Timer1000_Tick(object sender, EventArgs e)
        {
            if (Settings.Default.isCheckIn)
            {
                OpenCheckIn.Check();
            }

            foreach (var item in windows)
            {
                if (item.Window is FunctionWindow window)
                {
                    if (window.IsVisible && FunctionWindow.ColorActive)
                    {
                        window.ChangeColor();
                    }
                    window.Topmost = true;
                }
            }
            Public.SettingWindow.TbChangeHeTime.Text = "长河时间 " + Public.ChangHeTime().ToString();
#if !DEBUG
            try
            {
                Core.Load(Public.pathTime, Public.pathClass);
                Core.CurrentTime = Public.ChangHeTime();
            }
            catch (Exception ex)
            {
                ULogger.WriteException(ex);
                Public.timeTableWindow.ErrorLoad();
                Public.classTableWindow.ErrorLoad();
                return;
            }
#else
            Core.Load(Public.pathTime, Public.pathClass);
            Core.CurrentTime = Public.ChangHeTime();
#endif
            //timeWindow.Topmost = true;
            Public.timeTableWindow.Changedata(Core.CurrentTimeSection, Core.FormatProgress(Core.Progress));
            Public.timeTableWindow.MoveLineProgress(Core.Progress);
            Public.timeWindow.ChangeTime();
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
                if ((Image)sender == item.Image)
                {
                    string s = "";
                    if (item.Window.IsVisible)
                    {
                        item.Window.Hide();
                        s = "/Resources/Switch/Close/" + item.PicName + ".png";
                    }
                    else
                    {
                        item.Window.Show();
                        s = "/Resources/Switch/Open/" + item.PicName + ".png";
                    }
                    item.Image.Source = new BitmapImage(new Uri(s, UriKind.RelativeOrAbsolute));
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
                windows[index].Window.Show();
                s = "/Resources/Switch/Open/" + windows[index].PicName + ".png";
            }
            else
            {
                windows[index].Window.Hide();
                s = "/Resources/Switch/Close/" + windows[index].PicName + ".png";
            }
            windows[index].Image.Source = new BitmapImage(new Uri(s, UriKind.RelativeOrAbsolute));
        }

        private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Public.ExitProgram();
        }
    }
    [Serializable]
    public class Windows
    {
        [NonSerialized]
        /// <summary>
        /// 窗体
        /// </summary>
        private Window window;
        /// <summary>
        /// 图片资源名称
        /// </summary>
        private string picName;
        [NonSerialized]
        /// <summary>
        /// 对应图片
        /// </summary>
        private Image image;
        private double x;
        private double y;
        private bool showed;

        public Windows(Window window, string picName, Image image)
        {
            Window = window;
            PicName = picName;
            Image = image;
        }
        public void BindObject(Window window, Image image)
        {
            Window = window;
            Image = image;
        }
        /// <summary>
        /// 窗体的X位置
        /// </summary>
        public double X { get => x; set => x = value; }
        /// <summary>
        /// 窗体的Y位置
        /// </summary>
        public double Y { get => y; set => y = value; }
        /// <summary>
        /// 是否出现
        /// </summary>
        public bool Showed { get => showed; set => showed = value; }
        public Image Image { get => image; set => image = value; }
        public string PicName { get => picName; set => picName = value; }
        public Window Window { get => window; set => window = value; }

        /// <summary>
        /// 读取Data到Window
        /// </summary>
        public void DataToWindow()
        {
            Window.Left = X;
            Window.Top = Y;
            if (showed)
            {
                Window.Show();
            }
            else
            {
                Window.Hide();
            }

        }
        /// <summary>
        /// 将Window写入Data
        /// </summary>
        public void WindowToData()
        {
            Showed = Window.IsVisible;
            if (!double.IsNaN(Window.Left))
            {
                X = Window.Left;
                Y = Window.Top;
            }


        }
    }
}
