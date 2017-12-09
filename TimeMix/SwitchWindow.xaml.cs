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
        bool[] b = new bool[5];

        DispatcherTimer timer = new DispatcherTimer
        {
            IsEnabled = true,
            Interval = TimeSpan.FromSeconds(3)
        };
        class WindowCollection
        {
            public Window window;
            public string PicName;
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
            mainWindow = window;
            Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
            Left = SystemParameters.PrimaryScreenWidth - 1;
            timer.Tick += Timer_Tick;

            if (Settings.Default.isTimeWindowShowed)
            {
                //  SetVisible(0);
            }
            Public.timeWindow.Left = Settings.Default.pTimeWindow.X;
            Public.timeWindow.Top = Settings.Default.pTimeWindow.Y;
            if (Settings.Default.isClassTableWindowShowed)
            {
                //   SetVisible(1);
            }
            Public.classTableWindow.Left = Settings.Default.pClassTableWindow.X;
            Public.classTableWindow.Top = Settings.Default.pClassTableWindow.Y;
            if (Settings.Default.isTimeTableWindowShowed)
            {
                //SetVisible(2);
            }
            Public.timeTableWindow.Left = Settings.Default.pTimeTableWindow.X;
            Public.timeTableWindow.Top = Settings.Default.pTimeTableWindow.Y;

            Loaded += SwitchWindow_Loaded;

        }

        private void SwitchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.windows.Clear();
            this.windows.Add(new WindowCollection(Public.timeWindow, "Time", Public.switchWindow.ImgTime));
            this.windows.Add(new WindowCollection(Public.classTableWindow, "ClassTable", Public.switchWindow.ImgClassTable));
            this.windows.Add(new WindowCollection(Public.timeTableWindow, "TimeTable", Public.switchWindow.ImgTimeTable));
            this.windows.Add(new WindowCollection(Public.switchWindow.mainWindow, "Setting", Public.switchWindow.ImgSetting));
            this.windows.Add(new WindowCollection(Public.ScheduleWindow, "Schedule", Public.switchWindow.ImgSchedule));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Left = SystemParameters.PrimaryScreenWidth - 1;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Public.ExitProgram();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

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
            string controlName = ((Image)sender).Name;
            switch (controlName)
            {
                case "ImgTime":
                    SetVisible(0);
                    break;
                case "ImgClassTable":
                    SetVisible(1);
                    break;
                case "ImgTimeTable":
                    SetVisible(2);
                    break;
                case "ImgSetting":
                    SetVisible(3);
                    break;
                case "ImgSchedule":
                    SetVisible(4);
                    break;
                case "ImgClose":
                    Public.ExitProgram();
                    break;

                default:
                    break;
            }

        }

        public void SetVisible(int index)
        {
            string s = "";
            if (b[index])
            {

                windows[index].window.Hide();
                s = "/Resources/Switch/Close/" + windows[index].PicName + ".png";

            }
            else
            {
                windows[index].window.Show();
                s = "/Resources/Switch/Open/" + windows[index].PicName + ".png";

            }
            b[index] = !b[index];
            windows[index].image.Source = new BitmapImage(new Uri(s, UriKind.RelativeOrAbsolute));

            return;

        }


        private void Window_Activated(object sender, EventArgs e)
        {

        }
    }
}
