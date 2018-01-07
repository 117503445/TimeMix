using Microsoft.Win32;
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
using Settings = TimeMix.Properties.Settings;
namespace TimeMix
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string exePath = GetType().Assembly.Location;
            string exeName = "TimeMix";
            string exeFullName = exeName + ".exe";
            //  SelfRunning.TrySelfRunning(false, exeFullName, exePath);
            RegistryKey RKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            if (Settings.Default.isSelfRunning )
            {


                RKey.SetValue(exeName, exePath);

                //SelfRunning.TrySelfRunning(true,exeFullName,exePath);
            }
            else
            {
                string[] keyNames = RKey.GetValueNames();
                foreach (string keyName in keyNames)
                {
                    if (keyName.ToUpper() == exeName.ToUpper())
                    {
                        RKey.DeleteValue(exeName);
                    }
                }
            }

            RKey.Close();
            Public.switchWindow = new SwitchWindow();
            Public.switchWindow.Show();
            Close();
        }
    }
}
