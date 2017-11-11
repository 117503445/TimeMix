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

namespace TimeMix
{
    /// <summary>
    /// ManageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ManageWindow : Window
    {
        public delegate void ManageHandler(bool IsVisible);
        public event ManageHandler SetVisibleClassTable;
        public event ManageHandler SetVisibleTimeWindow;
        public event ManageHandler SetVisibleMainWindow;

        public ManageWindow()
        {
            InitializeComponent();
        }

        private void ChkMainWindow_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleMainWindow((bool)ChkMainWindow.IsChecked);
        }

        private void ChkClassTableWindow_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleClassTable((bool)ChkClassTableWindow.IsChecked);
        }

        private void ChkTimeWindow_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleTimeWindow((bool)ChkTimeWindow.IsChecked);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
