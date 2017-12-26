using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    public partial class TimeWindow : Window, IFunctionWindow
    {
        /// <summary>
        /// 更新时间
        /// </summary>
        public void ChangeTime()
        {
            string s1 = Public.ChangHeTime().ToShortTimeString();
            if (s1.Length == 4)
            {
                s1 = "0" + s1;
            }
            LblBig.Content = s1;
            string s2 = Public.ChangHeTime().Second.ToString();
            if (s2.Length == 1)
            {
                s2 = "0" + s2;
            }
            LblSmall.Content = s2;
        }

        public TimeWindow()
        {
            InitializeComponent();
        }

        public void GetColorDone(IAsyncResult itfAR)
        {
            AsyncResult ar = (AsyncResult)itfAR;
            Public.GetColor b = (Public.GetColor)ar.AsyncDelegate;
            Console.WriteLine("Done");

            if (b.EndInvoke(itfAR))//浅色
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    LblBig.Foreground = new SolidColorBrush(Colors.Black);
                    LblSmall.Foreground = new SolidColorBrush(Colors.Black);
                }));
            }
            else
            {
                Dispatcher.Invoke(new Action(delegate
                 {
                     LblBig.Foreground = new SolidColorBrush(Colors.White);
                     LblSmall.Foreground = new SolidColorBrush(Colors.White);
                 }));
            }

        }

        public void ChangeColor()
        {
            Public.getColor.BeginInvoke(this, new AsyncCallback(GetColorDone), null);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            Public.PreventOutOfScreen(this);
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
