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
    /// TimeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TimeWindow : Window
    {
        /// <summary>
        /// 更新时间
        /// </summary>
        public void ChangeTime()
        {
            LblBig.Content = DateTime.Now.ToShortTimeString();
            LblSmall.Content = DateTime.Now.Second.ToString();
        }

        public TimeWindow()
        {
            InitializeComponent();
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

        public void ChangeColor() {
            System.Drawing.Rectangle rc = new System.Drawing.Rectangle((int)Left, (int)Top, (int)SystemParameters.PrimaryScreenWidth, (int)System.Windows.SystemParameters.PrimaryScreenHeight);
            var bitmap = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen((int)(rc.X*Settings.Default.dpi), (int)(rc.Y*Settings.Default.dpi), 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
            }
            System.Drawing.Color color = bitmap.GetPixel(0, 0);
            if (color.R+color.G+color.B>384)//浅色
            {
                LblBig.Foreground= new SolidColorBrush(Colors.Black);
                LblSmall.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                LblBig.Foreground = new SolidColorBrush(Colors.White);
                LblSmall.Foreground = new SolidColorBrush(Colors.White);
            }

        }


    }
}
