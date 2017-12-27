using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Drawing = System.Drawing;
namespace TimeMix
{
    class ColorStyle
    {
        DispatcherTimer innertimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1),
        };
        private delegate bool IsBlackenEventHander(int deltaX, int deltaY);
        IsBlackenEventHander _invoke;

        private bool IsBlackStyle()
        {
            Console.WriteLine("InBlackStyle");
            if (!window.IsVisible)
            {
                return false;
            }
            bool[] b = new bool[3];

            b[0] = IsBlack( 0, 0);

            b[1] = IsBlack( (int)window.Width / 2, (int)window.Height / 2);

            b[2] = IsBlack( (int)window.Width, (int)window.Height);


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
        private bool IsBlack(int deltaX, int deltaY)
        {
            bool b = true;
            window.Dispatcher.Invoke(() => 
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                int Settings_Default_dpi = 3;

                Drawing.Rectangle rc = new Drawing.Rectangle((int)window.Left + deltaX, (int)window.Top + deltaY, 1, 1);
                var bitmap = new Drawing.Bitmap(1, 1);
                using (Drawing.Graphics g = Drawing.Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen((int)(rc.X * Settings_Default_dpi), (int)(rc.Y * Settings_Default_dpi), 0, 0, rc.Size);
                }

                 Drawing.Color color = bitmap.GetPixel(0, 0);
                bitmap.Dispose();

                if (color.R + color.G + color.B > 384)
                {
                    b = true;
                }
                else
                {
                    b = false;
                }
            });
            return b;
        }
        private void IsBlackStyleCallBack(IAsyncResult itfAR)
        {
            IsBlackenEventHander _result = (IsBlackenEventHander)((AsyncResult)itfAR).AsyncDelegate;
            bool result = _result.EndInvoke(itfAR);
            Console.WriteLine(result);
            foreach (var item in controls)
            {
                item.Dispatcher.Invoke(()=> {
                    if (result)
                    {
                        item.Foreground = Brushes.Black;
                    }
                    else
                    {
                        item.Foreground = Brushes.White;
                    }
                });

            }
        }

        Window window;
        List<Control> controls = new List<Control>();
        public ColorStyle(Window window,Control[] controls)
        {
            Window = window;
            _invoke = new IsBlackenEventHander(IsBlack);
             innertimer.Tick += Innertimer_Tick;
            innertimer.Start();
            this.controls = controls.ToList();
        }
        private void Innertimer_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            bool d = false;
            if (d)
            {
                for (int i = 0; i < 20; i++)
                {
                    IsBlackStyle();
                }
            }
            else
            {
                for (int i = 0; i < 1; i++)
                {
                    _invoke.BeginInvoke(0, 0, new AsyncCallback(IsBlackStyleCallBack),null);

                }
            }

        }
        public Window Window { get => window; set => window = value; }
        public List<Control> Controls { get => controls; set => controls = value; }
    }
}
