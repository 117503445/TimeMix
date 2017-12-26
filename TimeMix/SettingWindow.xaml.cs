﻿using System;
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
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
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
            TbDeltaTime.Text = Settings.Default.deltaTime.ToString();
            ChkTomorrowClass.IsChecked = Settings.Default.isTomorrowClass;
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
            Settings.Default.Save();
        }

        private void CboClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.nameClass = CboClass.SelectedItem.ToString();
            Public.pathClass = Public.PathData + "/" + CboClass.SelectedItem.ToString();
            Settings.Default.Save();
        }

        private void Tbdpi_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Settings.Default.dpi = double.Parse(Tbdpi.Text.ToString());
                Settings.Default.Save();
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
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }

        }

        private void ChkTomorrowClass_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.isTomorrowClass = (bool)ChkTomorrowClass.IsChecked;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}