using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using BaseMethodLib;
using System.ServiceModel;
using CarLocationPanelLib;

namespace CustomControlLib
{
    public class CUserTariffTempPanel: Panel
    {
        public CUserTariffTempPanel()
        {
            InitializeComponent();
            EventHandlerGroup();
            ChkWorkday_CheckedChanged(null, null);
            ChkWorkPeak_CheckedChanged(null, null);
            ChkNonworkPeak_CheckedChanged(null, null);
            this.CboWorkPeakFUnit.Text = "小时";
            this.CboWorkNonpeakFUnit.Text = "小时";
            this.CboNonworkPeakFUnit.Text = "小时";
            this.CboNonworkNonpeakFUnit.Text = "小时";
            this.CboWorkPeakInFee.SelectedIndex = 0;
            this.CboWorkPeakOutFee.SelectedIndex = 0;
            this.CboWorkNonpeakInFee.SelectedIndex = 0;
            this.CboWorkNonpeakOutFee.SelectedIndex = 0;
            this.CboNonworkPeakInFee.SelectedIndex = 0;
            this.CboNonworkPeakOutFee.SelectedIndex = 0;
            this.CboNonworkNonpeakInFee.SelectedIndex = 0;
            this.CboNonworkNonpeakOutFee.SelectedIndex = 0;
        }

