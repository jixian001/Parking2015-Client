namespace WindowsFormLib
{
    partial class CFormSystemConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormSystemConfig));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CboHallType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CboHallID = new System.Windows.Forms.ComboBox();
            this.BtnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CboHallWareHouse = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LblRed = new System.Windows.Forms.Label();
            this.CboEquipID = new System.Windows.Forms.ComboBox();
            this.BtnEnable = new System.Windows.Forms.Button();
            this.BtnDisable = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CboEquipWareHouse = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.当前状态 = new System.Windows.Forms.Label();
            this.lblAllow = new System.Windows.Forms.Label();
            this.btnAllow = new System.Windows.Forms.Button();
            this.btnFight = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(764, 442);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(756, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "参数配置";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(3, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(749, 153);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "设备状态";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CboHallType);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.CboHallID);
            this.groupBox2.Controls.Add(this.BtnOk);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.CboHallWareHouse);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(3, 250);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(749, 84);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置车厅类型";
            // 
            // CboHallType
            // 
            this.CboHallType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboHallType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboHallType.FormattingEnabled = true;
            this.CboHallType.Items.AddRange(new object[] {
            "进出两用车厅",
            "进车厅",
            "出车厅"});
            this.CboHallType.Location = new System.Drawing.Point(477, 35);
            this.CboHallType.Margin = new System.Windows.Forms.Padding(4);
            this.CboHallType.Name = "CboHallType";
            this.CboHallType.Size = new System.Drawing.Size(132, 24);
            this.CboHallType.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(372, 35);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(105, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "车厅类型";
            // 
            // CboHallID
            // 
            this.CboHallID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboHallID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboHallID.FormattingEnabled = true;
            this.CboHallID.Location = new System.Drawing.Point(292, 35);
            this.CboHallID.Margin = new System.Windows.Forms.Padding(4);
            this.CboHallID.Name = "CboHallID";
            this.CboHallID.Size = new System.Drawing.Size(79, 24);
            this.CboHallID.TabIndex = 6;
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(645, 32);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(4);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(80, 31);
            this.BtnOk.TabIndex = 5;
            this.BtnOk.Text = "确定";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(187, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(105, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "设备";
            // 
            // CboHallWareHouse
            // 
            this.CboHallWareHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboHallWareHouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboHallWareHouse.FormattingEnabled = true;
            this.CboHallWareHouse.Location = new System.Drawing.Point(107, 35);
            this.CboHallWareHouse.Margin = new System.Windows.Forms.Padding(4);
            this.CboHallWareHouse.Name = "CboHallWareHouse";
            this.CboHallWareHouse.Size = new System.Drawing.Size(79, 24);
            this.CboHallWareHouse.TabIndex = 1;
            this.CboHallWareHouse.SelectedIndexChanged += new System.EventHandler(this.CboHallWareHouse_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(2, 35);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(105, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "库区";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LblRed);
            this.groupBox1.Controls.Add(this.CboEquipID);
            this.groupBox1.Controls.Add(this.BtnEnable);
            this.groupBox1.Controls.Add(this.BtnDisable);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CboEquipWareHouse);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 160);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(749, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置设备状态";
            // 
            // LblRed
            // 
            this.LblRed.AutoSize = true;
            this.LblRed.ForeColor = System.Drawing.Color.Red;
            this.LblRed.Location = new System.Drawing.Point(139, 65);
            this.LblRed.Name = "LblRed";
            this.LblRed.Size = new System.Drawing.Size(312, 16);
            this.LblRed.TabIndex = 8;
            this.LblRed.Text = "注：启用会触发未完成的作业，请注意安全";
            // 
            // CboEquipID
            // 
            this.CboEquipID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboEquipID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboEquipID.FormattingEnabled = true;
            this.CboEquipID.Location = new System.Drawing.Point(292, 25);
            this.CboEquipID.Margin = new System.Windows.Forms.Padding(4);
            this.CboEquipID.Name = "CboEquipID";
            this.CboEquipID.Size = new System.Drawing.Size(79, 24);
            this.CboEquipID.TabIndex = 7;
            // 
            // BtnEnable
            // 
            this.BtnEnable.Location = new System.Drawing.Point(609, 21);
            this.BtnEnable.Margin = new System.Windows.Forms.Padding(4);
            this.BtnEnable.Name = "BtnEnable";
            this.BtnEnable.Size = new System.Drawing.Size(80, 31);
            this.BtnEnable.TabIndex = 5;
            this.BtnEnable.Text = "启用";
            this.BtnEnable.UseVisualStyleBackColor = true;
            this.BtnEnable.Click += new System.EventHandler(this.BtnEnable_Click);
            // 
            // BtnDisable
            // 
            this.BtnDisable.Location = new System.Drawing.Point(493, 21);
            this.BtnDisable.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDisable.Name = "BtnDisable";
            this.BtnDisable.Size = new System.Drawing.Size(80, 31);
            this.BtnDisable.TabIndex = 4;
            this.BtnDisable.Text = "禁用";
            this.BtnDisable.UseVisualStyleBackColor = true;
            this.BtnDisable.Click += new System.EventHandler(this.BtnDisable_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(187, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(105, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "设备";
            // 
            // CboEquipWareHouse
            // 
            this.CboEquipWareHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboEquipWareHouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboEquipWareHouse.FormattingEnabled = true;
            this.CboEquipWareHouse.Location = new System.Drawing.Point(107, 25);
            this.CboEquipWareHouse.Margin = new System.Windows.Forms.Padding(4);
            this.CboEquipWareHouse.Name = "CboEquipWareHouse";
            this.CboEquipWareHouse.Size = new System.Drawing.Size(79, 24);
            this.CboEquipWareHouse.TabIndex = 1;
            this.CboEquipWareHouse.SelectedIndexChanged += new System.EventHandler(this.CboEquipWareHouse_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(105, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "库区";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnFight);
            this.groupBox4.Controls.Add(this.btnAllow);
            this.groupBox4.Controls.Add(this.lblAllow);
            this.groupBox4.Controls.Add(this.当前状态);
            this.groupBox4.Location = new System.Drawing.Point(3, 341);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(749, 64);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "定期、固定卡允许在厅外刷卡取车";
            // 
            // 当前状态
            // 
            this.当前状态.AutoSize = true;
            this.当前状态.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.当前状态.Location = new System.Drawing.Point(159, 32);
            this.当前状态.Name = "当前状态";
            this.当前状态.Size = new System.Drawing.Size(93, 16);
            this.当前状态.TabIndex = 0;
            this.当前状态.Text = "当前状态：";
            // 
            // lblAllow
            // 
            this.lblAllow.AutoSize = true;
            this.lblAllow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAllow.Location = new System.Drawing.Point(250, 32);
            this.lblAllow.Name = "lblAllow";
            this.lblAllow.Size = new System.Drawing.Size(26, 16);
            this.lblAllow.TabIndex = 1;
            this.lblAllow.Text = " 0";
            // 
            // btnAllow
            // 
            this.btnAllow.Location = new System.Drawing.Point(410, 22);
            this.btnAllow.Margin = new System.Windows.Forms.Padding(4);
            this.btnAllow.Name = "btnAllow";
            this.btnAllow.Size = new System.Drawing.Size(89, 31);
            this.btnAllow.TabIndex = 5;
            this.btnAllow.Text = "允 许";
            this.btnAllow.UseVisualStyleBackColor = true;
            this.btnAllow.Click += new System.EventHandler(this.btnAllow_Click);
            // 
            // btnFight
            // 
            this.btnFight.Location = new System.Drawing.Point(540, 22);
            this.btnFight.Margin = new System.Windows.Forms.Padding(4);
            this.btnFight.Name = "btnFight";
            this.btnFight.Size = new System.Drawing.Size(89, 31);
            this.btnFight.TabIndex = 6;
            this.btnFight.Text = "禁 止";
            this.btnFight.UseVisualStyleBackColor = true;
            this.btnFight.Click += new System.EventHandler(this.btnAllow_Click);
            // 
            // CFormSystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 444);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "CFormSystemConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统配置";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnEnable;
        private System.Windows.Forms.Button BtnDisable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CboEquipWareHouse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CboHallType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CboHallID;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CboHallWareHouse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CboEquipID;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label LblRed;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnFight;
        private System.Windows.Forms.Button btnAllow;
        private System.Windows.Forms.Label lblAllow;
        private System.Windows.Forms.Label 当前状态;
    }
}