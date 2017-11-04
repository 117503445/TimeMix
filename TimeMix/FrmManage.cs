using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TimeMix.Public;
namespace TimeMix
{
    public partial class FrmManage : Form
    {
        public FrmManage()
        {
            InitializeComponent();
            frmTime = new FrmTime();
            frmTime.Show();
            frmTimeHook = new FrmHook(frmTime);
            //    form1 = new Form1();
            //   form1.Show();
            frmTable= new FrmTable();
            frmTable.Show();
            frmTableHook = new FrmHook(frmTable);
        }

        private void FrmManage_Load(object sender, EventArgs e)
        {

        }

        private void FrmManage_Activated(object sender, EventArgs e)
        {
            Hide();
        }

        private void Tmr1000_Tick(object sender, EventArgs e)
        {
            frmTimeHook.BackColor = Color.Black; frmTimeHook.Opacity = 0.2;
            frmTime.LblBig.ForeColor = Color.White; frmTime.LblSmall.ForeColor = Color.White;
            frmTime.TopMost = true;
            GetColor();


        }
    }
}