        #region 界面布局
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
            this.GbxTemp = new System.Windows.Forms.GroupBox();
            this.CboNonworkNonpeakOutFee = new System.Windows.Forms.ComboBox();
            this.GbxNonworkPeak = new System.Windows.Forms.GroupBox();
            this.CboNonworkPeakOutFee = new System.Windows.Forms.ComboBox();
            this.TxtNonworkPeakOutFee = new System.Windows.Forms.TextBox();
            this.LblNonworkPeakOutFee = new System.Windows.Forms.Label();
            this.LblNonworkPeakLine = new System.Windows.Forms.Label();
            this.DtpNonworkPeakEnd = new System.Windows.Forms.DateTimePicker();
            this.DtpNonworkPeakStart = new System.Windows.Forms.DateTimePicker();
            this.CboNonworkPeakInFee = new System.Windows.Forms.ComboBox();
            this.TxtNonworkPeakInFee = new System.Windows.Forms.TextBox();
            this.LblNonworkPeakInFee = new System.Windows.Forms.Label();
            this.CboNonworkPeakFUnit = new System.Windows.Forms.ComboBox();
            this.ChkNonworkPeakFUnit = new System.Windows.Forms.CheckBox();
            this.LblNonworkNonpeakOutFee = new System.Windows.Forms.Label();
            this.LblNonworkUnit = new System.Windows.Forms.Label();
            this.TxtNonworkNonpeakOutFee = new System.Windows.Forms.TextBox();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.LblNonworkQuota = new System.Windows.Forms.Label();
            this.LblWorkNonpeakInFee = new System.Windows.Forms.Label();
            this.CboWorkNonpeakFUnit = new System.Windows.Forms.ComboBox();
            this.ChkNonworkPeak = new System.Windows.Forms.CheckBox();
            this.TxtNonworkNonpeakInFee = new System.Windows.Forms.TextBox();
            this.ChkWorkNonpeakFUnit = new System.Windows.Forms.CheckBox();
            this.LblWorkUnit = new System.Windows.Forms.Label();
            this.ChkWorkPeak = new System.Windows.Forms.CheckBox();
            this.LblWorkQuota = new System.Windows.Forms.Label();
            this.LblNonworkNonpeakInFee = new System.Windows.Forms.Label();
            this.GbxNonworkdays = new System.Windows.Forms.GroupBox();
            this.GbxNonworkNonpeak = new System.Windows.Forms.GroupBox();
            this.LblNonworkNonpeakLine = new System.Windows.Forms.Label();
            this.DtpNonworkNonpeakEnd = new System.Windows.Forms.DateTimePicker();
            this.DtpNonworkNonpeakStart = new System.Windows.Forms.DateTimePicker();
            this.CboNonworkNonpeakInFee = new System.Windows.Forms.ComboBox();
            this.CboNonworkNonpeakFUnit = new System.Windows.Forms.ComboBox();
            this.ChkNonworkNonpeakFUnit = new System.Windows.Forms.CheckBox();
            this.TxtNonworkQuotaFee = new System.Windows.Forms.TextBox();
            this.GbxWorkday = new System.Windows.Forms.GroupBox();
            this.GbxWorkNonpeak = new System.Windows.Forms.GroupBox();
            this.CboWorkNonpeakOutFee = new System.Windows.Forms.ComboBox();
            this.TxtWorkNonpeakOutFee = new System.Windows.Forms.TextBox();
            this.LblWorkNonpeakOutFee = new System.Windows.Forms.Label();
            this.LblWorkNonpeakLine = new System.Windows.Forms.Label();
            this.DtpWorkNonpeakEnd = new System.Windows.Forms.DateTimePicker();
            this.DtpWorkNonpeakStart = new System.Windows.Forms.DateTimePicker();
            this.CboWorkNonpeakInFee = new System.Windows.Forms.ComboBox();
            this.TxtWorkNonpeakInFee = new System.Windows.Forms.TextBox();
            this.GbxWorkPeak = new System.Windows.Forms.GroupBox();
            this.CboWorkPeakOutFee = new System.Windows.Forms.ComboBox();
            this.TxtWorkPeakOutFee = new System.Windows.Forms.TextBox();
            this.LblWorkPeakOutFee = new System.Windows.Forms.Label();
            this.LblWorkPeakLine = new System.Windows.Forms.Label();
            this.DtpWorkPeakEnd = new System.Windows.Forms.DateTimePicker();
            this.DtpWorkPeakStart = new System.Windows.Forms.DateTimePicker();
            this.CboWorkPeakInFee = new System.Windows.Forms.ComboBox();
            this.TxtWorkPeakInFee = new System.Windows.Forms.TextBox();
            this.LblWorkPeakInFee = new System.Windows.Forms.Label();
            this.CboWorkPeakFUnit = new System.Windows.Forms.ComboBox();
            this.ChkWorkPeakFUnit = new System.Windows.Forms.CheckBox();
            this.TxtWorkQuotaFee = new System.Windows.Forms.TextBox();
            this.ChkWorkday = new System.Windows.Forms.CheckBox();
            this.GbxTemp.SuspendLayout();
            this.GbxNonworkPeak.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.GbxNonworkdays.SuspendLayout();
            this.GbxNonworkNonpeak.SuspendLayout();
            this.GbxWorkday.SuspendLayout();
            this.GbxWorkNonpeak.SuspendLayout();
            this.GbxWorkPeak.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.GbxTemp.Controls.Add(this.GbxNonworkdays);
            this.GbxTemp.Controls.Add(this.GbxWorkday);
            this.GbxTemp.Controls.Add(this.ChkWorkday);
            this.GbxTemp.Location = new System.Drawing.Point(0, 0);
            this.GbxTemp.Name = "groupBox1";
            this.GbxTemp.Size = new System.Drawing.Size(828, 405);
            this.GbxTemp.TabIndex = 0;
            this.GbxTemp.TabStop = false;
            // 
            // CboNonworkNonpeakOutFee
            // 
            this.CboNonworkNonpeakOutFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboNonworkNonpeakOutFee.FormattingEnabled = true;
            this.CboNonworkNonpeakOutFee.Items.AddRange(new object[] {
            "元/15分钟",
            "元/30分钟",
            "元/小时",
            "元/2小时"});
            this.CboNonworkNonpeakOutFee.Location = new System.Drawing.Point(212, 105);
            this.CboNonworkNonpeakOutFee.Margin = new System.Windows.Forms.Padding(4);
            this.CboNonworkNonpeakOutFee.Name = "CboNonworkNonpeakOutFee";
            this.CboNonworkNonpeakOutFee.Size = new System.Drawing.Size(105, 24);
            this.CboNonworkNonpeakOutFee.TabIndex = 11;
            // 
            // GbxNonworkPeak
            // 
            this.GbxNonworkPeak.Controls.Add(this.CboNonworkPeakOutFee);
            this.GbxNonworkPeak.Controls.Add(this.TxtNonworkPeakOutFee);
            this.GbxNonworkPeak.Controls.Add(this.LblNonworkPeakOutFee);
            this.GbxNonworkPeak.Controls.Add(this.LblNonworkPeakLine);
            this.GbxNonworkPeak.Controls.Add(this.DtpNonworkPeakEnd);
            this.GbxNonworkPeak.Controls.Add(this.DtpNonworkPeakStart);
            this.GbxNonworkPeak.Controls.Add(this.CboNonworkPeakInFee);
            this.GbxNonworkPeak.Controls.Add(this.TxtNonworkPeakInFee);
            this.GbxNonworkPeak.Controls.Add(this.LblNonworkPeakInFee);
            this.GbxNonworkPeak.Controls.Add(this.CboNonworkPeakFUnit);
            this.GbxNonworkPeak.Controls.Add(this.ChkNonworkPeakFUnit);
            this.GbxNonworkPeak.Location = new System.Drawing.Point(5, 35);
            this.GbxNonworkPeak.Margin = new System.Windows.Forms.Padding(4);
            this.GbxNonworkPeak.Name = "GbxNonworkPeak";
            this.GbxNonworkPeak.Padding = new System.Windows.Forms.Padding(4);
            this.GbxNonworkPeak.Size = new System.Drawing.Size(398, 140);
            this.GbxNonworkPeak.TabIndex = 1;
            this.GbxNonworkPeak.TabStop = false;
            this.GbxNonworkPeak.Text = "高峰时段";
            // 
            // CboNonworkPeakOutFee
            // 
            this.CboNonworkPeakOutFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboNonworkPeakOutFee.FormattingEnabled = true;
            this.CboNonworkPeakOutFee.Items.AddRange(new object[] {
            "元/15分钟",
            "元/30分钟",
            "元/小时",
            "元/2小时"});
            this.CboNonworkPeakOutFee.Location = new System.Drawing.Point(212, 105);
            this.CboNonworkPeakOutFee.Margin = new System.Windows.Forms.Padding(4);
            this.CboNonworkPeakOutFee.Name = "CboNonworkPeakOutFee";
            this.CboNonworkPeakOutFee.Size = new System.Drawing.Size(105, 24);
            this.CboNonworkPeakOutFee.TabIndex = 11;
            // 
            // TxtNonworkPeakOutFee
            // 
            this.TxtNonworkPeakOutFee.Location = new System.Drawing.Point(132, 105);
            this.TxtNonworkPeakOutFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNonworkPeakOutFee.Name = "TxtNonworkPeakOutFee";
            this.TxtNonworkPeakOutFee.Size = new System.Drawing.Size(80, 26);
            this.TxtNonworkPeakOutFee.TabIndex = 9;
            // 
            // LblNonworkPeakOutFee
            // 
            this.LblNonworkPeakOutFee.Location = new System.Drawing.Point(3, 105);
            this.LblNonworkPeakOutFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNonworkPeakOutFee.Name = "LblNonworkPeakOutFee";
            this.LblNonworkPeakOutFee.Size = new System.Drawing.Size(131, 27);
            this.LblNonworkPeakOutFee.TabIndex = 8;
            this.LblNonworkPeakOutFee.Text = "单价";
            this.LblNonworkPeakOutFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblNonworkPeakLine
            // 
            this.LblNonworkPeakLine.Location = new System.Drawing.Point(193, 15);
            this.LblNonworkPeakLine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNonworkPeakLine.Name = "LblNonworkPeakLine";
            this.LblNonworkPeakLine.Size = new System.Drawing.Size(11, 27);
            this.LblNonworkPeakLine.TabIndex = 14;
            this.LblNonworkPeakLine.Text = "-";
            this.LblNonworkPeakLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DtpNonworkPeakEnd
            // 
            this.DtpNonworkPeakEnd.Cursor = System.Windows.Forms.Cursors.Default;
            this.DtpNonworkPeakEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtpNonworkPeakEnd.Location = new System.Drawing.Point(203, 15);
            this.DtpNonworkPeakEnd.Name = "DtpNonworkPeakEnd";
            this.DtpNonworkPeakEnd.ShowUpDown = true;
            this.DtpNonworkPeakEnd.Size = new System.Drawing.Size(85, 26);
            this.DtpNonworkPeakEnd.TabIndex = 13;
            this.DtpNonworkPeakEnd.Value = new System.DateTime(2015, 6, 9, 21, 0, 0, 0);
            // 
            // DtpNonworkPeakStart
            // 
            this.DtpNonworkPeakStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.DtpNonworkPeakStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtpNonworkPeakStart.Location = new System.Drawing.Point(108, 15);
            this.DtpNonworkPeakStart.Name = "DtpNonworkPeakStart";
            this.DtpNonworkPeakStart.ShowUpDown = true;
            this.DtpNonworkPeakStart.Size = new System.Drawing.Size(85, 26);
            this.DtpNonworkPeakStart.TabIndex = 12;
            this.DtpNonworkPeakStart.Value = new System.DateTime(2015, 6, 9, 7, 0, 0, 0);
            // 
            // CboNonworkPeakInFee
            // 
            this.CboNonworkPeakInFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboNonworkPeakInFee.FormattingEnabled = true;
            this.CboNonworkPeakInFee.Items.AddRange(new object[] {
            "元/15分钟",
            "元/30分钟",
            "元/小时",
            "元/2小时"});
            this.CboNonworkPeakInFee.Location = new System.Drawing.Point(212, 73);
            this.CboNonworkPeakInFee.Margin = new System.Windows.Forms.Padding(4);
            this.CboNonworkPeakInFee.Name = "CboNonworkPeakInFee";
            this.CboNonworkPeakInFee.Size = new System.Drawing.Size(105, 24);
            this.CboNonworkPeakInFee.TabIndex = 7;
            // 
            // TxtNonworkPeakInFee
            // 
            this.TxtNonworkPeakInFee.Location = new System.Drawing.Point(132, 73);
            this.TxtNonworkPeakInFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNonworkPeakInFee.Name = "TxtNonworkPeakInFee";
            this.TxtNonworkPeakInFee.Size = new System.Drawing.Size(80, 26);
            this.TxtNonworkPeakInFee.TabIndex = 4;
            // 
            // LblNonworkPeakInFee
            // 
            this.LblNonworkPeakInFee.Location = new System.Drawing.Point(3, 73);
            this.LblNonworkPeakInFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNonworkPeakInFee.Name = "LblNonworkPeakInFee";
            this.LblNonworkPeakInFee.Size = new System.Drawing.Size(131, 27);
            this.LblNonworkPeakInFee.TabIndex = 3;
            this.LblNonworkPeakInFee.Text = "单价";
            this.LblNonworkPeakInFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CboNonworkPeakFUnit
            // 
            this.CboNonworkPeakFUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboNonworkPeakFUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboNonworkPeakFUnit.FormattingEnabled = true;
            this.CboNonworkPeakFUnit.Items.AddRange(new object[] {
            "15分钟",
            "30分钟",
            "小时",
            "2小时"});
            this.CboNonworkPeakFUnit.Location = new System.Drawing.Point(210, 45);
            this.CboNonworkPeakFUnit.Margin = new System.Windows.Forms.Padding(4);
            this.CboNonworkPeakFUnit.Name = "CboNonworkPeakFUnit";
            this.CboNonworkPeakFUnit.Size = new System.Drawing.Size(80, 24);
            this.CboNonworkPeakFUnit.TabIndex = 2;
            // 
            // ChkNonworkPeakFUnit
            // 
            this.ChkNonworkPeakFUnit.AutoSize = true;
            this.ChkNonworkPeakFUnit.Checked = true;
            this.ChkNonworkPeakFUnit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkNonworkPeakFUnit.Location = new System.Drawing.Point(108, 45);
            this.ChkNonworkPeakFUnit.Margin = new System.Windows.Forms.Padding(4);
            this.ChkNonworkPeakFUnit.Name = "ChkNonworkPeakFUnit";
            this.ChkNonworkPeakFUnit.Size = new System.Drawing.Size(107, 20);
            this.ChkNonworkPeakFUnit.TabIndex = 1;
            this.ChkNonworkPeakFUnit.Text = "是否区分首";
            this.ChkNonworkPeakFUnit.UseVisualStyleBackColor = true;
            // 
            // LblNonworkNonpeakOutFee
            // 
            this.LblNonworkNonpeakOutFee.Location = new System.Drawing.Point(3, 105);
            this.LblNonworkNonpeakOutFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNonworkNonpeakOutFee.Name = "LblNonworkNonpeakOutFee";
            this.LblNonworkNonpeakOutFee.Size = new System.Drawing.Size(131, 27);
            this.LblNonworkNonpeakOutFee.TabIndex = 8;
            this.LblNonworkNonpeakOutFee.Text = "首120分钟内单价";
            this.LblNonworkNonpeakOutFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblNonworkUnit
            // 
            this.LblNonworkUnit.Location = new System.Drawing.Point(392, 12);
            this.LblNonworkUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNonworkUnit.Name = "LblNonworkUnit";
            this.LblNonworkUnit.Size = new System.Drawing.Size(49, 27);
            this.LblNonworkUnit.TabIndex = 21;
            this.LblNonworkUnit.Text = "元/天";
            this.LblNonworkUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtNonworkNonpeakOutFee
            // 
            this.TxtNonworkNonpeakOutFee.Location = new System.Drawing.Point(132, 105);
            this.TxtNonworkNonpeakOutFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNonworkNonpeakOutFee.Name = "TxtNonworkNonpeakOutFee";
            this.TxtNonworkNonpeakOutFee.Size = new System.Drawing.Size(80, 26);
            this.TxtNonworkNonpeakOutFee.TabIndex = 9;
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // LblNonworkQuota
            // 
            this.LblNonworkQuota.Location = new System.Drawing.Point(243, 12);
            this.LblNonworkQuota.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNonworkQuota.Name = "LblNonworkQuota";
            this.LblNonworkQuota.Size = new System.Drawing.Size(51, 27);
            this.LblNonworkQuota.TabIndex = 19;
            this.LblNonworkQuota.Text = "限额";
            this.LblNonworkQuota.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblWorkNonpeakInFee
            // 
            this.LblWorkNonpeakInFee.Location = new System.Drawing.Point(3, 73);
            this.LblWorkNonpeakInFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblWorkNonpeakInFee.Name = "LblWorkNonpeakInFee";
            this.LblWorkNonpeakInFee.Size = new System.Drawing.Size(131, 27);
            this.LblWorkNonpeakInFee.TabIndex = 3;
            this.LblWorkNonpeakInFee.Text = "首120分钟内单价";
            this.LblWorkNonpeakInFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CboWorkNonpeakFUnit
            // 
            this.CboWorkNonpeakFUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWorkNonpeakFUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWorkNonpeakFUnit.FormattingEnabled = true;
            this.CboWorkNonpeakFUnit.Items.AddRange(new object[] {
            "15分钟",
            "30分钟",
            "小时",
            "2小时"});
            this.CboWorkNonpeakFUnit.Location = new System.Drawing.Point(210, 45);
            this.CboWorkNonpeakFUnit.Margin = new System.Windows.Forms.Padding(4);
            this.CboWorkNonpeakFUnit.Name = "CboWorkNonpeakFUnit";
            this.CboWorkNonpeakFUnit.Size = new System.Drawing.Size(80, 24);
            this.CboWorkNonpeakFUnit.TabIndex = 2;
            // 
            // ChkNonworkPeak
            // 
            this.ChkNonworkPeak.AutoSize = true;
            this.ChkNonworkPeak.Location = new System.Drawing.Point(80, 15);
            this.ChkNonworkPeak.Margin = new System.Windows.Forms.Padding(4);
            this.ChkNonworkPeak.Name = "ChkNonworkPeak";
            this.ChkNonworkPeak.Size = new System.Drawing.Size(155, 20);
            this.ChkNonworkPeak.TabIndex = 0;
            this.ChkNonworkPeak.Text = "是否区分高峰时段";
            this.ChkNonworkPeak.UseVisualStyleBackColor = true;
            // 
            // TxtNonworkNonpeakInFee
            // 
            this.TxtNonworkNonpeakInFee.Location = new System.Drawing.Point(132, 73);
            this.TxtNonworkNonpeakInFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNonworkNonpeakInFee.Name = "TxtNonworkNonpeakInFee";
            this.TxtNonworkNonpeakInFee.Size = new System.Drawing.Size(80, 26);
            this.TxtNonworkNonpeakInFee.TabIndex = 4;
            // 
            // ChkWorkNonpeakFUnit
            // 
            this.ChkWorkNonpeakFUnit.AutoSize = true;
            this.ChkWorkNonpeakFUnit.Location = new System.Drawing.Point(108, 45);
            this.ChkWorkNonpeakFUnit.Margin = new System.Windows.Forms.Padding(4);
            this.ChkWorkNonpeakFUnit.Name = "ChkWorkNonpeakFUnit";
            this.ChkWorkNonpeakFUnit.Size = new System.Drawing.Size(107, 20);
            this.ChkWorkNonpeakFUnit.TabIndex = 1;
            this.ChkWorkNonpeakFUnit.Text = "是否区分首";
            this.ChkWorkNonpeakFUnit.UseVisualStyleBackColor = true;
            // 
            // LblWorkUnit
            // 
            this.LblWorkUnit.Location = new System.Drawing.Point(392, 12);
            this.LblWorkUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblWorkUnit.Name = "LblWorkUnit";
            this.LblWorkUnit.Size = new System.Drawing.Size(49, 27);
            this.LblWorkUnit.TabIndex = 21;
            this.LblWorkUnit.Text = "元/天";
            this.LblWorkUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ChkWorkPeak
            // 
            this.ChkWorkPeak.AutoSize = true;
            this.ChkWorkPeak.Checked = true;
            this.ChkWorkPeak.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkWorkPeak.Location = new System.Drawing.Point(80, 15);
            this.ChkWorkPeak.Margin = new System.Windows.Forms.Padding(4);
            this.ChkWorkPeak.Name = "ChkWorkPeak";
            this.ChkWorkPeak.Size = new System.Drawing.Size(155, 20);
            this.ChkWorkPeak.TabIndex = 0;
            this.ChkWorkPeak.Text = "是否区分高峰时段";
            this.ChkWorkPeak.UseVisualStyleBackColor = true;
            // 
            // LblWorkQuota
            // 
            this.LblWorkQuota.Location = new System.Drawing.Point(243, 12);
            this.LblWorkQuota.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblWorkQuota.Name = "LblWorkQuota";
            this.LblWorkQuota.Size = new System.Drawing.Size(51, 27);
            this.LblWorkQuota.TabIndex = 19;
            this.LblWorkQuota.Text = "限额";
            this.LblWorkQuota.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblNonworkNonpeakInFee
            // 
            this.LblNonworkNonpeakInFee.Location = new System.Drawing.Point(3, 73);
            this.LblNonworkNonpeakInFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNonworkNonpeakInFee.Name = "LblNonworkNonpeakInFee";
            this.LblNonworkNonpeakInFee.Size = new System.Drawing.Size(131, 27);
            this.LblNonworkNonpeakInFee.TabIndex = 3;
            this.LblNonworkNonpeakInFee.Text = "首120分钟内单价";
            this.LblNonworkNonpeakInFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GbxNonworkdays
            // 
            this.GbxNonworkdays.Controls.Add(this.GbxNonworkNonpeak);
            this.GbxNonworkdays.Controls.Add(this.GbxNonworkPeak);
            this.GbxNonworkdays.Controls.Add(this.LblNonworkUnit);
            this.GbxNonworkdays.Controls.Add(this.ChkNonworkPeak);
            this.GbxNonworkdays.Controls.Add(this.LblNonworkQuota);
            this.GbxNonworkdays.Controls.Add(this.TxtNonworkQuotaFee);
            this.GbxNonworkdays.Font = new System.Drawing.Font("宋体", 12F);
            this.GbxNonworkdays.Location = new System.Drawing.Point(3, 220);
            this.GbxNonworkdays.Margin = new System.Windows.Forms.Padding(4);
            this.GbxNonworkdays.Name = "GbxNonworkdays";
            this.GbxNonworkdays.Padding = new System.Windows.Forms.Padding(4);
            this.GbxNonworkdays.Size = new System.Drawing.Size(816, 180);
            this.GbxNonworkdays.TabIndex = 11;
            this.GbxNonworkdays.TabStop = false;
            this.GbxNonworkdays.Text = "非工作日";
            // 
            // GbxNonworkNonpeak
            // 
            this.GbxNonworkNonpeak.Controls.Add(this.CboNonworkNonpeakOutFee);
            this.GbxNonworkNonpeak.Controls.Add(this.TxtNonworkNonpeakOutFee);
            this.GbxNonworkNonpeak.Controls.Add(this.LblNonworkNonpeakOutFee);
            this.GbxNonworkNonpeak.Controls.Add(this.LblNonworkNonpeakLine);
            this.GbxNonworkNonpeak.Controls.Add(this.DtpNonworkNonpeakEnd);
            this.GbxNonworkNonpeak.Controls.Add(this.DtpNonworkNonpeakStart);
            this.GbxNonworkNonpeak.Controls.Add(this.CboNonworkNonpeakInFee);
            this.GbxNonworkNonpeak.Controls.Add(this.TxtNonworkNonpeakInFee);
            this.GbxNonworkNonpeak.Controls.Add(this.LblNonworkNonpeakInFee);
            this.GbxNonworkNonpeak.Controls.Add(this.CboNonworkNonpeakFUnit);
            this.GbxNonworkNonpeak.Controls.Add(this.ChkNonworkNonpeakFUnit);
            this.GbxNonworkNonpeak.Location = new System.Drawing.Point(411, 35);
            this.GbxNonworkNonpeak.Margin = new System.Windows.Forms.Padding(4);
            this.GbxNonworkNonpeak.Name = "GbxNonworkNonpeak";
            this.GbxNonworkNonpeak.Padding = new System.Windows.Forms.Padding(4);
            this.GbxNonworkNonpeak.Size = new System.Drawing.Size(398, 140);
            this.GbxNonworkNonpeak.TabIndex = 22;
            this.GbxNonworkNonpeak.TabStop = false;
            this.GbxNonworkNonpeak.Text = "非高峰时段";
            // 
            // LblNonworkNonpeakLine
            // 
            this.LblNonworkNonpeakLine.Location = new System.Drawing.Point(193, 15);
            this.LblNonworkNonpeakLine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNonworkNonpeakLine.Name = "LblNonworkNonpeakLine";
            this.LblNonworkNonpeakLine.Size = new System.Drawing.Size(11, 27);
            this.LblNonworkNonpeakLine.TabIndex = 14;
            this.LblNonworkNonpeakLine.Text = "-";
            this.LblNonworkNonpeakLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DtpNonworkNonpeakEnd
            // 
            this.DtpNonworkNonpeakEnd.Cursor = System.Windows.Forms.Cursors.Default;
            this.DtpNonworkNonpeakEnd.Enabled = false;
            this.DtpNonworkNonpeakEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtpNonworkNonpeakEnd.Location = new System.Drawing.Point(203, 15);
            this.DtpNonworkNonpeakEnd.Name = "DtpNonworkNonpeakEnd";
            this.DtpNonworkNonpeakEnd.ShowUpDown = true;
            this.DtpNonworkNonpeakEnd.Size = new System.Drawing.Size(85, 26);
            this.DtpNonworkNonpeakEnd.TabIndex = 13;
            this.DtpNonworkNonpeakEnd.Value = new System.DateTime(2015, 6, 9, 7, 0, 0, 0);
            // 
            // DtpNonworkNonpeakStart
            // 
            this.DtpNonworkNonpeakStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.DtpNonworkNonpeakStart.Enabled = false;
            this.DtpNonworkNonpeakStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtpNonworkNonpeakStart.Location = new System.Drawing.Point(108, 15);
            this.DtpNonworkNonpeakStart.Name = "DtpNonworkNonpeakStart";
            this.DtpNonworkNonpeakStart.ShowUpDown = true;
            this.DtpNonworkNonpeakStart.Size = new System.Drawing.Size(85, 26);
            this.DtpNonworkNonpeakStart.TabIndex = 12;
            this.DtpNonworkNonpeakStart.Value = new System.DateTime(2015, 6, 9, 21, 0, 0, 0);
            // 
            // CboNonworkNonpeakInFee
            // 
            this.CboNonworkNonpeakInFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboNonworkNonpeakInFee.FormattingEnabled = true;
            this.CboNonworkNonpeakInFee.Items.AddRange(new object[] {
            "元/15分钟",
            "元/30分钟",
            "元/小时",
            "元/2小时"});
            this.CboNonworkNonpeakInFee.Location = new System.Drawing.Point(212, 73);
            this.CboNonworkNonpeakInFee.Margin = new System.Windows.Forms.Padding(4);
            this.CboNonworkNonpeakInFee.Name = "CboNonworkNonpeakInFee";
            this.CboNonworkNonpeakInFee.Size = new System.Drawing.Size(105, 24);
            this.CboNonworkNonpeakInFee.TabIndex = 7;
            // 
            // CboNonworkNonpeakFUnit
            // 
            this.CboNonworkNonpeakFUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboNonworkNonpeakFUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboNonworkNonpeakFUnit.FormattingEnabled = true;
            this.CboNonworkNonpeakFUnit.Items.AddRange(new object[] {
            "15分钟",
            "30分钟",
            "小时",
            "2小时"});
            this.CboNonworkNonpeakFUnit.Location = new System.Drawing.Point(210, 45);
            this.CboNonworkNonpeakFUnit.Margin = new System.Windows.Forms.Padding(4);
            this.CboNonworkNonpeakFUnit.Name = "CboNonworkNonpeakFUnit";
            this.CboNonworkNonpeakFUnit.Size = new System.Drawing.Size(80, 24);
            this.CboNonworkNonpeakFUnit.TabIndex = 2;
            // 
            // ChkNonworkNonpeakFUnit
            // 
            this.ChkNonworkNonpeakFUnit.AutoSize = true;
            this.ChkNonworkNonpeakFUnit.Location = new System.Drawing.Point(108, 45);
            this.ChkNonworkNonpeakFUnit.Margin = new System.Windows.Forms.Padding(4);
            this.ChkNonworkNonpeakFUnit.Name = "ChkNonworkNonpeakFUnit";
            this.ChkNonworkNonpeakFUnit.Size = new System.Drawing.Size(107, 20);
            this.ChkNonworkNonpeakFUnit.TabIndex = 1;
            this.ChkNonworkNonpeakFUnit.Text = "是否区分首";
            this.ChkNonworkNonpeakFUnit.UseVisualStyleBackColor = true;
            // 
            // TxtNonworkQuotaFee
            // 
            this.TxtNonworkQuotaFee.Location = new System.Drawing.Point(292, 12);
            this.TxtNonworkQuotaFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNonworkQuotaFee.Name = "TxtNonworkQuotaFee";
            this.TxtNonworkQuotaFee.Size = new System.Drawing.Size(100, 26);
            this.TxtNonworkQuotaFee.TabIndex = 20;
            // 
            // GbxWorkday
            // 
            this.GbxWorkday.Controls.Add(this.GbxWorkNonpeak);
            this.GbxWorkday.Controls.Add(this.GbxWorkPeak);
            this.GbxWorkday.Controls.Add(this.LblWorkUnit);
            this.GbxWorkday.Controls.Add(this.ChkWorkPeak);
            this.GbxWorkday.Controls.Add(this.LblWorkQuota);
            this.GbxWorkday.Controls.Add(this.TxtWorkQuotaFee);
            this.GbxWorkday.Font = new System.Drawing.Font("宋体", 12F);
            this.GbxWorkday.Location = new System.Drawing.Point(3, 38);
            this.GbxWorkday.Margin = new System.Windows.Forms.Padding(4);
            this.GbxWorkday.Name = "GbxWorkday";
            this.GbxWorkday.Padding = new System.Windows.Forms.Padding(4);
            this.GbxWorkday.Size = new System.Drawing.Size(816, 180);
            this.GbxWorkday.TabIndex = 9;
            this.GbxWorkday.TabStop = false;
            this.GbxWorkday.Text = "工作日";
            // 
            // GbxWorkNonpeak
            // 
            this.GbxWorkNonpeak.Controls.Add(this.CboWorkNonpeakOutFee);
            this.GbxWorkNonpeak.Controls.Add(this.TxtWorkNonpeakOutFee);
            this.GbxWorkNonpeak.Controls.Add(this.LblWorkNonpeakOutFee);
            this.GbxWorkNonpeak.Controls.Add(this.LblWorkNonpeakLine);
            this.GbxWorkNonpeak.Controls.Add(this.DtpWorkNonpeakEnd);
            this.GbxWorkNonpeak.Controls.Add(this.DtpWorkNonpeakStart);
            this.GbxWorkNonpeak.Controls.Add(this.CboWorkNonpeakInFee);
            this.GbxWorkNonpeak.Controls.Add(this.TxtWorkNonpeakInFee);
            this.GbxWorkNonpeak.Controls.Add(this.LblWorkNonpeakInFee);
            this.GbxWorkNonpeak.Controls.Add(this.CboWorkNonpeakFUnit);
            this.GbxWorkNonpeak.Controls.Add(this.ChkWorkNonpeakFUnit);
            this.GbxWorkNonpeak.Location = new System.Drawing.Point(411, 35);
            this.GbxWorkNonpeak.Margin = new System.Windows.Forms.Padding(4);
            this.GbxWorkNonpeak.Name = "GbxWorkNonpeak";
            this.GbxWorkNonpeak.Padding = new System.Windows.Forms.Padding(4);
            this.GbxWorkNonpeak.Size = new System.Drawing.Size(398, 140);
            this.GbxWorkNonpeak.TabIndex = 22;
            this.GbxWorkNonpeak.TabStop = false;
            this.GbxWorkNonpeak.Text = "非高峰时段";
            // 
            // CboWorkNonpeakOutFee
            // 
            this.CboWorkNonpeakOutFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWorkNonpeakOutFee.FormattingEnabled = true;
            this.CboWorkNonpeakOutFee.Items.AddRange(new object[] {
            "元/15分钟",
            "元/30分钟",
            "元/小时",
            "元/2小时"});
            this.CboWorkNonpeakOutFee.Location = new System.Drawing.Point(212, 105);
            this.CboWorkNonpeakOutFee.Margin = new System.Windows.Forms.Padding(4);
            this.CboWorkNonpeakOutFee.Name = "CboWorkNonpeakOutFee";
            this.CboWorkNonpeakOutFee.Size = new System.Drawing.Size(105, 24);
            this.CboWorkNonpeakOutFee.TabIndex = 11;
            // 
            // TxtWorkNonpeakOutFee
            // 
            this.TxtWorkNonpeakOutFee.Location = new System.Drawing.Point(132, 105);
            this.TxtWorkNonpeakOutFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtWorkNonpeakOutFee.Name = "TxtWorkNonpeakOutFee";
            this.TxtWorkNonpeakOutFee.Size = new System.Drawing.Size(80, 26);
            this.TxtWorkNonpeakOutFee.TabIndex = 9;
            // 
            // LblWorkNonpeakOutFee
            // 
            this.LblWorkNonpeakOutFee.Location = new System.Drawing.Point(3, 105);
            this.LblWorkNonpeakOutFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblWorkNonpeakOutFee.Name = "LblWorkNonpeakOutFee";
            this.LblWorkNonpeakOutFee.Size = new System.Drawing.Size(131, 27);
            this.LblWorkNonpeakOutFee.TabIndex = 8;
            this.LblWorkNonpeakOutFee.Text = "首120分钟内单价";
            this.LblWorkNonpeakOutFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblWorkNonpeakLine
            // 
            this.LblWorkNonpeakLine.Location = new System.Drawing.Point(193, 15);
            this.LblWorkNonpeakLine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblWorkNonpeakLine.Name = "LblWorkNonpeakLine";
            this.LblWorkNonpeakLine.Size = new System.Drawing.Size(11, 27);
            this.LblWorkNonpeakLine.TabIndex = 14;
            this.LblWorkNonpeakLine.Text = "-";
            this.LblWorkNonpeakLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DtpWorkNonpeakEnd
            // 
            this.DtpWorkNonpeakEnd.Cursor = System.Windows.Forms.Cursors.Default;
            this.DtpWorkNonpeakEnd.Enabled = false;
            this.DtpWorkNonpeakEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtpWorkNonpeakEnd.Location = new System.Drawing.Point(203, 15);
            this.DtpWorkNonpeakEnd.Name = "DtpWorkNonpeakEnd";
            this.DtpWorkNonpeakEnd.ShowUpDown = true;
            this.DtpWorkNonpeakEnd.Size = new System.Drawing.Size(85, 26);
            this.DtpWorkNonpeakEnd.TabIndex = 13;
            this.DtpWorkNonpeakEnd.Value = new System.DateTime(2015, 6, 9, 7, 0, 0, 0);
            // 
            // DtpWorkNonpeakStart
            // 
            this.DtpWorkNonpeakStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.DtpWorkNonpeakStart.Enabled = false;
            this.DtpWorkNonpeakStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtpWorkNonpeakStart.Location = new System.Drawing.Point(108, 15);
            this.DtpWorkNonpeakStart.Name = "DtpWorkNonpeakStart";
            this.DtpWorkNonpeakStart.ShowUpDown = true;
            this.DtpWorkNonpeakStart.Size = new System.Drawing.Size(85, 26);
            this.DtpWorkNonpeakStart.TabIndex = 12;
            this.DtpWorkNonpeakStart.Value = new System.DateTime(2015, 6, 9, 21, 0, 0, 0);
            // 
            // CboWorkNonpeakInFee
            // 
            this.CboWorkNonpeakInFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWorkNonpeakInFee.FormattingEnabled = true;
            this.CboWorkNonpeakInFee.Items.AddRange(new object[] {
            "元/15分钟",
            "元/30分钟",
            "元/小时",
            "元/2小时"});
            this.CboWorkNonpeakInFee.Location = new System.Drawing.Point(212, 73);
            this.CboWorkNonpeakInFee.Margin = new System.Windows.Forms.Padding(4);
            this.CboWorkNonpeakInFee.Name = "CboWorkNonpeakInFee";
            this.CboWorkNonpeakInFee.Size = new System.Drawing.Size(105, 24);
            this.CboWorkNonpeakInFee.TabIndex = 7;
            // 
            // TxtWorkNonpeakInFee
            // 
            this.TxtWorkNonpeakInFee.Location = new System.Drawing.Point(132, 73);
            this.TxtWorkNonpeakInFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtWorkNonpeakInFee.Name = "TxtWorkNonpeakInFee";
            this.TxtWorkNonpeakInFee.Size = new System.Drawing.Size(80, 26);
            this.TxtWorkNonpeakInFee.TabIndex = 4;
            // 
            // GbxWorkPeak
            // 
            this.GbxWorkPeak.Controls.Add(this.CboWorkPeakOutFee);
            this.GbxWorkPeak.Controls.Add(this.TxtWorkPeakOutFee);
            this.GbxWorkPeak.Controls.Add(this.LblWorkPeakOutFee);
            this.GbxWorkPeak.Controls.Add(this.LblWorkPeakLine);
            this.GbxWorkPeak.Controls.Add(this.DtpWorkPeakEnd);
            this.GbxWorkPeak.Controls.Add(this.DtpWorkPeakStart);
            this.GbxWorkPeak.Controls.Add(this.CboWorkPeakInFee);
            this.GbxWorkPeak.Controls.Add(this.TxtWorkPeakInFee);
            this.GbxWorkPeak.Controls.Add(this.LblWorkPeakInFee);
            this.GbxWorkPeak.Controls.Add(this.CboWorkPeakFUnit);
            this.GbxWorkPeak.Controls.Add(this.ChkWorkPeakFUnit);
            this.GbxWorkPeak.Location = new System.Drawing.Point(5, 35);
            this.GbxWorkPeak.Margin = new System.Windows.Forms.Padding(4);
            this.GbxWorkPeak.Name = "GbxWorkPeak";
            this.GbxWorkPeak.Padding = new System.Windows.Forms.Padding(4);
            this.GbxWorkPeak.Size = new System.Drawing.Size(398, 140);
            this.GbxWorkPeak.TabIndex = 1;
            this.GbxWorkPeak.TabStop = false;
            this.GbxWorkPeak.Text = "高峰时段";
            // 
            // CboWorkPeakOutFee
            // 
            this.CboWorkPeakOutFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWorkPeakOutFee.FormattingEnabled = true;
            this.CboWorkPeakOutFee.Items.AddRange(new object[] {
            "元/15分钟",
            "元/30分钟",
            "元/小时",
            "元/2小时"});
            this.CboWorkPeakOutFee.Location = new System.Drawing.Point(215, 105);
            this.CboWorkPeakOutFee.Margin = new System.Windows.Forms.Padding(4);
            this.CboWorkPeakOutFee.Name = "CboWorkPeakOutFee";
            this.CboWorkPeakOutFee.Size = new System.Drawing.Size(105, 24);
            this.CboWorkPeakOutFee.TabIndex = 11;
            // 
            // TxtWorkPeakOutFee
            // 
            this.TxtWorkPeakOutFee.Location = new System.Drawing.Point(135, 105);
            this.TxtWorkPeakOutFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtWorkPeakOutFee.Name = "TxtWorkPeakOutFee";
            this.TxtWorkPeakOutFee.Size = new System.Drawing.Size(80, 26);
            this.TxtWorkPeakOutFee.TabIndex = 9;
            // 
            // LblWorkPeakOutFee
            // 
            this.LblWorkPeakOutFee.Location = new System.Drawing.Point(6, 105);
            this.LblWorkPeakOutFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblWorkPeakOutFee.Name = "LblWorkPeakOutFee";
            this.LblWorkPeakOutFee.Size = new System.Drawing.Size(131, 27);
            this.LblWorkPeakOutFee.TabIndex = 8;
            this.LblWorkPeakOutFee.Text = "单价";
            this.LblWorkPeakOutFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblWorkPeakLine
            // 
            this.LblWorkPeakLine.Location = new System.Drawing.Point(193, 15);
            this.LblWorkPeakLine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblWorkPeakLine.Name = "LblWorkPeakLine";
            this.LblWorkPeakLine.Size = new System.Drawing.Size(11, 27);
            this.LblWorkPeakLine.TabIndex = 14;
            this.LblWorkPeakLine.Text = "-";
            this.LblWorkPeakLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DtpWorkPeakEnd
            // 
            this.DtpWorkPeakEnd.Cursor = System.Windows.Forms.Cursors.Default;
            this.DtpWorkPeakEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtpWorkPeakEnd.Location = new System.Drawing.Point(203, 15);
            this.DtpWorkPeakEnd.Name = "DtpWorkPeakEnd";
            this.DtpWorkPeakEnd.ShowUpDown = true;
            this.DtpWorkPeakEnd.Size = new System.Drawing.Size(85, 26);
            this.DtpWorkPeakEnd.TabIndex = 13;
            this.DtpWorkPeakEnd.Value = new System.DateTime(2015, 6, 9, 21, 0, 0, 0);
            // 
            // DtpWorkPeakStart
            // 
            this.DtpWorkPeakStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.DtpWorkPeakStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtpWorkPeakStart.Location = new System.Drawing.Point(108, 15);
            this.DtpWorkPeakStart.Name = "DtpWorkPeakStart";
            this.DtpWorkPeakStart.ShowUpDown = true;
            this.DtpWorkPeakStart.Size = new System.Drawing.Size(85, 26);
            this.DtpWorkPeakStart.TabIndex = 12;
            this.DtpWorkPeakStart.Value = new System.DateTime(2015, 6, 9, 7, 0, 0, 0);
            // 
            // CboWorkPeakInFee
            // 
            this.CboWorkPeakInFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWorkPeakInFee.FormattingEnabled = true;
            this.CboWorkPeakInFee.Items.AddRange(new object[] {
            "元/15分钟",
            "元/30分钟",
            "元/小时",
            "元/2小时"});
            this.CboWorkPeakInFee.Location = new System.Drawing.Point(215, 74);
            this.CboWorkPeakInFee.Margin = new System.Windows.Forms.Padding(4);
            this.CboWorkPeakInFee.Name = "CboWorkPeakInFee";
            this.CboWorkPeakInFee.Size = new System.Drawing.Size(105, 24);
            this.CboWorkPeakInFee.TabIndex = 7;
            // 
            // TxtWorkPeakInFee
            // 
            this.TxtWorkPeakInFee.Location = new System.Drawing.Point(135, 73);
            this.TxtWorkPeakInFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtWorkPeakInFee.Name = "TxtWorkPeakInFee";
            this.TxtWorkPeakInFee.Size = new System.Drawing.Size(80, 26);
            this.TxtWorkPeakInFee.TabIndex = 4;
            // 
            // LblWorkPeakInFee
            // 
            this.LblWorkPeakInFee.Location = new System.Drawing.Point(6, 73);
            this.LblWorkPeakInFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblWorkPeakInFee.Name = "LblWorkPeakInFee";
            this.LblWorkPeakInFee.Size = new System.Drawing.Size(131, 27);
            this.LblWorkPeakInFee.TabIndex = 3;
            this.LblWorkPeakInFee.Text = "单价";
            this.LblWorkPeakInFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CboWorkPeakFUnit
            // 
            this.CboWorkPeakFUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboWorkPeakFUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboWorkPeakFUnit.FormatString = "15分钟";
            this.CboWorkPeakFUnit.Items.AddRange(new object[] {
            "15分钟",
            "30分钟",
            "小时",
            "2小时"});
            this.CboWorkPeakFUnit.Location = new System.Drawing.Point(210, 45);
            this.CboWorkPeakFUnit.Margin = new System.Windows.Forms.Padding(4);
            this.CboWorkPeakFUnit.Name = "CboWorkPeakFUnit";
            this.CboWorkPeakFUnit.Size = new System.Drawing.Size(80, 24);
            this.CboWorkPeakFUnit.TabIndex = 2;
            // 
            // ChkWorkPeakFUnit
            // 
            this.ChkWorkPeakFUnit.AutoSize = true;
            this.ChkWorkPeakFUnit.Checked = true;
            this.ChkWorkPeakFUnit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkWorkPeakFUnit.Location = new System.Drawing.Point(108, 45);
            this.ChkWorkPeakFUnit.Margin = new System.Windows.Forms.Padding(4);
            this.ChkWorkPeakFUnit.Name = "ChkWorkPeakFUnit";
            this.ChkWorkPeakFUnit.Size = new System.Drawing.Size(107, 20);
            this.ChkWorkPeakFUnit.TabIndex = 1;
            this.ChkWorkPeakFUnit.Text = "是否区分首";
            this.ChkWorkPeakFUnit.UseVisualStyleBackColor = true;
            // 
            // TxtWorkQuotaFee
            // 
            this.TxtWorkQuotaFee.Location = new System.Drawing.Point(292, 12);
            this.TxtWorkQuotaFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtWorkQuotaFee.Name = "TxtWorkQuotaFee";
            this.TxtWorkQuotaFee.Size = new System.Drawing.Size(100, 26);
            this.TxtWorkQuotaFee.TabIndex = 20;
            // 
            // ChkWorkday
            // 
            this.ChkWorkday.AutoSize = true;
            this.ChkWorkday.Location = new System.Drawing.Point(83, 21);
            this.ChkWorkday.Margin = new System.Windows.Forms.Padding(4);
            this.ChkWorkday.Name = "ChkWorkday";
            this.ChkWorkday.Size = new System.Drawing.Size(139, 20);
            this.ChkWorkday.TabIndex = 10;
            this.ChkWorkday.Text = "是否区分工作日";
            this.ChkWorkday.UseVisualStyleBackColor = true;
            // 
            // CUserTariffForm
            // 
            this.ClientSize = new System.Drawing.Size(832, 400);
            this.Controls.Add(this.GbxTemp);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CUserTariffForm";
            this.Text = "临时卡计时";
            this.GbxTemp.ResumeLayout(false);
            this.GbxTemp.PerformLayout();
            this.GbxNonworkPeak.ResumeLayout(false);
            this.GbxNonworkPeak.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.GbxNonworkdays.ResumeLayout(false);
            this.GbxNonworkdays.PerformLayout();
            this.GbxNonworkNonpeak.ResumeLayout(false);
            this.GbxNonworkNonpeak.PerformLayout();
            this.GbxWorkday.ResumeLayout(false);
            this.GbxWorkday.PerformLayout();
            this.GbxWorkNonpeak.ResumeLayout(false);
            this.GbxWorkNonpeak.PerformLayout();
            this.GbxWorkPeak.ResumeLayout(false);
            this.GbxWorkPeak.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbxTemp;
        private System.Windows.Forms.GroupBox GbxNonworkdays;
        private System.Windows.Forms.GroupBox GbxNonworkNonpeak;
        private System.Windows.Forms.ComboBox CboNonworkNonpeakOutFee;
        private System.Windows.Forms.TextBox TxtNonworkNonpeakOutFee;
        private System.Windows.Forms.Label LblNonworkNonpeakOutFee;
        private System.Windows.Forms.Label LblNonworkNonpeakLine;
        private System.Windows.Forms.DateTimePicker DtpNonworkNonpeakEnd;
        private System.Windows.Forms.DateTimePicker DtpNonworkNonpeakStart;
        private System.Windows.Forms.ComboBox CboNonworkNonpeakInFee;
        private System.Windows.Forms.TextBox TxtNonworkNonpeakInFee;
        private System.Windows.Forms.Label LblNonworkNonpeakInFee;
        private System.Windows.Forms.ComboBox CboNonworkNonpeakFUnit;
        private System.Windows.Forms.CheckBox ChkNonworkNonpeakFUnit;
        private System.Windows.Forms.GroupBox GbxNonworkPeak;
        private System.Windows.Forms.ComboBox CboNonworkPeakOutFee;
        private System.Windows.Forms.TextBox TxtNonworkPeakOutFee;
        private System.Windows.Forms.Label LblNonworkPeakOutFee;
        private System.Windows.Forms.Label LblNonworkPeakLine;
        private System.Windows.Forms.DateTimePicker DtpNonworkPeakEnd;
        private System.Windows.Forms.DateTimePicker DtpNonworkPeakStart;
        private System.Windows.Forms.ComboBox CboNonworkPeakInFee;
        private System.Windows.Forms.TextBox TxtNonworkPeakInFee;
        private System.Windows.Forms.Label LblNonworkPeakInFee;
        private System.Windows.Forms.ComboBox CboNonworkPeakFUnit;
        private System.Windows.Forms.CheckBox ChkNonworkPeakFUnit;
        private System.Windows.Forms.Label LblNonworkUnit;
        private System.Windows.Forms.CheckBox ChkNonworkPeak;
        private System.Windows.Forms.Label LblNonworkQuota;
        private System.Windows.Forms.TextBox TxtNonworkQuotaFee;
        private System.Windows.Forms.GroupBox GbxWorkday;
        private System.Windows.Forms.GroupBox GbxWorkNonpeak;
        private System.Windows.Forms.ComboBox CboWorkNonpeakOutFee;
        private System.Windows.Forms.TextBox TxtWorkNonpeakOutFee;
        private System.Windows.Forms.Label LblWorkNonpeakOutFee;
        private System.Windows.Forms.Label LblWorkNonpeakLine;
        private System.Windows.Forms.DateTimePicker DtpWorkNonpeakEnd;
        private System.Windows.Forms.DateTimePicker DtpWorkNonpeakStart;
        private System.Windows.Forms.ComboBox CboWorkNonpeakInFee;
        private System.Windows.Forms.TextBox TxtWorkNonpeakInFee;
        private System.Windows.Forms.Label LblWorkNonpeakInFee;
        private System.Windows.Forms.ComboBox CboWorkNonpeakFUnit;
        private System.Windows.Forms.CheckBox ChkWorkNonpeakFUnit;
        private System.Windows.Forms.GroupBox GbxWorkPeak;
        private System.Windows.Forms.ComboBox CboWorkPeakOutFee;
        private System.Windows.Forms.TextBox TxtWorkPeakOutFee;
        private System.Windows.Forms.Label LblWorkPeakOutFee;
        private System.Windows.Forms.Label LblWorkPeakLine;
        private System.Windows.Forms.DateTimePicker DtpWorkPeakEnd;
        private System.Windows.Forms.DateTimePicker DtpWorkPeakStart;
        private System.Windows.Forms.ComboBox CboWorkPeakInFee;
        private System.Windows.Forms.TextBox TxtWorkPeakInFee;
        private System.Windows.Forms.Label LblWorkPeakInFee;
        private System.Windows.Forms.ComboBox CboWorkPeakFUnit;
        private System.Windows.Forms.CheckBox ChkWorkPeakFUnit;
        private System.Windows.Forms.Label LblWorkUnit;
        private System.Windows.Forms.CheckBox ChkWorkPeak;
        private System.Windows.Forms.Label LblWorkQuota;
        private System.Windows.Forms.TextBox TxtWorkQuotaFee;
        private System.Windows.Forms.CheckBox ChkWorkday;
        private System.Diagnostics.EventLog eventLog1;
        #endregion

