namespace CustomControlLib
{
    partial class UserForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtDelayDays = new System.Windows.Forms.TextBox();
            this.BtnOK = new System.Windows.Forms.Button();
            this.RbtnTariff = new System.Windows.Forms.RadioButton();
            this.RbtnDelay = new System.Windows.Forms.RadioButton();
            this.CboTariff = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(40, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "推迟天数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtDelayDays
            // 
            this.TxtDelayDays.Location = new System.Drawing.Point(140, 72);
            this.TxtDelayDays.Margin = new System.Windows.Forms.Padding(4);
            this.TxtDelayDays.Name = "TxtDelayDays";
            this.TxtDelayDays.Size = new System.Drawing.Size(132, 26);
            this.TxtDelayDays.TabIndex = 1;
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(101, 145);
            this.BtnOK.Margin = new System.Windows.Forms.Padding(4);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(133, 31);
            this.BtnOK.TabIndex = 2;
            this.BtnOK.Text = "确定批量修改";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // RbtnTariff
            // 
            this.RbtnTariff.AutoSize = true;
            this.RbtnTariff.Location = new System.Drawing.Point(41, 23);
            this.RbtnTariff.Name = "RbtnTariff";
            this.RbtnTariff.Size = new System.Drawing.Size(122, 20);
            this.RbtnTariff.TabIndex = 3;
            this.RbtnTariff.Text = "修改计费标准";
            this.RbtnTariff.UseVisualStyleBackColor = true;
            this.RbtnTariff.CheckedChanged += new System.EventHandler(this.RbtnTariff_CheckedChanged);
            // 
            // RbtnDelay
            // 
            this.RbtnDelay.AutoSize = true;
            this.RbtnDelay.Checked = true;
            this.RbtnDelay.Location = new System.Drawing.Point(184, 23);
            this.RbtnDelay.Name = "RbtnDelay";
            this.RbtnDelay.Size = new System.Drawing.Size(90, 20);
            this.RbtnDelay.TabIndex = 4;
            this.RbtnDelay.TabStop = true;
            this.RbtnDelay.Text = "推迟天数";
            this.RbtnDelay.UseVisualStyleBackColor = true;
            // 
            // CboTariff
            // 
            this.CboTariff.FormattingEnabled = true;
            this.CboTariff.Location = new System.Drawing.Point(140, 72);
            this.CboTariff.Name = "CboTariff";
            this.CboTariff.Size = new System.Drawing.Size(132, 24);
            this.CboTariff.TabIndex = 5;
            this.CboTariff.Visible = false;
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 224);
            this.Controls.Add(this.CboTariff);
            this.Controls.Add(this.RbtnDelay);
            this.Controls.Add(this.RbtnTariff);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.TxtDelayDays);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量修改";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtDelayDays;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.RadioButton RbtnTariff;
        private System.Windows.Forms.RadioButton RbtnDelay;
        private System.Windows.Forms.ComboBox CboTariff;
    }
}