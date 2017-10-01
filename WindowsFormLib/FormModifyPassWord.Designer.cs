namespace WindowsFormLib
{
    partial class CFormModifyPassWord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormModifyPassWord));
            this.BtnPassWordCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.TxtConfirmNewPW = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtNewPassWord = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtOldPassWord = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnPassWordCancel
            // 
            this.BtnPassWordCancel.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnPassWordCancel.Location = new System.Drawing.Point(201, 182);
            this.BtnPassWordCancel.Name = "BtnPassWordCancel";
            this.BtnPassWordCancel.Size = new System.Drawing.Size(75, 26);
            this.BtnPassWordCancel.TabIndex = 33;
            this.BtnPassWordCancel.Text = "取消";
            this.BtnPassWordCancel.UseVisualStyleBackColor = true;
            this.BtnPassWordCancel.Click += new System.EventHandler(this.BtnPassWordCancel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnOk.Location = new System.Drawing.Point(60, 182);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 26);
            this.BtnOk.TabIndex = 32;
            this.BtnOk.Text = "确认";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // TxtConfirmNewPW
            // 
            this.TxtConfirmNewPW.Font = new System.Drawing.Font("宋体", 12F);
            this.TxtConfirmNewPW.Location = new System.Drawing.Point(113, 121);
            this.TxtConfirmNewPW.Margin = new System.Windows.Forms.Padding(4);
            this.TxtConfirmNewPW.Name = "TxtConfirmNewPW";
            this.TxtConfirmNewPW.Size = new System.Drawing.Size(216, 26);
            this.TxtConfirmNewPW.TabIndex = 31;
            this.TxtConfirmNewPW.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 12F);
            this.label7.Location = new System.Drawing.Point(13, 121);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(100, 26);
            this.label7.TabIndex = 30;
            this.label7.Text = "确认新密码";
            // 
            // TxtNewPassWord
            // 
            this.TxtNewPassWord.Font = new System.Drawing.Font("宋体", 12F);
            this.TxtNewPassWord.Location = new System.Drawing.Point(113, 77);
            this.TxtNewPassWord.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNewPassWord.Name = "TxtNewPassWord";
            this.TxtNewPassWord.Size = new System.Drawing.Size(216, 26);
            this.TxtNewPassWord.TabIndex = 29;
            this.TxtNewPassWord.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(13, 77);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(100, 26);
            this.label8.TabIndex = 28;
            this.label8.Text = "新密码";
            // 
            // TxtOldPassWord
            // 
            this.TxtOldPassWord.Font = new System.Drawing.Font("宋体", 12F);
            this.TxtOldPassWord.Location = new System.Drawing.Point(113, 33);
            this.TxtOldPassWord.Margin = new System.Windows.Forms.Padding(4);
            this.TxtOldPassWord.Name = "TxtOldPassWord";
            this.TxtOldPassWord.Size = new System.Drawing.Size(216, 26);
            this.TxtOldPassWord.TabIndex = 27;
            this.TxtOldPassWord.UseSystemPasswordChar = true;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 12F);
            this.label9.Location = new System.Drawing.Point(13, 33);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(100, 26);
            this.label9.TabIndex = 26;
            this.label9.Text = "原密码";
            // 
            // CFormModifyPassWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 232);
            this.Controls.Add(this.BtnPassWordCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.TxtConfirmNewPW);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TxtNewPassWord);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TxtOldPassWord);
            this.Controls.Add(this.label9);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CFormModifyPassWord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnPassWordCancel;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.TextBox TxtConfirmNewPW;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtNewPassWord;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtOldPassWord;
        private System.Windows.Forms.Label label9;
    }
}