        #region 公有函数
        /// <summary>
        /// 获取临时卡-计时卡计费标准内容
        /// </summary>
        /// <param name="tariff"></param>
        public void GetTariffInfo(ref CTariffDto tariff)
        {
            // 是否区分工作日
            if (this.ChkWorkday.Checked)
            {
                tariff.isworkday = 1 ;
            }
            else
            {
                tariff.isworkday = 0;
            }

            #region 工作日
            // 限额
            double dWorkdayQuotaFee = 0.0;
            CBaseMethods.MyBase.StringToDouble(this.TxtWorkQuotaFee.Text, out dWorkdayQuotaFee);
            tariff.workdayquotafee = (float)dWorkdayQuotaFee;
           
            // 高峰时段
            if (this.ChkWorkPeak.Checked)
            {
                tariff.workpeakperiod = this.DtpWorkPeakStart.Text.Trim() + "-" + this.DtpWorkPeakEnd.Text.Trim();
            }
            else
            {
                tariff.workpeakperiod = string.Empty;
            }
          
            // 高峰首单位
            if (this.ChkWorkPeakFUnit.Checked)
            {
                tariff.workpeakfirstunit = this.CboWorkPeakFUnit.Text.Trim();
                // 高峰首单位内
                tariff.workpeakinunitfee = this.TxtWorkPeakInFee.Text.Trim() + this.CboWorkPeakInFee.Text.Trim();
            }
            else
            {
                tariff.workpeakfirstunit = string.Empty;
            }
            // 高峰首单位外
            tariff.workpeakoutunitfee = this.TxtWorkPeakOutFee.Text.Trim() + this.CboWorkPeakOutFee.Text.Trim();
           
            // 非高峰首单位
            if (this.ChkWorkNonpeakFUnit.Checked)
            {
                tariff.worknonpeakfirstunit = this.CboWorkNonpeakFUnit.Text.Trim();
                // 非高峰首单位内
                tariff.worknonpeakinunitfee = this.TxtWorkNonpeakInFee.Text.Trim() + this.CboWorkNonpeakInFee.Text.Trim();
            }
            else
            {
                tariff.worknonpeakfirstunit = string.Empty;
            }
            // 非高峰首单位外
            tariff.worknonpeakoutunitfee = this.TxtWorkNonpeakOutFee.Text.Trim() + this.CboWorkNonpeakOutFee.Text.Trim();
            #endregion

            #region 非工作日
            // 限额
            double dNonworkdayQuotaFee = 0.0;
            CBaseMethods.MyBase.StringToDouble(this.TxtNonworkQuotaFee.Text, out dNonworkdayQuotaFee);
            tariff.nonworkdayquotafee = (float)dNonworkdayQuotaFee;
           
            // 高峰时段
            if (this.ChkNonworkPeak.Checked)
            {
                tariff.nonworkpeakperiod = this.DtpNonworkPeakStart.Text.Trim() + "-" + this.DtpNonworkPeakEnd.Text.Trim();
            }
            else
            {
                tariff.nonworkpeakperiod = string.Empty;
            }

            // 高峰首单位
            if (this.ChkNonworkPeakFUnit.Checked)
            {
                tariff.nonworkpeakfirstunit = this.CboNonworkPeakFUnit.Text.Trim();
                // 高峰首单位内
                tariff.nonworkpeakinunitfee = this.TxtNonworkPeakInFee.Text.Trim() + this.CboNonworkPeakInFee.Text.Trim();
            }
            else
            {
                tariff.nonworkpeakfirstunit = string.Empty;
            }
            // 高峰首单位外
            tariff.nonworkpeakoutunitfee = this.TxtNonworkPeakOutFee.Text.Trim() + this.CboNonworkPeakOutFee.Text.Trim();
            
            // 非高峰首单位
            if (this.ChkNonworkNonpeakFUnit.Checked)
            {
                tariff.nonworknonpeakfirstunit = this.CboNonworkNonpeakFUnit.Text.Trim();
                // 非高峰首单位内
                tariff.nonworknonpeakinunitfee = this.TxtNonworkNonpeakInFee.Text.Trim() + this.CboNonworkNonpeakInFee.Text.Trim();
            }
            else
            {
                tariff.nonworknonpeakfirstunit = string.Empty;
            }
            // 非高峰首单位外
            tariff.nonworknonpeakoutunitfee = this.TxtNonworkNonpeakOutFee.Text.Trim() + this.CboNonworkNonpeakOutFee.Text.Trim();
            #endregion

            // 临时卡一天的费用（默认工作日）一天总共24*60 = 1400分钟
            float dPeakMinutes = (float)(this.DtpWorkPeakEnd.Value - this.DtpWorkPeakStart.Value).TotalMinutes;
            if (0 >= dPeakMinutes)
            {
                dPeakMinutes += 1440;//24 * 60;
            }
            float fPeakFee = 0.0f, fNonpeakFee = 0.0f;
            float.TryParse(this.TxtWorkPeakOutFee.Text.Trim(), out fPeakFee);
            float.TryParse(this.TxtWorkNonpeakOutFee.Text.Trim(), out fNonpeakFee);
            int nPeakUnit = ConvertUnitToInt(this.CboWorkPeakOutFee.Text);
            int nNonpeakUnit = ConvertUnitToInt(this.CboWorkNonpeakOutFee.Text);
            tariff.fee = dPeakMinutes / nPeakUnit * fPeakFee + (1440 - dPeakMinutes) / nNonpeakUnit * fNonpeakFee;
        }

