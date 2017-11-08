using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary_117503445;
namespace TimeMix
{
    public partial class FrmHook : Form
    {
        Form frmHost;//主人
        public FrmHook(Form form)
        {
            InitializeComponent();
            frmHost = form;
            Show();
        }

        private void FrmTimeHook_Load(object sender, EventArgs e)
        {
            Location = frmHost.Location;
            Size = frmHost.Size;
            ControlMove moveMe =new ControlMove(this,this);//移动自己
            ControlMove moveMain = new ControlMove(this, frmHost);//移动主人窗口
        }
    }
}
