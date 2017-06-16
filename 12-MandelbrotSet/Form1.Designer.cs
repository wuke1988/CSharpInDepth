namespace _12_MandelbrotSet
{
    partial class Form1
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
            this.pcbMS = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMS)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbMS
            // 
            this.pcbMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcbMS.Location = new System.Drawing.Point(0, 0);
            this.pcbMS.Name = "pcbMS";
            this.pcbMS.Size = new System.Drawing.Size(1301, 791);
            this.pcbMS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pcbMS.TabIndex = 0;
            this.pcbMS.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 791);
            this.Controls.Add(this.pcbMS);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbMS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbMS;
    }
}