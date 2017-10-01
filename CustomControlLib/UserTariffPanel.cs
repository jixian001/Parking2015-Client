using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using System.ServiceModel;
using CarLocationPanelLib;
using System.Drawing;

namespace CustomControlLib
{
    public class CUserTariffPanel: Panel
    {
        #region 自定义事件
        /// <summary>
        /// 修改界面计费标准列表行数据事件(包含添加)
        /// </summary>
        public event EventHandler ModifyDgvTariffRow;
        /// <summary>
        /// 取消按钮事件
        /// </summary>
        public event EventHandler BtnCancelClick;
        #endregion

        private System.Windows.Forms.Label LblCardType;
        private System.Windows.Forms.ComboBox CboCardType;
        private System.Windows.Forms.Label LblFeeTypeTariff;
        private System.Windows.Forms.ComboBox CboFeeTypeTariff;
        private System.Windows.Forms.Label LblTariffDescp;
        private System.Windows.Forms.TextBox TxtTariffDescp;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label LblFee;
        private System.Windows.Forms.TextBox TxtFee;
        private System.Windows.Forms.Label LblUnit;
        private CUserTariffTempPanel CuttpHour;
        private System.Windows.Forms.Label LblICCardStart;
        private System.Windows.Forms.DateTimePicker DtpICCardStart;
        private System.Windows.Forms.Label LblICCardEnd;
        private System.Windows.Forms.DateTimePicker DtpICCardEnd;
        private System.Windows.Forms.Label LblTariff;
        private System.Windows.Forms.ComboBox CboTariff;

