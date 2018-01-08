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
using System.Xml.Linq;

namespace TimeMix
{
    /// <summary>
    /// ScheduleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScheduleWindow : FunctionWindow
    {

        public ScheduleWindow()
        {
            InitializeComponent();
            Controls = new Control[] { Lbl0, Lbl1, Lbl2 };
            Update();
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        public void Update()
        {
            foreach (var item in Controls)
            {
                ((Label)item).Content = "";
                item.Visibility = Visibility.Visible;
            }
            XElement x = XElement.Load(System.AppDomain.CurrentDomain.BaseDirectory + "/File/Schedule.xml");
            int i = 0;
            foreach (var field in x.Elements())
            {
                if (i > 2)
                {
                    break;
                }
               ((Label)Controls[i]).Content = field.Attribute("Name").Value;
                DateTime d = Convert.ToDateTime(field.Attribute("Date").Value);
                ((Label)Controls[i]).Content += "剩余" + ((d - DateTime.Now).Days) + "天";
                i++;
            }
            foreach (var item in Controls)
            {
                if ((string)((Label)item).Content == "")
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
