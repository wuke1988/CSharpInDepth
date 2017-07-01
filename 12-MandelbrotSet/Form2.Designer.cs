namespace _12_MandelbrotSet
{
    partial class Form2
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
            this.pcbMS = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMS)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbMS
            // 
            this.pcbMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcbMS.Location = new System.Drawing.Point(0, 0);
            this.pcbMS.Name = "pcbMS";
            this.pcbMS.Size = new System.Drawing.Size(1322, 783);
            this.pcbMS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pcbMS.TabIndex = 1;
            this.pcbMS.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 783);
            this.Controls.Add(this.pcbMS);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbMS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbMS;
    }
}