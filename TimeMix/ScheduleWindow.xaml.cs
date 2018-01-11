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
using System.Windows.Threading;
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
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromHours(5)
            };
            timer.Tick += (s, e) => { Update(); };
            timer.Start();
            Load();
            Update();
        }
        Schedules[] schedules;
        /// <summary>
        /// 加载数据
        /// </summary>
        public void Load()
        {
            schedules = new Schedules[] { new Schedules(Lbl0), new Schedules(Lbl1), new Schedules(Lbl2) };
            XElement x = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "/File/Schedule.xml");
            int i = 0;
            foreach (var field in x.Elements())
            {
                if (i > 2)
                {
                    break;
                }
                schedules[i].Name = field.Attribute("Name").Value;
                schedules[i].Time = Convert.ToDateTime(field.Attribute("Date").Value);
                i++;
            }

        }
        /// <summary>
        /// 更新数据
        /// </summary>
        public void Update()
        {
            foreach (var item in schedules)
            {
                item.Update();
            }
        }

        class Schedules
        {
            string name;
            DateTime time;
            Label label;

            public Schedules(Label label)
            {
                this.label = label;
            }

            public string Name { get => name; set => name = value; }
            public DateTime Time { get => time; set => time = value; }
            public Label Label { get => label; set => label = value; }

            public override string ToString()
            {
                string s = Name + "剩余" + ((Time - DateTime.Now).Days) + "天";
                return s;
            }
            public void Update()
            {
                if (name == null)
                {

                    label.Visibility = Visibility.Collapsed;
                }
                else
                {
                    label.Visibility = Visibility.Visible;

                }
                label.Content = this.ToString();
            }
        }
    }

}
