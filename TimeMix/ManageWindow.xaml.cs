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
        public delegate void ManageHandler(Window window,bool IsVisible);
        public event ManageHandler SetVisible;
        MainWindow mainWindow;
        
        public ManageWindow(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
        }

        private void ChkMainWindow_Click(object sender, RoutedEventArgs e)
        {
            SetVisible(mainWindow,(bool)ChkMainWindow.IsChecked);
        }

        private void ChkClassTableWindow_Click(object sender, RoutedEventArgs e)
        {
            SetVisible(mainWindow.classTableWindow,(bool)ChkClassTableWindow.IsChecked);
        }

        private void ChkTimeWindow_Click(object sender, RoutedEventArgs e)
        {
            SetVisible(mainWindow.timeWindow,(bool)ChkTimeWindow.IsChecked);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void ChkTimeTableWindow_Click(object sender, RoutedEventArgs e)
        {
            SetVisible(mainWindow.timeTableWindow, (bool)ChkTimeTableWindow.IsChecked);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
