namespace WindowsFormLib
{
    partial class FormRotation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRotation));
            this.LblStart = new System.Windows.Forms.Label();
            this.DtpStart = new System.Windows.Forms.DateTimePicker();
            this.DtpEnd = new System.Windows.Forms.DateTimePicker();
            this.LblEnd = new System.Windows.Forms.Label();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.CTxtSrcLocAddr = new CustomControlLib.CUserTextButton();
            this.LblSrc = new System.Windows.Forms.Label();
            this.BtnRotation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblStart
            // 
            this.LblStart.Location = new System.Drawing.Point(22, 35);
            this.LblStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblStart.Name = "LblStart";
            this.LblStart.Size = new System.Drawing.Size(86, 26);
            this.LblStart.TabIndex = 0;
            this.LblStart.Text = "起始时间";
            this.LblStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DtpStart
            // 
            this.DtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpStart.Location = new System.Drawing.Point(117, 35);
            this.DtpStart.Margin = new System.Windows.Forms.Padding(4);
            this.DtpStart.Name = "DtpStart";
            this.DtpStart.Size = new System.Drawing.Size(265, 26);
            this.DtpStart.TabIndex = 1;
            // 
            // DtpEnd
            // 
            this.DtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpEnd.Location = new System.Drawing.Point(117, 79);
            this.DtpEnd.Margin = new System.Windows.Forms.Padding(4);
            this.DtpEnd.Name = "DtpEnd";
            this.DtpEnd.Size = new System.Drawing.Size(265, 26);
            this.DtpEnd.TabIndex = 3;
            // 
            // LblEnd
            // 
            this.LblEnd.Location = new System.Drawing.Point(22, 79);
            this.LblEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblEnd.Name = "LblEnd";
            this.LblEnd.Size = new System.Drawing.Size(86, 26);
            this.LblEnd.TabIndex = 2;
            this.LblEnd.Text = "截止时间";
            this.LblEnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(38, 195);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(87, 26);
            this.BtnStart.TabIndex = 18;
            this.BtnStart.Text = "开始";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Enabled = false;
            this.BtnStop.Location = new System.Drawing.Point(158, 195);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(87, 26);
            this.BtnStop.TabIndex = 19;
            this.BtnStop.Text = "停止";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CTxtSrcLocAddr
            // 
            this.CTxtSrcLocAddr.EnabledButton = true;
            this.CTxtSrcLocAddr.EnmTxtType = CustomControlLib.EnmTxtBoxType.CarLocationAddr;
            this.CTxtSrcLocAddr.ImageButton = global::WindowsFormLib.Properties.Resources.car;
            this.CTxtSrcLocAddr.Location = new System.Drawing.Point(117, 132);
            this.CTxtSrcLocAddr.Name = "CTxtSrcLocAddr";
            this.CTxtSrcLocAddr.Size = new System.Drawing.Size(265, 26);
            this.CTxtSrcLocAddr.TabIndex = 21;
            this.CTxtSrcLocAddr.CallbackTextButtonEvent += new System.EventHandler(this.TxtSrcLocAddr_Click);
            // 
            // LblSrc
            // 
            this.LblSrc.Location = new System.Drawing.Point(11, 132);
            this.LblSrc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblSrc.Name = "LblSrc";
            this.LblSrc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblSrc.Size = new System.Drawing.Size(100, 26);
            this.LblSrc.TabIndex = 20;
            this.LblSrc.Text = "源地址";
            // 
            // BtnRotation
            // 
            this.BtnRotation.Location = new System.Drawing.Point(274, 195);
            this.BtnRotation.Name = "BtnRotation";
            this.BtnRotation.Size = new System.Drawing.Size(117, 26);
            this.BtnRotation.TabIndex = 22;
            this.BtnRotation.Text = "单个车辆调头";
            this.BtnRotation.UseVisualStyleBackColor = true;
            this.BtnRotation.Click += new System.EventHandler(this.BtnRotation_Click);
            // 
            // FormRotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 240);
            this.Controls.Add(this.BtnRotation);
            this.Controls.Add(this.CTxtSrcLocAddr);
            this.Controls.Add(this.LblSrc);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.DtpEnd);
            this.Controls.Add(this.LblEnd);
            this.Controls.Add(this.DtpStart);
            this.Controls.Add(this.LblStart);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormRotation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "掉头";
            this.Load += new System.EventHandler(this.FormRotation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblStart;
        private System.Windows.Forms.DateTimePicker DtpStart;
        private System.Windows.Forms.DateTimePicker DtpEnd;
        private System.Windows.Forms.Label LblEnd;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.Timer timer1;
        private CustomControlLib.CUserTextButton CTxtSrcLocAddr;
        private System.Windows.Forms.Label LblSrc;
        private System.Windows.Forms.Button BtnRotation;
    }
}