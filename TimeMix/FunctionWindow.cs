using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TimeMix
{
    /// <summary>
    /// 用于提供功能窗体的基础字段,方法
    /// </summary>
    public partial class FunctionWindow : Window
    {
        /// <summary>
        /// 初始化窗体
        /// </summary>
        public FunctionWindow() : base()
        {
            Background = new System.Windows.Media.SolidColorBrush
            {
                Color = System.Windows.Media.Color.FromArgb(1, 0, 0, 0)
            };
            AllowsTransparency = true;
            Topmost = true;
            ResizeMode = ResizeMode.NoResize;
            ShowInTaskbar = false;
            Closing += Window_Closing;
            WindowStyle = WindowStyle.None;
            MouseLeftButtonDown += Window_MouseLeftButtonDown;
        }
        /// <summary>
        /// 不许退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
        /// <summary>
        /// 需要变色的控件
        /// </summary>
        protected Control[] Controls;
        /// <summary>
        /// 添加拖动支持
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            Public.PreventOutOfScreen(this);
        }

        public async void ChangeColor()
        {
            if (await Public.IsBlack(Left, Top,Width,Height))
            {
                foreach (var item in Controls)
                    item.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
            }
            else
            {
                foreach (var item in Controls)
                    item.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            }
        }
    }
}
