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
    /// ClassTableWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClassTableWindow : Window
    {
        Label[] labels;
        public ClassTableWindow()
        {
            InitializeComponent();
             labels =new Label[] { Lbl1, Lbl2, Lbl3, Lbl4, Lbl5,Lbl6,Lbl7,Lbl8,Lbl9};
    }

        private void Lbl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public void ChangeColor()
        {
            System.Drawing.Rectangle rc = new System.Drawing.Rectangle((int)Left, (int)Top, (int)SystemParameters.PrimaryScreenWidth, (int)System.Windows.SystemParameters.PrimaryScreenHeight);
            var bitmap = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
            }
            System.Drawing.Color color = bitmap.GetPixel(0, 0);

            if (color.R + color.G + color.B > 384)//浅色
            {
                foreach (var item in labels)
                {
                    item.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                foreach (var item in labels)
                {
                    item.Foreground = new SolidColorBrush(Colors.White);
                }
            }
        }
        public void ChangeClass(string[] classTable) {

            for (int i = 0; i < 9; i++)
            {
                labels[i].Content = classTable[i];
                
            }
        }
    }
}
