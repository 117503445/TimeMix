using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeChanger
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static System.Windows.Threading.DispatcherTimer Timer1 = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            UpdateCurrentTime(new object(), new EventArgs());
            TxtMyTime.Text = DateTime.Now.ToLocalTime().ToString();
            Timer1.Tick += new EventHandler(UpdateCurrentTime);
            Timer1.Interval = new TimeSpan(0, 0, 0, 0, 500);
            Timer1.Start();
        }

        /// <summary>
        /// 绑定在Timer1上的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateCurrentTime(object sender, EventArgs e)
        {
            TbSystemTimeShow.Text = DateTime.Now.ToLocalTime().ToString();
            TbNetworkTimeShow.Text = GMT2Local(GetNetDateTime()).ToString();

        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            SetSystemDateTime.SetLocalTimeByStr(TxtMyTime.Text);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }
        public class SetSystemDateTime
        {
            [DllImport("Kernel32.dll")]
            public static extern bool SetLocalTime(ref SystemTime sysTime);

            public static bool SetLocalTimeByStr(string timestr)
            {
                try
                {
                    bool flag = false;
                    SystemTime sysTime = new SystemTime();
                    DateTime dt = Convert.ToDateTime(timestr);
                    sysTime.wYear = Convert.ToUInt16(dt.Year);
                    sysTime.wMonth = Convert.ToUInt16(dt.Month);
                    sysTime.wDay = Convert.ToUInt16(dt.Day);
                    sysTime.wHour = Convert.ToUInt16(dt.Hour);
                    sysTime.wMinute = Convert.ToUInt16(dt.Minute);
                    sysTime.wSecond = Convert.ToUInt16(dt.Second);
                    try
                    {
                        flag = SetSystemDateTime.SetLocalTime(ref sysTime);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("SetSystemDateTime函数执行异常" + e.Message);
                    }
                    return flag;
                }
                catch (Exception)
                {

                    throw;
                }



            }
        }

        private void TbNetworkTimeShow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetSystemDateTime.SetLocalTimeByStr(TbNetworkTimeShow.Text);
        }


        /// <summary>  
        /// 获取网络日期时间  
        /// </summary>  
        /// <returns></returns>  
        public static string GetNetDateTime()
        {
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse();
                headerCollection = response.Headers;
                foreach (var h in headerCollection.AllKeys)
                { if (h == "Date") { datetime = headerCollection[h]; } }
                return datetime;
            }
            catch (Exception) { return datetime; }
            finally
            {
                if (request != null)
                { request.Abort(); }
                if (response != null)
                { response.Close(); }
                if (headerCollection != null)
                { headerCollection.Clear(); }
            }
        }


        /// <summary>    
        /// GMT时间转成本地时间   
        /// DateTime dt1 = GMT2Local("Thu, 29 Sep 2014 07:04:39 GMT");  
        /// 转换后的dt1为：2014-9-29 15:04:39  
        /// DateTime dt2 = GMT2Local("Thu, 29 Sep 2014 15:04:39 GMT+0800");  
        /// 转换后的dt2为：2014-9-29 15:04:39  
        /// </summary>    
        /// <param name="gmt">字符串形式的GMT时间</param>    
        /// <returns></returns>    
        public static DateTime GMT2Local(string gmt)
        {
            DateTime dt = DateTime.MinValue;
            try
            {
                string pattern = "";
                if (gmt.IndexOf("+0") != -1)
                {
                    gmt = gmt.Replace("GMT", "");
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss zzz";
                }
                if (gmt.ToUpper().IndexOf("GMT") != -1)
                {
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
                }
                if (pattern != "")
                {
                    dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    dt = dt.ToLocalTime();
                }
                else
                {
                    dt = Convert.ToDateTime(gmt);
                }
            }
            catch
            {
            }
            return dt;
        }




    }
}

