using CustomControlLib;
namespace WindowsFormLib
{
    partial class CFormBillManagement
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormBillManagement));
            this.TctlBill = new System.Windows.Forms.TabControl();
            this.TpICCard = new System.Windows.Forms.TabPage();
            this.GbxICCard = new System.Windows.Forms.GroupBox();
            this.LblFeeType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblDescpCalu = new System.Windows.Forms.Label();
            this.CboHall = new System.Windows.Forms.ComboBox();
            this.TxtPayableFee = new System.Windows.Forms.TextBox();
            this.BtnFind = new System.Windows.Forms.Button();
            this.BtnVipOut = new System.Windows.Forms.Button();
            this.LblHall = new System.Windows.Forms.Label();
            this.TxtWareHouse = new System.Windows.Forms.TextBox();
            this.DtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.DtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.LblChange = new System.Windows.Forms.Label();
            this.TxtChange = new System.Windows.Forms.TextBox();
            this.TxtActualFee = new System.Windows.Forms.TextBox();
            this.CboFeeType = new System.Windows.Forms.ComboBox();
            this.BtnPay = new System.Windows.Forms.Button();
            this.BtnAutoRead = new System.Windows.Forms.Button();
            this.CTxtTariff = new CustomControlLib.CUserTextButton();
            this.TxtCalculateDays = new System.Windows.Forms.TextBox();
            this.LblCalculateDays = new System.Windows.Forms.Label();
            this.LblTariff = new System.Windows.Forms.Label();
            this.TxtICCardType = new System.Windows.Forms.TextBox();
            this.LblEndTime = new System.Windows.Forms.Label();
            this.LblICCardType = new System.Windows.Forms.Label();
            this.LblActualFee = new System.Windows.Forms.Label();
            this.lblwarehouse = new System.Windows.Forms.Label();
            this.CTxtICCardID = new CustomControlLib.CUserTextButton();
            this.LblStartTime = new System.Windows.Forms.Label();
            this.LblICCardID = new System.Windows.Forms.Label();
            this.TpCustomer = new System.Windows.Forms.TabPage();
            this.CucbipCustomer = new CustomControlLib.CUserCustomerBillInfoPanel();
            this.TpTariff = new System.Windows.Forms.TabPage();
            this.CutpTariff = new CustomControlLib.CUserTariffPanel();
            this.TctlBill.SuspendLayout();
            this.TpICCard.SuspendLayout();
            this.GbxICCard.SuspendLayout();
            this.TpCustomer.SuspendLayout();
            this.TpTariff.SuspendLayout();
            this.SuspendLayout();
            // 
            // TctlBill
            // 
            this.TctlBill.Controls.Add(this.TpICCard);
            this.TctlBill.Controls.Add(this.TpCustomer);
            this.TctlBill.Controls.Add(this.TpTariff);
            this.TctlBill.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TctlBill.Location = new System.Drawing.Point(3, 8);
            this.TctlBill.Margin = new System.Windows.Forms.Padding(4);
            this.TctlBill.Multiline = true;
            this.TctlBill.Name = "TctlBill";
            this.TctlBill.SelectedIndex = 0;
            this.TctlBill.Size = new System.Drawing.Size(857, 542);
            this.TctlBill.TabIndex = 2;
            this.TctlBill.SelectedIndexChanged += new System.EventHandler(this.TctlBill_SelectedIndexChanged);
            // 
            // TpICCard
            // 
            this.TpICCard.BackColor = System.Drawing.SystemColors.Control;
            this.TpICCard.Controls.Add(this.GbxICCard);
            this.TpICCard.Location = new System.Drawing.Point(4, 26);
            this.TpICCard.Margin = new System.Windows.Forms.Padding(4);
            this.TpICCard.Name = "TpICCard";
            this.TpICCard.Padding = new System.Windows.Forms.Padding(4);
            this.TpICCard.Size = new System.Drawing.Size(849, 512);
            this.TpICCard.TabIndex = 1;
            this.TpICCard.Text = "IC卡缴费";
            // 
            // GbxICCard
            // 
            this.GbxICCard.Controls.Add(this.LblFeeType);
            this.GbxICCard.Controls.Add(this.label1);
            this.GbxICCard.Controls.Add(this.LblDescpCalu);
            this.GbxICCard.Controls.Add(this.CboHall);
            this.GbxICCard.Controls.Add(this.TxtPayableFee);
            this.GbxICCard.Controls.Add(this.BtnFind);
            this.GbxICCard.Controls.Add(this.BtnVipOut);
            this.GbxICCard.Controls.Add(this.LblHall);
            this.GbxICCard.Controls.Add(this.TxtWareHouse);
            this.GbxICCard.Controls.Add(this.DtpEndTime);
            this.GbxICCard.Controls.Add(this.DtpStartTime);
            this.GbxICCard.Controls.Add(this.LblChange);
            this.GbxICCard.Controls.Add(this.TxtChange);
            this.GbxICCard.Controls.Add(this.TxtActualFee);
            this.GbxICCard.Controls.Add(this.CboFeeType);
            this.GbxICCard.Controls.Add(this.BtnPay);
            this.GbxICCard.Controls.Add(this.BtnAutoRead);
            this.GbxICCard.Controls.Add(this.CTxtTariff);
            this.GbxICCard.Controls.Add(this.TxtCalculateDays);
            this.GbxICCard.Controls.Add(this.LblCalculateDays);
            this.GbxICCard.Controls.Add(this.LblTariff);
            this.GbxICCard.Controls.Add(this.TxtICCardType);
            this.GbxICCard.Controls.Add(this.LblEndTime);
            this.GbxICCard.Controls.Add(this.LblICCardType);
            this.GbxICCard.Controls.Add(this.LblActualFee);
            this.GbxICCard.Controls.Add(this.lblwarehouse);
            this.GbxICCard.Controls.Add(this.CTxtICCardID);
            this.GbxICCard.Controls.Add(this.LblStartTime);
            this.GbxICCard.Controls.Add(this.LblICCardID);
            this.GbxICCard.Location = new System.Drawing.Point(4, 3);
            this.GbxICCard.Margin = new System.Windows.Forms.Padding(4);
            this.GbxICCard.Name = "GbxICCard";
            this.GbxICCard.Padding = new System.Windows.Forms.Padding(4);
            this.GbxICCard.Size = new System.Drawing.Size(841, 507);
            this.GbxICCard.TabIndex = 1;
            this.GbxICCard.TabStop = false;
            // 
            // LblFeeType
            // 
            this.LblFeeType.AutoSize = true;
            this.LblFeeType.Location = new System.Drawing.Point(430, 163);
            this.LblFeeType.Name = "LblFeeType";
            this.LblFeeType.Size = new System.Drawing.Size(72, 16);
            this.LblFeeType.TabIndex = 61;
            this.LblFeeType.Text = "费用类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "0";
            // 
            // LblDescpCalu
            // 
            this.LblDescpCalu.ForeColor = System.Drawing.Color.Brown;
            this.LblDescpCalu.Location = new System.Drawing.Point(71, 420);
            this.LblDescpCalu.Name = "LblDescpCalu";
            this.LblDescpCalu.Size = new System.Drawing.Size(700, 60);
            this.LblDescpCalu.TabIndex = 60;
            this.LblDescpCalu.Text = "0";
            this.LblDescpCalu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CboHall
            // 
            this.CboHall.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CboHall.FormattingEnabled = true;
            this.CboHall.Location = new System.Drawing.Point(599, 269);
            this.CboHall.Name = "CboHall";
            this.CboHall.Size = new System.Drawing.Size(150, 30);
            this.CboHall.TabIndex = 59;
            // 
            // TxtPayableFee
            // 
            this.TxtPayableFee.Enabled = false;
            this.TxtPayableFee.Location = new System.Drawing.Point(150, 218);
            this.TxtPayableFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtPayableFee.Name = "TxtPayableFee";
            this.TxtPayableFee.Size = new System.Drawing.Size(241, 26);
            this.TxtPayableFee.TabIndex = 58;
            // 
            // BtnFind
            // 
            this.BtnFind.Location = new System.Drawing.Point(279, 345);
            this.BtnFind.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFind.Name = "BtnFind";
            this.BtnFind.Size = new System.Drawing.Size(130, 40);
            this.BtnFind.TabIndex = 56;
            this.BtnFind.Text = "查 询";
            this.BtnFind.UseVisualStyleBackColor = true;
            this.BtnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // BtnVipOut
            // 
            this.BtnVipOut.Location = new System.Drawing.Point(619, 345);
            this.BtnVipOut.Margin = new System.Windows.Forms.Padding(4);
            this.BtnVipOut.Name = "BtnVipOut";
            this.BtnVipOut.Size = new System.Drawing.Size(130, 40);
            this.BtnVipOut.TabIndex = 55;
            this.BtnVipOut.Text = "确认出车";
            this.BtnVipOut.UseVisualStyleBackColor = true;
            this.BtnVipOut.Click += new System.EventHandler(this.BtnVipOut_Click);
            // 
            // LblHall
            // 
            this.LblHall.Location = new System.Drawing.Point(522, 276);
            this.LblHall.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblHall.Name = "LblHall";
            this.LblHall.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblHall.Size = new System.Drawing.Size(72, 24);
            this.LblHall.TabIndex = 53;
            this.LblHall.Text = "出库车厅";
            // 
            // TxtWareHouse
            // 
            this.TxtWareHouse.Enabled = false;
            this.TxtWareHouse.Location = new System.Drawing.Point(385, 274);
            this.TxtWareHouse.Margin = new System.Windows.Forms.Padding(4);
            this.TxtWareHouse.Name = "TxtWareHouse";
            this.TxtWareHouse.Size = new System.Drawing.Size(116, 26);
            this.TxtWareHouse.TabIndex = 52;
            // 
            // DtpEndTime
            // 
            this.DtpEndTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.DtpEndTime.Enabled = false;
            this.DtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpEndTime.Location = new System.Drawing.Point(508, 103);
            this.DtpEndTime.Name = "DtpEndTime";
            this.DtpEndTime.Size = new System.Drawing.Size(241, 26);
            this.DtpEndTime.TabIndex = 50;
            this.DtpEndTime.Value = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            // 
            // DtpStartTime
            // 
            this.DtpStartTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.DtpStartTime.Enabled = false;
            this.DtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpStartTime.Location = new System.Drawing.Point(150, 103);
            this.DtpStartTime.Name = "DtpStartTime";
            this.DtpStartTime.Size = new System.Drawing.Size(241, 26);
            this.DtpStartTime.TabIndex = 49;
            this.DtpStartTime.Value = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            // 
            // LblChange
            // 
            this.LblChange.Location = new System.Drawing.Point(74, 277);
            this.LblChange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblChange.Name = "LblChange";
            this.LblChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblChange.Size = new System.Drawing.Size(74, 27);
            this.LblChange.TabIndex = 47;
            this.LblChange.Text = "找零";
            // 
            // TxtChange
            // 
            this.TxtChange.Enabled = false;
            this.TxtChange.Location = new System.Drawing.Point(150, 273);
            this.TxtChange.Margin = new System.Windows.Forms.Padding(4);
            this.TxtChange.Name = "TxtChange";
            this.TxtChange.Size = new System.Drawing.Size(155, 26);
            this.TxtChange.TabIndex = 46;
            // 
            // TxtActualFee
            // 
            this.TxtActualFee.Location = new System.Drawing.Point(508, 215);
            this.TxtActualFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtActualFee.Name = "TxtActualFee";
            this.TxtActualFee.Size = new System.Drawing.Size(241, 26);
            this.TxtActualFee.TabIndex = 45;
            this.TxtActualFee.TextChanged += new System.EventHandler(this.TxtActualFee_TextChanged);
            // 
            // CboFeeType
            // 
            this.CboFeeType.Enabled = false;
            this.CboFeeType.FormattingEnabled = true;
            this.CboFeeType.Items.AddRange(new object[] {
            "季卡",
            "年卡",
            "月卡"});
            this.CboFeeType.Location = new System.Drawing.Point(508, 160);
            this.CboFeeType.Margin = new System.Windows.Forms.Padding(4);
            this.CboFeeType.Name = "CboFeeType";
            this.CboFeeType.Size = new System.Drawing.Size(90, 24);
            this.CboFeeType.TabIndex = 41;
            // 
            // BtnPay
            // 
            this.BtnPay.Location = new System.Drawing.Point(449, 345);
            this.BtnPay.Margin = new System.Windows.Forms.Padding(4);
            this.BtnPay.Name = "BtnPay";
            this.BtnPay.Size = new System.Drawing.Size(133, 40);
            this.BtnPay.TabIndex = 38;
            this.BtnPay.Text = "确认缴费";
            this.BtnPay.UseVisualStyleBackColor = true;
            this.BtnPay.Click += new System.EventHandler(this.BtnPay_Click);
            // 
            // BtnAutoRead
            // 
            this.BtnAutoRead.Location = new System.Drawing.Point(109, 345);
            this.BtnAutoRead.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAutoRead.Name = "BtnAutoRead";
            this.BtnAutoRead.Size = new System.Drawing.Size(130, 40);
            this.BtnAutoRead.TabIndex = 36;
            this.BtnAutoRead.Text = "开启读卡";
            this.BtnAutoRead.UseVisualStyleBackColor = true;
            this.BtnAutoRead.Click += new System.EventHandler(this.BtnAutoRead_Click);
            // 
            // CTxtTariff
            // 
            this.CTxtTariff.EnabledButton = false;
            this.CTxtTariff.EnmTxtType = CustomControlLib.EnmTxtBoxType.Init;
            this.CTxtTariff.ForeColor = System.Drawing.Color.Gray;
            this.CTxtTariff.ImageButton = global::WindowsFormLib.Properties.Resources.CIMC中集空港设备;
            this.CTxtTariff.Location = new System.Drawing.Point(598, 159);
            this.CTxtTariff.Margin = new System.Windows.Forms.Padding(4);
            this.CTxtTariff.Name = "CTxtTariff";
            this.CTxtTariff.Size = new System.Drawing.Size(152, 26);
            this.CTxtTariff.TabIndex = 35;
            this.CTxtTariff.CallbackTextButtonEvent += new System.EventHandler(this.TxtTariff_DoubleClick);
            // 
            // TxtCalculateDays
            // 
            this.TxtCalculateDays.Enabled = false;
            this.TxtCalculateDays.Location = new System.Drawing.Point(150, 159);
            this.TxtCalculateDays.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCalculateDays.Name = "TxtCalculateDays";
            this.TxtCalculateDays.Size = new System.Drawing.Size(241, 26);
            this.TxtCalculateDays.TabIndex = 34;
            // 
            // LblCalculateDays
            // 
            this.LblCalculateDays.Location = new System.Drawing.Point(68, 163);
            this.LblCalculateDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblCalculateDays.Name = "LblCalculateDays";
            this.LblCalculateDays.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblCalculateDays.Size = new System.Drawing.Size(82, 26);
            this.LblCalculateDays.TabIndex = 33;
            this.LblCalculateDays.Text = "剩余时间";
            // 
            // LblTariff
            // 
            this.LblTariff.Location = new System.Drawing.Point(68, 222);
            this.LblTariff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTariff.Name = "LblTariff";
            this.LblTariff.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblTariff.Size = new System.Drawing.Size(82, 26);
            this.LblTariff.TabIndex = 32;
            this.LblTariff.Text = "应缴费用";
            // 
            // TxtICCardType
            // 
            this.TxtICCardType.Enabled = false;
            this.TxtICCardType.Location = new System.Drawing.Point(508, 50);
            this.TxtICCardType.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardType.Name = "TxtICCardType";
            this.TxtICCardType.Size = new System.Drawing.Size(241, 26);
            this.TxtICCardType.TabIndex = 31;
            // 
            // LblEndTime
            // 
            this.LblEndTime.Location = new System.Drawing.Point(423, 107);
            this.LblEndTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblEndTime.Name = "LblEndTime";
            this.LblEndTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblEndTime.Size = new System.Drawing.Size(82, 26);
            this.LblEndTime.TabIndex = 29;
            this.LblEndTime.Text = "截止日期";
            // 
            // LblICCardType
            // 
            this.LblICCardType.Location = new System.Drawing.Point(423, 54);
            this.LblICCardType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblICCardType.Name = "LblICCardType";
            this.LblICCardType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblICCardType.Size = new System.Drawing.Size(82, 26);
            this.LblICCardType.TabIndex = 28;
            this.LblICCardType.Text = "卡片类型";
            // 
            // LblActualFee
            // 
            this.LblActualFee.Location = new System.Drawing.Point(434, 218);
            this.LblActualFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblActualFee.Name = "LblActualFee";
            this.LblActualFee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblActualFee.Size = new System.Drawing.Size(74, 26);
            this.LblActualFee.TabIndex = 25;
            this.LblActualFee.Text = "实收金额";
            // 
            // lblwarehouse
            // 
            this.lblwarehouse.Location = new System.Drawing.Point(316, 278);
            this.lblwarehouse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblwarehouse.Name = "lblwarehouse";
            this.lblwarehouse.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblwarehouse.Size = new System.Drawing.Size(61, 24);
            this.lblwarehouse.TabIndex = 24;
            this.lblwarehouse.Text = "库区";
            // 
            // CTxtICCardID
            // 
            this.CTxtICCardID.EnabledButton = false;
            this.CTxtICCardID.EnmTxtType = CustomControlLib.EnmTxtBoxType.ICCard;
            this.CTxtICCardID.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CTxtICCardID.ImageButton = null;
            this.CTxtICCardID.Location = new System.Drawing.Point(150, 50);
            this.CTxtICCardID.Margin = new System.Windows.Forms.Padding(4);
            this.CTxtICCardID.Name = "CTxtICCardID";
            this.CTxtICCardID.Size = new System.Drawing.Size(241, 26);
            this.CTxtICCardID.TabIndex = 23;
            // 
            // LblStartTime
            // 
            this.LblStartTime.Location = new System.Drawing.Point(68, 107);
            this.LblStartTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblStartTime.Name = "LblStartTime";
            this.LblStartTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblStartTime.Size = new System.Drawing.Size(82, 26);
            this.LblStartTime.TabIndex = 21;
            this.LblStartTime.Text = "缴费日期";
            // 
            // LblICCardID
            // 
            this.LblICCardID.Location = new System.Drawing.Point(68, 54);
            this.LblICCardID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblICCardID.Name = "LblICCardID";
            this.LblICCardID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblICCardID.Size = new System.Drawing.Size(82, 26);
            this.LblICCardID.TabIndex = 20;
            this.LblICCardID.Text = "用户卡号";
            // 
            // TpCustomer
            // 
            this.TpCustomer.Controls.Add(this.CucbipCustomer);
            this.TpCustomer.Location = new System.Drawing.Point(4, 26);
            this.TpCustomer.Name = "TpCustomer";
            this.TpCustomer.Padding = new System.Windows.Forms.Padding(3);
            this.TpCustomer.Size = new System.Drawing.Size(849, 512);
            this.TpCustomer.TabIndex = 4;
            this.TpCustomer.Text = "车主管理";
            this.TpCustomer.UseVisualStyleBackColor = true;
            // 
            // CucbipCustomer
            // 
            this.CucbipCustomer.BackColor = System.Drawing.SystemColors.Control;
            this.CucbipCustomer.ImageEndBtn = global::WindowsFormLib.Properties.Resources.endPage;
            this.CucbipCustomer.ImageLeftBtn = global::WindowsFormLib.Properties.Resources.leftPage;
            this.CucbipCustomer.ImageRightBtn = global::WindowsFormLib.Properties.Resources.rightPage;
            this.CucbipCustomer.ImageStartBtn = global::WindowsFormLib.Properties.Resources.startPage;
            this.CucbipCustomer.Location = new System.Drawing.Point(0, 0);
            this.CucbipCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.CucbipCustomer.Name = "CucbipCustomer";
            this.CucbipCustomer.Padding = new System.Windows.Forms.Padding(4);
            this.CucbipCustomer.Size = new System.Drawing.Size(844, 511);
            this.CucbipCustomer.TabIndex = 0;
            this.CucbipCustomer.Text = "车主管理";
            this.CucbipCustomer.BtnModifyClick += new System.EventHandler(this.BtnCustomerModify_Click);
            // 
            // TpTariff
            // 
            this.TpTariff.BackColor = System.Drawing.SystemColors.Control;
            this.TpTariff.Controls.Add(this.CutpTariff);
            this.TpTariff.Location = new System.Drawing.Point(4, 26);
            this.TpTariff.Name = "TpTariff";
            this.TpTariff.Padding = new System.Windows.Forms.Padding(3);
            this.TpTariff.Size = new System.Drawing.Size(849, 512);
            this.TpTariff.TabIndex = 5;
            this.TpTariff.Text = "计费标准";
            // 
            // CutpTariff
            // 
            this.CutpTariff.Location = new System.Drawing.Point(0, 0);
            this.CutpTariff.Margin = new System.Windows.Forms.Padding(4);
            this.CutpTariff.Name = "CutpTariff";
            this.CutpTariff.Padding = new System.Windows.Forms.Padding(4);
            this.CutpTariff.Size = new System.Drawing.Size(845, 514);
            this.CutpTariff.TabIndex = 26;
            this.CutpTariff.Text = "计费";
            // 
            // CFormBillManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(860, 552);
            this.Controls.Add(this.TctlBill);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CFormBillManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "缴费管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CFormBillManagement_FormClosing);
            this.Load += new System.EventHandler(this.CFormBillManagement_Load);
            this.TctlBill.ResumeLayout(false);
            this.TpICCard.ResumeLayout(false);
            this.GbxICCard.ResumeLayout(false);
            this.GbxICCard.PerformLayout();
            this.TpCustomer.ResumeLayout(false);
            this.TpTariff.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TctlBill;
        private System.Windows.Forms.TabPage TpICCard;
        private System.Windows.Forms.GroupBox GbxICCard;
        private System.Windows.Forms.Button BtnPay;
        private System.Windows.Forms.Button BtnAutoRead;
        private System.Windows.Forms.TextBox TxtCalculateDays;
        private System.Windows.Forms.Label LblCalculateDays;
        private System.Windows.Forms.Label LblTariff;
        private System.Windows.Forms.TextBox TxtICCardType;
        private System.Windows.Forms.Label LblEndTime;
        private System.Windows.Forms.Label LblICCardType;
        private System.Windows.Forms.Label LblActualFee;
        private System.Windows.Forms.Label lblwarehouse;
        private CUserTextButton CTxtICCardID;
        private System.Windows.Forms.Label LblStartTime;
        private System.Windows.Forms.Label LblICCardID;
        private System.Windows.Forms.ComboBox CboFeeType;
        private CUserTextButton CTxtTariff;
        private System.Windows.Forms.TextBox TxtActualFee;
        private System.Windows.Forms.Label LblChange;
        private System.Windows.Forms.TextBox TxtChange;
        private System.Windows.Forms.DateTimePicker DtpStartTime;
        private System.Windows.Forms.DateTimePicker DtpEndTime;
        private System.Windows.Forms.TextBox TxtWareHouse;
        private System.Windows.Forms.Label LblHall;
        private System.Windows.Forms.TextBox TxtPayableFee;
        private System.Windows.Forms.Button BtnVipOut;
        private System.Windows.Forms.Button BtnFind;
        private System.Windows.Forms.TabPage TpCustomer;
        private CUserCustomerBillInfoPanel CucbipCustomer;
        private System.Windows.Forms.TabPage TpTariff;
        private CUserTariffPanel CutpTariff;
        private System.Windows.Forms.ComboBox CboHall;
        private System.Windows.Forms.Label LblDescpCalu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblFeeType;
    }
}

