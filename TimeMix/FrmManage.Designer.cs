namespace TimeMix
{
    partial class FrmManage
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Tmr1000 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Tmr1000
            // 
            this.Tmr1000.Enabled = true;
            this.Tmr1000.Interval = 1000;
            this.Tmr1000.Tick += new System.EventHandler(this.Tmr1000_Tick);
            // 
            // FrmManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 334);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmManage";
            this.Text = "FrmManage";
            this.Activated += new System.EventHandler(this.FrmManage_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Tmr1000;
    }
}