        /// <summary>
        /// 填充临时卡-计时卡计费标准内容
        /// </summary>
        /// <param name="tariff"></param>
        public void SetTariffInfo(CTariffDto tariff)
        {
            // 是否区分工作日
            if (0 == tariff.isworkday)
            {
                this.ChkWorkday.Checked = false;
            }
            else
            {
                this.ChkWorkday.Checked = true;
            }

            #region 工作日
            // 限额
            this.TxtWorkQuotaFee.Text = tariff.workdayquotafee.ToString();
            
            // 高峰时段
            if (!string.IsNullOrEmpty(tariff.workpeakperiod))
            {
                int index = tariff.workpeakperiod.IndexOf("-");

                if (-1 != index)
                {
                    this.DtpWorkPeakStart.Text = tariff.workpeakperiod.Substring(0, index);
                    this.DtpWorkPeakEnd.Text = tariff.workpeakperiod.Substring(index + 1);
                }
                this.ChkWorkPeak.Checked = true;
            }
            else
            {
                this.ChkWorkPeak.Checked = false;
            }
            
            // 高峰首单位
            if (!string.IsNullOrEmpty(tariff.workpeakfirstunit))
            {
                this.CboWorkPeakFUnit.Text = tariff.workpeakfirstunit;
                this.ChkWorkPeakFUnit.Checked = true;
            }
            else
            {
                this.ChkWorkPeakFUnit.Checked = false;
            }
            // 高峰首单位内
            string strWorkPeakInUnitFee = string.Empty;
            if (!string.IsNullOrEmpty(tariff.workpeakinunitfee))
            {
                strWorkPeakInUnitFee = tariff.workpeakinunitfee;
            }
            int nIndex = strWorkPeakInUnitFee.IndexOf("元");
            if (-1 != nIndex)
            {
                this.TxtWorkPeakInFee.Text = strWorkPeakInUnitFee.Substring(0, nIndex);
                this.CboWorkPeakInFee.Text = strWorkPeakInUnitFee.Substring(nIndex);
            }
            else
            {
                this.TxtWorkPeakInFee.Text = string.Empty;
                this.CboWorkPeakInFee.Text = null;
            }
            // 高峰首单位外
            string strWorkPeakOutUnitFee = string.Empty;
            if (!string.IsNullOrEmpty(tariff.workpeakoutunitfee))
            {
                strWorkPeakOutUnitFee = tariff.workpeakoutunitfee;
            }
            nIndex = strWorkPeakOutUnitFee.IndexOf("元");
            if (-1 != nIndex)
            {
                this.TxtWorkPeakOutFee.Text = strWorkPeakOutUnitFee.Substring(0, nIndex);
                this.CboWorkPeakOutFee.Text = strWorkPeakOutUnitFee.Substring(nIndex);
            }
            else
            {
                this.TxtWorkPeakOutFee.Text = string.Empty;
                this.CboWorkPeakOutFee.Text = null;
            }
           
            // 非高峰首单位
            if (!string.IsNullOrEmpty(tariff.worknonpeakfirstunit))
            {
                this.CboWorkNonpeakFUnit.Text = tariff.worknonpeakfirstunit;
                this.ChkWorkNonpeakFUnit.Checked = true;
            }
            else
            {
                this.ChkWorkNonpeakFUnit.Checked = false;
            }
            // 非高峰首单位内
            string strWorkNonpeakInUnitFee = string.Empty;
            if(!string.IsNullOrEmpty(tariff.worknonpeakinunitfee))
            {
                strWorkNonpeakInUnitFee = tariff.worknonpeakinunitfee;
            }
            nIndex = strWorkNonpeakInUnitFee.IndexOf("元");
            if (-1 != nIndex)
            {
                this.TxtWorkNonpeakInFee.Text = strWorkNonpeakInUnitFee.Substring(0, nIndex);
                this.CboWorkNonpeakInFee.Text = strWorkNonpeakInUnitFee.Substring(nIndex);
            }
            else
            {
                this.TxtWorkNonpeakInFee.Text = string.Empty;
                this.CboWorkNonpeakInFee.Text = null;
            }
            // 非高峰首单位外
            string strWorkNonpeakOutUnitFee = string.Empty;
            if(!string.IsNullOrEmpty(tariff.worknonpeakoutunitfee))
            {
                strWorkNonpeakOutUnitFee = tariff.worknonpeakoutunitfee;
            }
            nIndex = strWorkNonpeakOutUnitFee.IndexOf("元");
            if (-1 != nIndex)
            {
                this.TxtWorkNonpeakOutFee.Text = strWorkNonpeakOutUnitFee.Substring(0, nIndex);
                this.CboWorkNonpeakOutFee.Text = strWorkNonpeakOutUnitFee.Substring(nIndex);
            }
            else
            {
                this.TxtWorkNonpeakOutFee.Text = string.Empty;
                this.CboWorkNonpeakOutFee.Text = null;
            }
            #endregion

            #region 非工作日
            // 限额
            this.TxtNonworkQuotaFee.Text = tariff.nonworkdayquotafee.ToString();
           
            // 高峰时段
            if (!string.IsNullOrEmpty(tariff.nonworkpeakperiod))
            {
                int index = tariff.nonworkpeakperiod.IndexOf("-");

                if (-1 != index)
                {
                    this.DtpNonworkPeakStart.Text = tariff.nonworkpeakperiod.Substring(0, index);
                    this.DtpNonworkPeakEnd.Text = tariff.nonworkpeakperiod.Substring(index + 1);
                }
                this.ChkNonworkPeak.Checked = true;
            }
            else
            {
                this.ChkNonworkPeak.Checked = false;
            }
           
            // 高峰首单位
            if (!string.IsNullOrEmpty(tariff.nonworkpeakfirstunit))
            {
                this.CboNonworkPeakFUnit.Text = tariff.nonworkpeakfirstunit;
                this.ChkNonworkPeakFUnit.Checked = true;
            }
            else
            {
                this.ChkNonworkPeakFUnit.Checked = false;
            }
            // 高峰首单位内
            string strNonworkPeakInUnitFee = string.Empty;
            if(!string.IsNullOrEmpty(tariff.nonworkpeakinunitfee))
            {
                strNonworkPeakInUnitFee = tariff.nonworkpeakinunitfee;
            }
            nIndex = strNonworkPeakInUnitFee.IndexOf("元");
            if (-1 != nIndex)
            {
                this.TxtNonworkPeakInFee.Text = strNonworkPeakInUnitFee.Substring(0, nIndex);
                this.CboNonworkPeakInFee.Text = strNonworkPeakInUnitFee.Substring(nIndex);
            }
            else
            {
                this.TxtNonworkPeakInFee.Text = string.Empty;
                this.CboNonworkPeakInFee.Text = null;
            }
            // 高峰首单位外
            string strNonworkPeakOutUnitFee = string.Empty;
            if(!string.IsNullOrEmpty(tariff.nonworkpeakoutunitfee))
            {
                strNonworkPeakOutUnitFee = tariff.nonworkpeakoutunitfee;
            }
            nIndex = strNonworkPeakOutUnitFee.IndexOf("元");
            if (-1 != nIndex)
            {
                this.TxtNonworkPeakOutFee.Text = strNonworkPeakOutUnitFee.Substring(0, nIndex);
                this.CboNonworkPeakOutFee.Text = strNonworkPeakOutUnitFee.Substring(nIndex);
            }
            else
            {
                this.TxtNonworkPeakOutFee.Text = string.Empty;
                this.CboNonworkPeakOutFee.Text = null;
            }
           
            // 非高峰首单位
            if (!string.IsNullOrEmpty(tariff.worknonpeakfirstunit))
            {
                this.CboNonworkNonpeakFUnit.Text = tariff.nonworknonpeakfirstunit;
                this.ChkNonworkNonpeakFUnit.Checked = true;
            }
            else
            {
                this.ChkNonworkNonpeakFUnit.Checked = false;
            }
            // 非高峰首单位内
            string strNonworkNonpeakInUnitFee = string.Empty;
            if(!string.IsNullOrEmpty(tariff.nonworknonpeakinunitfee))
            {
                strNonworkNonpeakInUnitFee = tariff.nonworknonpeakinunitfee;
            }
            nIndex = strNonworkNonpeakInUnitFee.IndexOf("元");
            if (-1 != nIndex)
            {
                this.TxtNonworkNonpeakInFee.Text = strNonworkNonpeakInUnitFee.Substring(0, nIndex);
                this.CboNonworkNonpeakInFee.Text = strNonworkNonpeakInUnitFee.Substring(nIndex);
            }
            else
            {
                this.TxtNonworkNonpeakInFee.Text = string.Empty;
                this.CboNonworkNonpeakInFee.Text = null;
            }
            // 非高峰首单位外
            string strNonworkNonpeakOutUnitFee = string.Empty;
            if(!string.IsNullOrEmpty(tariff.nonworknonpeakoutunitfee))
            {
                strNonworkNonpeakOutUnitFee = tariff.nonworknonpeakoutunitfee;
            }
            nIndex = strNonworkNonpeakOutUnitFee.IndexOf("元");
            if (-1 != nIndex)
            {
                this.TxtNonworkNonpeakOutFee.Text = strNonworkNonpeakOutUnitFee.Substring(0, nIndex);
                this.CboNonworkNonpeakOutFee.Text = strNonworkNonpeakOutUnitFee.Substring(nIndex);
            }
            else
            {
                this.TxtNonworkNonpeakOutFee.Text = string.Empty;
                this.CboNonworkNonpeakOutFee.Text = null;
            }
            #endregion
        }
        #endregion

