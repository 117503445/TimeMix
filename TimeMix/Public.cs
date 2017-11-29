using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drawing = System.Drawing;
using System.Windows;

namespace TimeMix
{
    public static class Public
    {
        public static TimeWindow timeWindow;
        public static ClassTableWindow classTableWindow;
        public static TimeTableWindow timeTableWindow;
        public static SwitchWindow switchWindow;

        /// <summary>
        /// 获取长河时间
        /// </summary>
        /// <returns></returns>
        public static DateTime ChangHetime()
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
        public static bool InBlackStyle(Window window)
        {
          Drawing.Rectangle rc = new System.Drawing.Rectangle((int)window.Left, (int)window.Top, (int)SystemParameters.PrimaryScreenWidth, (int)System.Windows.SystemParameters.PrimaryScreenHeight);
            var bitmap = new Drawing. Bitmap(1, 1);
            using (Drawing. Graphics g =Drawing. Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen((int)(rc.X * Settings.Default.dpi), (int)(rc.Y * Settings.Default.dpi), 0, 0, rc.Size, Drawing.CopyPixelOperation.SourceCopy);
            }
            System.Drawing.Color color = bitmap.GetPixel(0, 0);
            if (color.R + color.G + color.B > 384)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 退出程序
        /// </summary>
        public static void ExitProgram()
        {
            Settings.Default.isClassTableWindowShowed = classTableWindow.IsVisible;
            Settings.Default.isTimeTableWindowShowed = timeTableWindow.IsVisible;
            Settings.Default.isTimeWindowShowed = timeWindow.IsVisible;

            if (!Double.IsNaN(timeWindow.Left)&&!Double.IsNaN(timeWindow.Top))
            {
            Settings.Default.pTimeWindow = new Point(timeWindow.Left, timeWindow.Top);
            }
            if (!Double.IsNaN(classTableWindow.Left) && !Double.IsNaN(classTableWindow.Top))
            {
                Settings.Default.pClassTableWindow = new Point(classTableWindow.Left, classTableWindow.Top);
            }
            if (!Double.IsNaN(timeTableWindow.Left) && !Double.IsNaN(timeTableWindow.Top))
            {
                Settings.Default.pTimeTableWindow = new Point(timeTableWindow.Left, timeTableWindow.Top);
            }


            Settings.Default.Save();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