        public CUserTariffPanel()
            :base()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.LblFee = new System.Windows.Forms.Label();
            this.TxtFee = new System.Windows.Forms.TextBox();
            this.CboFeeTypeTariff = new System.Windows.Forms.ComboBox();
            this.LblFeeTypeTariff = new System.Windows.Forms.Label();
            this.CboCardType = new System.Windows.Forms.ComboBox();
            this.LblCardType = new System.Windows.Forms.Label();
            this.LblUnit = new System.Windows.Forms.Label();
            this.CuttpHour = new CUserTariffTempPanel();
            this.LblTariffDescp = new System.Windows.Forms.Label();
            this.TxtTariffDescp = new System.Windows.Forms.TextBox();
            this.LblICCardStart = new System.Windows.Forms.Label();
            this.DtpICCardStart = new System.Windows.Forms.DateTimePicker();
            this.LblICCardEnd = new System.Windows.Forms.Label();
            this.DtpICCardEnd = new System.Windows.Forms.DateTimePicker();
            this.LblTariff = new System.Windows.Forms.Label();
            this.CboTariff = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // PanelTariff
            // 
            this.Controls.Add(this.LblTariff);
            this.Controls.Add(this.CboTariff);
            this.Controls.Add(this.LblCardType);
            this.Controls.Add(this.CboCardType);
            this.Controls.Add(this.LblFeeTypeTariff);
            this.Controls.Add(this.CboFeeTypeTariff);
            this.Controls.Add(this.LblTariffDescp);
            this.Controls.Add(this.TxtTariffDescp);
            this.Controls.Add(this.LblICCardStart);
            this.Controls.Add(this.DtpICCardStart);
            this.Controls.Add(this.LblICCardEnd);
            this.Controls.Add(this.DtpICCardEnd);
            this.Controls.Add(this.LblFee);
            this.Controls.Add(this.TxtFee);
            this.Controls.Add(this.LblUnit);
            this.Controls.Add(this.CuttpHour);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnDelete);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PanelTariff";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(800, 480);
            this.TabIndex = 26;
            this.TabStop = false;
            this.Text = "计费";
            // 
            // LblTariff
            // 
            this.LblTariff.Location = new System.Drawing.Point(2, 10);
            this.LblTariff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTariff.Name = "LblCardType";
            this.LblTariff.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblTariff.Size = new System.Drawing.Size(98, 31);
            this.LblTariff.TabIndex = 0;
            this.LblTariff.Text = "计费标准";
            // 
            // CboCardType
            // 
            this.CboTariff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTariff.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboTariff.FormattingEnabled = true;
            this.CboTariff.Location = new System.Drawing.Point(100, 10);
            this.CboTariff.Margin = new System.Windows.Forms.Padding(4);
            this.CboTariff.Name = "CboTariff";
            this.CboTariff.Size = new System.Drawing.Size(160, 24);
            this.CboTariff.TabIndex = 1;
            this.CboTariff.DisplayMember = "tariffdescp";
            this.CboTariff.ValueMember = "id";
            this.CboTariff.SelectedIndexChanged += new EventHandler(CboTariff_SelectedIndexChanged);
            // 
            // LblCardType
            // 
            this.LblCardType.Location = new System.Drawing.Point(2, 46);
            this.LblCardType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblCardType.Name = "LblCardType";
            this.LblCardType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblCardType.Size = new System.Drawing.Size(98, 31);
            this.LblCardType.TabIndex = 0;
            this.LblCardType.Text = "卡类型";
            // 
            // CboCardType
            // 
            this.CboCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboCardType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboCardType.FormattingEnabled = true;
            this.CboCardType.Items.AddRange(new object[] {
            "临时卡",
            "定期卡",
            "固定车位卡"});
            this.CboCardType.Location = new System.Drawing.Point(100, 46);
            this.CboCardType.Margin = new System.Windows.Forms.Padding(4);
            this.CboCardType.Name = "CboCardType";
            this.CboCardType.Size = new System.Drawing.Size(160, 24);
            this.CboCardType.TabIndex = 1;
            this.CboCardType.SelectedIndexChanged += new System.EventHandler(this.CboCardType_SelectedIndexChanged);
            // 
            // LblFeeTypeTariff
            // 
            this.LblFeeTypeTariff.Location = new System.Drawing.Point(260, 46);
            this.LblFeeTypeTariff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblFeeTypeTariff.Name = "LblFeeTypeTariff";
            this.LblFeeTypeTariff.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblFeeTypeTariff.Size = new System.Drawing.Size(100, 31);
            this.LblFeeTypeTariff.TabIndex = 2;
            this.LblFeeTypeTariff.Text = "计费类型";
            // 
            // CboFeeTypeTariff
            // 
            this.CboFeeTypeTariff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboFeeTypeTariff.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboFeeTypeTariff.FormattingEnabled = true;
            this.CboFeeTypeTariff.Items.AddRange(new object[] {
            "小时卡",
            "月卡",
            "季卡",
            "年卡"});
            this.CboFeeTypeTariff.Location = new System.Drawing.Point(360, 46);
            this.CboFeeTypeTariff.Margin = new System.Windows.Forms.Padding(4);
            this.CboFeeTypeTariff.Name = "CboFeeTypeTariff";
            this.CboFeeTypeTariff.Size = new System.Drawing.Size(160, 24);
            this.CboFeeTypeTariff.TabIndex = 3;
            this.CboFeeTypeTariff.SelectedIndexChanged += new System.EventHandler(this.CboFeeTypeTariff_SelectedIndexChanged);
            // 
            // LblTariffDescp
            // 
            this.LblTariffDescp.Location = new System.Drawing.Point(2, 80);
            this.LblTariffDescp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTariffDescp.Name = "LblTariffDescp";
            this.LblTariffDescp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblTariffDescp.Size = new System.Drawing.Size(98, 31);
            this.LblTariffDescp.TabIndex = 4;
            this.LblTariffDescp.Text = "计费描述";
            // 
            // TxtTariffDescp
            // 
            this.TxtTariffDescp.Location = new System.Drawing.Point(100, 80);
            this.TxtTariffDescp.Margin = new System.Windows.Forms.Padding(4);
            this.TxtTariffDescp.Name = "TxtTariffDescp";
            this.TxtTariffDescp.Size = new System.Drawing.Size(324, 26);
            this.TxtTariffDescp.TabIndex = 5;
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(300, 10);
            this.BtnOK.Margin = new System.Windows.Forms.Padding(4);
            this.BtnOK.Name = "BtnAdd";
            this.BtnOK.Size = new System.Drawing.Size(100, 31);
            this.BtnOK.TabIndex = 6;
            this.BtnOK.Text = "添加";//"添加/修改";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(420, 10);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(100, 31);
            this.BtnDelete.TabIndex = 7;
            this.BtnDelete.Text = "删除";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(670, 46);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(100, 31);
            this.BtnCancel.TabIndex = 8;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // LblICCardStart
            // 
            this.LblICCardStart.Location = new System.Drawing.Point(117, 160);
            this.LblICCardStart.Name = "LblICCardStart";
            this.LblICCardStart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblICCardStart.Size = new System.Drawing.Size(99, 26);
            this.LblICCardStart.TabIndex = 15;
            this.LblICCardStart.Text = "起始日期";
            this.LblICCardStart.Visible = false;
            // 
            // DtpICCardStart
            // 
            this.DtpICCardStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DtpICCardStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpICCardStart.Location = new System.Drawing.Point(217, 160);
            this.DtpICCardStart.Name = "DtpICCardStart";
            this.DtpICCardStart.Size = new System.Drawing.Size(220, 26);
            this.DtpICCardStart.TabIndex = 9;
            this.DtpICCardStart.Visible = false;
            // 
            // LblICCardEnd
            // 
            this.LblICCardEnd.Location = new System.Drawing.Point(117, 190);
            this.LblICCardEnd.Name = "LblICCardEnd";
            this.LblICCardEnd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblICCardEnd.Size = new System.Drawing.Size(99, 26);
            this.LblICCardEnd.TabIndex = 16;
            this.LblICCardEnd.Text = "截止日期";
            this.LblICCardEnd.Visible = false;
            // 
            // DtpICCardEnd
            // 
            this.DtpICCardEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DtpICCardEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpICCardEnd.Location = new System.Drawing.Point(217, 190);
            this.DtpICCardEnd.Name = "DtpICCardEnd";
            this.DtpICCardEnd.Size = new System.Drawing.Size(220, 26);
            this.DtpICCardEnd.TabIndex = 10;
            this.DtpICCardEnd.Visible = false;
            // 
            // LblFee
            // 
            this.LblFee.Location = new System.Drawing.Point(117, 220);
            this.LblFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblFee.Name = "LblFee";
            this.LblFee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblFee.Size = new System.Drawing.Size(98, 31);
            this.LblFee.TabIndex = 11;
            this.LblFee.Text = "费用";
            this.LblFee.Visible = false;
            // 
            // TxtFee
            // 
            this.TxtFee.Location = new System.Drawing.Point(217, 220);
            this.TxtFee.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFee.Name = "TxtFee";
            this.TxtFee.Size = new System.Drawing.Size(324, 26);
            this.TxtFee.TabIndex = 12;
            this.TxtFee.Visible = false;
            // 
            // LblUnit
            // 
            this.LblUnit.Location = new System.Drawing.Point(541, 220);
            this.LblUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblUnit.Name = "LblUnit";
            this.LblUnit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblUnit.Size = new System.Drawing.Size(100, 31);
            this.LblUnit.TabIndex = 13;
            this.LblUnit.Text = "元";
            this.LblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblUnit.Visible = false;
            // 
            // CuttpHour
            // 
            this.CuttpHour.Location = new System.Drawing.Point(0, 110);
            this.CuttpHour.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CuttpHour.Name = "CuttpHour";
            this.CuttpHour.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CuttpHour.Size = new System.Drawing.Size(840, 400);
            this.CuttpHour.TabIndex = 14;
            this.CuttpHour.Text = "计时卡布局";
            this.CuttpHour.Visible = true;
            this.ResumeLayout(false);
        }

        #region 公有函数
        /// <summary>
        /// 登陆界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CUserTariffPanel_Load(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                // 查询所有标准收费信息
                List<CTariffDto> lstTariff = proxy.GetTariffList(); 
                this.CboTariff.Items.Clear();
                // 添加列表信息
                this.CboTariff.Items.AddRange(lstTariff.ToArray());
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
            proxy.Close();
        }

        /// <summary>
        /// 窗体首次显示时触发(窗体大小改变触发 OnShown-OnResize)
        /// </summary>
        /// <param name="e"></param>
        public void OnResize()
        {
            int width = this.ClientSize.Width > CStaticClass.ConfigMinWidth() ? this.ClientSize.Width : CStaticClass.ConfigMinWidth();
            int height = this.ClientSize.Height > CStaticClass.ConfigMinHeight() ? this.ClientSize.Height : CStaticClass.ConfigMinHeight();
            int gap = CStaticClass.ConfigMainGap();
            int minGap = CStaticClass.ConfigMinGap();

            // 计费标准第一行
            int nWithLen = this.LblCardType.Width + this.CboCardType.Width + this.LblFeeTypeTariff.Width + this.CboFeeTypeTariff.Width + this.BtnOK.Width + this.BtnCancel.Width + 5 * gap;
            nWithLen = Math.Max(nWithLen, this.CuttpHour.Width + gap);
            int nLeft = Math.Max((width - nWithLen) / 2, minGap);
            int nHeightLen = this.LblCardType.Height + this.LblTariffDescp.Height + this.CuttpHour.Height + 3 * gap;
            int nTop = Math.Max((height - nHeightLen) / 2, minGap);
            this.LblTariff.Location = new Point(nLeft, nTop);
            this.CboTariff.Location = new Point(LblTariff.Location.X + LblTariff.Width, nTop);
            this.BtnOK.Location = new Point(CboTariff.Location.X + CboTariff.Width + gap, nTop);
            this.BtnDelete.Location = new Point(BtnOK.Location.X + BtnOK.Width + 2 * gap, nTop);
            // 卡类型、计费类型第二行
            nTop += this.LblTariff.Height + gap;
            this.LblCardType.Location = new Point(nLeft, nTop);
            this.CboCardType.Location = new Point(LblCardType.Location.X + LblCardType.Width, nTop);
            this.LblFeeTypeTariff.Location = new Point(CboCardType.Location.X + CboCardType.Width + gap, nTop);
            this.CboFeeTypeTariff.Location = new Point(LblFeeTypeTariff.Location.X + LblFeeTypeTariff.Width, nTop);
            // 计费描述第三行
            nTop += this.LblCardType.Height + gap;
            this.LblTariffDescp.Location = new Point(nLeft, nTop);
            this.TxtTariffDescp.Location = new Point(LblTariffDescp.Location.X + LblTariffDescp.Width, nTop);
            // 临时卡-计时卡 
            nTop += this.LblTariffDescp.Height + gap;
            this.CuttpHour.Location = new Point(nLeft, nTop);
            // 固定卡费用
            this.LblICCardStart.Location = new Point(nLeft, nTop);
            this.DtpICCardStart.Location = new Point(LblICCardStart.Location.X + LblICCardStart.Width, nTop);
            nTop += this.LblICCardStart.Height + gap;
            this.LblICCardEnd.Location = new Point(nLeft, nTop);
            this.DtpICCardEnd.Location = new Point(LblICCardEnd.Location.X + LblICCardEnd.Width, nTop);
            nTop += this.LblICCardEnd.Height + gap;
            this.LblFee.Location = new Point(nLeft, nTop);
            this.TxtFee.Location = new Point(LblFee.Location.X + LblFee.Width, nTop);
            this.LblUnit.Location = new Point(TxtFee.Location.X + TxtFee.Width, nTop);
        }

        /// <summary>
        /// 固定卡时收费标准“卡类型”值
        /// </summary>
        public void SetCboCardType(EnmICCardType cardType)
        {
            this.CboCardType.Items.Clear();

            switch (cardType)
            {
                case EnmICCardType.Temp:// 临时卡
                    {
                        this.CboCardType.Items.AddRange(new object[] { "临时卡" });
                        break;
                    }
                case EnmICCardType.Fixed:
                    {
                        this.CboCardType.Items.AddRange(new object[] { "定期卡", "固定车位卡" });
                        break;
                    }
                case EnmICCardType.FixedLocation:
                    {
                        this.CboCardType.Items.AddRange(new object[] { "定期卡", "固定车位卡" });
                        break;
                    }
                default:
                    {
                        this.CboCardType.Items.AddRange(new object[] { "临时卡", "定期卡", "固定车位卡" });
                        break;
                    }
            }
        }
       
        /// <summary>
        /// 设置收费标准实例
        /// </summary>
        /// <returns></returns>
        public void SetTariffInfo(CTariffDto tariff)
        {
            try
            {
                this.Tag = 0;
                this.CboCardType.SelectedIndex = -1;
                this.CboFeeTypeTariff.SelectedIndex = -1;
                this.TxtFee.Text = "";
                this.DtpICCardStart.Value = CStaticClass.DefDatetime;
                this.DtpICCardEnd.Value = CStaticClass.DefDatetime;

                if (null == tariff)
                {
                    SetTempHourControl(false);
                    return;
                }

                CuttpHour.SetTariffInfo(tariff);
                this.Tag = tariff.id;
                this.CboCardType.Text = CStaticClass.ConvertICCardType(tariff.iccardtype);
                this.CboFeeTypeTariff.Text = CStaticClass.ConvertFeeType(tariff.feetype);
                this.TxtFee.Text = tariff.fee.ToString();
                this.TxtTariffDescp.Text = tariff.tariffdescp;
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 设置固定卡起始日期和截止日期可见否
        /// </summary>
        /// <param name="flag"></param>
        public void SetFixVisable(bool flag)
        {
            this.LblICCardStart.Visible = flag;
            this.DtpICCardStart.Visible = flag;
            this.LblICCardEnd.Visible = flag;
            this.DtpICCardEnd.Visible = flag;

            this.Enabled = flag;
            if (flag)
            {
                this.CboCardType.Enabled = !flag;
                this.CboFeeTypeTariff.Enabled = !flag;
                this.TxtTariffDescp.Enabled = !flag;
                this.BtnOK.Enabled = !flag;
                this.TxtFee.Enabled = !flag;
                this.DtpICCardStart.Value = CStaticClass.CurruntDateTime();

                if (string.IsNullOrEmpty(this.CboFeeTypeTariff.Text))
                {
                    this.DtpICCardEnd.Value = CStaticClass.CurruntDateTime();
                }
                else if (this.CboFeeTypeTariff.Text.Contains("月"))
                {
                    this.DtpICCardEnd.Value = CStaticClass.CurruntDateTime().AddMonths(1);
                }
                else if (this.CboFeeTypeTariff.Text.Contains("季"))
                {
                    this.DtpICCardEnd.Value = CStaticClass.CurruntDateTime().AddMonths(3);
                }
                else if (this.CboFeeTypeTariff.Text.Contains("年"))
                {
                    this.DtpICCardEnd.Value = CStaticClass.CurruntDateTime().AddMonths(12);
                }
            }
        }

        /// <summary>
        /// 获取固定卡起始时间和截止时间
        /// </summary>
        /// <returns></returns>
        public void GetFixDateTime(out DateTime fixStartTime, out DateTime fixEndTime)
        {
            fixStartTime = this.DtpICCardStart.Value;
            fixEndTime = this.DtpICCardEnd.Value;
        }
        #endregion

        #region 组合框选项变化、button按钮单击触发事件
        /// <summary>
        /// 卡类型文本改变触发 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.CboFeeTypeTariff.Items.Clear();
                EnmICCardType eType = CStaticClass.ConvertICCardType(this.CboCardType.Text);

                if (EnmICCardType.Temp == eType)
                {
                    this.CboFeeTypeTariff.Items.AddRange(new object[] { "小时卡" });
                    SetTempHourControl(true);
                }
                else
                {
                    this.CboFeeTypeTariff.Items.AddRange(new object[] { "月卡", "季卡", "年卡" });
                    SetTempHourControl(false);
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
        /// 计费类型文本改变触发 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboFeeTypeTariff_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EnmFeeType eType = (EnmFeeType)CStaticClass.ConvertFeeType(this.CboFeeTypeTariff.Text);

                if (EnmFeeType.Hour == eType)
                {
                    SetTempHourControl(true);
                }
                else
                {
                    SetTempHourControl(false);
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
        /// 计费标准文本改变触发 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTariff_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CTariffDto tariff = (CTariffDto)this.CboTariff.SelectedItem; 
                SetTariffInfo(tariff);
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
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (string.IsNullOrEmpty(this.CboCardType.Text) || string.IsNullOrEmpty(this.CboFeeTypeTariff.Text))
                {
                    MessageBox.Show("IC卡类型、计费类型都不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CTariffDto tariff = GetTariffInfo();
                EnmFaultType type = proxy.SaveTariff(tariff);
                switch (type)
                {
                    case EnmFaultType.Success:
                        {
                            if (null != ModifyDgvTariffRow)
                            {
                                ModifyDgvTariffRow(tariff, e);
                            }
                            tariff.id = this.CboTariff.Items.Count + 1;
                            this.CboTariff.Items.Add(tariff);
                            SetTariffInfo(null);
                            MessageBox.Show("添加/修改成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case EnmFaultType.FailToInsert:
                        {
                            MessageBox.Show("插入数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("保存失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
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
            proxy.Close();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (null == this.CboTariff.SelectedItem || typeof(CTariffDto) != this.CboTariff.SelectedItem.GetType())
                {
                    MessageBox.Show("计费标准不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CTariffDto tariff = (CTariffDto)this.CboTariff.SelectedItem;
                EnmFaultType type = proxy.DeleteTariff(tariff);
                switch (type)
                {
                    case EnmFaultType.Success:
                        {
                            this.CboTariff.Items.Remove(tariff);
                            foreach(CTariffDto dto in this.CboTariff.Items)
                            {
                                if (dto.id > tariff.id)
                                {
                                    dto.id -= 1;
                                }
                            }

                            SetTariffInfo(null);
                            MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case EnmFaultType.FailToDelete:
                        {
                            MessageBox.Show("删除数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("删除失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
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
            proxy.Close();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (null != BtnCancelClick)
            {
                BtnCancelClick(sender, e);
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 获取收费标准实例
        /// </summary>
        /// <returns></returns>
        private CTariffDto GetTariffInfo()
        {
            CTariffDto tariff = new CTariffDto();
            //if (null != this.Tag && typeof(int) == this.Tag.GetType())
            //{
            //    tariff.id = (int)this.Tag;
            //}
            tariff.iccardtype = (int)CStaticClass.ConvertICCardType(this.CboCardType.Text);
            tariff.feetype = (int)CStaticClass.ConvertFeeType(this.CboFeeTypeTariff.Text);
            tariff.tariffdescp = this.TxtTariffDescp.Text;
            float fee;

            if (this.TxtFee.Visible)
            {
                float.TryParse(this.TxtFee.Text.Trim(), out fee);
                tariff.fee = fee;// 固定卡总费用
                return tariff;
            }

            if (CuttpHour.Visible)
            {
                CuttpHour.GetTariffInfo(ref tariff);
            }
            return tariff;
        }

        /// <summary>
        /// 设置小时卡控件可见（其他类型不可见）
        /// </summary>
        /// <param name="flag"></param>
        private void SetTempHourControl(bool flag)
        {
            this.CuttpHour.Visible = flag;

            this.TxtFee.Visible = !flag;
            this.LblFee.Visible = !flag;
            this.LblUnit.Visible = !flag;
            //this.LblICCardStart.Visible = !flag;
            //this.DtpICCardStart.Visible = !flag;
            //this.LblICCardEnd.Visible = !flag;
            //this.DtpICCardEnd.Visible = !flag;
        }
        #endregion
    }
}