        #region 勾选框处理
        /// <summary>
        /// 是否区分工作日cheaked变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkWorkday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ChkWorkday.Checked)
                {
                    this.GbxNonworkdays.Enabled = true;
                    this.GbxWorkday.Text = "工作日";
                }
                else
                {
                    this.GbxNonworkdays.Enabled = false;
                    this.GbxWorkday.Text = "";
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 工作日是否区分高峰时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkWorkPeak_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ChkWorkPeak.Checked)
                {
                    this.GbxWorkNonpeak.Enabled = true;
                    this.GbxWorkPeak.Text = "高峰时段";
                    this.DtpWorkPeakStart.Enabled = true;
                    this.LblWorkPeakLine.Enabled = true;
                    this.DtpWorkPeakEnd.Enabled = true;
                }
                else
                {
                    this.GbxWorkNonpeak.Enabled = false;
                    this.GbxWorkPeak.Text = "";
                    this.DtpWorkPeakStart.Enabled = false;
                    this.LblWorkPeakLine.Enabled = false;
                    this.DtpWorkPeakEnd.Enabled = false;
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 非工作日是否区分高峰时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkNonworkPeak_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ChkNonworkPeak.Checked)
                {
                    this.GbxNonworkNonpeak.Enabled = true;
                    this.GbxNonworkPeak.Text = "高峰时段";
                    this.DtpNonworkPeakStart.Enabled = true;
                    this.LblNonworkPeakLine.Enabled = true;
                    this.DtpNonworkPeakEnd.Enabled = true;
                }
                else
                {
                    this.GbxNonworkNonpeak.Enabled = false;
                    this.GbxNonworkPeak.Text = "";
                    this.DtpNonworkPeakStart.Enabled = false;
                    this.LblNonworkPeakLine.Enabled = false;
                    this.DtpNonworkPeakEnd.Enabled = false;
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 工作日高峰时段是否区分首单位cheaked变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkWorkPeakFUnit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ChkWorkPeakFUnit.Checked)
                {
                    this.LblWorkPeakInFee.Text = "首" + this.CboWorkPeakFUnit.Text + "内单价";
                    this.LblWorkPeakOutFee.Text = "首" + this.CboWorkPeakFUnit.Text + "外单价";
                    this.LblWorkPeakInFee.Enabled = true;
                    this.TxtWorkPeakInFee.Enabled = true;
                    this.CboWorkPeakInFee.Enabled = true;
                    this.CboWorkPeakFUnit.Enabled = true;
                }
                else
                {
                    this.LblWorkPeakOutFee.Text = "单价";
                    this.LblWorkPeakInFee.Enabled = false;
                    this.TxtWorkPeakInFee.Enabled = false;
                    this.CboWorkPeakInFee.Enabled = false;
                    this.CboWorkPeakFUnit.Enabled = false;
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 工作日非高峰时段是否区分首单位cheaked变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkWorkNonpeakFUnit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ChkWorkNonpeakFUnit.Checked)
                {
                    this.LblWorkNonpeakInFee.Text = "首" + this.CboWorkNonpeakFUnit.Text + "内单价";
                    this.LblWorkNonpeakOutFee.Text = "首" + this.CboWorkNonpeakFUnit.Text + "外单价";
                    this.LblWorkNonpeakInFee.Enabled = true;
                    this.TxtWorkNonpeakInFee.Enabled = true;
                    this.CboWorkNonpeakInFee.Enabled = true;
                    this.CboWorkNonpeakFUnit.Enabled = true;
                }
                else
                {
                    this.LblWorkNonpeakOutFee.Text = "单价";
                    this.LblWorkNonpeakInFee.Enabled = false;
                    this.TxtWorkNonpeakInFee.Enabled = false;
                    this.CboWorkNonpeakInFee.Enabled = false;
                    this.CboWorkNonpeakFUnit.Enabled = false;
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 非工作日高峰时段是否区分首单位cheaked变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkNonworkPeakFUnit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ChkNonworkPeakFUnit.Checked)
                {
                    this.LblNonworkPeakInFee.Text = "首" + this.CboNonworkPeakFUnit.Text + "内单价";
                    this.LblNonworkPeakOutFee.Text = "首" + this.CboNonworkPeakFUnit.Text + "外单价";
                    this.LblNonworkPeakInFee.Enabled = true;
                    this.TxtNonworkPeakInFee.Enabled = true;
                    this.CboNonworkPeakInFee.Enabled = true;
                    this.CboNonworkPeakFUnit.Enabled = true;
                }
                else
                {
                    this.LblNonworkPeakOutFee.Text = "单价";
                    this.LblNonworkPeakInFee.Enabled = false;
                    this.TxtNonworkPeakInFee.Enabled = false;
                    this.CboNonworkPeakInFee.Enabled = false;
                    this.CboNonworkPeakFUnit.Enabled = false;
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 非工作日非高峰时段是否区分首单位cheaked变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkNonworkNonpeakFUnit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ChkNonworkNonpeakFUnit.Checked)
                {
                    this.LblNonworkNonpeakInFee.Text = "首" + this.CboNonworkNonpeakFUnit.Text + "内单价";
                    this.LblNonworkNonpeakOutFee.Text = "首" + this.CboNonworkNonpeakFUnit.Text + "外单价";
                    this.LblNonworkNonpeakInFee.Enabled = true;
                    this.TxtNonworkNonpeakInFee.Enabled = true;
                    this.CboNonworkNonpeakInFee.Enabled = true;
                    this.CboNonworkNonpeakFUnit.Enabled = true;
                }
                else
                {
                    this.LblNonworkNonpeakOutFee.Text = "单价";
                    this.LblNonworkNonpeakInFee.Enabled = false;
                    this.TxtNonworkNonpeakInFee.Enabled = false;
                    this.CboNonworkNonpeakInFee.Enabled = false;
                    this.CboNonworkNonpeakFUnit.Enabled = false;
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 高峰时段与非高峰时段时间处理
        /// <summary>
        /// 工作日高峰时段起始时间文本改变触发(赋值给工作日非高峰时段结束时间)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpWorkPeakStart_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.DtpWorkNonpeakEnd.Value = this.DtpWorkPeakStart.Value;
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 工作日高峰时段结束时间文本改变触发(赋值给工作日非高峰时段起始时间)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpWorkPeakEnd_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.DtpWorkNonpeakStart.Value = this.DtpWorkPeakEnd.Value;
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 非工作日高峰时段起始时间文本改变触发(赋值给非工作日非高峰时段结束时间)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpNonworkPeakStart_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.DtpNonworkNonpeakEnd.Value = this.DtpNonworkPeakStart.Value;
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 非工作日高峰时段结束时间文本改变触发(赋值给非工作日非高峰时段起始时间)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpNonworkPeakEnd_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.DtpNonworkNonpeakStart.Value = this.DtpNonworkPeakEnd.Value;
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 事件订阅
        /// <summary>
        /// 控件事件绑定
        /// </summary>
        private void EventHandlerGroup()
        {
            this.ChkWorkday.CheckedChanged += new System.EventHandler(this.ChkWorkday_CheckedChanged);
            this.ChkWorkPeak.CheckedChanged += new System.EventHandler(this.ChkWorkPeak_CheckedChanged);
            this.ChkNonworkPeak.CheckedChanged += new System.EventHandler(this.ChkNonworkPeak_CheckedChanged);
            this.ChkWorkPeakFUnit.CheckedChanged += new System.EventHandler(this.ChkWorkPeakFUnit_CheckedChanged);
            this.ChkWorkNonpeakFUnit.CheckedChanged += new System.EventHandler(this.ChkWorkNonpeakFUnit_CheckedChanged);
            this.ChkNonworkPeakFUnit.CheckedChanged += new System.EventHandler(this.ChkNonworkPeakFUnit_CheckedChanged);
            this.ChkNonworkNonpeakFUnit.CheckedChanged += new System.EventHandler(this.ChkNonworkNonpeakFUnit_CheckedChanged);
            this.DtpWorkPeakStart.ValueChanged += new System.EventHandler(this.DtpWorkPeakStart_ValueChanged);
            this.DtpWorkPeakEnd.ValueChanged += new System.EventHandler(this.DtpWorkPeakEnd_ValueChanged);
            this.DtpNonworkPeakStart.ValueChanged += new System.EventHandler(this.DtpNonworkPeakStart_ValueChanged);
            this.DtpNonworkPeakEnd.ValueChanged += new System.EventHandler(this.DtpNonworkPeakEnd_ValueChanged);
            this.CboWorkPeakFUnit.SelectedIndexChanged += new System.EventHandler(this.ChkWorkPeakFUnit_CheckedChanged);
            this.CboWorkNonpeakFUnit.SelectedIndexChanged += new System.EventHandler(this.ChkWorkNonpeakFUnit_CheckedChanged);
            this.CboNonworkPeakFUnit.SelectedIndexChanged += new System.EventHandler(this.ChkNonworkPeakFUnit_CheckedChanged);
            this.CboNonworkNonpeakFUnit.SelectedIndexChanged += new System.EventHandler(this.ChkNonworkNonpeakFUnit_CheckedChanged);
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 转换字符串单位为以分钟为单元的具体值
        /// </summary>
        /// <param name="strUnit"></param>
        /// <returns></returns>
        private int ConvertUnitToInt(string strUnit)
        {
            int nUnit = 30;// 默认30分钟
            int nIndex = strUnit.IndexOf("/");

            // 获取单位值
            strUnit = strUnit.Substring(nIndex + 1);
            if (strUnit.Contains("分钟"))
            {
                nIndex = strUnit.IndexOf("分钟");
                strUnit = strUnit.Replace("分钟", "");
                if (string.IsNullOrEmpty(strUnit))
                {
                    nUnit = 1;
                }
                else
                {
                    CBaseMethods.MyBase.StringToUInt32(strUnit, out nUnit);
                }
            }
            else if (strUnit.Contains("小时"))
            {
                nIndex = strUnit.IndexOf("小时");
                strUnit = strUnit.Replace("小时", "");
                if (string.IsNullOrEmpty(strUnit))
                {
                    nUnit = 1;
                }
                else
                {
                    CBaseMethods.MyBase.StringToUInt32(strUnit, out nUnit);
                }

                nUnit = nUnit * 60;
            }
            return nUnit;
        }
        #endregion
    }
}