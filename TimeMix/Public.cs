using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drawing = System.Drawing;
using System.Windows;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace TimeMix
{
    /// <summary>
    /// 提供公共的窗体,方法
    /// </summary>
    public static class Public
    {
        public static TimeWindow timeWindow;
        public static ClassTableWindow classTableWindow;
        public static TimeTableWindow timeTableWindow;
        public static SwitchWindow switchWindow;
        public static ScheduleWindow ScheduleWindow;
        public static EditTimeWindow editTimeWindow;
        public static SettingWindow SettingWindow;
        /// <summary>
        /// Environment.CurrentDirectory + @"\File\Data"
        /// </summary>
        public static string PathData => Environment.CurrentDirectory + @"\File\Data";


        public static string pathTime = PathData + "\\" + Settings.Default.nameTime + "\\";
        public static string pathClass = PathData + "\\" + Settings.Default.nameClass;
        /// <summary>
        /// 获取长河时间
        /// </summary>
        /// <returns></returns>
        public static DateTime ChangHeTime()
        {
            return DateTime.Now.AddSeconds(Settings.Default.deltaTime);//长河时间
        }
        /// <summary>
        /// 检测是否超出屏幕
        /// </summary>
        /// <param name="window"></param>
        public static void PreventOutOfScreen(Window window)
        {
            if (window.Left < 0)
            {
                window.Left = 0;
            }
            if (window.Top < 0)
            {
                window.Top = 0;
            }
            if (window.Left + window.Width > SystemParameters.PrimaryScreenWidth)
            {
                window.Left = SystemParameters.PrimaryScreenWidth - window.Width;
            }
            if (window.Top + window.Height > SystemParameters.PrimaryScreenHeight)
            {
                window.Top = SystemParameters.PrimaryScreenHeight - window.Height;
            }

        }
        /// <summary>
        /// true-黑色字体,false-白色字体
        /// </summary>
        /// <returns></returns>
        public static Task<bool> IsBlack(double Left, double Top)
        {
            return Task.Run(() =>
            {
                int deltaX = 0;
                int deltaY = 0;
                Drawing.Rectangle rc = new Drawing.Rectangle((int)Left + deltaX, (int)Top + deltaY, (int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);
                var bitmap = new Drawing.Bitmap(1, 1);
                using (Drawing.Graphics g = Drawing.Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen((int)(rc.X * Settings.Default.dpi), (int)(rc.Y * Settings.Default.dpi), 0, 0, rc.Size, Drawing.CopyPixelOperation.SourceCopy);
                }
                Drawing.Color color = bitmap.GetPixel(0, 0);
                bitmap.Dispose();
                if (color.R + color.G + color.B > 384)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

        }
        /// <summary>
        /// 退出程序
        /// </summary>
        public static void ExitProgram()
        {
            foreach (var item in switchWindow.windows)
            {
                item.WindowToData();
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream("windows.dat", FileMode.Create, FileAccess.Write))
            {
                binaryFormatter.Serialize(fileStream, switchWindow.windows);
            }
            Settings.Default.Save();
            System.Diagnostics.Process.GetCurrentProcess().Kill();

        }
    }
}
