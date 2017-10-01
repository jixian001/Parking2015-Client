namespace WindowsFormLib
{
    partial class CFormCIMCWorker
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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.CbSelectAllSound = new System.Windows.Forms.CheckBox();
            this.BtnSaveSound = new System.Windows.Forms.Button();
            this.DgvSound = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CupttsSound = new CustomControlLib.CUserPageTurnToolStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CupttsLed = new CustomControlLib.CUserPageTurnToolStrip();
            this.BtnSaveLed = new System.Windows.Forms.Button();
            this.DgvLed = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.GbLED = new System.Windows.Forms.GroupBox();
            this.BtnLEDOk = new System.Windows.Forms.Button();
            this.TxtLEDNew = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtLEDOld = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnFindQueue = new System.Windows.Forms.Button();
            this.CboDeviceCode = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.CboWareHouseTask = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.GbQueue = new System.Windows.Forms.GroupBox();
            this.DgvQueue = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.GbICCard = new System.Windows.Forms.GroupBox();
            this.TxtFeeEndTime = new System.Windows.Forms.TextBox();
            this.LblFeeEndTime = new System.Windows.Forms.Label();
            this.TxtFeeStartTime = new System.Windows.Forms.TextBox();
            this.LblFeeStartTime = new System.Windows.Forms.Label();
            this.BtnReadICCardData = new System.Windows.Forms.Button();
            this.TxtFeeType = new System.Windows.Forms.TextBox();
            this.LblFeeType = new System.Windows.Forms.Label();
            this.TxtICCardID = new System.Windows.Forms.TextBox();
            this.LblICCardID = new System.Windows.Forms.Label();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSound)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLed)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.GbLED.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.GbQueue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvQueue)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.GbICCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.CbSelectAllSound);
            this.tabPage3.Controls.Add(this.BtnSaveSound);
            this.tabPage3.Controls.Add(this.DgvSound);
            this.tabPage3.Controls.Add(this.CupttsSound);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage3.Size = new System.Drawing.Size(1042, 470);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "语音配置";
            // 
            // CbSelectAllSound
            // 
            this.CbSelectAllSound.AutoSize = true;
            this.CbSelectAllSound.Location = new System.Drawing.Point(900, 17);
            this.CbSelectAllSound.Margin = new System.Windows.Forms.Padding(4);
            this.CbSelectAllSound.Name = "CbSelectAllSound";
            this.CbSelectAllSound.Size = new System.Drawing.Size(59, 20);
            this.CbSelectAllSound.TabIndex = 10;
            this.CbSelectAllSound.Text = "全选";
            this.CbSelectAllSound.UseVisualStyleBackColor = true;
            this.CbSelectAllSound.CheckedChanged += new System.EventHandler(this.CbSelectAllSound_CheckedChanged);
            // 
            // BtnSaveSound
            // 
            this.BtnSaveSound.Location = new System.Drawing.Point(43, 9);
            this.BtnSaveSound.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSaveSound.Name = "BtnSaveSound";
            this.BtnSaveSound.Size = new System.Drawing.Size(100, 35);
            this.BtnSaveSound.TabIndex = 9;
            this.BtnSaveSound.Text = "保存";
            this.BtnSaveSound.UseVisualStyleBackColor = true;
            this.BtnSaveSound.Click += new System.EventHandler(this.BtnSaveSound_Click);
            // 
            // DgvSound
            // 
            this.DgvSound.AllowUserToAddRows = false;
            this.DgvSound.AllowUserToResizeRows = false;
            this.DgvSound.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvSound.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2,
            this.Column4});
            this.DgvSound.Location = new System.Drawing.Point(2, 53);
            this.DgvSound.Margin = new System.Windows.Forms.Padding(5);
            this.DgvSound.Name = "DgvSound";
            this.DgvSound.RowHeadersVisible = false;
            this.DgvSound.RowTemplate.Height = 23;
            this.DgvSound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvSound.Size = new System.Drawing.Size(1040, 380);
            this.DgvSound.TabIndex = 1;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "soundcode";
            this.Column3.FillWeight = 67.92112F;
            this.Column3.HeaderText = "自动语音文件";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "soundname";
            this.Column1.FillWeight = 86.64259F;
            this.Column1.HeaderText = "项目名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "soundcontent";
            this.Column2.FillWeight = 199.3543F;
            this.Column2.HeaderText = "人工输入语音提示内容";
            this.Column2.Name = "Column2";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "soundishand";
            this.Column4.FillWeight = 46.08193F;
            this.Column4.HeaderText = "人工否";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CupttsSound
            // 
            this.CupttsSound.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CupttsSound.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CupttsSound.ImageEndBtn = global::WindowsFormLib.Properties.Resources.endPage;
            this.CupttsSound.ImageLeftBtn = global::WindowsFormLib.Properties.Resources.leftPage;
            this.CupttsSound.ImageRightBtn = global::WindowsFormLib.Properties.Resources.rightPage;
            this.CupttsSound.ImageStartBtn = global::WindowsFormLib.Properties.Resources.startPage;
            this.CupttsSound.Location = new System.Drawing.Point(5, 432);
            this.CupttsSound.Name = "CupttsSound";
            this.CupttsSound.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.CupttsSound.Size = new System.Drawing.Size(1032, 33);
            this.CupttsSound.TabIndex = 8;
            this.CupttsSound.Text = "cUserPageTurnToolStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1050, 500);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1042, 470);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "车厅运行流程架构";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.CupttsLed);
            this.tabPage2.Controls.Add(this.BtnSaveLed);
            this.tabPage2.Controls.Add(this.DgvLed);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1042, 470);
            this.tabPage2.TabIndex = 5;
            this.tabPage2.Text = "LED配置";
            // 
            // CupttsLed
            // 
            this.CupttsLed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CupttsLed.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CupttsLed.ImageEndBtn = global::WindowsFormLib.Properties.Resources.endPage;
            this.CupttsLed.ImageLeftBtn = global::WindowsFormLib.Properties.Resources.leftPage;
            this.CupttsLed.ImageRightBtn = global::WindowsFormLib.Properties.Resources.rightPage;
            this.CupttsLed.ImageStartBtn = global::WindowsFormLib.Properties.Resources.startPage;
            this.CupttsLed.Location = new System.Drawing.Point(3, 434);
            this.CupttsLed.Name = "CupttsLed";
            this.CupttsLed.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.CupttsLed.Size = new System.Drawing.Size(1036, 33);
            this.CupttsLed.TabIndex = 8;
            this.CupttsLed.Text = "cUserPageTurnToolStrip1";
            // 
            // BtnSaveLed
            // 
            this.BtnSaveLed.Location = new System.Drawing.Point(43, 9);
            this.BtnSaveLed.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSaveLed.Name = "BtnSaveLed";
            this.BtnSaveLed.Size = new System.Drawing.Size(100, 35);
            this.BtnSaveLed.TabIndex = 12;
            this.BtnSaveLed.Text = "保存";
            this.BtnSaveLed.UseVisualStyleBackColor = true;
            this.BtnSaveLed.Click += new System.EventHandler(this.BtnSaveLed_Click);
            // 
            // DgvLed
            // 
            this.DgvLed.AllowUserToAddRows = false;
            this.DgvLed.AllowUserToResizeRows = false;
            this.DgvLed.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvLed.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.DgvLed.Location = new System.Drawing.Point(2, 53);
            this.DgvLed.Margin = new System.Windows.Forms.Padding(5);
            this.DgvLed.Name = "DgvLed";
            this.DgvLed.RowHeadersVisible = false;
            this.DgvLed.RowTemplate.Height = 23;
            this.DgvLed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvLed.Size = new System.Drawing.Size(1040, 380);
            this.DgvLed.TabIndex = 11;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.FillWeight = 10F;
            this.dataGridViewTextBoxColumn1.HeaderText = "序号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ledcontent";
            this.dataGridViewTextBoxColumn2.HeaderText = "LED内容";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.GbLED);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(1042, 470);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "LED内容修改";
            // 
            // GbLED
            // 
            this.GbLED.Controls.Add(this.BtnLEDOk);
            this.GbLED.Controls.Add(this.TxtLEDNew);
            this.GbLED.Controls.Add(this.label7);
            this.GbLED.Controls.Add(this.TxtLEDOld);
            this.GbLED.Controls.Add(this.label6);
            this.GbLED.Location = new System.Drawing.Point(274, 104);
            this.GbLED.Name = "GbLED";
            this.GbLED.Size = new System.Drawing.Size(390, 250);
            this.GbLED.TabIndex = 5;
            this.GbLED.TabStop = false;
            // 
            // BtnLEDOk
            // 
            this.BtnLEDOk.Location = new System.Drawing.Point(139, 195);
            this.BtnLEDOk.Margin = new System.Windows.Forms.Padding(4);
            this.BtnLEDOk.Name = "BtnLEDOk";
            this.BtnLEDOk.Size = new System.Drawing.Size(100, 35);
            this.BtnLEDOk.TabIndex = 9;
            this.BtnLEDOk.Text = "修改";
            this.BtnLEDOk.UseVisualStyleBackColor = true;
            this.BtnLEDOk.Click += new System.EventHandler(this.BtnLEDOk_Click);
            // 
            // TxtLEDNew
            // 
            this.TxtLEDNew.Location = new System.Drawing.Point(122, 125);
            this.TxtLEDNew.Margin = new System.Windows.Forms.Padding(4);
            this.TxtLEDNew.Name = "TxtLEDNew";
            this.TxtLEDNew.Size = new System.Drawing.Size(240, 26);
            this.TxtLEDNew.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(2, 125);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 27);
            this.label7.TabIndex = 7;
            this.label7.Text = "修改后内容";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtLEDOld
            // 
            this.TxtLEDOld.Location = new System.Drawing.Point(122, 41);
            this.TxtLEDOld.Margin = new System.Windows.Forms.Padding(4);
            this.TxtLEDOld.Name = "TxtLEDOld";
            this.TxtLEDOld.Size = new System.Drawing.Size(240, 26);
            this.TxtLEDOld.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(2, 41);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 27);
            this.label6.TabIndex = 5;
            this.label6.Text = "关键字";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage6.Controls.Add(this.BtnDelete);
            this.tabPage6.Controls.Add(this.BtnFindQueue);
            this.tabPage6.Controls.Add(this.CboDeviceCode);
            this.tabPage6.Controls.Add(this.label22);
            this.tabPage6.Controls.Add(this.CboWareHouseTask);
            this.tabPage6.Controls.Add(this.label21);
            this.tabPage6.Controls.Add(this.GbQueue);
            this.tabPage6.Location = new System.Drawing.Point(4, 26);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage6.Size = new System.Drawing.Size(1042, 470);
            this.tabPage6.TabIndex = 7;
            this.tabPage6.Text = "队列报文";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(585, 20);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(80, 31);
            this.BtnDelete.TabIndex = 35;
            this.BtnDelete.Text = "删除作业";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnFindQueue
            // 
            this.BtnFindQueue.Location = new System.Drawing.Point(480, 20);
            this.BtnFindQueue.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFindQueue.Name = "BtnFindQueue";
            this.BtnFindQueue.Size = new System.Drawing.Size(80, 31);
            this.BtnFindQueue.TabIndex = 34;
            this.BtnFindQueue.Text = "查询";
            this.BtnFindQueue.UseVisualStyleBackColor = true;
            this.BtnFindQueue.Click += new System.EventHandler(this.BtnFindQueue_Click);
            // 
            // CboDeviceCode
            // 
            this.CboDeviceCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboDeviceCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboDeviceCode.FormattingEnabled = true;
            this.CboDeviceCode.Location = new System.Drawing.Point(260, 20);
            this.CboDeviceCode.Name = "CboDeviceCode";
            this.CboDeviceCode.Size = new System.Drawing.Size(100, 24);
            this.CboDeviceCode.TabIndex = 33;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(180, 20);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label22.Size = new System.Drawing.Size(80, 21);
            this.label22.TabIndex = 32;
            this.label22.Text = "设备";
            // 
            // CboWareHouseTask
            // 
            this.CboWareHouseTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWareHouseTask.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWareHouseTask.FormattingEnabled = true;
            this.CboWareHouseTask.Location = new System.Drawing.Point(80, 20);
            this.CboWareHouseTask.Name = "CboWareHouseTask";
            this.CboWareHouseTask.Size = new System.Drawing.Size(100, 24);
            this.CboWareHouseTask.TabIndex = 31;
            this.CboWareHouseTask.SelectedIndexChanged += new System.EventHandler(this.CboWareHouseTask_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(3, 20);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label21.Size = new System.Drawing.Size(77, 21);
            this.label21.TabIndex = 30;
            this.label21.Text = "库区";
            // 
            // GbQueue
            // 
            this.GbQueue.Controls.Add(this.DgvQueue);
            this.GbQueue.Location = new System.Drawing.Point(0, 60);
            this.GbQueue.Name = "GbQueue";
            this.GbQueue.Size = new System.Drawing.Size(706, 348);
            this.GbQueue.TabIndex = 2;
            this.GbQueue.TabStop = false;
            this.GbQueue.Text = "报文列表";
            // 
            // DgvQueue
            // 
            this.DgvQueue.AllowUserToAddRows = false;
            this.DgvQueue.AllowUserToResizeRows = false;
            this.DgvQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.Column5});
            this.DgvQueue.Location = new System.Drawing.Point(2, 25);
            this.DgvQueue.Name = "DgvQueue";
            this.DgvQueue.RowHeadersVisible = false;
            this.DgvQueue.RowTemplate.Height = 23;
            this.DgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvQueue.Size = new System.Drawing.Size(698, 318);
            this.DgvQueue.TabIndex = 0;
            this.DgvQueue.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvQueue_CellFormatting);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "devicecode";
            this.dataGridViewTextBoxColumn3.HeaderText = "设备";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "warehouse";
            this.dataGridViewTextBoxColumn4.HeaderText = "库区";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "iccode";
            this.dataGridViewTextBoxColumn5.HeaderText = "卡号";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "swipecount";
            this.dataGridViewTextBoxColumn6.HeaderText = "作业类型";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "waitsendtelegram";
            this.Column5.HeaderText = "队列报文";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.GbICCard);
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage5.Size = new System.Drawing.Size(1042, 470);
            this.tabPage5.TabIndex = 8;
            this.tabPage5.Text = "读取IC卡内存";
            // 
            // GbICCard
            // 
            this.GbICCard.Controls.Add(this.TxtFeeEndTime);
            this.GbICCard.Controls.Add(this.LblFeeEndTime);
            this.GbICCard.Controls.Add(this.TxtFeeStartTime);
            this.GbICCard.Controls.Add(this.LblFeeStartTime);
            this.GbICCard.Controls.Add(this.BtnReadICCardData);
            this.GbICCard.Controls.Add(this.TxtFeeType);
            this.GbICCard.Controls.Add(this.LblFeeType);
            this.GbICCard.Controls.Add(this.TxtICCardID);
            this.GbICCard.Controls.Add(this.LblICCardID);
            this.GbICCard.Location = new System.Drawing.Point(274, 61);
            this.GbICCard.Name = "GbICCard";
            this.GbICCard.Size = new System.Drawing.Size(390, 292);
            this.GbICCard.TabIndex = 5;
            this.GbICCard.TabStop = false;
            // 
            // TxtFeeEndTime
            // 
            this.TxtFeeEndTime.Enabled = false;
            this.TxtFeeEndTime.Location = new System.Drawing.Point(137, 245);
            this.TxtFeeEndTime.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFeeEndTime.Name = "TxtFeeEndTime";
            this.TxtFeeEndTime.Size = new System.Drawing.Size(240, 26);
            this.TxtFeeEndTime.TabIndex = 13;
            // 
            // LblFeeEndTime
            // 
            this.LblFeeEndTime.Location = new System.Drawing.Point(17, 245);
            this.LblFeeEndTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblFeeEndTime.Name = "LblFeeEndTime";
            this.LblFeeEndTime.Size = new System.Drawing.Size(120, 27);
            this.LblFeeEndTime.TabIndex = 12;
            this.LblFeeEndTime.Text = "计费结束时间";
            this.LblFeeEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtFeeStartTime
            // 
            this.TxtFeeStartTime.Enabled = false;
            this.TxtFeeStartTime.Location = new System.Drawing.Point(137, 195);
            this.TxtFeeStartTime.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFeeStartTime.Name = "TxtFeeStartTime";
            this.TxtFeeStartTime.Size = new System.Drawing.Size(240, 26);
            this.TxtFeeStartTime.TabIndex = 11;
            // 
            // LblFeeStartTime
            // 
            this.LblFeeStartTime.Location = new System.Drawing.Point(17, 195);
            this.LblFeeStartTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblFeeStartTime.Name = "LblFeeStartTime";
            this.LblFeeStartTime.Size = new System.Drawing.Size(120, 27);
            this.LblFeeStartTime.TabIndex = 10;
            this.LblFeeStartTime.Tag = "";
            this.LblFeeStartTime.Text = "计费开始时间";
            this.LblFeeStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnReadICCardData
            // 
            this.BtnReadICCardData.Location = new System.Drawing.Point(152, 26);
            this.BtnReadICCardData.Margin = new System.Windows.Forms.Padding(4);
            this.BtnReadICCardData.Name = "BtnReadICCardData";
            this.BtnReadICCardData.Size = new System.Drawing.Size(100, 35);
            this.BtnReadICCardData.TabIndex = 9;
            this.BtnReadICCardData.Text = "读取";
            this.BtnReadICCardData.UseVisualStyleBackColor = true;
            this.BtnReadICCardData.Click += new System.EventHandler(this.BtnReadICCardData_Click);
            // 
            // TxtFeeType
            // 
            this.TxtFeeType.Enabled = false;
            this.TxtFeeType.Location = new System.Drawing.Point(137, 145);
            this.TxtFeeType.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFeeType.Name = "TxtFeeType";
            this.TxtFeeType.Size = new System.Drawing.Size(240, 26);
            this.TxtFeeType.TabIndex = 8;
            // 
            // LblFeeType
            // 
            this.LblFeeType.Location = new System.Drawing.Point(17, 145);
            this.LblFeeType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblFeeType.Name = "LblFeeType";
            this.LblFeeType.Size = new System.Drawing.Size(120, 27);
            this.LblFeeType.TabIndex = 7;
            this.LblFeeType.Text = "计费类型";
            this.LblFeeType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtICCardID
            // 
            this.TxtICCardID.Enabled = false;
            this.TxtICCardID.Location = new System.Drawing.Point(132, 95);
            this.TxtICCardID.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardID.Name = "TxtICCardID";
            this.TxtICCardID.Size = new System.Drawing.Size(240, 26);
            this.TxtICCardID.TabIndex = 6;
            // 
            // LblICCardID
            // 
            this.LblICCardID.Location = new System.Drawing.Point(12, 95);
            this.LblICCardID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblICCardID.Name = "LblICCardID";
            this.LblICCardID.Size = new System.Drawing.Size(120, 27);
            this.LblICCardID.TabIndex = 5;
            this.LblICCardID.Tag = "";
            this.LblICCardID.Text = "IC卡号";
            this.LblICCardID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CFormCIMCWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1054, 502);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CFormCIMCWorker";
            this.Text = "维保管理";
            this.Load += new System.EventHandler(this.CFormCIMCWorker_Load);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSound)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLed)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.GbLED.ResumeLayout(false);
            this.GbLED.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.GbQueue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvQueue)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.GbICCard.ResumeLayout(false);
            this.GbICCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox CbSelectAllSound;
        private System.Windows.Forms.Button BtnSaveSound;
        private CustomControlLib.CUserPageTurnToolStrip CupttsSound;
        private System.Windows.Forms.DataGridView DgvSound;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox GbLED;
        private System.Windows.Forms.Button BtnLEDOk;
        private System.Windows.Forms.TextBox TxtLEDNew;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtLEDOld;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button BtnSaveLed;
        private System.Windows.Forms.DataGridView DgvLed;
        private CustomControlLib.CUserPageTurnToolStrip CupttsLed;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button BtnFindQueue;
        private System.Windows.Forms.ComboBox CboDeviceCode;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox CboWareHouseTask;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox GbQueue;
        private System.Windows.Forms.DataGridView DgvQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox GbICCard;
        private System.Windows.Forms.Button BtnReadICCardData;
        private System.Windows.Forms.TextBox TxtFeeType;
        private System.Windows.Forms.Label LblFeeType;
        private System.Windows.Forms.TextBox TxtICCardID;
        private System.Windows.Forms.Label LblICCardID;
        private System.Windows.Forms.TextBox TxtFeeEndTime;
        private System.Windows.Forms.Label LblFeeEndTime;
        private System.Windows.Forms.TextBox TxtFeeStartTime;
        private System.Windows.Forms.Label LblFeeStartTime;
    }
}