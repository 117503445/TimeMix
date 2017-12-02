using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace TimeMix
{
    /// <summary>
    /// TimeTableWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TimeTableWindow : Window
    {
        Label[] Labels;
        public TimeTableWindow()
        {
            InitializeComponent();
            Labels = new Label[] { LblClass, LblBeginTime, LblEndTime, LblProgress };

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            Public.PreventOutOfScreen(this);
        }
        public void Changedata(Core.TimeSection timeSection,string progress)
        {
            LblClass.Content = timeSection.name;
            LblBeginTime.Content = timeSection.beginTime.ToShortTimeString();
            LblEndTime.Content = timeSection.endTime.ToShortTimeString();
            LblProgress.Content = progress;
        }
        public void ChangeColor()
        {


            foreach (var item in Labels)
            {
                    if (Public.InBlackStyle(this))//浅色
                    {
                        item.Foreground = new SolidColorBrush(Colors.Black);
                    }
                    else
                    {
                        item.Foreground = new SolidColorBrush(Colors.White);
                    }
                }
            }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
    }

