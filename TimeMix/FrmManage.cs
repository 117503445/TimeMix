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
using TimeCore;
namespace TimeMix
{
    public partial class FrmManage : Form
    {
        public FrmManage()
        {
            InitializeComponent();
            //frmTime = new FrmTime();
            //frmTime.Show();
            //frmTimeHook = new FrmHook(frmTime);
            //frmTable= new FrmTable();
            //frmTable.Show();
            //frmTableHook = new FrmHook(frmTable);

        }

        private void FrmManage_Load(object sender, EventArgs e)
        {

        }

        private void FrmManage_Activated(object sender, EventArgs e)
        {
            Hide();
        }
        public Core core;
        private void Tmr1000_Tick(object sender, EventArgs e)
        {
            //   frmTime.TopMost = true;
            //GetColor();
            core= new Core(@"C: \User\File\Program\TimeMix\TimeMix\File\Data\Source\时间NEW.txt", @"C:\User\File\Program\TimeMix\TimeMix\File\Data\Source\课表NEW.txt",0);

        }
    }
}
