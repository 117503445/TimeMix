using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// TimeTableWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TimeTableWindow : FunctionWindow
    {
        public TimeTableWindow()
        {
            InitializeComponent();
            Controls = new FrameworkElement[] { LblClass, LblBeginTime, LblEndTime, LblProgress,LineVertical,LineProgress };
        }

        public void Changedata(Core.TimeSection timeSection, string progress)
        {
            LblClass.Content = timeSection.name;
            string s1 = timeSection.beginTime.ToShortTimeString();
            if (s1.Length == 4)
            {
                s1 = "  " + s1;
            }
            LblBeginTime.Content = s1;
            string s2 = timeSection.endTime.ToShortTimeString();
            if (s2.Length == 4)
            {
                s2 = "  " + s2;
            }
            LblEndTime.Content = s2;
            LblProgress.Content = progress;
        }
        public void MoveLineProgress(double progress)
        {
            double p = progress / 100;
            LineProgress.Y1 = p * this.Height;
            LineProgress.Y2 = LineProgress.Y1;
        }

    }
}

