namespace TimeMix
{
    partial class FrmTime
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblBig = new System.Windows.Forms.Label();
            this.LblSmall = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblBig
            // 
            this.LblBig.AutoSize = true;
            this.LblBig.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LblBig.Location = new System.Drawing.Point(0, 0);
            this.LblBig.Name = "LblBig";
            this.LblBig.Size = new System.Drawing.Size(202, 83);
            this.LblBig.TabIndex = 0;
            this.LblBig.Text = "17:41";
            // 
            // LblSmall
            // 
            this.LblSmall.AutoSize = true;
            this.LblSmall.Location = new System.Drawing.Point(180, 66);
            this.LblSmall.Name = "LblSmall";
            this.LblSmall.Size = new System.Drawing.Size(22, 17);
            this.LblSmall.TabIndex = 1;
            this.LblSmall.Text = "59";
            // 
            // FrmTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(207, 91);
            this.Controls.Add(this.LblSmall);
            this.Controls.Add(this.LblBig);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmTime";
            this.Text = "FrmTime";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label LblBig;
        public System.Windows.Forms.Label LblSmall;
    }
}