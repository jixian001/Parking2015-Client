namespace WindowsFormLib
{
    partial class CFormTempFetch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormTempFetch));
            this.label1 = new System.Windows.Forms.Label();
            this.TxtICCardID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnReadICCard = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.CboWareHouse = new System.Windows.Forms.ComboBox();
            this.CboHallID = new System.Windows.Forms.ComboBox();
            this.BtnFind = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(44, 33);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(100, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户卡号";
            // 
            // TxtICCardID
            // 
            this.TxtICCardID.Location = new System.Drawing.Point(144, 33);
            this.TxtICCardID.Name = "TxtICCardID";
            this.TxtICCardID.Size = new System.Drawing.Size(180, 26);
            this.TxtICCardID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(44, 83);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(100, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "出车库区";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(44, 133);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(100, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "出车车厅";
            // 
            // BtnReadICCard
            // 
            this.BtnReadICCard.Location = new System.Drawing.Point(35, 190);
            this.BtnReadICCard.Name = "BtnReadICCard";
            this.BtnReadICCard.Size = new System.Drawing.Size(75, 26);
            this.BtnReadICCard.TabIndex = 6;
            this.BtnReadICCard.Text = "读卡";
            this.BtnReadICCard.UseVisualStyleBackColor = true;
            this.BtnReadICCard.Click += new System.EventHandler(this.BtnReadICCard_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(232, 190);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(120, 26);
            this.BtnOk.TabIndex = 7;
            this.BtnOk.Text = "确认临时取物";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // CboWareHouse
            // 
            this.CboWareHouse.Enabled = false;
            this.CboWareHouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouse.FormattingEnabled = true;
            this.CboWareHouse.Location = new System.Drawing.Point(144, 83);
            this.CboWareHouse.Name = "CboWareHouse";
            this.CboWareHouse.Size = new System.Drawing.Size(180, 24);
            this.CboWareHouse.TabIndex = 8;
            // 
            // CboHallID
            // 
            this.CboHallID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboHallID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboHallID.FormattingEnabled = true;
            this.CboHallID.Location = new System.Drawing.Point(144, 130);
            this.CboHallID.Name = "CboHallID";
            this.CboHallID.Size = new System.Drawing.Size(180, 24);
            this.CboHallID.TabIndex = 9;
            // 
            // BtnFind
            // 
            this.BtnFind.Location = new System.Drawing.Point(134, 190);
            this.BtnFind.Name = "BtnFind";
            this.BtnFind.Size = new System.Drawing.Size(75, 26);
            this.BtnFind.TabIndex = 10;
            this.BtnFind.Text = "查询";
            this.BtnFind.UseVisualStyleBackColor = true;
            this.BtnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // CFormTempFetch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 241);
            this.Controls.Add(this.BtnFind);
            this.Controls.Add(this.CboHallID);
            this.Controls.Add(this.CboWareHouse);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnReadICCard);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtICCardID);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "CFormTempFetch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "临时取物";
            this.Load += new System.EventHandler(this.CFormTempFetch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtICCardID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnReadICCard;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.ComboBox CboWareHouse;
        private System.Windows.Forms.ComboBox CboHallID;
        private System.Windows.Forms.Button BtnFind;
    }
}