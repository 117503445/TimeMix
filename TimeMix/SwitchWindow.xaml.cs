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

        public SwitchWindow(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
            Left = SystemParameters.PrimaryScreenWidth - 1;
            timer.Tick += Timer_Tick;

            if (Settings.Default.isTimeWindowShowed)
            {
                SetVisible(0);
            }
            Public.timeWindow.Left = Settings.Default.pTimeWindow.X;
            Public.timeWindow.Top = Settings.Default.pTimeWindow.Y;
            if (Settings.Default.isClassTableWindowShowed)
            {
                SetVisible(1);
            }
            Public.classTableWindow.Left = Settings.Default.pClassTableWindow.X;
            Public.classTableWindow.Top = Settings.Default.pClassTableWindow.Y;
            if (Settings.Default.isTimeTableWindowShowed)
            {
                SetVisible(2);
            }
            Public.timeTableWindow.Left = Settings.Default.pTimeTableWindow.X;
            Public.timeTableWindow.Top = Settings.Default.pTimeTableWindow.Y;
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
                case "ImgOther":
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
            switch (index)
            {
                case 0:
                    if (b[index])//关闭操作
                    {
                        b[index] = false;
                        Public.timeWindow.Hide();
                        ImgTime.Source = new BitmapImage(new Uri("/Resources/Switch/Close/Time.png", UriKind.RelativeOrAbsolute));
                    }
                    else//展开操作
                    {
                        b[index] = true;
                        Public.timeWindow.Show();
                        ImgTime.Source = new BitmapImage(new Uri("/Resources/Switch/Open/Time.png", UriKind.RelativeOrAbsolute));
                    }
                    break;
                case 1:
                    if (b[index])//关闭操作
                    {
                        b[index] = false;
                        Public.classTableWindow.Hide();
                        ImgClassTable.Source = new BitmapImage(new Uri("/Resources/Switch/Close/ClassTable.png", UriKind.RelativeOrAbsolute));
                    }
                    else//展开操作
                    {
                        b[index] = true;
                        Public.classTableWindow.Show();
                        ImgClassTable.Source = new BitmapImage(new Uri("/Resources/Switch/Open/ClassTable.png", UriKind.RelativeOrAbsolute));
                    }
                    break;
                case 2:
                    if (b[index])//关闭操作
                    {
                        b[index] = false;
                        Public.timeTableWindow.Hide();
                        ImgTimeTable.Source = new BitmapImage(new Uri("/Resources/Switch/Close/TimeTable.png", UriKind.RelativeOrAbsolute));
                    }
                    else//展开操作
                    {
                        b[index] = true;
                        Public.timeTableWindow.Show();
                        ImgTimeTable.Source = new BitmapImage(new Uri("/Resources/Switch/Open/TimeTable.png", UriKind.RelativeOrAbsolute));
                    }
                    break;
                case 3:
                    if (b[index])//关闭操作
                    {
                        b[index] = false;
                        mainWindow.Hide();
                        ImgSetting.Source = new BitmapImage(new Uri("/Resources/Switch/Close/Setting.png", UriKind.RelativeOrAbsolute));
                    }
                    else//展开操作
                    {
                        b[index] = true;
                        mainWindow.Show();
                        ImgSetting.Source = new BitmapImage(new Uri("/Resources/Switch/Open/Setting.png", UriKind.RelativeOrAbsolute));
                    }
                    break;

                case 4:
                    if (b[index])//关闭操作
                    {
                        b[index] = false;
                        Public.ScheduleWindow.Hide();
                        ImgOther.Source = new BitmapImage(new Uri("/Resources/Switch/Close/other.png", UriKind.RelativeOrAbsolute));
                    }
                    else//展开操作
                    {
                        b[index] = true;
                        Public.ScheduleWindow.Show();
                        ImgOther.Source = new BitmapImage(new Uri("/Resources/Switch/Open/other.png", UriKind.RelativeOrAbsolute));
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
