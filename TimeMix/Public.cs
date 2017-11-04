using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeMix
{
  public static  class Public
    {

        /// <summary>
        /// 小课表
        /// </summary>
        public static FrmTime frmTime;
        public static FrmHook frmTimeHook;
        /// <summary>
        /// 大课表
        /// </summary>
        public static FrmTable frmTable;
        public static FrmHook frmTableHook;
        public static void GetColor() {
            SendKeys.Send("{PRTSC}");
            if (Clipboard.ContainsImage())
            {
                Bitmap bit = (Bitmap)Clipboard.GetImage();
                Color c = bit.GetPixel(frmTime.Left, frmTime.Top);
                if (c.R + c.G + c.B > 384) {
                    frmTime.LblBig.ForeColor = Color.Black;
                    frmTime.LblSmall.ForeColor = Color.Black;
                } else
                {
                    frmTime.LblBig.ForeColor = Color.White;
                    frmTime.LblSmall.ForeColor = Color.White;
                }
                bit.Dispose();
            }
        }
    }
}
