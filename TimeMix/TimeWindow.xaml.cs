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
            string s1= Public.ChangHetime().ToShortTimeString();
            if (s1.Length==4)
            {
                s1 = "0" + s1;
            }
            LblBig.Content = s1;
            string s2 = Public.ChangHetime().Second.ToString();
            if (s2.Length==1)
            {
                s2 = "0" + s2;
            }
            LblSmall.Content = s2;
        }

        public TimeWindow()
        {
            InitializeComponent();
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

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
