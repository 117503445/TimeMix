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
        public static EditTimeWindow editTimeWindow;
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
        public static bool InBlackStyle(Window window)
        {
            if (!window.IsVisible)
            {
                return false;
            }
            bool[] b = new bool[3];

            b[0] = IsBlack(window, 0, 0);

            b[1] = IsBlack(window, (int)window.Width / 2, (int)window.Height / 2);

            b[2] = IsBlack(window, (int)window.Width, (int)window.Height);


            int count = 0;
            foreach (var item in b)
            {
                if (item)
                {
                    count++;
                }
            }
            return count > 1;
        }
        /// <summary>
        /// true-黑色字体,false-白色字体
        /// </summary>
        /// <returns></returns>
        private static bool IsBlack(Window window, int deltaX, int deltaY)
        {
            Drawing.Rectangle rc = new Drawing.Rectangle((int)window.Left + deltaX, (int)window.Top + deltaY, (int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);
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
        }
        /// <summary>
        /// 退出程序
        /// </summary>
        public static void ExitProgram()
        {
            Settings.Default.isClassTableWindowShowed = classTableWindow.IsVisible;
            Settings.Default.isTimeTableWindowShowed = timeTableWindow.IsVisible;
            Settings.Default.isTimeWindowShowed = timeWindow.IsVisible;

            if (!Double.IsNaN(timeWindow.Left) && !Double.IsNaN(timeWindow.Top))
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
            Application.Current.Shutdown();
        }
    }
}
