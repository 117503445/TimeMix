﻿using System;
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
    /// ClassTableWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClassTableWindow : FunctionWindow
    {
        public ClassTableWindow()
        {
            InitializeComponent();

            Controls = new Control[] { Lbl1, Lbl2, Lbl3, Lbl4, Lbl5, Lbl6, Lbl7, Lbl8, Lbl9, LblWeek };

        }



        public void ChangeClass(List<Core.ClassSection> classTable)
        {

            ChangeClass(classTable, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classTable"></param>
        /// <param name="isTomorrow">是否为明日课表</param>
        public void ChangeClass(List<Core.ClassSection> classTable, bool isTomorrow)
        {
            if (isTomorrow)
            {
                for (int i = 0; i < classTable.Count; i++)
                {
                    classTable[i].EndTime = classTable[i].EndTime.AddDays(1);
                }
            }
            //Console.WriteLine();
            //Console.WriteLine("Now:" + Public.ChangHeTime());
            for (int i = 0; i < 9; i++)
            {

                ((Label)Controls[i]).Content = classTable[i].Name;

                //Console.Write(classTable[i].EndTime + "   " + classTable[i].Name + "  ");
                //Console.WriteLine(classTable[i].EndTime.CompareTo(Public.ChangHeTime()) < 0);
                if (classTable[i].EndTime.CompareTo(Public.ChangHeTime()) < 0)
                {

                    (Controls[i]).Visibility = Visibility.Collapsed;
                }
                else
                {
                    Controls[i].Visibility = Visibility.Visible;
                }
            }

        }
        public void ChangeWeek(DayOfWeek dayOfWeek)
        {
            string sWeek = "日一二三四五六";
            string s = "周" + sWeek.Substring((int)dayOfWeek, 1);
            LblWeek.Content = s;

        }


    }
}
