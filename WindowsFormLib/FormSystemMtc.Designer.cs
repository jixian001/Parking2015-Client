using CustomControlLib;
namespace WindowsFormLib
{
    partial class CFormSystemMtc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormSystemMtc));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnMuroFini = new System.Windows.Forms.Button();
            this.btnMuro = new System.Windows.Forms.Button();
            this.CboWareHouse = new System.Windows.Forms.ComboBox();
            this.BtnReset = new System.Windows.Forms.Button();
            this.BtnFinish = new System.Windows.Forms.Button();
            this.TxtTaskStatus = new System.Windows.Forms.TextBox();
            this.TxtICCardID = new System.Windows.Forms.TextBox();
            this.TxtHall = new System.Windows.Forms.TextBox();
            this.CboTaskType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblHall = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BtnAllEnable = new System.Windows.Forms.Button();
            this.BtnAllDisable = new System.Windows.Forms.Button();
            this.CboWareHouseDis = new System.Windows.Forms.ComboBox();
            this.BtnEnable = new System.Windows.Forms.Button();
            this.BtnDisable = new System.Windows.Forms.Button();
            this.TxtCarLocAddrDis = new CustomControlLib.CUserTextButton();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CboWareHouseInJog = new System.Windows.Forms.ComboBox();
            this.TxtSrcAddrInJog = new CustomControlLib.CUserTextButton();
            this.label12 = new System.Windows.Forms.Label();
            this.BtnInJog = new System.Windows.Forms.Button();
            this.TxtDestAddrInJog = new CustomControlLib.CUserTextButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CboWareHouseFind = new System.Windows.Forms.ComboBox();
            this.BtnFind = new System.Windows.Forms.Button();
            this.TxtCarLocAddrFind = new System.Windows.Forms.TextBox();
            this.CTxtICCardFind = new CustomControlLib.CUserTextButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BtnAllHandOut = new System.Windows.Forms.Button();
            this.CboWareHouseHandOut = new System.Windows.Forms.ComboBox();
            this.BtnHandOut = new System.Windows.Forms.Button();
            this.TxtCarLocAddrHandOut = new CustomControlLib.CUserTextButton();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtOverRang = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.CboWareHouseHandIn = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtCarSize = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCarleght = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.DtpInTime = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.TxtCarLocAddrHandIn = new CustomControlLib.CUserTextButton();
            this.BtnHandIn = new System.Windows.Forms.Button();
            this.TxtWheelBase = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.TxtICCardHand = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnAllDelete = new System.Windows.Forms.Button();
            this.BtnFindTask = new System.Windows.Forms.Button();
            this.CboDeviceCode = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.CboWareHouseTask = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.DgvTask = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTask)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(710, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.btnMuroFini);
            this.tabPage1.Controls.Add(this.btnMuro);
            this.tabPage1.Controls.Add(this.CboWareHouse);
            this.tabPage1.Controls.Add(this.BtnReset);
            this.tabPage1.Controls.Add(this.BtnFinish);
            this.tabPage1.Controls.Add(this.TxtTaskStatus);
            this.tabPage1.Controls.Add(this.TxtICCardID);
            this.tabPage1.Controls.Add(this.TxtHall);
            this.tabPage1.Controls.Add(this.CboTaskType);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.LblHall);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(702, 420);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "故障处理";
            // 
            // btnMuroFini
            // 
            this.btnMuroFini.Enabled = false;
            this.btnMuroFini.Location = new System.Drawing.Point(381, 370);
            this.btnMuroFini.Margin = new System.Windows.Forms.Padding(4);
            this.btnMuroFini.Name = "btnMuroFini";
            this.btnMuroFini.Size = new System.Drawing.Size(120, 36);
            this.btnMuroFini.TabIndex = 35;
            this.btnMuroFini.Text = "MURO完成";
            this.btnMuroFini.UseVisualStyleBackColor = true;
            this.btnMuroFini.Click += new System.EventHandler(this.btnMuroFini_Click);
            // 
            // btnMuro
            // 
            this.btnMuro.Enabled = false;
            this.btnMuro.Location = new System.Drawing.Point(196, 370);
            this.btnMuro.Margin = new System.Windows.Forms.Padding(4);
            this.btnMuro.Name = "btnMuro";
            this.btnMuro.Size = new System.Drawing.Size(120, 36);
            this.btnMuro.TabIndex = 34;
            this.btnMuro.Text = "MURO继续";
            this.btnMuro.UseVisualStyleBackColor = true;
            this.btnMuro.Click += new System.EventHandler(this.btnMuro_Click);
            // 
            // CboWareHouse
            // 
            this.CboWareHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWareHouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouse.FormattingEnabled = true;
            this.CboWareHouse.Location = new System.Drawing.Point(253, 87);
            this.CboWareHouse.Name = "CboWareHouse";
            this.CboWareHouse.Size = new System.Drawing.Size(305, 24);
            this.CboWareHouse.TabIndex = 33;
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(381, 315);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(4);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(120, 36);
            this.BtnReset.TabIndex = 29;
            this.BtnReset.Text = "手动复位";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // BtnFinish
            // 
            this.BtnFinish.Location = new System.Drawing.Point(196, 315);
            this.BtnFinish.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFinish.Name = "BtnFinish";
            this.BtnFinish.Size = new System.Drawing.Size(120, 36);
            this.BtnFinish.TabIndex = 28;
            this.BtnFinish.Text = "手动完成";
            this.BtnFinish.UseVisualStyleBackColor = true;
            this.BtnFinish.Click += new System.EventHandler(this.BtnFinish_Click);
            // 
            // TxtTaskStatus
            // 
            this.TxtTaskStatus.Location = new System.Drawing.Point(253, 207);
            this.TxtTaskStatus.Margin = new System.Windows.Forms.Padding(4);
            this.TxtTaskStatus.Multiline = true;
            this.TxtTaskStatus.Name = "TxtTaskStatus";
            this.TxtTaskStatus.Size = new System.Drawing.Size(305, 80);
            this.TxtTaskStatus.TabIndex = 27;
            // 
            // TxtICCardID
            // 
            this.TxtICCardID.Location = new System.Drawing.Point(253, 167);
            this.TxtICCardID.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardID.Name = "TxtICCardID";
            this.TxtICCardID.Size = new System.Drawing.Size(305, 26);
            this.TxtICCardID.TabIndex = 26;
            // 
            // TxtHall
            // 
            this.TxtHall.Location = new System.Drawing.Point(253, 127);
            this.TxtHall.Margin = new System.Windows.Forms.Padding(4);
            this.TxtHall.Name = "TxtHall";
            this.TxtHall.Size = new System.Drawing.Size(305, 26);
            this.TxtHall.TabIndex = 25;
            // 
            // CboTaskType
            // 
            this.CboTaskType.DisplayMember = "Type";
            this.CboTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTaskType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboTaskType.FormattingEnabled = true;
            this.CboTaskType.Location = new System.Drawing.Point(253, 47);
            this.CboTaskType.Margin = new System.Windows.Forms.Padding(4);
            this.CboTaskType.Name = "CboTaskType";
            this.CboTaskType.Size = new System.Drawing.Size(305, 24);
            this.CboTaskType.TabIndex = 23;
            this.CboTaskType.ValueMember = "Type";
            this.CboTaskType.SelectedIndexChanged += new System.EventHandler(this.CboTaskType_SelectedIndexChanged);
            this.CboTaskType.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.CboTaskType_Format);
            this.CboTaskType.Click += new System.EventHandler(this.CboTaskType_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(147, 207);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(107, 21);
            this.label2.TabIndex = 22;
            this.label2.Text = "作业状态";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(147, 87);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(107, 21);
            this.label8.TabIndex = 21;
            this.label8.Text = "所在库区";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(147, 47);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(107, 21);
            this.label7.TabIndex = 20;
            this.label7.Text = "作业类型";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(147, 167);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(107, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "用户卡号";
            // 
            // LblHall
            // 
            this.LblHall.Location = new System.Drawing.Point(147, 127);
            this.LblHall.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblHall.Name = "LblHall";
            this.LblHall.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblHall.Size = new System.Drawing.Size(107, 21);
            this.LblHall.TabIndex = 18;
            this.LblHall.Text = "所在车厅";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(702, 420);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "车位维护";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BtnAllEnable);
            this.groupBox4.Controls.Add(this.BtnAllDisable);
            this.groupBox4.Controls.Add(this.CboWareHouseDis);
            this.groupBox4.Controls.Add(this.BtnEnable);
            this.groupBox4.Controls.Add(this.BtnDisable);
            this.groupBox4.Controls.Add(this.TxtCarLocAddrDis);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Location = new System.Drawing.Point(0, 280);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(699, 120);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "禁用车位";
            // 
            // BtnAllEnable
            // 
            this.BtnAllEnable.Location = new System.Drawing.Point(561, 33);
            this.BtnAllEnable.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAllEnable.Name = "BtnAllEnable";
            this.BtnAllEnable.Size = new System.Drawing.Size(100, 31);
            this.BtnAllEnable.TabIndex = 48;
            this.BtnAllEnable.Text = "一键启用";
            this.BtnAllEnable.UseVisualStyleBackColor = true;
            this.BtnAllEnable.Click += new System.EventHandler(this.BtnAllEnable_Click);
            // 
            // BtnAllDisable
            // 
            this.BtnAllDisable.Location = new System.Drawing.Point(413, 33);
            this.BtnAllDisable.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAllDisable.Name = "BtnAllDisable";
            this.BtnAllDisable.Size = new System.Drawing.Size(100, 31);
            this.BtnAllDisable.TabIndex = 47;
            this.BtnAllDisable.Text = "一键禁用";
            this.BtnAllDisable.UseVisualStyleBackColor = true;
            this.BtnAllDisable.Click += new System.EventHandler(this.BtnAllDisable_Click);
            // 
            // CboWareHouseDis
            // 
            this.CboWareHouseDis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWareHouseDis.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouseDis.FormattingEnabled = true;
            this.CboWareHouseDis.Location = new System.Drawing.Point(117, 37);
            this.CboWareHouseDis.Name = "CboWareHouseDis";
            this.CboWareHouseDis.Size = new System.Drawing.Size(221, 24);
            this.CboWareHouseDis.TabIndex = 30;
            // 
            // BtnEnable
            // 
            this.BtnEnable.Location = new System.Drawing.Point(561, 77);
            this.BtnEnable.Margin = new System.Windows.Forms.Padding(4);
            this.BtnEnable.Name = "BtnEnable";
            this.BtnEnable.Size = new System.Drawing.Size(100, 31);
            this.BtnEnable.TabIndex = 29;
            this.BtnEnable.Text = "启用";
            this.BtnEnable.UseVisualStyleBackColor = true;
            this.BtnEnable.Click += new System.EventHandler(this.BtnEnable_Click);
            // 
            // BtnDisable
            // 
            this.BtnDisable.Location = new System.Drawing.Point(413, 77);
            this.BtnDisable.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDisable.Name = "BtnDisable";
            this.BtnDisable.Size = new System.Drawing.Size(100, 31);
            this.BtnDisable.TabIndex = 28;
            this.BtnDisable.Text = "禁用";
            this.BtnDisable.UseVisualStyleBackColor = true;
            this.BtnDisable.Click += new System.EventHandler(this.BtnDisable_Click);
            // 
            // TxtCarLocAddrDis
            // 
            this.TxtCarLocAddrDis.EnabledButton = true;
            this.TxtCarLocAddrDis.EnmTxtType = CustomControlLib.EnmTxtBoxType.CarLocationAddr;
            this.TxtCarLocAddrDis.ForeColor = System.Drawing.Color.Black;
            this.TxtCarLocAddrDis.ImageButton = global::WindowsFormLib.Properties.Resources.car;
            this.TxtCarLocAddrDis.Location = new System.Drawing.Point(117, 77);
            this.TxtCarLocAddrDis.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCarLocAddrDis.Name = "TxtCarLocAddrDis";
            this.TxtCarLocAddrDis.Size = new System.Drawing.Size(221, 26);
            this.TxtCarLocAddrDis.TabIndex = 27;
            this.TxtCarLocAddrDis.CallbackTextButtonEvent += new System.EventHandler(this.TxtCarLocAddr_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(1, 77);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label13.Size = new System.Drawing.Size(116, 21);
            this.label13.TabIndex = 23;
            this.label13.Text = "车位";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(1, 37);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label14.Size = new System.Drawing.Size(116, 21);
            this.label14.TabIndex = 22;
            this.label14.Text = "库区";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CboWareHouseInJog);
            this.groupBox3.Controls.Add(this.TxtSrcAddrInJog);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.BtnInJog);
            this.groupBox3.Controls.Add(this.TxtDestAddrInJog);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(0, 147);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(699, 120);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "手动挪移";
            // 
            // CboWareHouseInJog
            // 
            this.CboWareHouseInJog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWareHouseInJog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouseInJog.FormattingEnabled = true;
            this.CboWareHouseInJog.Location = new System.Drawing.Point(117, 37);
            this.CboWareHouseInJog.Name = "CboWareHouseInJog";
            this.CboWareHouseInJog.Size = new System.Drawing.Size(221, 24);
            this.CboWareHouseInJog.TabIndex = 32;
            // 
            // TxtSrcAddrInJog
            // 
            this.TxtSrcAddrInJog.EnabledButton = true;
            this.TxtSrcAddrInJog.EnmTxtType = CustomControlLib.EnmTxtBoxType.CarLocationAddr;
            this.TxtSrcAddrInJog.ForeColor = System.Drawing.Color.Black;
            this.TxtSrcAddrInJog.ImageButton = global::WindowsFormLib.Properties.Resources.car;
            this.TxtSrcAddrInJog.Location = new System.Drawing.Point(451, 37);
            this.TxtSrcAddrInJog.Margin = new System.Windows.Forms.Padding(4);
            this.TxtSrcAddrInJog.Name = "TxtSrcAddrInJog";
            this.TxtSrcAddrInJog.Size = new System.Drawing.Size(221, 26);
            this.TxtSrcAddrInJog.TabIndex = 31;
            this.TxtSrcAddrInJog.CallbackTextButtonEvent += new System.EventHandler(this.TxtCarLocAddr_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(340, 37);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label12.Size = new System.Drawing.Size(111, 21);
            this.label12.TabIndex = 30;
            this.label12.Text = "源车位";
            // 
            // BtnInJog
            // 
            this.BtnInJog.Location = new System.Drawing.Point(413, 77);
            this.BtnInJog.Margin = new System.Windows.Forms.Padding(4);
            this.BtnInJog.Name = "BtnInJog";
            this.BtnInJog.Size = new System.Drawing.Size(100, 31);
            this.BtnInJog.TabIndex = 28;
            this.BtnInJog.Text = "挪移";
            this.BtnInJog.UseVisualStyleBackColor = true;
            this.BtnInJog.Click += new System.EventHandler(this.BtnInJog_Click);
            // 
            // TxtDestAddrInJog
            // 
            this.TxtDestAddrInJog.EnabledButton = true;
            this.TxtDestAddrInJog.EnmTxtType = CustomControlLib.EnmTxtBoxType.CarLocationAddr;
            this.TxtDestAddrInJog.ForeColor = System.Drawing.Color.Black;
            this.TxtDestAddrInJog.ImageButton = global::WindowsFormLib.Properties.Resources.car;
            this.TxtDestAddrInJog.Location = new System.Drawing.Point(117, 77);
            this.TxtDestAddrInJog.Margin = new System.Windows.Forms.Padding(4);
            this.TxtDestAddrInJog.Name = "TxtDestAddrInJog";
            this.TxtDestAddrInJog.Size = new System.Drawing.Size(221, 26);
            this.TxtDestAddrInJog.TabIndex = 27;
            this.TxtDestAddrInJog.CallbackTextButtonEvent += new System.EventHandler(this.TxtCarLocAddr_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(1, 77);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(116, 21);
            this.label6.TabIndex = 23;
            this.label6.Text = "目的车位";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(1, 37);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label11.Size = new System.Drawing.Size(116, 21);
            this.label11.TabIndex = 22;
            this.label11.Text = "库区";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CboWareHouseFind);
            this.groupBox1.Controls.Add(this.BtnFind);
            this.groupBox1.Controls.Add(this.TxtCarLocAddrFind);
            this.groupBox1.Controls.Add(this.CTxtICCardFind);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(0, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(699, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询车位";
            // 
            // CboWareHouseFind
            // 
            this.CboWareHouseFind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWareHouseFind.Enabled = false;
            this.CboWareHouseFind.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouseFind.FormattingEnabled = true;
            this.CboWareHouseFind.Location = new System.Drawing.Point(451, 37);
            this.CboWareHouseFind.Name = "CboWareHouseFind";
            this.CboWareHouseFind.Size = new System.Drawing.Size(221, 24);
            this.CboWareHouseFind.TabIndex = 29;
            // 
            // BtnFind
            // 
            this.BtnFind.Location = new System.Drawing.Point(413, 77);
            this.BtnFind.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFind.Name = "BtnFind";
            this.BtnFind.Size = new System.Drawing.Size(100, 31);
            this.BtnFind.TabIndex = 28;
            this.BtnFind.Text = "查询";
            this.BtnFind.UseVisualStyleBackColor = true;
            this.BtnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // TxtCarLocAddrFind
            // 
            this.TxtCarLocAddrFind.Enabled = false;
            this.TxtCarLocAddrFind.Location = new System.Drawing.Point(117, 77);
            this.TxtCarLocAddrFind.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCarLocAddrFind.Name = "TxtCarLocAddrFind";
            this.TxtCarLocAddrFind.Size = new System.Drawing.Size(221, 26);
            this.TxtCarLocAddrFind.TabIndex = 27;
            // 
            // CTxtICCardFind
            // 
            this.CTxtICCardFind.EnabledButton = false;
            this.CTxtICCardFind.EnmTxtType = CustomControlLib.EnmTxtBoxType.ICCard;
            this.CTxtICCardFind.ImageButton = null;
            this.CTxtICCardFind.Location = new System.Drawing.Point(117, 37);
            this.CTxtICCardFind.Margin = new System.Windows.Forms.Padding(4);
            this.CTxtICCardFind.Name = "CTxtICCardFind";
            this.CTxtICCardFind.Size = new System.Drawing.Size(221, 26);
            this.CTxtICCardFind.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(340, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(111, 21);
            this.label5.TabIndex = 24;
            this.label5.Text = "库区";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(116, 21);
            this.label3.TabIndex = 23;
            this.label3.Text = "对应车位";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(116, 21);
            this.label4.TabIndex = 22;
            this.label4.Text = "用户卡号";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(702, 420);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "手动入/出库";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BtnAllHandOut);
            this.groupBox5.Controls.Add(this.CboWareHouseHandOut);
            this.groupBox5.Controls.Add(this.BtnHandOut);
            this.groupBox5.Controls.Add(this.TxtCarLocAddrHandOut);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Location = new System.Drawing.Point(370, 10);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(329, 412);
            this.groupBox5.TabIndex = 43;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "手动出库";
            // 
            // BtnAllHandOut
            // 
            this.BtnAllHandOut.Location = new System.Drawing.Point(117, 234);
            this.BtnAllHandOut.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAllHandOut.Name = "BtnAllHandOut";
            this.BtnAllHandOut.Size = new System.Drawing.Size(100, 31);
            this.BtnAllHandOut.TabIndex = 44;
            this.BtnAllHandOut.Text = "一键出库";
            this.BtnAllHandOut.UseVisualStyleBackColor = true;
            this.BtnAllHandOut.Click += new System.EventHandler(this.BtnAllHandOut_Click);
            // 
            // CboWareHouseHandOut
            // 
            this.CboWareHouseHandOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWareHouseHandOut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouseHandOut.FormattingEnabled = true;
            this.CboWareHouseHandOut.Location = new System.Drawing.Point(102, 44);
            this.CboWareHouseHandOut.Name = "CboWareHouseHandOut";
            this.CboWareHouseHandOut.Size = new System.Drawing.Size(221, 24);
            this.CboWareHouseHandOut.TabIndex = 35;
            // 
            // BtnHandOut
            // 
            this.BtnHandOut.Location = new System.Drawing.Point(117, 150);
            this.BtnHandOut.Margin = new System.Windows.Forms.Padding(4);
            this.BtnHandOut.Name = "BtnHandOut";
            this.BtnHandOut.Size = new System.Drawing.Size(100, 31);
            this.BtnHandOut.TabIndex = 34;
            this.BtnHandOut.Text = "出库";
            this.BtnHandOut.UseVisualStyleBackColor = true;
            this.BtnHandOut.Click += new System.EventHandler(this.BtnHandOut_Click);
            // 
            // TxtCarLocAddrHandOut
            // 
            this.TxtCarLocAddrHandOut.EnabledButton = true;
            this.TxtCarLocAddrHandOut.EnmTxtType = CustomControlLib.EnmTxtBoxType.CarLocationAddr;
            this.TxtCarLocAddrHandOut.ForeColor = System.Drawing.Color.Black;
            this.TxtCarLocAddrHandOut.ImageButton = global::WindowsFormLib.Properties.Resources.car;
            this.TxtCarLocAddrHandOut.Location = new System.Drawing.Point(102, 84);
            this.TxtCarLocAddrHandOut.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCarLocAddrHandOut.Name = "TxtCarLocAddrHandOut";
            this.TxtCarLocAddrHandOut.Size = new System.Drawing.Size(215, 26);
            this.TxtCarLocAddrHandOut.TabIndex = 31;
            this.TxtCarLocAddrHandOut.CallbackTextButtonEvent += new System.EventHandler(this.TxtCarLocAddr_Click);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(2, 84);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(100, 21);
            this.label19.TabIndex = 29;
            this.label19.Text = "车位";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(2, 44);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label20.Size = new System.Drawing.Size(100, 21);
            this.label20.TabIndex = 28;
            this.label20.Text = "库区";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOverRang);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.CboWareHouseHandIn);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.TxtCarSize);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.txtCarleght);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.DtpInTime);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.TxtCarLocAddrHandIn);
            this.groupBox2.Controls.Add(this.BtnHandIn);
            this.groupBox2.Controls.Add(this.TxtWheelBase);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.TxtICCardHand);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Location = new System.Drawing.Point(4, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(355, 412);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "手动入库";
            // 
            // txtOverRang
            // 
            this.txtOverRang.Location = new System.Drawing.Point(112, 275);
            this.txtOverRang.Margin = new System.Windows.Forms.Padding(4);
            this.txtOverRang.Name = "txtOverRang";
            this.txtOverRang.Size = new System.Drawing.Size(215, 26);
            this.txtOverRang.TabIndex = 48;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(3, 277);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label24.Size = new System.Drawing.Size(107, 21);
            this.label24.TabIndex = 47;
            this.label24.Text = "前悬长度";
            // 
            // CboWareHouseHandIn
            // 
            this.CboWareHouseHandIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWareHouseHandIn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouseHandIn.FormattingEnabled = true;
            this.CboWareHouseHandIn.Location = new System.Drawing.Point(111, 41);
            this.CboWareHouseHandIn.Name = "CboWareHouseHandIn";
            this.CboWareHouseHandIn.Size = new System.Drawing.Size(215, 24);
            this.CboWareHouseHandIn.TabIndex = 42;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 84);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(107, 21);
            this.label9.TabIndex = 30;
            this.label9.Text = "车位";
            // 
            // TxtCarSize
            // 
            this.TxtCarSize.Location = new System.Drawing.Point(112, 159);
            this.TxtCarSize.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCarSize.Name = "TxtCarSize";
            this.TxtCarSize.Size = new System.Drawing.Size(215, 26);
            this.TxtCarSize.TabIndex = 40;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(3, 161);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label18.Size = new System.Drawing.Size(107, 21);
            this.label18.TabIndex = 38;
            this.label18.Text = "车辆尺寸";
            // 
            // txtCarleght
            // 
            this.txtCarleght.Location = new System.Drawing.Point(112, 237);
            this.txtCarleght.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarleght.Name = "txtCarleght";
            this.txtCarleght.Size = new System.Drawing.Size(215, 26);
            this.txtCarleght.TabIndex = 46;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(3, 239);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label25.Size = new System.Drawing.Size(107, 21);
            this.label25.TabIndex = 45;
            this.label25.Text = "全车长度";
            // 
            // DtpInTime
            // 
            this.DtpInTime.CustomFormat = " yyyy-MM-dd HH:mm:ss";
            this.DtpInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpInTime.Location = new System.Drawing.Point(113, 312);
            this.DtpInTime.Margin = new System.Windows.Forms.Padding(4);
            this.DtpInTime.Name = "DtpInTime";
            this.DtpInTime.Size = new System.Drawing.Size(215, 26);
            this.DtpInTime.TabIndex = 41;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 44);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(107, 21);
            this.label10.TabIndex = 29;
            this.label10.Text = "库区";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(4, 314);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label17.Size = new System.Drawing.Size(107, 21);
            this.label17.TabIndex = 39;
            this.label17.Text = "入库时间";
            // 
            // TxtCarLocAddrHandIn
            // 
            this.TxtCarLocAddrHandIn.EnabledButton = true;
            this.TxtCarLocAddrHandIn.EnmTxtType = CustomControlLib.EnmTxtBoxType.CarLocationAddr;
            this.TxtCarLocAddrHandIn.ForeColor = System.Drawing.Color.Black;
            this.TxtCarLocAddrHandIn.ImageButton = global::WindowsFormLib.Properties.Resources.car;
            this.TxtCarLocAddrHandIn.Location = new System.Drawing.Point(112, 82);
            this.TxtCarLocAddrHandIn.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCarLocAddrHandIn.Name = "TxtCarLocAddrHandIn";
            this.TxtCarLocAddrHandIn.Size = new System.Drawing.Size(215, 26);
            this.TxtCarLocAddrHandIn.TabIndex = 32;
            this.TxtCarLocAddrHandIn.CallbackTextButtonEvent += new System.EventHandler(this.TxtCarLocAddr_Click);
            // 
            // BtnHandIn
            // 
            this.BtnHandIn.Location = new System.Drawing.Point(137, 363);
            this.BtnHandIn.Margin = new System.Windows.Forms.Padding(4);
            this.BtnHandIn.Name = "BtnHandIn";
            this.BtnHandIn.Size = new System.Drawing.Size(100, 31);
            this.BtnHandIn.TabIndex = 33;
            this.BtnHandIn.Text = "入库";
            this.BtnHandIn.UseVisualStyleBackColor = true;
            this.BtnHandIn.Click += new System.EventHandler(this.BtnHandIn_Click);
            // 
            // TxtWheelBase
            // 
            this.TxtWheelBase.Location = new System.Drawing.Point(111, 200);
            this.TxtWheelBase.Margin = new System.Windows.Forms.Padding(4);
            this.TxtWheelBase.Name = "TxtWheelBase";
            this.TxtWheelBase.Size = new System.Drawing.Size(215, 26);
            this.TxtWheelBase.TabIndex = 37;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(3, 124);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label16.Size = new System.Drawing.Size(107, 21);
            this.label16.TabIndex = 34;
            this.label16.Text = "用户卡号";
            // 
            // TxtICCardHand
            // 
            this.TxtICCardHand.Location = new System.Drawing.Point(112, 122);
            this.TxtICCardHand.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardHand.Name = "TxtICCardHand";
            this.TxtICCardHand.Size = new System.Drawing.Size(215, 26);
            this.TxtICCardHand.TabIndex = 36;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(2, 202);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label15.Size = new System.Drawing.Size(107, 21);
            this.label15.TabIndex = 35;
            this.label15.Text = "车辆轴距";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.btnAllDelete);
            this.tabPage4.Controls.Add(this.BtnFindTask);
            this.tabPage4.Controls.Add(this.CboDeviceCode);
            this.tabPage4.Controls.Add(this.label22);
            this.tabPage4.Controls.Add(this.CboWareHouseTask);
            this.tabPage4.Controls.Add(this.label21);
            this.tabPage4.Controls.Add(this.BtnDelete);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(702, 420);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "作业维护";
            // 
            // btnAllDelete
            // 
            this.btnAllDelete.Location = new System.Drawing.Point(608, 18);
            this.btnAllDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnAllDelete.Name = "btnAllDelete";
            this.btnAllDelete.Size = new System.Drawing.Size(80, 31);
            this.btnAllDelete.TabIndex = 35;
            this.btnAllDelete.Text = "全部删除";
            this.btnAllDelete.UseVisualStyleBackColor = true;
            this.btnAllDelete.Click += new System.EventHandler(this.btnAllDelete_Click);
            // 
            // BtnFindTask
            // 
            this.BtnFindTask.Location = new System.Drawing.Point(385, 19);
            this.BtnFindTask.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFindTask.Name = "BtnFindTask";
            this.BtnFindTask.Size = new System.Drawing.Size(80, 31);
            this.BtnFindTask.TabIndex = 34;
            this.BtnFindTask.Text = "查询";
            this.BtnFindTask.UseVisualStyleBackColor = true;
            this.BtnFindTask.Click += new System.EventHandler(this.BtnFindTask_Click);
            // 
            // CboDeviceCode
            // 
            this.CboDeviceCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboDeviceCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboDeviceCode.FormattingEnabled = true;
            this.CboDeviceCode.Location = new System.Drawing.Point(248, 23);
            this.CboDeviceCode.Name = "CboDeviceCode";
            this.CboDeviceCode.Size = new System.Drawing.Size(100, 24);
            this.CboDeviceCode.TabIndex = 33;
            this.CboDeviceCode.SelectedIndexChanged += new System.EventHandler(this.CboDeviceCode_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(199, 25);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label22.Size = new System.Drawing.Size(45, 21);
            this.label22.TabIndex = 32;
            this.label22.Text = "设备";
            // 
            // CboWareHouseTask
            // 
            this.CboWareHouseTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWareHouseTask.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouseTask.FormattingEnabled = true;
            this.CboWareHouseTask.Location = new System.Drawing.Point(68, 23);
            this.CboWareHouseTask.Name = "CboWareHouseTask";
            this.CboWareHouseTask.Size = new System.Drawing.Size(100, 24);
            this.CboWareHouseTask.TabIndex = 31;
            this.CboWareHouseTask.SelectedIndexChanged += new System.EventHandler(this.CboWareHouseTask_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(16, 25);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label21.Size = new System.Drawing.Size(49, 21);
            this.label21.TabIndex = 30;
            this.label21.Text = "库区";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(498, 18);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(80, 31);
            this.BtnDelete.TabIndex = 29;
            this.BtnDelete.Text = "删除作业";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.DgvTask);
            this.groupBox6.Location = new System.Drawing.Point(5, 65);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(692, 348);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "作业列表";
            // 
            // DgvTask
            // 
            this.DgvTask.AllowUserToAddRows = false;
            this.DgvTask.AllowUserToResizeRows = false;
            this.DgvTask.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvTask.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column1,
            this.Column3,
            this.Column4});
            this.DgvTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvTask.Location = new System.Drawing.Point(3, 22);
            this.DgvTask.Name = "DgvTask";
            this.DgvTask.RowHeadersVisible = false;
            this.DgvTask.RowTemplate.Height = 23;
            this.DgvTask.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvTask.Size = new System.Drawing.Size(686, 323);
            this.DgvTask.TabIndex = 0;
            this.DgvTask.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvTask_CellFormatting);
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "warehouse";
            this.Column2.HeaderText = "库区";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "devicecode";
            this.Column1.HeaderText = "设备";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "iccode";
            this.Column3.HeaderText = "卡号";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "tasktype";
            this.Column4.HeaderText = "作业类型";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // CFormSystemMtc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 461);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "CFormSystemMtc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统维护";
            this.Load += new System.EventHandler(this.CFormSystemMtc_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvTask)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox CboTaskType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblHall;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Button BtnFinish;
        private System.Windows.Forms.TextBox TxtTaskStatus;
        private System.Windows.Forms.TextBox TxtICCardID;
        private System.Windows.Forms.TextBox TxtHall;
        private System.Windows.Forms.GroupBox groupBox3;
        private CUserTextButton TxtSrcAddrInJog;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button BtnInJog;
        private CUserTextButton TxtDestAddrInJog;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnFind;
        private System.Windows.Forms.TextBox TxtCarLocAddrFind;
        private CUserTextButton CTxtICCardFind;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button BtnEnable;
        private System.Windows.Forms.Button BtnDisable;
        private CUserTextButton TxtCarLocAddrDis;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker DtpInTime;
        private System.Windows.Forms.TextBox TxtCarSize;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox TxtWheelBase;
        private System.Windows.Forms.TextBox TxtICCardHand;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button BtnHandIn;
        private CUserTextButton TxtCarLocAddrHandIn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox2;
        private CUserTextButton TxtCarLocAddrHandOut;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button BtnHandOut;
        private System.Windows.Forms.ComboBox CboWareHouseFind;
        private System.Windows.Forms.ComboBox CboWareHouseInJog;
        private System.Windows.Forms.ComboBox CboWareHouseDis;
        private System.Windows.Forms.ComboBox CboWareHouseHandIn;
        private System.Windows.Forms.ComboBox CboWareHouseHandOut;
        private System.Windows.Forms.ComboBox CboWareHouse;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView DgvTask;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.ComboBox CboDeviceCode;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox CboWareHouseTask;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button BtnFindTask;
        private System.Windows.Forms.Button BtnAllEnable;
        private System.Windows.Forms.Button BtnAllDisable;
        private System.Windows.Forms.Button BtnAllHandOut;
        private System.Windows.Forms.TextBox txtOverRang;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtCarleght;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btnAllDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button btnMuroFini;
        private System.Windows.Forms.Button btnMuro;
    }
}