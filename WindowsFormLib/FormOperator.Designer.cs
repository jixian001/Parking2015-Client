namespace WindowsFormLib
{
    partial class CFormOperator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormOperator));
            this.BtnModify = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.TxtAddr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CboType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CTxtCode = new CustomControlLib.CUserTextButton();
            this.CTxtPassWord = new CustomControlLib.CUserTextButton();
            this.CTxtNewPassWord = new CustomControlLib.CUserTextButton();
            this.CTxtName = new CustomControlLib.CUserTextButton();
            this.CTxtPhone = new CustomControlLib.CUserTextButton();
            this.LtbAllocPermission = new System.Windows.Forms.ListBox();
            this.BtnMove = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LtbAssignedPermission = new System.Windows.Forms.ListBox();
            this.BtnRemove = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnModify
            // 
            this.BtnModify.Enabled = false;
            this.BtnModify.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnModify.Location = new System.Drawing.Point(150, 488);
            this.BtnModify.Margin = new System.Windows.Forms.Padding(4);
            this.BtnModify.Name = "BtnModify";
            this.BtnModify.Size = new System.Drawing.Size(100, 35);
            this.BtnModify.TabIndex = 36;
            this.BtnModify.Text = "修改";
            this.BtnModify.UseVisualStyleBackColor = true;
            this.BtnModify.Click += new System.EventHandler(this.BtnModify_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnDelete.Location = new System.Drawing.Point(285, 488);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(100, 35);
            this.BtnDelete.TabIndex = 35;
            this.BtnDelete.Text = "删除";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnSave.Location = new System.Drawing.Point(15, 488);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(100, 35);
            this.BtnSave.TabIndex = 34;
            this.BtnSave.Text = "保存";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtAddr
            // 
            this.TxtAddr.Font = new System.Drawing.Font("宋体", 12F);
            this.TxtAddr.Location = new System.Drawing.Point(110, 170);
            this.TxtAddr.Margin = new System.Windows.Forms.Padding(5);
            this.TxtAddr.Name = "TxtAddr";
            this.TxtAddr.Size = new System.Drawing.Size(395, 26);
            this.TxtAddr.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(14, 170);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(95, 35);
            this.label5.TabIndex = 32;
            this.label5.Text = "地址";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(260, 125);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(95, 35);
            this.label6.TabIndex = 30;
            this.label6.Text = "电话";
            // 
            // CboType
            // 
            this.CboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboType.Font = new System.Drawing.Font("宋体", 12F);
            this.CboType.FormattingEnabled = true;
            this.CboType.Items.AddRange(new object[] {
            "操作员",
            "管理员",
            "计费人员",
            "其他"});
            this.CboType.Location = new System.Drawing.Point(110, 125);
            this.CboType.Margin = new System.Windows.Forms.Padding(4);
            this.CboType.Name = "CboType";
            this.CboType.Size = new System.Drawing.Size(150, 24);
            this.CboType.TabIndex = 29;
            this.CboType.SelectedIndexChanged += new System.EventHandler(this.CboType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(260, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(95, 35);
            this.label3.TabIndex = 27;
            this.label3.Text = "密码";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(14, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(95, 35);
            this.label1.TabIndex = 25;
            this.label1.Text = "用户名";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(260, 80);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(95, 35);
            this.label4.TabIndex = 23;
            this.label4.Text = "姓名";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(14, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(95, 35);
            this.label2.TabIndex = 37;
            this.label2.Text = "类型";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 12F);
            this.label7.Location = new System.Drawing.Point(14, 80);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(95, 35);
            this.label7.TabIndex = 38;
            this.label7.Text = "确认密码";
            // 
            // CTxtCode
            // 
            this.CTxtCode.EnabledButton = false;
            this.CTxtCode.EnmTxtType = CustomControlLib.EnmTxtBoxType.Name;
            this.CTxtCode.ImageButton = null;
            this.CTxtCode.Location = new System.Drawing.Point(110, 35);
            this.CTxtCode.Name = "CTxtCode";
            this.CTxtCode.Size = new System.Drawing.Size(150, 26);
            this.CTxtCode.TabIndex = 40;
            // 
            // CTxtPassWord
            // 
            this.CTxtPassWord.EnabledButton = false;
            this.CTxtPassWord.EnmTxtType = CustomControlLib.EnmTxtBoxType.PassWord;
            this.CTxtPassWord.ImageButton = null;
            this.CTxtPassWord.Location = new System.Drawing.Point(355, 35);
            this.CTxtPassWord.Name = "CTxtPassWord";
            this.CTxtPassWord.Size = new System.Drawing.Size(150, 26);
            this.CTxtPassWord.TabIndex = 41;
            this.CTxtPassWord.UseSystemPasswordChar = true;
            // 
            // CTxtNewPassWord
            // 
            this.CTxtNewPassWord.EnabledButton = false;
            this.CTxtNewPassWord.EnmTxtType = CustomControlLib.EnmTxtBoxType.PassWord;
            this.CTxtNewPassWord.ImageButton = null;
            this.CTxtNewPassWord.Location = new System.Drawing.Point(110, 80);
            this.CTxtNewPassWord.Name = "CTxtNewPassWord";
            this.CTxtNewPassWord.Size = new System.Drawing.Size(150, 26);
            this.CTxtNewPassWord.TabIndex = 42;
            this.CTxtNewPassWord.UseSystemPasswordChar = true;
            // 
            // CTxtName
            // 
            this.CTxtName.EnabledButton = false;
            this.CTxtName.EnmTxtType = CustomControlLib.EnmTxtBoxType.Name;
            this.CTxtName.ImageButton = null;
            this.CTxtName.Location = new System.Drawing.Point(355, 80);
            this.CTxtName.Name = "CTxtName";
            this.CTxtName.Size = new System.Drawing.Size(150, 26);
            this.CTxtName.TabIndex = 43;
            // 
            // CTxtPhone
            // 
            this.CTxtPhone.EnabledButton = false;
            this.CTxtPhone.EnmTxtType = CustomControlLib.EnmTxtBoxType.Mobile;
            this.CTxtPhone.ImageButton = null;
            this.CTxtPhone.Location = new System.Drawing.Point(355, 125);
            this.CTxtPhone.Name = "CTxtPhone";
            this.CTxtPhone.Size = new System.Drawing.Size(150, 26);
            this.CTxtPhone.TabIndex = 44;
            // 
            // LtbAllocPermission
            // 
            this.LtbAllocPermission.FormattingEnabled = true;
            this.LtbAllocPermission.ItemHeight = 16;
            this.LtbAllocPermission.Location = new System.Drawing.Point(5, 20);
            this.LtbAllocPermission.Name = "LtbAllocPermission";
            this.LtbAllocPermission.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LtbAllocPermission.Size = new System.Drawing.Size(169, 228);
            this.LtbAllocPermission.TabIndex = 45;
            this.LtbAllocPermission.DoubleClick += new System.EventHandler(this.BtnMove_Click);
            // 
            // BtnMove
            // 
            this.BtnMove.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnMove.Location = new System.Drawing.Point(240, 311);
            this.BtnMove.Margin = new System.Windows.Forms.Padding(4);
            this.BtnMove.Name = "BtnMove";
            this.BtnMove.Size = new System.Drawing.Size(83, 26);
            this.BtnMove.TabIndex = 46;
            this.BtnMove.Text = "==>";
            this.BtnMove.UseVisualStyleBackColor = true;
            this.BtnMove.Click += new System.EventHandler(this.BtnMove_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LtbAllocPermission);
            this.groupBox1.Location = new System.Drawing.Point(55, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 255);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "可分配权限";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LtbAssignedPermission);
            this.groupBox2.Location = new System.Drawing.Point(325, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 255);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已分配权限";
            // 
            // LtbAssignedPermission
            // 
            this.LtbAssignedPermission.FormattingEnabled = true;
            this.LtbAssignedPermission.ItemHeight = 16;
            this.LtbAssignedPermission.Location = new System.Drawing.Point(5, 20);
            this.LtbAssignedPermission.Name = "LtbAssignedPermission";
            this.LtbAssignedPermission.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LtbAssignedPermission.Size = new System.Drawing.Size(169, 228);
            this.LtbAssignedPermission.TabIndex = 45;
            // 
            // BtnRemove
            // 
            this.BtnRemove.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnRemove.Location = new System.Drawing.Point(240, 365);
            this.BtnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.BtnRemove.Name = "BtnRemove";
            this.BtnRemove.Size = new System.Drawing.Size(83, 26);
            this.BtnRemove.TabIndex = 49;
            this.BtnRemove.Text = "<==";
            this.BtnRemove.UseVisualStyleBackColor = true;
            this.BtnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnClose.Location = new System.Drawing.Point(420, 488);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(100, 35);
            this.BtnClose.TabIndex = 50;
            this.BtnClose.Text = "关闭";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // CFormOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 531);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnRemove);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnMove);
            this.Controls.Add(this.CTxtPhone);
            this.Controls.Add(this.CTxtName);
            this.Controls.Add(this.CTxtNewPassWord);
            this.Controls.Add(this.CTxtPassWord);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnModify);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.TxtAddr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CboType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CTxtCode);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "CFormOperator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "操作员信息";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnModify;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TextBox TxtAddr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CboType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private CustomControlLib.CUserTextButton CTxtCode;
        private CustomControlLib.CUserTextButton CTxtPassWord;
        private CustomControlLib.CUserTextButton CTxtNewPassWord;
        private CustomControlLib.CUserTextButton CTxtName;
        private CustomControlLib.CUserTextButton CTxtPhone;
        private System.Windows.Forms.ListBox LtbAllocPermission;
        private System.Windows.Forms.Button BtnMove;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox LtbAssignedPermission;
        private System.Windows.Forms.Button BtnRemove;
        private System.Windows.Forms.Button BtnClose;
    }
}