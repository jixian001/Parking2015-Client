using CustomControlLib;
namespace WindowsFormLib
{
    partial class CFormHandOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormHandOrder));
            this.label1 = new System.Windows.Forms.Label();
            this.CboWareHouse = new System.Windows.Forms.ComboBox();
            this.LblSrc = new System.Windows.Forms.Label();
            this.LblDest = new System.Windows.Forms.Label();
            this.RbtnInJog = new System.Windows.Forms.RadioButton();
            this.RbtnMove = new System.Windows.Forms.RadioButton();
            this.RbtnOut = new System.Windows.Forms.RadioButton();
            this.BtnOk = new System.Windows.Forms.Button();
            this.CboDeviceID = new System.Windows.Forms.ComboBox();
            this.CboHallID = new System.Windows.Forms.ComboBox();
            this.CTxtDestLocAddr = new CustomControlLib.CUserTextButton();
            this.CTxtSrcLocAddr = new CustomControlLib.CUserTextButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRdICCard = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(45, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(100, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "库区";
            // 
            // CboWareHouse
            // 
            this.CboWareHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWareHouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouse.FormattingEnabled = true;
            this.CboWareHouse.Location = new System.Drawing.Point(151, 79);
            this.CboWareHouse.Name = "CboWareHouse";
            this.CboWareHouse.Size = new System.Drawing.Size(121, 24);
            this.CboWareHouse.TabIndex = 4;
            this.CboWareHouse.SelectedIndexChanged += new System.EventHandler(this.CboWareHouse_SelectedIndexChanged);
            // 
            // LblSrc
            // 
            this.LblSrc.Location = new System.Drawing.Point(45, 129);
            this.LblSrc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblSrc.Name = "LblSrc";
            this.LblSrc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblSrc.Size = new System.Drawing.Size(100, 26);
            this.LblSrc.TabIndex = 5;
            this.LblSrc.Text = "源地址";
            // 
            // LblDest
            // 
            this.LblDest.Location = new System.Drawing.Point(45, 176);
            this.LblDest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblDest.Name = "LblDest";
            this.LblDest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblDest.Size = new System.Drawing.Size(100, 26);
            this.LblDest.TabIndex = 7;
            this.LblDest.Text = "目的地址";
            // 
            // RbtnInJog
            // 
            this.RbtnInJog.AutoSize = true;
            this.RbtnInJog.Location = new System.Drawing.Point(89, 34);
            this.RbtnInJog.Name = "RbtnInJog";
            this.RbtnInJog.Size = new System.Drawing.Size(58, 20);
            this.RbtnInJog.TabIndex = 9;
            this.RbtnInJog.TabStop = true;
            this.RbtnInJog.Text = "挪移";
            this.RbtnInJog.UseVisualStyleBackColor = true;
            this.RbtnInJog.CheckedChanged += new System.EventHandler(this.RbtnInJog_CheckedChanged);
            // 
            // RbtnMove
            // 
            this.RbtnMove.AutoSize = true;
            this.RbtnMove.Location = new System.Drawing.Point(159, 34);
            this.RbtnMove.Name = "RbtnMove";
            this.RbtnMove.Size = new System.Drawing.Size(58, 20);
            this.RbtnMove.TabIndex = 10;
            this.RbtnMove.TabStop = true;
            this.RbtnMove.Text = "移动";
            this.RbtnMove.UseVisualStyleBackColor = true;
            this.RbtnMove.CheckedChanged += new System.EventHandler(this.RbtnMove_CheckedChanged);
            // 
            // RbtnOut
            // 
            this.RbtnOut.AutoSize = true;
            this.RbtnOut.Location = new System.Drawing.Point(229, 34);
            this.RbtnOut.Name = "RbtnOut";
            this.RbtnOut.Size = new System.Drawing.Size(58, 20);
            this.RbtnOut.TabIndex = 11;
            this.RbtnOut.TabStop = true;
            this.RbtnOut.Text = "出库";
            this.RbtnOut.UseVisualStyleBackColor = true;
            this.RbtnOut.CheckedChanged += new System.EventHandler(this.RbtnOut_CheckedChanged);
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(203, 227);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(87, 33);
            this.BtnOk.TabIndex = 12;
            this.BtnOk.Text = "确 定";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // CboDeviceID
            // 
            this.CboDeviceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboDeviceID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboDeviceID.Location = new System.Drawing.Point(151, 126);
            this.CboDeviceID.Name = "CboDeviceID";
            this.CboDeviceID.Size = new System.Drawing.Size(121, 24);
            this.CboDeviceID.TabIndex = 15;
            this.CboDeviceID.Visible = false;
            // 
            // CboHallID
            // 
            this.CboHallID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboHallID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboHallID.FormattingEnabled = true;
            this.CboHallID.Location = new System.Drawing.Point(151, 173);
            this.CboHallID.Name = "CboHallID";
            this.CboHallID.Size = new System.Drawing.Size(121, 24);
            this.CboHallID.TabIndex = 16;
            this.CboHallID.Visible = false;
            // 
            // CTxtDestLocAddr
            // 
            this.CTxtDestLocAddr.EnabledButton = true;
            this.CTxtDestLocAddr.EnmTxtType = CustomControlLib.EnmTxtBoxType.CarLocationAddr;
            this.CTxtDestLocAddr.ImageButton = global::WindowsFormLib.Properties.Resources.car;
            this.CTxtDestLocAddr.Location = new System.Drawing.Point(151, 173);
            this.CTxtDestLocAddr.Name = "CTxtDestLocAddr";
            this.CTxtDestLocAddr.Size = new System.Drawing.Size(121, 26);
            this.CTxtDestLocAddr.TabIndex = 14;
            this.CTxtDestLocAddr.CallbackTextButtonEvent += new System.EventHandler(this.TxtDestLocAddr_Click);
            // 
            // CTxtSrcLocAddr
            // 
            this.CTxtSrcLocAddr.EnabledButton = true;
            this.CTxtSrcLocAddr.EnmTxtType = CustomControlLib.EnmTxtBoxType.CarLocationAddr;
            this.CTxtSrcLocAddr.ImageButton = global::WindowsFormLib.Properties.Resources.car;
            this.CTxtSrcLocAddr.Location = new System.Drawing.Point(151, 126);
            this.CTxtSrcLocAddr.Name = "CTxtSrcLocAddr";
            this.CTxtSrcLocAddr.Size = new System.Drawing.Size(121, 26);
            this.CTxtSrcLocAddr.TabIndex = 13;
            this.CTxtSrcLocAddr.CallbackTextButtonEvent += new System.EventHandler(this.TxtSrcLocAddr_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRdICCard);
            this.groupBox1.Controls.Add(this.RbtnInJog);
            this.groupBox1.Controls.Add(this.CboHallID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CboDeviceID);
            this.groupBox1.Controls.Add(this.CboWareHouse);
            this.groupBox1.Controls.Add(this.CTxtDestLocAddr);
            this.groupBox1.Controls.Add(this.LblSrc);
            this.groupBox1.Controls.Add(this.CTxtSrcLocAddr);
            this.groupBox1.Controls.Add(this.LblDest);
            this.groupBox1.Controls.Add(this.BtnOk);
            this.groupBox1.Controls.Add(this.RbtnMove);
            this.groupBox1.Controls.Add(this.RbtnOut);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 282);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // btnRdICCard
            // 
            this.btnRdICCard.Location = new System.Drawing.Point(74, 227);
            this.btnRdICCard.Name = "btnRdICCard";
            this.btnRdICCard.Size = new System.Drawing.Size(87, 33);
            this.btnRdICCard.TabIndex = 17;
            this.btnRdICCard.Text = "读 卡";
            this.btnRdICCard.UseVisualStyleBackColor = true;
            this.btnRdICCard.Click += new System.EventHandler(this.btnRdICCard_Click);
            // 
            // CFormHandOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 305);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "CFormHandOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动指令";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CboWareHouse;
        private System.Windows.Forms.Label LblSrc;
        private System.Windows.Forms.Label LblDest;
        private System.Windows.Forms.RadioButton RbtnInJog;
        private System.Windows.Forms.RadioButton RbtnMove;
        private System.Windows.Forms.RadioButton RbtnOut;
        private System.Windows.Forms.Button BtnOk;
        private CUserTextButton CTxtSrcLocAddr;
        private CUserTextButton CTxtDestLocAddr;
        private System.Windows.Forms.ComboBox CboDeviceID;
        private System.Windows.Forms.ComboBox CboHallID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRdICCard;
    }
}