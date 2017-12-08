﻿using System;
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
    public partial class ScheduleWindow : Window, IFunctionWindow
    {
        TextBlock[] TextBlock = new TextBlock[3];
        public ScheduleWindow()
        {
            InitializeComponent();
            TextBlock[0] = Tb0;
            TextBlock[1] = Tb1;
            TextBlock[2] = Tb2;
            foreach (var item in TextBlock)
            {
                item.Text = "";
                item.Visibility = Visibility.Visible;
            }
            XElement x = XElement.Load(Environment.CurrentDirectory + "/File/Schedule.xml");
            int i = 0;
            foreach (var field in x.Elements())
            {
                if (i > 2)
                {
                    break;
                }
                TextBlock[i].Text = field.Attribute("Name").Value;
                DateTime d = Convert.ToDateTime(field.Attribute("Date").Value);
                TextBlock[i].Text += "剩余" + (d.Day - DateTime.Now.Day) + "天";
                i++;
            }
            foreach (var item in TextBlock)
            {
                if (item.Text == "")
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
        }

        public void ChangeColor()
        {
            if (Public.InBlackStyle(this))//浅色
            {
                foreach (var item in TextBlock)
                {
                    item.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                foreach (var item in TextBlock)
                {
                    item.Foreground = new SolidColorBrush(Colors.White);
                }
            }
        }
        public void Update()
        {


        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}