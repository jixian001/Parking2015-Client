namespace WindowsFormLib
{
    partial class CFormDeviceFault
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormDeviceFault));
            this.TabDeviceFault = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // TabDeviceFault
            // 
            this.TabDeviceFault.Location = new System.Drawing.Point(13, 13);
            this.TabDeviceFault.Margin = new System.Windows.Forms.Padding(4);
            this.TabDeviceFault.Name = "TabDeviceFault";
            this.TabDeviceFault.SelectedIndex = 0;
            this.TabDeviceFault.Size = new System.Drawing.Size(864, 495);
            this.TabDeviceFault.TabIndex = 0;
            // 
            // CFormDeviceFault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(890, 521);
            this.Controls.Add(this.TabDeviceFault);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CFormDeviceFault";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备故障汇总";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabDeviceFault;
    }
}