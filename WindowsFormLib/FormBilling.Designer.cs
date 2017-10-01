using CustomControlLib;
namespace WindowsFormLib
{
    partial class CFormBilling
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormBilling));
            this.TctlBill = new System.Windows.Forms.TabControl();
            this.TpICCard = new System.Windows.Forms.TabPage();
            this.GbxICCard = new System.Windows.Forms.GroupBox();
            this.LblFeeStand = new System.Windows.Forms.Label();
            this.LblFeeType = new System.Windows.Forms.Label();
            this.BtnFind = new System.Windows.Forms.Button();
            this.BtnPay = new System.Windows.Forms.Button();
            this.BtnReadAndFind = new System.Windows.Forms.Button();
            this.LBBillingInfoReturn = new System.Windows.Forms.Label();
            this.TxtPayableFee = new System.Windows.Forms.TextBox();
            this.LblHall = new System.Windows.Forms.Label();
            this.DtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.DtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.LblChange = new System.Windows.Forms.Label();
            this.TxtChange = new System.Windows.Forms.TextBox();
            this.TxtActualFee = new System.Windows.Forms.TextBox();
            this.CboFeeType = new System.Windows.Forms.ComboBox();
            this.CTxtTariff = new CustomControlLib.CUserTextButton();
            this.TxtCalculateDays = new System.Windows.Forms.TextBox();
            this.LblCalculateDays = new System.Windows.Forms.Label();
            this.LblTariff = new System.Windows.Forms.Label();
            this.TxtICCardType = new System.Windows.Forms.TextBox();
            this.LblEndTime = new System.Windows.Forms.Label();
            this.LblICCardType = new System.Windows.Forms.Label();
            this.LblActualFee = new System.Windows.Forms.Label();
            this.LblWareHouse = new System.Windows.Forms.Label();
            this.CTxtICCardID = new CustomControlLib.CUserTextButton();
            this.LblStartTime = new System.Windows.Forms.Label();
            this.LblICCardID = new System.Windows.Forms.Label();
            this.TxtWareHouse = new System.Windows.Forms.TextBox();
            this.CboHall = new System.Windows.Forms.ComboBox();
            this.LblDescpCalu = new System.Windows.Forms.Label();
            this.TpCustomer = new System.Windows.Forms.TabPage();
            this.CucbipCustomer = new CustomControlLib.CUserCustomerBillInfoPanel();
            this.TpTariff = new System.Windows.Forms.TabPage();
            this.CutpTariff = new CustomControlLib.CUserTariffPanel();
            this.TpICCardLog = new System.Windows.Forms.TabPage();
            this.GbxLogList = new System.Windows.Forms.GroupBox();
            this.CupttsICCard = new CustomControlLib.CUserPageTurnToolStrip();
            this.DgvICCard = new System.Windows.Forms.DataGridView();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GbxFindCondition = new System.Windows.Forms.GroupBox();
            this.CboICCardContent = new System.Windows.Forms.ComboBox();
            this.BtnICCardReport = new System.Windows.Forms.Button();
            this.BtnICCardFind = new System.Windows.Forms.Button();
            this.TxtICCardContent = new System.Windows.Forms.TextBox();
            this.CboICCardCondition = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DtpICCardEnd = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DtpICCardStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.TsslOptLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslOptTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslSumLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslSumTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslOccupyLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslOccupyTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslSpaceLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslSpaceTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslSpaceMaxLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslSpaceMaxTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslSplit = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslCurTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PicEditModifyPassword = new System.Windows.Forms.PictureBox();
            this.PicEditLogout = new System.Windows.Forms.PictureBox();
            this.LBModifyPassword = new System.Windows.Forms.Label();
            this.LBLogout = new System.Windows.Forms.Label();
            this.TctlBill.SuspendLayout();
            this.TpICCard.SuspendLayout();
            this.GbxICCard.SuspendLayout();
            this.TpCustomer.SuspendLayout();
            this.TpTariff.SuspendLayout();
            this.TpICCardLog.SuspendLayout();
            this.GbxLogList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvICCard)).BeginInit();
            this.GbxFindCondition.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicEditModifyPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicEditLogout)).BeginInit();
            this.SuspendLayout();
            // 
            // TctlBill
            // 
            this.TctlBill.Controls.Add(this.TpICCard);
            this.TctlBill.Controls.Add(this.TpCustomer);
            this.TctlBill.Controls.Add(this.TpTariff);
            this.TctlBill.Controls.Add(this.TpICCardLog);
            this.TctlBill.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TctlBill.Location = new System.Drawing.Point(0, 0);
            this.TctlBill.Margin = new System.Windows.Forms.Padding(4);
            this.TctlBill.Name = "TctlBill";
            this.TctlBill.SelectedIndex = 0;
            this.TctlBill.Size = new System.Drawing.Size(847, 509);
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
            this.TpICCard.Size = new System.Drawing.Size(839, 479);
            this.TpICCard.TabIndex = 1;
            this.TpICCard.Text = "IC卡缴费";
            // 
            // GbxICCard
            // 
            this.GbxICCard.Controls.Add(this.LblFeeStand);
            this.GbxICCard.Controls.Add(this.LblFeeType);
            this.GbxICCard.Controls.Add(this.BtnFind);
            this.GbxICCard.Controls.Add(this.BtnPay);
            this.GbxICCard.Controls.Add(this.BtnReadAndFind);
            this.GbxICCard.Controls.Add(this.LBBillingInfoReturn);
            this.GbxICCard.Controls.Add(this.TxtPayableFee);
            this.GbxICCard.Controls.Add(this.LblHall);
            this.GbxICCard.Controls.Add(this.DtpEndTime);
            this.GbxICCard.Controls.Add(this.DtpStartTime);
            this.GbxICCard.Controls.Add(this.LblChange);
            this.GbxICCard.Controls.Add(this.TxtChange);
            this.GbxICCard.Controls.Add(this.TxtActualFee);
            this.GbxICCard.Controls.Add(this.CboFeeType);
            this.GbxICCard.Controls.Add(this.CTxtTariff);
            this.GbxICCard.Controls.Add(this.TxtCalculateDays);
            this.GbxICCard.Controls.Add(this.LblCalculateDays);
            this.GbxICCard.Controls.Add(this.LblTariff);
            this.GbxICCard.Controls.Add(this.TxtICCardType);
            this.GbxICCard.Controls.Add(this.LblEndTime);
            this.GbxICCard.Controls.Add(this.LblICCardType);
            this.GbxICCard.Controls.Add(this.LblActualFee);
            this.GbxICCard.Controls.Add(this.LblWareHouse);
            this.GbxICCard.Controls.Add(this.CTxtICCardID);
            this.GbxICCard.Controls.Add(this.LblStartTime);
            this.GbxICCard.Controls.Add(this.LblICCardID);
            this.GbxICCard.Controls.Add(this.TxtWareHouse);
            this.GbxICCard.Controls.Add(this.CboHall);
            this.GbxICCard.Controls.Add(this.LblDescpCalu);
            this.GbxICCard.Location = new System.Drawing.Point(4, 3);
            this.GbxICCard.Margin = new System.Windows.Forms.Padding(4);
            this.GbxICCard.Name = "GbxICCard";
            this.GbxICCard.Padding = new System.Windows.Forms.Padding(4);
            this.GbxICCard.Size = new System.Drawing.Size(813, 464);
            this.GbxICCard.TabIndex = 1;
            this.GbxICCard.TabStop = false;
            // 
            // LblFeeStand
            // 
            this.LblFeeStand.Location = new System.Drawing.Point(200, 315);
            this.LblFeeStand.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblFeeStand.Name = "LblFeeStand";
            this.LblFeeStand.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblFeeStand.Size = new System.Drawing.Size(80, 40);
            this.LblFeeStand.TabIndex = 62;
            this.LblFeeStand.Text = "收费标准";
            // 
            // LblFeeType
            // 
            this.LblFeeType.Location = new System.Drawing.Point(10, 314);
            this.LblFeeType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblFeeType.Name = "LblFeeType";
            this.LblFeeType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblFeeType.Size = new System.Drawing.Size(100, 40);
            this.LblFeeType.TabIndex = 61;
            this.LblFeeType.Text = "收费类型";
            // 
            // BtnFind
            // 
            this.BtnFind.Location = new System.Drawing.Point(327, 360);
            this.BtnFind.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFind.Name = "BtnFind";
            this.BtnFind.Size = new System.Drawing.Size(130, 40);
            this.BtnFind.TabIndex = 56;
            this.BtnFind.Text = "查询";
            this.BtnFind.UseVisualStyleBackColor = true;
            this.BtnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // BtnPay
            // 
            this.BtnPay.BackColor = System.Drawing.SystemColors.Control;
            this.BtnPay.Enabled = false;
            this.BtnPay.Location = new System.Drawing.Point(527, 360);
            this.BtnPay.Margin = new System.Windows.Forms.Padding(4);
            this.BtnPay.Name = "BtnPay";
            this.BtnPay.Size = new System.Drawing.Size(133, 40);
            this.BtnPay.TabIndex = 38;
            this.BtnPay.Text = "确认缴费";
            this.BtnPay.UseVisualStyleBackColor = false;
            this.BtnPay.Click += new System.EventHandler(this.BtnPay_Click);
            // 
            // BtnReadAndFind
            // 
            this.BtnReadAndFind.Location = new System.Drawing.Point(127, 360);
            this.BtnReadAndFind.Margin = new System.Windows.Forms.Padding(4);
            this.BtnReadAndFind.Name = "BtnReadAndFind";
            this.BtnReadAndFind.Size = new System.Drawing.Size(130, 40);
            this.BtnReadAndFind.TabIndex = 36;
            this.BtnReadAndFind.Text = "读卡及查询";
            this.BtnReadAndFind.UseVisualStyleBackColor = true;
            this.BtnReadAndFind.Click += new System.EventHandler(this.BtnReadAndFind_Click);
            // 
            // LBBillingInfoReturn
            // 
            this.LBBillingInfoReturn.AutoSize = true;
            this.LBBillingInfoReturn.Location = new System.Drawing.Point(110, 27);
            this.LBBillingInfoReturn.Name = "LBBillingInfoReturn";
            this.LBBillingInfoReturn.Size = new System.Drawing.Size(0, 16);
            this.LBBillingInfoReturn.TabIndex = 59;
            // 
            // TxtPayableFee
            // 
            this.TxtPayableFee.Enabled = false;
            this.TxtPayableFee.Location = new System.Drawing.Point(110, 267);
            this.TxtPayableFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtPayableFee.Name = "TxtPayableFee";
            this.TxtPayableFee.Size = new System.Drawing.Size(150, 26);
            this.TxtPayableFee.TabIndex = 58;
            // 
            // LblHall
            // 
            this.LblHall.Location = new System.Drawing.Point(549, 201);
            this.LblHall.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblHall.Name = "LblHall";
            this.LblHall.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblHall.Size = new System.Drawing.Size(80, 23);
            this.LblHall.TabIndex = 53;
            this.LblHall.Text = "出车车厅";
            // 
            // DtpEndTime
            // 
            this.DtpEndTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.DtpEndTime.Enabled = false;
            this.DtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpEndTime.Location = new System.Drawing.Point(460, 133);
            this.DtpEndTime.Name = "DtpEndTime";
            this.DtpEndTime.Size = new System.Drawing.Size(320, 26);
            this.DtpEndTime.TabIndex = 50;
            this.DtpEndTime.Value = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            // 
            // DtpStartTime
            // 
            this.DtpStartTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.DtpStartTime.Enabled = false;
            this.DtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpStartTime.Location = new System.Drawing.Point(110, 133);
            this.DtpStartTime.Name = "DtpStartTime";
            this.DtpStartTime.Size = new System.Drawing.Size(250, 26);
            this.DtpStartTime.TabIndex = 49;
            this.DtpStartTime.Value = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            // 
            // LblChange
            // 
            this.LblChange.Location = new System.Drawing.Point(517, 267);
            this.LblChange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblChange.Name = "LblChange";
            this.LblChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblChange.Size = new System.Drawing.Size(117, 40);
            this.LblChange.TabIndex = 47;
            this.LblChange.Text = "找零";
            // 
            // TxtChange
            // 
            this.TxtChange.Enabled = false;
            this.TxtChange.Location = new System.Drawing.Point(635, 267);
            this.TxtChange.Margin = new System.Windows.Forms.Padding(4);
            this.TxtChange.Name = "TxtChange";
            this.TxtChange.Size = new System.Drawing.Size(150, 26);
            this.TxtChange.TabIndex = 46;
            // 
            // TxtActualFee
            // 
            this.TxtActualFee.Location = new System.Drawing.Point(367, 267);
            this.TxtActualFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtActualFee.Name = "TxtActualFee";
            this.TxtActualFee.Size = new System.Drawing.Size(150, 26);
            this.TxtActualFee.TabIndex = 45;
            this.TxtActualFee.TextChanged += new System.EventHandler(this.TxtActualFee_TextChanged);
            // 
            // CboFeeType
            // 
            this.CboFeeType.Enabled = false;
            this.CboFeeType.FormattingEnabled = true;
            this.CboFeeType.Items.AddRange(new object[] {
            "月卡",
            "季卡",
            "年卡"});
            this.CboFeeType.Location = new System.Drawing.Point(110, 311);
            this.CboFeeType.Margin = new System.Windows.Forms.Padding(4);
            this.CboFeeType.Name = "CboFeeType";
            this.CboFeeType.Size = new System.Drawing.Size(90, 24);
            this.CboFeeType.TabIndex = 41;
            // 
            // CTxtTariff
            // 
            this.CTxtTariff.EnabledButton = false;
            this.CTxtTariff.EnmTxtType = CustomControlLib.EnmTxtBoxType.Init;
            this.CTxtTariff.ImageButton = null;
            this.CTxtTariff.Location = new System.Drawing.Point(280, 311);
            this.CTxtTariff.Margin = new System.Windows.Forms.Padding(4);
            this.CTxtTariff.Name = "CTxtTariff";
            this.CTxtTariff.ReadOnly = true;
            this.CTxtTariff.Size = new System.Drawing.Size(150, 26);
            this.CTxtTariff.TabIndex = 35;
            // 
            // TxtCalculateDays
            // 
            this.TxtCalculateDays.Enabled = false;
            this.TxtCalculateDays.Location = new System.Drawing.Point(110, 200);
            this.TxtCalculateDays.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCalculateDays.Name = "TxtCalculateDays";
            this.TxtCalculateDays.Size = new System.Drawing.Size(250, 26);
            this.TxtCalculateDays.TabIndex = 34;
            // 
            // LblCalculateDays
            // 
            this.LblCalculateDays.Location = new System.Drawing.Point(3, 200);
            this.LblCalculateDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblCalculateDays.Name = "LblCalculateDays";
            this.LblCalculateDays.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblCalculateDays.Size = new System.Drawing.Size(107, 40);
            this.LblCalculateDays.TabIndex = 33;
            this.LblCalculateDays.Text = "剩余时间";
            // 
            // LblTariff
            // 
            this.LblTariff.Location = new System.Drawing.Point(3, 267);
            this.LblTariff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTariff.Name = "LblTariff";
            this.LblTariff.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblTariff.Size = new System.Drawing.Size(107, 40);
            this.LblTariff.TabIndex = 32;
            this.LblTariff.Text = "应缴费用";
            // 
            // TxtICCardType
            // 
            this.TxtICCardType.Enabled = false;
            this.TxtICCardType.Location = new System.Drawing.Point(460, 67);
            this.TxtICCardType.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardType.Name = "TxtICCardType";
            this.TxtICCardType.Size = new System.Drawing.Size(320, 26);
            this.TxtICCardType.TabIndex = 31;
            // 
            // LblEndTime
            // 
            this.LblEndTime.Location = new System.Drawing.Point(360, 133);
            this.LblEndTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblEndTime.Name = "LblEndTime";
            this.LblEndTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblEndTime.Size = new System.Drawing.Size(100, 40);
            this.LblEndTime.TabIndex = 29;
            this.LblEndTime.Text = "截止日期";
            // 
            // LblICCardType
            // 
            this.LblICCardType.Location = new System.Drawing.Point(360, 67);
            this.LblICCardType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblICCardType.Name = "LblICCardType";
            this.LblICCardType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblICCardType.Size = new System.Drawing.Size(100, 40);
            this.LblICCardType.TabIndex = 28;
            this.LblICCardType.Text = "卡类型";
            // 
            // LblActualFee
            // 
            this.LblActualFee.Location = new System.Drawing.Point(260, 267);
            this.LblActualFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblActualFee.Name = "LblActualFee";
            this.LblActualFee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblActualFee.Size = new System.Drawing.Size(107, 40);
            this.LblActualFee.TabIndex = 25;
            this.LblActualFee.Text = "实收金额";
            // 
            // LblWareHouse
            // 
            this.LblWareHouse.Location = new System.Drawing.Point(368, 201);
            this.LblWareHouse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblWareHouse.Name = "LblWareHouse";
            this.LblWareHouse.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblWareHouse.Size = new System.Drawing.Size(76, 24);
            this.LblWareHouse.TabIndex = 24;
            this.LblWareHouse.Text = "库区";
            // 
            // CTxtICCardID
            // 
            this.CTxtICCardID.EnabledButton = false;
            this.CTxtICCardID.EnmTxtType = CustomControlLib.EnmTxtBoxType.ICCard;
            this.CTxtICCardID.ImageButton = null;
            this.CTxtICCardID.Location = new System.Drawing.Point(110, 67);
            this.CTxtICCardID.Margin = new System.Windows.Forms.Padding(4);
            this.CTxtICCardID.Name = "CTxtICCardID";
            this.CTxtICCardID.Size = new System.Drawing.Size(250, 26);
            this.CTxtICCardID.TabIndex = 23;
            // 
            // LblStartTime
            // 
            this.LblStartTime.Location = new System.Drawing.Point(33, 133);
            this.LblStartTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblStartTime.Name = "LblStartTime";
            this.LblStartTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblStartTime.Size = new System.Drawing.Size(77, 26);
            this.LblStartTime.TabIndex = 21;
            this.LblStartTime.Text = "缴费日期";
            // 
            // LblICCardID
            // 
            this.LblICCardID.Location = new System.Drawing.Point(3, 67);
            this.LblICCardID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblICCardID.Name = "LblICCardID";
            this.LblICCardID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblICCardID.Size = new System.Drawing.Size(107, 40);
            this.LblICCardID.TabIndex = 20;
            this.LblICCardID.Text = "用户卡号";
            // 
            // TxtWareHouse
            // 
            this.TxtWareHouse.Enabled = false;
            this.TxtWareHouse.Location = new System.Drawing.Point(452, 201);
            this.TxtWareHouse.Margin = new System.Windows.Forms.Padding(4);
            this.TxtWareHouse.Name = "TxtWareHouse";
            this.TxtWareHouse.Size = new System.Drawing.Size(90, 26);
            this.TxtWareHouse.TabIndex = 52;
            // 
            // CboHall
            // 
            this.CboHall.Location = new System.Drawing.Point(630, 200);
            this.CboHall.Margin = new System.Windows.Forms.Padding(4);
            this.CboHall.Name = "CboHall";
            this.CboHall.Size = new System.Drawing.Size(150, 24);
            this.CboHall.TabIndex = 54;
            // 
            // LblDescpCalu
            // 
            this.LblDescpCalu.ForeColor = System.Drawing.Color.SkyBlue;
            this.LblDescpCalu.Location = new System.Drawing.Point(40, 318);
            this.LblDescpCalu.Name = "LblDescpCalu";
            this.LblDescpCalu.Size = new System.Drawing.Size(700, 60);
            this.LblDescpCalu.TabIndex = 60;
            this.LblDescpCalu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TpCustomer
            // 
            this.TpCustomer.BackColor = System.Drawing.SystemColors.Control;
            this.TpCustomer.Controls.Add(this.CucbipCustomer);
            this.TpCustomer.Location = new System.Drawing.Point(4, 26);
            this.TpCustomer.Name = "TpCustomer";
            this.TpCustomer.Padding = new System.Windows.Forms.Padding(3);
            this.TpCustomer.Size = new System.Drawing.Size(839, 479);
            this.TpCustomer.TabIndex = 3;
            this.TpCustomer.Text = "车主管理";
            // 
            // CucbipCustomer
            // 
            this.CucbipCustomer.BackColor = System.Drawing.SystemColors.Control;
            this.CucbipCustomer.ImageEndBtn = global::WindowsFormLib.Properties.Resources.endPage;
            this.CucbipCustomer.ImageLeftBtn = global::WindowsFormLib.Properties.Resources.leftPage;
            this.CucbipCustomer.ImageRightBtn = global::WindowsFormLib.Properties.Resources.rightPage;
            this.CucbipCustomer.ImageStartBtn = global::WindowsFormLib.Properties.Resources.rightPage;
            this.CucbipCustomer.Location = new System.Drawing.Point(0, 0);
            this.CucbipCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.CucbipCustomer.Name = "CucbipCustomer";
            this.CucbipCustomer.Padding = new System.Windows.Forms.Padding(4);
            this.CucbipCustomer.Size = new System.Drawing.Size(841, 487);
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
            this.TpTariff.Size = new System.Drawing.Size(839, 479);
            this.TpTariff.TabIndex = 4;
            this.TpTariff.Text = "计费标准";
            // 
            // CutpTariff
            // 
            this.CutpTariff.Location = new System.Drawing.Point(4, 7);
            this.CutpTariff.Margin = new System.Windows.Forms.Padding(4);
            this.CutpTariff.Name = "CutpTariff";
            this.CutpTariff.Padding = new System.Windows.Forms.Padding(4);
            this.CutpTariff.Size = new System.Drawing.Size(831, 468);
            this.CutpTariff.TabIndex = 26;
            this.CutpTariff.Text = "计费";
            // 
            // TpICCardLog
            // 
            this.TpICCardLog.BackColor = System.Drawing.SystemColors.Control;
            this.TpICCardLog.Controls.Add(this.GbxLogList);
            this.TpICCardLog.Controls.Add(this.GbxFindCondition);
            this.TpICCardLog.Location = new System.Drawing.Point(4, 26);
            this.TpICCardLog.Name = "TpICCardLog";
            this.TpICCardLog.Padding = new System.Windows.Forms.Padding(3);
            this.TpICCardLog.Size = new System.Drawing.Size(839, 479);
            this.TpICCardLog.TabIndex = 6;
            this.TpICCardLog.Text = "IC卡缴费查询";
            // 
            // GbxLogList
            // 
            this.GbxLogList.Controls.Add(this.CupttsICCard);
            this.GbxLogList.Controls.Add(this.DgvICCard);
            this.GbxLogList.Location = new System.Drawing.Point(1, 120);
            this.GbxLogList.Name = "GbxLogList";
            this.GbxLogList.Size = new System.Drawing.Size(964, 414);
            this.GbxLogList.TabIndex = 1;
            this.GbxLogList.TabStop = false;
            this.GbxLogList.Text = "日志列表";
            // 
            // CupttsICCard
            // 
            this.CupttsICCard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CupttsICCard.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CupttsICCard.ImageEndBtn = global::WindowsFormLib.Properties.Resources.endPage;
            this.CupttsICCard.ImageLeftBtn = global::WindowsFormLib.Properties.Resources.leftPage;
            this.CupttsICCard.ImageRightBtn = global::WindowsFormLib.Properties.Resources.rightPage;
            this.CupttsICCard.ImageStartBtn = global::WindowsFormLib.Properties.Resources.startPage;
            this.CupttsICCard.Location = new System.Drawing.Point(3, 378);
            this.CupttsICCard.Name = "CupttsICCard";
            this.CupttsICCard.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.CupttsICCard.Size = new System.Drawing.Size(958, 33);
            this.CupttsICCard.TabIndex = 8;
            this.CupttsICCard.Tag = this.DgvICCard;
            this.CupttsICCard.Text = "CupttsICCard";
            // 
            // DgvICCard
            // 
            this.DgvICCard.AllowUserToAddRows = false;
            this.DgvICCard.AllowUserToResizeRows = false;
            this.DgvICCard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvICCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvICCard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column19,
            this.Column1,
            this.Column25,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.DgvICCard.Location = new System.Drawing.Point(5, 23);
            this.DgvICCard.Name = "DgvICCard";
            this.DgvICCard.RowHeadersVisible = false;
            this.DgvICCard.RowTemplate.Height = 23;
            this.DgvICCard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvICCard.Size = new System.Drawing.Size(960, 352);
            this.DgvICCard.TabIndex = 0;
            this.DgvICCard.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvICCard_CellFormatting);
            // 
            // Column19
            // 
            this.Column19.DataPropertyName = "username";
            this.Column19.HeaderText = "车主姓名";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "iccode";
            this.Column1.HeaderText = "卡号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column25
            // 
            this.Column25.DataPropertyName = "ictype";
            this.Column25.HeaderText = "卡类型";
            this.Column25.Name = "Column25";
            this.Column25.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "starttime";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column2.HeaderText = "缴费日期/入库时间";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "endtime";
            dataGridViewCellStyle2.Format = "yyyy-MM-dd HH:mm:ss";
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "截止日期/出库时间";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "payablefee";
            this.Column4.HeaderText = "应缴费用";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "actualfee";
            this.Column5.HeaderText = "实缴费用";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "optcode";
            this.Column6.HeaderText = "操作员";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // GbxFindCondition
            // 
            this.GbxFindCondition.Controls.Add(this.CboICCardContent);
            this.GbxFindCondition.Controls.Add(this.BtnICCardReport);
            this.GbxFindCondition.Controls.Add(this.BtnICCardFind);
            this.GbxFindCondition.Controls.Add(this.TxtICCardContent);
            this.GbxFindCondition.Controls.Add(this.CboICCardCondition);
            this.GbxFindCondition.Controls.Add(this.label4);
            this.GbxFindCondition.Controls.Add(this.DtpICCardEnd);
            this.GbxFindCondition.Controls.Add(this.label5);
            this.GbxFindCondition.Controls.Add(this.label2);
            this.GbxFindCondition.Controls.Add(this.DtpICCardStart);
            this.GbxFindCondition.Controls.Add(this.label1);
            this.GbxFindCondition.Location = new System.Drawing.Point(1, 14);
            this.GbxFindCondition.Name = "GbxFindCondition";
            this.GbxFindCondition.Size = new System.Drawing.Size(964, 100);
            this.GbxFindCondition.TabIndex = 0;
            this.GbxFindCondition.TabStop = false;
            this.GbxFindCondition.Text = "查询条件";
            // 
            // CboICCardContent
            // 
            this.CboICCardContent.DisplayMember = "所有";
            this.CboICCardContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboICCardContent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboICCardContent.FormattingEnabled = true;
            this.CboICCardContent.Items.AddRange(new object[] {
            "卡号",
            "卡类型",
            "车主姓名",
            "操作员",
            "所有"});
            this.CboICCardContent.Location = new System.Drawing.Point(421, 65);
            this.CboICCardContent.Name = "CboICCardContent";
            this.CboICCardContent.Size = new System.Drawing.Size(220, 24);
            this.CboICCardContent.TabIndex = 11;
            this.CboICCardContent.Visible = false;
            // 
            // BtnICCardReport
            // 
            this.BtnICCardReport.Location = new System.Drawing.Point(689, 65);
            this.BtnICCardReport.Name = "BtnICCardReport";
            this.BtnICCardReport.Size = new System.Drawing.Size(100, 26);
            this.BtnICCardReport.TabIndex = 12;
            this.BtnICCardReport.Text = "报表";
            this.BtnICCardReport.UseVisualStyleBackColor = true;
            this.BtnICCardReport.Click += new System.EventHandler(this.BtnICCardReport_Click);
            // 
            // BtnICCardFind
            // 
            this.BtnICCardFind.Location = new System.Drawing.Point(689, 26);
            this.BtnICCardFind.Name = "BtnICCardFind";
            this.BtnICCardFind.Size = new System.Drawing.Size(100, 26);
            this.BtnICCardFind.TabIndex = 11;
            this.BtnICCardFind.Text = "查询";
            this.BtnICCardFind.UseVisualStyleBackColor = true;
            this.BtnICCardFind.Click += new System.EventHandler(this.BtnICCardFind_Click);
            // 
            // TxtICCardContent
            // 
            this.TxtICCardContent.Enabled = false;
            this.TxtICCardContent.Location = new System.Drawing.Point(421, 65);
            this.TxtICCardContent.Name = "TxtICCardContent";
            this.TxtICCardContent.Size = new System.Drawing.Size(220, 26);
            this.TxtICCardContent.TabIndex = 10;
            // 
            // CboICCardCondition
            // 
            this.CboICCardCondition.DisplayMember = "所有";
            this.CboICCardCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboICCardCondition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboICCardCondition.FormattingEnabled = true;
            this.CboICCardCondition.Items.AddRange(new object[] {
            "卡号",
            "卡类型",
            "车主姓名",
            "操作员",
            "所有"});
            this.CboICCardCondition.Location = new System.Drawing.Point(421, 26);
            this.CboICCardCondition.Name = "CboICCardCondition";
            this.CboICCardCondition.Size = new System.Drawing.Size(220, 24);
            this.CboICCardCondition.TabIndex = 9;
            this.CboICCardCondition.SelectedIndexChanged += new System.EventHandler(this.CboICCardCondition_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(321, 65);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(100, 26);
            this.label4.TabIndex = 8;
            this.label4.Text = "查询内容";
            // 
            // DtpICCardEnd
            // 
            this.DtpICCardEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DtpICCardEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpICCardEnd.Location = new System.Drawing.Point(101, 65);
            this.DtpICCardEnd.Name = "DtpICCardEnd";
            this.DtpICCardEnd.Size = new System.Drawing.Size(220, 26);
            this.DtpICCardEnd.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(2, 65);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(99, 26);
            this.label5.TabIndex = 6;
            this.label5.Text = "截止日期";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(321, 26);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(100, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "查询条件";
            // 
            // DtpICCardStart
            // 
            this.DtpICCardStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DtpICCardStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpICCardStart.Location = new System.Drawing.Point(101, 26);
            this.DtpICCardStart.Name = "DtpICCardStart";
            this.DtpICCardStart.Size = new System.Drawing.Size(220, 26);
            this.DtpICCardStart.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 26);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(99, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "起始日期";
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsslOptLbl,
            this.TsslOptTxt,
            this.TsslSumLbl,
            this.TsslSumTxt,
            this.TsslOccupyLbl,
            this.TsslOccupyTxt,
            this.TsslSpaceLbl,
            this.TsslSpaceTxt,
            this.TsslSpaceMaxLbl,
            this.TsslSpaceMaxTxt,
            this.TsslSplit,
            this.TsslConnected,
            this.TsslCurTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 590);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1007, 25);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip";
            // 
            // TsslOptLbl
            // 
            this.TsslOptLbl.Name = "TsslOptLbl";
            this.TsslOptLbl.Size = new System.Drawing.Size(56, 20);
            this.TsslOptLbl.Text = "操作人：";
            // 
            // TsslOptTxt
            // 
            this.TsslOptTxt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TsslOptTxt.Name = "TsslOptTxt";
            this.TsslOptTxt.Size = new System.Drawing.Size(0, 20);
            // 
            // TsslSumLbl
            // 
            this.TsslSumLbl.Name = "TsslSumLbl";
            this.TsslSumLbl.Size = new System.Drawing.Size(56, 20);
            this.TsslSumLbl.Text = "总车位：";
            // 
            // TsslSumTxt
            // 
            this.TsslSumTxt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TsslSumTxt.Name = "TsslSumTxt";
            this.TsslSumTxt.Size = new System.Drawing.Size(22, 20);
            this.TsslSumTxt.Text = "88";
            // 
            // TsslOccupyLbl
            // 
            this.TsslOccupyLbl.Name = "TsslOccupyLbl";
            this.TsslOccupyLbl.Size = new System.Drawing.Size(68, 20);
            this.TsslOccupyLbl.Text = "占用车位：";
            // 
            // TsslOccupyTxt
            // 
            this.TsslOccupyTxt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TsslOccupyTxt.Name = "TsslOccupyTxt";
            this.TsslOccupyTxt.Size = new System.Drawing.Size(15, 20);
            this.TsslOccupyTxt.Text = "0";
            // 
            // TsslSpaceLbl
            // 
            this.TsslSpaceLbl.Name = "TsslSpaceLbl";
            this.TsslSpaceLbl.Size = new System.Drawing.Size(68, 20);
            this.TsslSpaceLbl.Text = "空余车位：";
            // 
            // TsslSpaceTxt
            // 
            this.TsslSpaceTxt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TsslSpaceTxt.Name = "TsslSpaceTxt";
            this.TsslSpaceTxt.Size = new System.Drawing.Size(22, 20);
            this.TsslSpaceTxt.Text = "88";
            // 
            // TsslSpaceMaxLbl
            // 
            this.TsslSpaceMaxLbl.Name = "TsslSpaceMaxLbl";
            this.TsslSpaceMaxLbl.Size = new System.Drawing.Size(68, 20);
            this.TsslSpaceMaxLbl.Text = "空余大车：";
            // 
            // TsslSpaceMaxTxt
            // 
            this.TsslSpaceMaxTxt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TsslSpaceMaxTxt.Name = "TsslSpaceMaxTxt";
            this.TsslSpaceMaxTxt.Size = new System.Drawing.Size(15, 20);
            this.TsslSpaceMaxTxt.Text = "0";
            // 
            // TsslSplit
            // 
            this.TsslSplit.Name = "TsslSplit";
            this.TsslSplit.Size = new System.Drawing.Size(505, 20);
            this.TsslSplit.Spring = true;
            // 
            // TsslConnected
            // 
            this.TsslConnected.Name = "TsslConnected";
            this.TsslConnected.Size = new System.Drawing.Size(92, 20);
            this.TsslConnected.Text = "服务器连接状态";
            this.TsslConnected.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.TsslConnected.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // TsslCurTime
            // 
            this.TsslCurTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TsslCurTime.Name = "TsslCurTime";
            this.TsslCurTime.Size = new System.Drawing.Size(0, 20);
            this.TsslCurTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TsslCurTime.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PicEditModifyPassword
            // 
            this.PicEditModifyPassword.Image = global::WindowsFormLib.Properties.Resources.modifyPassWord;
            this.PicEditModifyPassword.Location = new System.Drawing.Point(921, 26);
            this.PicEditModifyPassword.Name = "PicEditModifyPassword";
            this.PicEditModifyPassword.Size = new System.Drawing.Size(40, 40);
            this.PicEditModifyPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicEditModifyPassword.TabIndex = 8;
            this.PicEditModifyPassword.TabStop = false;
            this.PicEditModifyPassword.Click += new System.EventHandler(this.PicEditModifyPassword_Click);
            this.PicEditModifyPassword.MouseLeave += new System.EventHandler(this.PicEditModifyPassword_MouseLeave);
            this.PicEditModifyPassword.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicEditModifyPassword_MouseMove);
            // 
            // PicEditLogout
            // 
            this.PicEditLogout.Image = global::WindowsFormLib.Properties.Resources.changeAccount;
            this.PicEditLogout.Location = new System.Drawing.Point(921, 96);
            this.PicEditLogout.Name = "PicEditLogout";
            this.PicEditLogout.Size = new System.Drawing.Size(40, 40);
            this.PicEditLogout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicEditLogout.TabIndex = 9;
            this.PicEditLogout.TabStop = false;
            this.PicEditLogout.Click += new System.EventHandler(this.PicEditLogout_Click);
            this.PicEditLogout.MouseLeave += new System.EventHandler(this.PicEditLogout_MouseLeave);
            this.PicEditLogout.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicEditLogout_MouseMove);
            // 
            // LBModifyPassword
            // 
            this.LBModifyPassword.AutoSize = true;
            this.LBModifyPassword.Location = new System.Drawing.Point(919, 41);
            this.LBModifyPassword.Name = "LBModifyPassword";
            this.LBModifyPassword.Size = new System.Drawing.Size(0, 12);
            this.LBModifyPassword.TabIndex = 10;
            // 
            // LBLogout
            // 
            this.LBLogout.AutoSize = true;
            this.LBLogout.Location = new System.Drawing.Point(919, 86);
            this.LBLogout.Name = "LBLogout";
            this.LBLogout.Size = new System.Drawing.Size(0, 12);
            this.LBLogout.TabIndex = 11;
            // 
            // CFormBilling
            // 
            this.AcceptButton = this.BtnPay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1007, 615);
            this.Controls.Add(this.LBLogout);
            this.Controls.Add(this.LBModifyPassword);
            this.Controls.Add(this.PicEditLogout);
            this.Controls.Add(this.PicEditModifyPassword);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.TctlBill);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "CFormBilling";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "缴费管理";
            this.Load += new System.EventHandler(this.CFormBilling_Load);
            this.TctlBill.ResumeLayout(false);
            this.TpICCard.ResumeLayout(false);
            this.GbxICCard.ResumeLayout(false);
            this.GbxICCard.PerformLayout();
            this.TpCustomer.ResumeLayout(false);
            this.TpTariff.ResumeLayout(false);
            this.TpICCardLog.ResumeLayout(false);
            this.GbxLogList.ResumeLayout(false);
            this.GbxLogList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvICCard)).EndInit();
            this.GbxFindCondition.ResumeLayout(false);
            this.GbxFindCondition.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicEditModifyPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicEditLogout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TctlBill;
        private System.Windows.Forms.TabPage TpICCard;
        private System.Windows.Forms.GroupBox GbxICCard;
        private System.Windows.Forms.Button BtnPay;
        private System.Windows.Forms.Button BtnReadAndFind;
        private System.Windows.Forms.TextBox TxtCalculateDays;
        private System.Windows.Forms.Label LblCalculateDays;
        private System.Windows.Forms.Label LblTariff;
        private System.Windows.Forms.TextBox TxtICCardType;
        private System.Windows.Forms.Label LblEndTime;
        private System.Windows.Forms.Label LblICCardType;
        private System.Windows.Forms.Label LblActualFee;
        private System.Windows.Forms.Label LblWareHouse;
        private CUserTextButton CTxtICCardID;
        private System.Windows.Forms.Label LblStartTime;
        private System.Windows.Forms.Label LblICCardID;
        private System.Windows.Forms.TabPage TpCustomer;
        private System.Windows.Forms.ComboBox CboFeeType;
        private CUserTextButton CTxtTariff;
        private System.Windows.Forms.TextBox TxtActualFee;
        private System.Windows.Forms.Label LblChange;
        private System.Windows.Forms.TextBox TxtChange;
        private System.Windows.Forms.DateTimePicker DtpStartTime;
        private System.Windows.Forms.DateTimePicker DtpEndTime;
        private System.Windows.Forms.TextBox TxtWareHouse;
        private System.Windows.Forms.ComboBox CboHall;
        private System.Windows.Forms.Label LblHall;
        private System.Windows.Forms.Button BtnFind;
        private CustomControlLib.CUserCustomerBillInfoPanel CucbipCustomer;
        private System.Windows.Forms.TabPage TpTariff;
        private CUserTariffPanel CutpTariff;
        private System.Windows.Forms.TabPage TpICCardLog;
        private System.Windows.Forms.GroupBox GbxLogList;
        private CUserPageTurnToolStrip CupttsICCard;
        private System.Windows.Forms.DataGridView DgvICCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column25;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.GroupBox GbxFindCondition;
        private System.Windows.Forms.ComboBox CboICCardContent;
        private System.Windows.Forms.Button BtnICCardReport;
        private System.Windows.Forms.Button BtnICCardFind;
        private System.Windows.Forms.TextBox TxtICCardContent;
        private System.Windows.Forms.ComboBox CboICCardCondition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DtpICCardEnd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DtpICCardStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel TsslOptLbl;
        private System.Windows.Forms.ToolStripStatusLabel TsslOptTxt;
        private System.Windows.Forms.ToolStripStatusLabel TsslSumLbl;
        private System.Windows.Forms.ToolStripStatusLabel TsslSumTxt;
        private System.Windows.Forms.ToolStripStatusLabel TsslOccupyLbl;
        private System.Windows.Forms.ToolStripStatusLabel TsslOccupyTxt;
        private System.Windows.Forms.ToolStripStatusLabel TsslSpaceLbl;
        private System.Windows.Forms.ToolStripStatusLabel TsslSpaceTxt;
        private System.Windows.Forms.ToolStripStatusLabel TsslSpaceMaxLbl;
        private System.Windows.Forms.ToolStripStatusLabel TsslSpaceMaxTxt;
        private System.Windows.Forms.ToolStripStatusLabel TsslCurTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox TxtPayableFee;
        private System.Windows.Forms.ToolStripStatusLabel TsslSplit;
        private System.Windows.Forms.ToolStripStatusLabel TsslConnected;
		private System.Windows.Forms.Label LBBillingInfoReturn;
        private System.Windows.Forms.Label LblDescpCalu;
        private System.Windows.Forms.PictureBox PicEditModifyPassword;
        private System.Windows.Forms.PictureBox PicEditLogout;
        private System.Windows.Forms.Label LBModifyPassword;
        private System.Windows.Forms.Label LBLogout;
        private System.Windows.Forms.Label LblFeeStand;
        private System.Windows.Forms.Label LblFeeType;
    }
}

