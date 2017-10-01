namespace WindowsFormLib
{
    partial class CFormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormMain));
            this.TlsManage = new System.Windows.Forms.ToolStrip();
            this.TsbSystemMtc = new System.Windows.Forms.ToolStripButton();
            this.TsbSystemConfig = new System.Windows.Forms.ToolStripButton();
            this.TsbUserManage = new System.Windows.Forms.ToolStripButton();
            this.TsbTollManage = new System.Windows.Forms.ToolStripButton();
            this.TsbOperatorManage = new System.Windows.Forms.ToolStripButton();
            this.TabQueryStatistics = new System.Windows.Forms.ToolStripButton();
            this.TsbTempFetch = new System.Windows.Forms.ToolStripButton();
            this.TsbHandOrder = new System.Windows.Forms.ToolStripButton();
            this.TsbClose = new System.Windows.Forms.ToolStripButton();
            this.TsbLogout = new System.Windows.Forms.ToolStripButton();
            this.TsbModifyPassWord = new System.Windows.Forms.ToolStripButton();
            this.TsbWindowMode = new System.Windows.Forms.ToolStripSplitButton();
            this.TsmtFull = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmtFix = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbDeviceFault = new System.Windows.Forms.ToolStripButton();
            this.TsbCIMCWorker = new System.Windows.Forms.ToolStripButton();
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
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslPLC = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslSplit = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslCurTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.carLocationStatus = new System.Windows.Forms.TabControl();
            this.GbColor = new System.Windows.Forms.GroupBox();
            this.LblTitle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TsbRotation = new System.Windows.Forms.ToolStripButton();
            this.TlsManage.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TlsManage
            // 
            this.TlsManage.BackColor = System.Drawing.SystemColors.Control;
            this.TlsManage.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.TlsManage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbSystemMtc,
            this.TsbSystemConfig,
            this.TsbUserManage,
            this.TsbTollManage,
            this.TsbOperatorManage,
            this.TabQueryStatistics,
            this.TsbTempFetch,
            this.TsbHandOrder,
            this.TsbClose,
            this.TsbLogout,
            this.TsbModifyPassWord,
            this.TsbWindowMode,
            this.TsbDeviceFault,
            this.TsbCIMCWorker,
            this.TsbRotation});
            this.TlsManage.Location = new System.Drawing.Point(0, 0);
            this.TlsManage.Name = "TlsManage";
            this.TlsManage.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.TlsManage.Size = new System.Drawing.Size(1008, 38);
            this.TlsManage.TabIndex = 8;
            this.TlsManage.Text = "toolStrip1";
            // 
            // TsbSystemMtc
            // 
            this.TsbSystemMtc.AutoSize = false;
            this.TsbSystemMtc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbSystemMtc.Image = ((System.Drawing.Image)(resources.GetObject("TsbSystemMtc.Image")));
            this.TsbSystemMtc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbSystemMtc.Name = "TsbSystemMtc";
            this.TsbSystemMtc.Size = new System.Drawing.Size(80, 35);
            this.TsbSystemMtc.Text = "系统维护";
            this.TsbSystemMtc.Click += new System.EventHandler(this.TsbSystemMtc_Click);
            // 
            // TsbSystemConfig
            // 
            this.TsbSystemConfig.AutoSize = false;
            this.TsbSystemConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbSystemConfig.Image = ((System.Drawing.Image)(resources.GetObject("TsbSystemConfig.Image")));
            this.TsbSystemConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbSystemConfig.Name = "TsbSystemConfig";
            this.TsbSystemConfig.Size = new System.Drawing.Size(80, 35);
            this.TsbSystemConfig.Text = "系统配置";
            this.TsbSystemConfig.Click += new System.EventHandler(this.TsbSystemConfig_Click);
            // 
            // TsbUserManage
            // 
            this.TsbUserManage.AutoSize = false;
            this.TsbUserManage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbUserManage.Image = ((System.Drawing.Image)(resources.GetObject("TsbUserManage.Image")));
            this.TsbUserManage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbUserManage.Name = "TsbUserManage";
            this.TsbUserManage.Size = new System.Drawing.Size(80, 35);
            this.TsbUserManage.Text = "用户管理";
            this.TsbUserManage.Click += new System.EventHandler(this.TsbUserManage_Click);
            // 
            // TsbTollManage
            // 
            this.TsbTollManage.AutoSize = false;
            this.TsbTollManage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbTollManage.Image = ((System.Drawing.Image)(resources.GetObject("TsbTollManage.Image")));
            this.TsbTollManage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbTollManage.Name = "TsbTollManage";
            this.TsbTollManage.Size = new System.Drawing.Size(80, 35);
            this.TsbTollManage.Text = "收费管理";
            this.TsbTollManage.Click += new System.EventHandler(this.TsbTollManage_Click);
            // 
            // TsbOperatorManage
            // 
            this.TsbOperatorManage.AutoSize = false;
            this.TsbOperatorManage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbOperatorManage.Image = ((System.Drawing.Image)(resources.GetObject("TsbOperatorManage.Image")));
            this.TsbOperatorManage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbOperatorManage.Name = "TsbOperatorManage";
            this.TsbOperatorManage.Size = new System.Drawing.Size(90, 35);
            this.TsbOperatorManage.Text = "操作员管理";
            this.TsbOperatorManage.Click += new System.EventHandler(this.TsbOperatorManage_Click);
            // 
            // TabQueryStatistics
            // 
            this.TabQueryStatistics.AutoSize = false;
            this.TabQueryStatistics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TabQueryStatistics.Image = ((System.Drawing.Image)(resources.GetObject("TabQueryStatistics.Image")));
            this.TabQueryStatistics.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TabQueryStatistics.Name = "TabQueryStatistics";
            this.TabQueryStatistics.Size = new System.Drawing.Size(80, 35);
            this.TabQueryStatistics.Text = "查询统计";
            this.TabQueryStatistics.Click += new System.EventHandler(this.TabQueryStatistics_Click);
            // 
            // TsbTempFetch
            // 
            this.TsbTempFetch.AutoSize = false;
            this.TsbTempFetch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbTempFetch.Image = ((System.Drawing.Image)(resources.GetObject("TsbTempFetch.Image")));
            this.TsbTempFetch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbTempFetch.Name = "TsbTempFetch";
            this.TsbTempFetch.Size = new System.Drawing.Size(80, 35);
            this.TsbTempFetch.Text = "临时取物";
            this.TsbTempFetch.Click += new System.EventHandler(this.TsbTempFetch_Click);
            // 
            // TsbHandOrder
            // 
            this.TsbHandOrder.AutoSize = false;
            this.TsbHandOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbHandOrder.Image = ((System.Drawing.Image)(resources.GetObject("TsbHandOrder.Image")));
            this.TsbHandOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbHandOrder.Name = "TsbHandOrder";
            this.TsbHandOrder.Size = new System.Drawing.Size(80, 35);
            this.TsbHandOrder.Text = "手动指令";
            this.TsbHandOrder.Click += new System.EventHandler(this.TsbHandOrder_Click);
            // 
            // TsbClose
            // 
            this.TsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbClose.Image = global::WindowsFormLib.Properties.Resources.close;
            this.TsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbClose.Name = "TsbClose";
            this.TsbClose.Size = new System.Drawing.Size(23, 35);
            this.TsbClose.Text = "关闭";
            this.TsbClose.Visible = false;
            this.TsbClose.Click += new System.EventHandler(this.TsbClose_Click);
            // 
            // TsbLogout
            // 
            this.TsbLogout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsbLogout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbLogout.Image = global::WindowsFormLib.Properties.Resources.changeAccount;
            this.TsbLogout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbLogout.Name = "TsbLogout";
            this.TsbLogout.Size = new System.Drawing.Size(23, 35);
            this.TsbLogout.Text = "切换用户";
            this.TsbLogout.Click += new System.EventHandler(this.TsbLogout_Click);
            // 
            // TsbModifyPassWord
            // 
            this.TsbModifyPassWord.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsbModifyPassWord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbModifyPassWord.Image = global::WindowsFormLib.Properties.Resources.modifyPassWord;
            this.TsbModifyPassWord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbModifyPassWord.Name = "TsbModifyPassWord";
            this.TsbModifyPassWord.Size = new System.Drawing.Size(23, 35);
            this.TsbModifyPassWord.Text = "修改密码";
            this.TsbModifyPassWord.Click += new System.EventHandler(this.TsbModifyPassWord_Click);
            // 
            // TsbWindowMode
            // 
            this.TsbWindowMode.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsbWindowMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbWindowMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmtFull,
            this.TsmtFix});
            this.TsbWindowMode.Image = global::WindowsFormLib.Properties.Resources.setting;
            this.TsbWindowMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbWindowMode.Name = "TsbWindowMode";
            this.TsbWindowMode.Size = new System.Drawing.Size(32, 35);
            this.TsbWindowMode.Text = "窗体模式";
            this.TsbWindowMode.Visible = false;
            // 
            // TsmtFull
            // 
            this.TsmtFull.Name = "TsmtFull";
            this.TsmtFull.Size = new System.Drawing.Size(144, 26);
            this.TsmtFull.Text = "全屏模式";
            this.TsmtFull.Click += new System.EventHandler(this.TsmtFull_Click);
            // 
            // TsmtFix
            // 
            this.TsmtFix.Name = "TsmtFix";
            this.TsmtFix.Size = new System.Drawing.Size(144, 26);
            this.TsmtFix.Text = "普通模式";
            this.TsmtFix.Click += new System.EventHandler(this.TsmtFix_Click);
            // 
            // TsbDeviceFault
            // 
            this.TsbDeviceFault.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbDeviceFault.Image = ((System.Drawing.Image)(resources.GetObject("TsbDeviceFault.Image")));
            this.TsbDeviceFault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbDeviceFault.Name = "TsbDeviceFault";
            this.TsbDeviceFault.Size = new System.Drawing.Size(78, 35);
            this.TsbDeviceFault.Text = "故障汇总";
            this.TsbDeviceFault.Click += new System.EventHandler(this.TsbDeviceFault_Click);
            // 
            // TsbCIMCWorker
            // 
            this.TsbCIMCWorker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbCIMCWorker.Image = ((System.Drawing.Image)(resources.GetObject("TsbCIMCWorker.Image")));
            this.TsbCIMCWorker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbCIMCWorker.Name = "TsbCIMCWorker";
            this.TsbCIMCWorker.Size = new System.Drawing.Size(78, 35);
            this.TsbCIMCWorker.Text = "维保管理";
            this.TsbCIMCWorker.Click += new System.EventHandler(this.TsbCIMCWorker_Click);
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
            this.toolStripStatusLabel1,
            this.TsslPLC,
            this.TsslSplit,
            this.TsslConnected,
            this.TsslCurTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 661);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1008, 25);
            this.statusStrip.TabIndex = 6;
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
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(136, 20);
            this.toolStripStatusLabel1.Text = "                                  故障设备有：";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel1.Visible = false;
            // 
            // TsslPLC
            // 
            this.TsslPLC.ForeColor = System.Drawing.Color.Red;
            this.TsslPLC.Name = "TsslPLC";
            this.TsslPLC.Size = new System.Drawing.Size(20, 20);
            this.TsslPLC.Text = "无";
            this.TsslPLC.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.TsslPLC.Visible = false;
            // 
            // TsslSplit
            // 
            this.TsslSplit.Name = "TsslSplit";
            this.TsslSplit.Size = new System.Drawing.Size(506, 20);
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
            // carLocationStatus
            // 
            this.carLocationStatus.Location = new System.Drawing.Point(0, 65);
            this.carLocationStatus.Name = "carLocationStatus";
            this.carLocationStatus.SelectedIndex = 0;
            this.carLocationStatus.Size = new System.Drawing.Size(927, 418);
            this.carLocationStatus.TabIndex = 0;
            // 
            // GbColor
            // 
            this.GbColor.Location = new System.Drawing.Point(21, 501);
            this.GbColor.Name = "GbColor";
            this.GbColor.Size = new System.Drawing.Size(200, 50);
            this.GbColor.TabIndex = 9;
            this.GbColor.TabStop = false;
            this.GbColor.Text = "车位颜色标签";
            // 
            // LblTitle
            // 
            this.LblTitle.Font = new System.Drawing.Font("黑体", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LblTitle.Location = new System.Drawing.Point(13, 47);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(200, 40);
            this.LblTitle.TabIndex = 10;
            this.LblTitle.Text = "标题";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblTitle.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TsbRotation
            // 
            this.TsbRotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbRotation.Image = ((System.Drawing.Image)(resources.GetObject("TsbRotation.Image")));
            this.TsbRotation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbRotation.Name = "TsbRotation";
            this.TsbRotation.Size = new System.Drawing.Size(78, 25);
            this.TsbRotation.Text = "车辆掉头";
            this.TsbRotation.Click += new System.EventHandler(this.TsbRotation_Click);
            // 
            // CFormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 686);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.GbColor);
            this.Controls.Add(this.TlsManage);
            this.Controls.Add(this.carLocationStatus);
            this.Controls.Add(this.statusStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CFormMain";
            this.Text = "中集天达智能车库管理系统";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.TlsManage.ResumeLayout(false);
            this.TlsManage.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TabControl carLocationStatus;
        private System.Windows.Forms.ToolStripButton TsbSystemMtc;
        private System.Windows.Forms.ToolStripButton TsbSystemConfig;
        private System.Windows.Forms.ToolStripButton TsbUserManage;
        private System.Windows.Forms.ToolStripButton TsbTollManage;
        private System.Windows.Forms.ToolStripButton TsbOperatorManage;
        private System.Windows.Forms.ToolStripButton TabQueryStatistics;
        private System.Windows.Forms.ToolStripButton TsbTempFetch;
        private System.Windows.Forms.ToolStripButton TsbHandOrder;
        private System.Windows.Forms.ToolStrip TlsManage;
        private System.Windows.Forms.ToolStripStatusLabel TsslOptLbl;
        private System.Windows.Forms.ToolStripStatusLabel TsslOptTxt;
        private System.Windows.Forms.ToolStripStatusLabel TsslSumLbl;
        private System.Windows.Forms.ToolStripStatusLabel TsslSumTxt;
        private System.Windows.Forms.ToolStripStatusLabel TsslOccupyLbl;
        private System.Windows.Forms.ToolStripStatusLabel TsslOccupyTxt;
        private System.Windows.Forms.ToolStripStatusLabel TsslSpaceLbl;
        private System.Windows.Forms.ToolStripStatusLabel TsslSpaceTxt;
        private System.Windows.Forms.ToolStripButton TsbLogout;
        private System.Windows.Forms.ToolStripButton TsbClose;
        private System.Windows.Forms.ToolStripSplitButton TsbWindowMode;
        private System.Windows.Forms.ToolStripMenuItem TsmtFull;
        private System.Windows.Forms.ToolStripMenuItem TsmtFix;
        private System.Windows.Forms.ToolStripButton TsbModifyPassWord;
        private System.Windows.Forms.ToolStripStatusLabel TsslPLC;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton TsbDeviceFault;
        private System.Windows.Forms.GroupBox GbColor;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.ToolStripStatusLabel TsslCurTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton TsbCIMCWorker;
        private System.Windows.Forms.ToolStripStatusLabel TsslSpaceMaxLbl;
        private System.Windows.Forms.ToolStripStatusLabel TsslSpaceMaxTxt;
        private System.Windows.Forms.ToolStripStatusLabel TsslConnected;
        private System.Windows.Forms.ToolStripStatusLabel TsslSplit;
        private System.Windows.Forms.ToolStripButton TsbRotation;
    }
}

