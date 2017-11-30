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

             labels =new Label[] { Lbl1, Lbl2, Lbl3, Lbl4, Lbl5,Lbl6,Lbl7,Lbl8,Lbl9,LblWeek};

    }

        private void Lbl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        public void ChangeColor()
        {
            if (Public.InBlackStyle(this))//浅色
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
        public void ChangeClass(List<string> classTable) {
            for (int i = 0; i < 9; i++)
            {
                labels[i].Content = classTable[i];
            }
        }
        public void ChangeWeek(DayOfWeek dayOfWeek) {
            string sWeek = "日一二三四五六";
            string s = "周" + sWeek.Substring((int)dayOfWeek,1);
            LblWeek.Content = s;

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            DragMove();
            Public.PreventOutOfScreen(this);
        }

    }
}
