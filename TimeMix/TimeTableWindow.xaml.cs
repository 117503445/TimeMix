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
            if (Left < 0)
            {
                Left = 0;
            }
            if (Top < 0)
            {
                Top = 0;
            }
            if (Left + Width > SystemParameters.PrimaryScreenWidth)
            {
                Left = SystemParameters.PrimaryScreenWidth - Width;
            }
            if (Top + Height > SystemParameters.PrimaryScreenHeight)
            {
                Top = SystemParameters.PrimaryScreenHeight - Height;
            }
        }
        public void Changedata(TimeSection timeSection)
        {
            LblClass.Content = timeSection.name;
            LblBeginTime.Content = timeSection.beginTime.ToShortTimeString();
            LblEndTime.Content = timeSection.endTime.ToShortTimeString();
            LblProgress.Content = timeSection.progress;
        }
        public void ChangeColor()
        {
            System.Drawing.Rectangle rc = new System.Drawing.Rectangle((int)Left, (int)Top, (int)SystemParameters.PrimaryScreenWidth, (int)System.Windows.SystemParameters.PrimaryScreenHeight);
            var bitmap = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen((int)(rc.X * Settings.Default.dpi), (int)(rc.Y * Settings.Default.dpi), 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
            }
            System.Drawing.Color color = bitmap.GetPixel(0, 0);


            foreach (var item in Labels)
            {
                    if (color.R + color.G + color.B > 384)//浅色
                    {
                        item.Foreground = new SolidColorBrush(Colors.Black);
                    }
                    else
                    {
                        item.Foreground = new SolidColorBrush(Colors.White);
                    }
                }
            }
        }
    }

