using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TimeMix.Properties;
using Settings = TimeMix.Properties.Settings;

namespace TimeMix
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {

        public SettingWindow()
        {
            InitializeComponent();
            if (Settings.Default.isEnableNetTime)
            {
                SetDeltaTimeByNet();
            }
        }
        /// <summary>
        /// 通过网络时间设置时间差
        /// </summary>
        private async void SetDeltaTimeByNet()
        {

            await Task.Run(() =>
            {
                BtnSetDeltaTimeByNet.Dispatcher.Invoke(() =>
                {
                    BtnSetDeltaTimeByNet.IsEnabled = false;
                });
                try
                {
                    string t = File.ReadAllText(Settings.Default.NetPath);
                    Settings.Default.deltaTime = Convert.ToInt32(t);
                    BtnSetDeltaTimeByNet.Dispatcher.Invoke(() =>
                    {
                        BtnSetDeltaTimeByNet.IsEnabled = true;
                        BtnSetDeltaTimeByNet.Content = ":)";
                    }); Thread.Sleep(1000);//如果成功,给予一个美丽的微笑!
                    BtnSetDeltaTimeByNet.Dispatcher.Invoke(() =>
                    {
                        BtnSetDeltaTimeByNet.Content = "立即刷新";
                    });
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
#if !DEBUG
                    MessageBox.Show(@"在试图获取网络时间时出错,若一直出现此错误请关闭'网络时间'功能");
#endif
                }
                BtnSetDeltaTimeByNet.Dispatcher.Invoke(() =>
                {
                    BtnSetDeltaTimeByNet.IsEnabled = true;
                });
            });

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(Public.PathData);
            if (dir.Exists)
            {
                FileInfo[] fiList = dir.GetFiles();
                DirectoryInfo[] dList = dir.GetDirectories();
                List<string> timeList = new List<string>();
                List<string> classList = new List<string>();
                foreach (var item in fiList)
                {
                    if (item.Name.Length >= 2 && item.Name.Substring(0, 2) == "课表")
                    {
                        classList.Add(item.Name);
                    }
                }

                foreach (var item in dList)
                {
                    if (item.Name.Length >= 2 && item.Name.Substring(0, 2) == "时间")
                    {
                        timeList.Add(item.Name);
                    }
                }
                CboTime.ItemsSource = timeList;
                CboClass.ItemsSource = classList;

            }
            //TimeCore.Core core = new TimeCore.Core(@"C:\User\File\Program\TimeMix\TimeMix\File\Data\Source\时间NEW.txt", @"C:\User\File\Program\TimeMix\TimeMix\File\Data\Source\课表NEW.txt", deltaTime: 0);
            CboTime.SelectedItem = Settings.Default.nameTime;
            CboClass.SelectedItem = Settings.Default.nameClass;
            if (CboTime.SelectedItem == null)
            {
                CboTime.SelectedIndex = 0;
            }
            if (CboClass.SelectedItem == null)
            {
                CboClass.SelectedIndex = 0;
            }
            Tbdpi.Text = Settings.Default.dpi.ToString();
            
            ChkTomorrowClass.IsChecked = Settings.Default.isTomorrowClass;
            ChkNetTime.IsChecked = Settings.Default.isEnableNetTime;
            TbNetPath.Text = Settings.Default.NetPath;

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Public.ExitProgram();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            Public.switchWindow.SetVisible(3, false);
            e.Cancel = true;
        }
        private void CboTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.nameTime = CboTime.SelectedItem.ToString();
            Public.pathTime = Public.PathData + "/" + CboTime.SelectedItem.ToString() + "/";

        }
        private void CboClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.nameClass = CboClass.SelectedItem.ToString();
            Public.pathClass = Public.PathData + "/" + CboClass.SelectedItem.ToString();

        }
        private void Tbdpi_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Settings.Default.dpi = double.Parse(Tbdpi.Text.ToString());
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }
        private void TbDeltaTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Settings.Default.deltaTime = int.Parse(TbDeltaTime.Text.ToString());
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }

        }
        private void ChkTomorrowClass_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.isTomorrowClass = (bool)ChkTomorrowClass.IsChecked;
            MessageBox.Show("Click" + ((bool)ChkTomorrowClass.IsChecked).ToString());
        }
        private void BtnMinusTime_Click(object sender, RoutedEventArgs e)
        {
            TbDeltaTime.Text = (int.Parse(TbDeltaTime.Text) - 1).ToString();
        }
        private void BtnAddTime_Click(object sender, RoutedEventArgs e)
        {
            TbDeltaTime.Text = (int.Parse(TbDeltaTime.Text) + 1).ToString();
        }
        private void BtnData_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + "File\\data");
        }
        private void BtnTimeMix_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", ".");
        }
        private void BtnOpenEditTime_Click(object sender, RoutedEventArgs e)
        {
            Public.editTimeWindow.Show();
        }
        /// <summary>
        /// 初始化设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefaultSetting_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Reset();
            //重启
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();

        }
        private void ChkNetTime_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.isEnableNetTime = (bool)ChkNetTime.IsChecked;

        }
        private void TbNetPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Default.NetPath = TbNetPath.Text;
        }

        private void BtnSetDeltaTimeByNet_Click(object sender, RoutedEventArgs e)
        {
            SetDeltaTimeByNet();
        }
    }
}
