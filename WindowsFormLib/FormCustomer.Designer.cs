using CustomControlLib;
namespace WindowsFormLib
{
    partial class CFormCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormCustomer));
            this.TxtAddr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CboICCardStatus = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.CboICCardType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CTxtName = new CustomControlLib.CUserTextButton();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.CTxtCarNumber = new CustomControlLib.CUserTextButton();
            this.label5 = new System.Windows.Forms.Label();
            this.CTxtCarLocAddr = new CustomControlLib.CUserTextButton();
            this.LblCarLocAddr = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CTxtMobile = new CustomControlLib.CUserTextButton();
            this.label3 = new System.Windows.Forms.Label();
            this.CTxtPhone = new CustomControlLib.CUserTextButton();
            this.label1 = new System.Windows.Forms.Label();
            this.CTxtICCardID = new CustomControlLib.CUserTextButton();
            this.label4 = new System.Windows.Forms.Label();
            this.LblTariff = new System.Windows.Forms.Label();
            this.CTxtTariff = new CustomControlLib.CUserTextButton();
            this.LblFixChange = new System.Windows.Forms.Label();
            this.TxtFixChange = new System.Windows.Forms.TextBox();
            this.CTxtFixActualFee = new CustomControlLib.CUserTextButton();
            this.BtnFixPay = new System.Windows.Forms.Button();
            this.LblFixActualFee = new System.Windows.Forms.Label();
            this.CboWareHouse = new System.Windows.Forms.ComboBox();
            this.BtnModify = new System.Windows.Forms.Button();
            this.CboPriorityID = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxtAddr
            // 
            this.TxtAddr.Font = new System.Drawing.Font("宋体", 12F);
            this.TxtAddr.Location = new System.Drawing.Point(434, 294);
            this.TxtAddr.Margin = new System.Windows.Forms.Padding(7);
            this.TxtAddr.Name = "TxtAddr";
            this.TxtAddr.Size = new System.Drawing.Size(204, 26);
            this.TxtAddr.TabIndex = 68;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(332, 294);
            this.label6.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(102, 47);
            this.label6.TabIndex = 67;
            this.label6.Text = "住址";
            // 
            // CboICCardStatus
            // 
            this.CboICCardStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboICCardStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboICCardStatus.Font = new System.Drawing.Font("宋体", 12F);
            this.CboICCardStatus.FormattingEnabled = true;
            this.CboICCardStatus.Items.AddRange(new object[] {
            "正常",
            "挂失",
            "注销"});
            this.CboICCardStatus.Location = new System.Drawing.Point(127, 83);
            this.CboICCardStatus.Margin = new System.Windows.Forms.Padding(5);
            this.CboICCardStatus.Name = "CboICCardStatus";
            this.CboICCardStatus.Size = new System.Drawing.Size(204, 24);
            this.CboICCardStatus.TabIndex = 66;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 12F);
            this.label9.Location = new System.Drawing.Point(332, 240);
            this.label9.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(102, 47);
            this.label9.TabIndex = 65;
            this.label9.Text = "移动电话";
            // 
            // CboICCardType
            // 
            this.CboICCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboICCardType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboICCardType.Font = new System.Drawing.Font("宋体", 12F);
            this.CboICCardType.FormattingEnabled = true;
            this.CboICCardType.Items.AddRange(new object[] {
            "临时卡",
            "定期卡",
            "固定车位卡"});
            this.CboICCardType.Location = new System.Drawing.Point(435, 29);
            this.CboICCardType.Margin = new System.Windows.Forms.Padding(5);
            this.CboICCardType.Name = "CboICCardType";
            this.CboICCardType.Size = new System.Drawing.Size(204, 24);
            this.CboICCardType.TabIndex = 64;
            this.CboICCardType.SelectedIndexChanged += new System.EventHandler(this.CboICCardType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(0, 240);
            this.label8.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(125, 47);
            this.label8.TabIndex = 63;
            this.label8.Text = "住宅电话";
            // 
            // CTxtName
            // 
            this.CTxtName.EnabledButton = false;
            this.CTxtName.EnmTxtType = CustomControlLib.EnmTxtBoxType.Name;
            this.CTxtName.Font = new System.Drawing.Font("宋体", 12F);
            this.CTxtName.ImageButton = null;
            this.CTxtName.Location = new System.Drawing.Point(126, 190);
            this.CTxtName.Margin = new System.Windows.Forms.Padding(7);
            this.CTxtName.Name = "CTxtName";
            this.CTxtName.Size = new System.Drawing.Size(204, 26);
            this.CTxtName.TabIndex = 62;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 12F);
            this.label7.Location = new System.Drawing.Point(2, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(125, 47);
            this.label7.TabIndex = 61;
            this.label7.Text = "用户卡号";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnCancel.Location = new System.Drawing.Point(514, 411);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(100, 30);
            this.BtnCancel.TabIndex = 60;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnDelete.Location = new System.Drawing.Point(364, 411);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(100, 30);
            this.BtnDelete.TabIndex = 59;
            this.BtnDelete.Text = "删除";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnSave.Location = new System.Drawing.Point(64, 411);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(100, 30);
            this.BtnSave.TabIndex = 58;
            this.BtnSave.Text = "保存";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // CTxtCarNumber
            // 
            this.CTxtCarNumber.EnabledButton = false;
            this.CTxtCarNumber.EnmTxtType = CustomControlLib.EnmTxtBoxType.Name;
            this.CTxtCarNumber.Font = new System.Drawing.Font("宋体", 12F);
            this.CTxtCarNumber.ImageButton = null;
            this.CTxtCarNumber.Location = new System.Drawing.Point(126, 294);
            this.CTxtCarNumber.Margin = new System.Windows.Forms.Padding(7);
            this.CTxtCarNumber.Name = "CTxtCarNumber";
            this.CTxtCarNumber.Size = new System.Drawing.Size(204, 26);
            this.CTxtCarNumber.TabIndex = 57;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(0, 294);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(125, 47);
            this.label5.TabIndex = 56;
            this.label5.Text = "车牌号";
            // 
            // CTxtCarLocAddr
            // 
            this.CTxtCarLocAddr.EnabledButton = true;
            this.CTxtCarLocAddr.EnmTxtType = CustomControlLib.EnmTxtBoxType.CarLocationAddr;
            this.CTxtCarLocAddr.Font = new System.Drawing.Font("宋体", 12F);
            this.CTxtCarLocAddr.ImageButton = global::WindowsFormLib.Properties.Resources.car;
            this.CTxtCarLocAddr.Location = new System.Drawing.Point(127, 136);
            this.CTxtCarLocAddr.Margin = new System.Windows.Forms.Padding(7);
            this.CTxtCarLocAddr.Name = "CTxtCarLocAddr";
            this.CTxtCarLocAddr.Size = new System.Drawing.Size(204, 26);
            this.CTxtCarLocAddr.TabIndex = 55;
            this.CTxtCarLocAddr.CallbackTextButtonEvent += new System.EventHandler(this.CTxtCarLocAddr_DoubleClick);
            // 
            // LblCarLocAddr
            // 
            this.LblCarLocAddr.Font = new System.Drawing.Font("宋体", 12F);
            this.LblCarLocAddr.Location = new System.Drawing.Point(1, 136);
            this.LblCarLocAddr.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.LblCarLocAddr.Name = "LblCarLocAddr";
            this.LblCarLocAddr.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblCarLocAddr.Size = new System.Drawing.Size(125, 47);
            this.LblCarLocAddr.TabIndex = 54;
            this.LblCarLocAddr.Text = "分配车位";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(333, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(102, 47);
            this.label2.TabIndex = 53;
            this.label2.Text = "库区";
            // 
            // CTxtMobile
            // 
            this.CTxtMobile.EnabledButton = false;
            this.CTxtMobile.EnmTxtType = CustomControlLib.EnmTxtBoxType.Mobile;
            this.CTxtMobile.Font = new System.Drawing.Font("宋体", 12F);
            this.CTxtMobile.ImageButton = null;
            this.CTxtMobile.Location = new System.Drawing.Point(434, 240);
            this.CTxtMobile.Margin = new System.Windows.Forms.Padding(7);
            this.CTxtMobile.Name = "CTxtMobile";
            this.CTxtMobile.Size = new System.Drawing.Size(204, 26);
            this.CTxtMobile.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(1, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(125, 47);
            this.label3.TabIndex = 51;
            this.label3.Text = "卡状态";
            // 
            // CTxtPhone
            // 
            this.CTxtPhone.EnabledButton = false;
            this.CTxtPhone.EnmTxtType = CustomControlLib.EnmTxtBoxType.Mobile;
            this.CTxtPhone.Font = new System.Drawing.Font("宋体", 12F);
            this.CTxtPhone.ImageButton = null;
            this.CTxtPhone.Location = new System.Drawing.Point(126, 240);
            this.CTxtPhone.Margin = new System.Windows.Forms.Padding(7);
            this.CTxtPhone.Name = "CTxtPhone";
            this.CTxtPhone.Size = new System.Drawing.Size(204, 26);
            this.CTxtPhone.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(333, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(102, 47);
            this.label1.TabIndex = 49;
            this.label1.Text = "卡类型";
            // 
            // CTxtICCardID
            // 
            this.CTxtICCardID.EnabledButton = false;
            this.CTxtICCardID.EnmTxtType = CustomControlLib.EnmTxtBoxType.ICCard;
            this.CTxtICCardID.Font = new System.Drawing.Font("宋体", 12F);
            this.CTxtICCardID.ImageButton = null;
            this.CTxtICCardID.Location = new System.Drawing.Point(127, 29);
            this.CTxtICCardID.Margin = new System.Windows.Forms.Padding(7);
            this.CTxtICCardID.Name = "CTxtICCardID";
            this.CTxtICCardID.Size = new System.Drawing.Size(204, 26);
            this.CTxtICCardID.TabIndex = 48;
            this.CTxtICCardID.TextChanged += new System.EventHandler(this.CTxtICCardID_TextChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(24, 190);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(101, 47);
            this.label4.TabIndex = 47;
            this.label4.Text = "姓名";
            // 
            // LblTariff
            // 
            this.LblTariff.Font = new System.Drawing.Font("宋体", 12F);
            this.LblTariff.Location = new System.Drawing.Point(0, 347);
            this.LblTariff.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.LblTariff.Name = "LblTariff";
            this.LblTariff.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblTariff.Size = new System.Drawing.Size(125, 47);
            this.LblTariff.TabIndex = 70;
            this.LblTariff.Text = "收费标准";
            // 
            // CTxtTariff
            // 
            this.CTxtTariff.EnabledButton = true;
            this.CTxtTariff.EnmTxtType = CustomControlLib.EnmTxtBoxType.Init;
            this.CTxtTariff.Font = new System.Drawing.Font("宋体", 12F);
            this.CTxtTariff.ImageButton = global::WindowsFormLib.Properties.Resources.CIMC中集空港设备;
            this.CTxtTariff.Location = new System.Drawing.Point(126, 347);
            this.CTxtTariff.Margin = new System.Windows.Forms.Padding(7);
            this.CTxtTariff.Name = "CTxtTariff";
            this.CTxtTariff.ReadOnly = true;
            this.CTxtTariff.Size = new System.Drawing.Size(165, 26);
            this.CTxtTariff.TabIndex = 71;
            this.CTxtTariff.CallbackTextButtonEvent += new System.EventHandler(this.CTxtTariff_DoubleClick);
            // 
            // LblFixChange
            // 
            this.LblFixChange.Location = new System.Drawing.Point(491, 347);
            this.LblFixChange.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LblFixChange.Name = "LblFixChange";
            this.LblFixChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblFixChange.Size = new System.Drawing.Size(48, 53);
            this.LblFixChange.TabIndex = 76;
            this.LblFixChange.Text = "找零";
            // 
            // TxtFixChange
            // 
            this.TxtFixChange.Enabled = false;
            this.TxtFixChange.Location = new System.Drawing.Point(539, 347);
            this.TxtFixChange.Margin = new System.Windows.Forms.Padding(5);
            this.TxtFixChange.Name = "TxtFixChange";
            this.TxtFixChange.Size = new System.Drawing.Size(100, 26);
            this.TxtFixChange.TabIndex = 75;
            // 
            // CTxtFixActualFee
            // 
            this.CTxtFixActualFee.EnabledButton = false;
            this.CTxtFixActualFee.EnmTxtType = CustomControlLib.EnmTxtBoxType.ICCard;
            this.CTxtFixActualFee.ImageButton = null;
            this.CTxtFixActualFee.Location = new System.Drawing.Point(376, 347);
            this.CTxtFixActualFee.Margin = new System.Windows.Forms.Padding(5);
            this.CTxtFixActualFee.Name = "CTxtFixActualFee";
            this.CTxtFixActualFee.Size = new System.Drawing.Size(120, 26);
            this.CTxtFixActualFee.TabIndex = 74;
            this.CTxtFixActualFee.TextChanged += new System.EventHandler(this.CTxtFixActualFee_TextChanged);
            // 
            // BtnFixPay
            // 
            this.BtnFixPay.Location = new System.Drawing.Point(64, 411);
            this.BtnFixPay.Margin = new System.Windows.Forms.Padding(5);
            this.BtnFixPay.Name = "BtnFixPay";
            this.BtnFixPay.Size = new System.Drawing.Size(100, 30);
            this.BtnFixPay.TabIndex = 73;
            this.BtnFixPay.Text = "确认缴费";
            this.BtnFixPay.UseVisualStyleBackColor = true;
            this.BtnFixPay.Click += new System.EventHandler(this.BtnFixPay_Click);
            // 
            // LblFixActualFee
            // 
            this.LblFixActualFee.Location = new System.Drawing.Point(303, 347);
            this.LblFixActualFee.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LblFixActualFee.Name = "LblFixActualFee";
            this.LblFixActualFee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblFixActualFee.Size = new System.Drawing.Size(73, 53);
            this.LblFixActualFee.TabIndex = 72;
            this.LblFixActualFee.Text = "实收金额";
            // 
            // CboWareHouse
            // 
            this.CboWareHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWareHouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouse.Font = new System.Drawing.Font("宋体", 12F);
            this.CboWareHouse.FormattingEnabled = true;
            this.CboWareHouse.Location = new System.Drawing.Point(435, 83);
            this.CboWareHouse.Margin = new System.Windows.Forms.Padding(5);
            this.CboWareHouse.Name = "CboWareHouse";
            this.CboWareHouse.Size = new System.Drawing.Size(204, 24);
            this.CboWareHouse.TabIndex = 77;
            // 
            // BtnModify
            // 
            this.BtnModify.Font = new System.Drawing.Font("宋体", 12F);
            this.BtnModify.Location = new System.Drawing.Point(214, 411);
            this.BtnModify.Margin = new System.Windows.Forms.Padding(5);
            this.BtnModify.Name = "BtnModify";
            this.BtnModify.Size = new System.Drawing.Size(100, 30);
            this.BtnModify.TabIndex = 78;
            this.BtnModify.Text = "修改";
            this.BtnModify.UseVisualStyleBackColor = true;
            this.BtnModify.Click += new System.EventHandler(this.BtnModify_Click);
            // 
            // CboPriorityID
            // 
            this.CboPriorityID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboPriorityID.Enabled = false;
            this.CboPriorityID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboPriorityID.Font = new System.Drawing.Font("宋体", 12F);
            this.CboPriorityID.FormattingEnabled = true;
            this.CboPriorityID.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.CboPriorityID.Location = new System.Drawing.Point(435, 130);
            this.CboPriorityID.Margin = new System.Windows.Forms.Padding(5);
            this.CboPriorityID.Name = "CboPriorityID";
            this.CboPriorityID.Size = new System.Drawing.Size(204, 24);
            this.CboPriorityID.TabIndex = 80;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("宋体", 12F);
            this.label10.Location = new System.Drawing.Point(333, 130);
            this.label10.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(102, 47);
            this.label10.TabIndex = 79;
            this.label10.Text = "优先级";
            // 
            // CFormCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 458);
            this.Controls.Add(this.CboPriorityID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.BtnModify);
            this.Controls.Add(this.CboWareHouse);
            this.Controls.Add(this.LblFixChange);
            this.Controls.Add(this.TxtFixChange);
            this.Controls.Add(this.CTxtFixActualFee);
            this.Controls.Add(this.BtnFixPay);
            this.Controls.Add(this.LblFixActualFee);
            this.Controls.Add(this.CTxtTariff);
            this.Controls.Add(this.LblTariff);
            this.Controls.Add(this.TxtAddr);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CboICCardStatus);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.CboICCardType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CTxtName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.CTxtCarNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CTxtCarLocAddr);
            this.Controls.Add(this.LblCarLocAddr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CTxtMobile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CTxtPhone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CTxtICCardID);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "CFormCustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车主信息";
            this.Load += new System.EventHandler(this.CFormCustomer_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtAddr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CboICCardStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox CboICCardType;
        private System.Windows.Forms.Label label8;
        private CUserTextButton CTxtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private CUserTextButton CTxtCarNumber;
        private System.Windows.Forms.Label label5;
        private CUserTextButton CTxtCarLocAddr;
        private System.Windows.Forms.Label LblCarLocAddr;
        private System.Windows.Forms.Label label2;
        private CUserTextButton CTxtMobile;
        private System.Windows.Forms.Label label3;
        private CUserTextButton CTxtPhone;
        private System.Windows.Forms.Label label1;
        private CUserTextButton CTxtICCardID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LblTariff;
        private CUserTextButton CTxtTariff;
        private System.Windows.Forms.Label LblFixChange;
        private System.Windows.Forms.TextBox TxtFixChange;
        private CUserTextButton CTxtFixActualFee;
        private System.Windows.Forms.Button BtnFixPay;
        private System.Windows.Forms.Label LblFixActualFee;
        private System.Windows.Forms.ComboBox CboWareHouse;
        private System.Windows.Forms.Button BtnModify;
        private System.Windows.Forms.ComboBox CboPriorityID;
        private System.Windows.Forms.Label label10;
    }
}