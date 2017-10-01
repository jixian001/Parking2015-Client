using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;

namespace WindowsFormLib
{
    public partial class CFormFindStatistics : Form
    {
        private CFormReport m_formReport = new CFormReport();
        List<CICCardLogDto> m_lstICCardLog = new List<CICCardLogDto>();
        List<CSystemLogDto> m_lstSystemLog = new List<CSystemLogDto>();
        List<CDeviceFaultLogDto> m_lstDeviceFaultLog = new List<CDeviceFaultLogDto>();
        List<CTelegramLogDto> m_lstTelegramLog = new List<CTelegramLogDto>();
        List<CDeviceStatusLogDto> m_lstDeviceStatusLog = new List<CDeviceStatusLogDto>();

        public CFormFindStatistics()
        {
            InitializeComponent();
            try
            {
                this.DgvICCard.AutoGenerateColumns = false;
                this.DgvDo.AutoGenerateColumns = false;
                this.DgvFault.AutoGenerateColumns = false;
                this.DgvBusiness.AutoGenerateColumns = false;
                this.DgvStatus.AutoGenerateColumns = false;
                this.DgvICCard.DataSourceChanged += new EventHandler(this.CupttsICCard.UpdateLayout);
                this.DgvDo.DataSourceChanged += new EventHandler(this.CupttsDo.UpdateLayout);
                this.DgvFault.DataSourceChanged += new EventHandler(this.CupttsFault.UpdateLayout);
                this.DgvBusiness.DataSourceChanged += new EventHandler(this.CupttsBusiness.UpdateLayout);
                this.DgvStatus.DataSourceChanged += new EventHandler(this.CupttsStatus.UpdateLayout);
                this.CboICCardCondition.Text = "所有";
                this.CboDoCondition.Text = "所有";
                this.CboFaultCondition.Text = "所有";
                this.CboBusinessCondition.Text = "所有";
                this.CboStatusCondition.Text = "所有";

                // 设置键盘“Esc”按钮
                Button BtnCancel = new Button();
                this.CancelButton = BtnCancel;
                BtnCancel.Click += new EventHandler(BtnCancel_Click);

                if (!CStaticClass.ConfigBillingFlag())
                {// 没有收费管理系统,  删除IC卡缴费日志查询
                    this.tabControl1.Controls.Remove(this.tabPage1);
                }
                CFormFindStatistics_Load(null, null);
            }
            catch { }
        }

        /// <summary>
        /// 窗体首次显示时触发(窗体大小改变触发 OnResize  OnShown)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            int minGap = CStaticClass.ConfigMinGap();
            int nGap = 4 * minGap;

            this.tabControl1.Size = new Size(this.Width - nGap, this.Height - 2 * nGap);
            this.tabPage1.Size = new Size(this.Width - nGap, this.Height - 3 * nGap);
            int nWidth = this.tabPage1.Width - minGap;
            int nHeight = this.tabPage1.Height;

            // tabPage1-IC卡日志
            this.GbICCardFind.Size = new Size(nWidth, this.GbICCardFind.Height);
            this.GbICCardLst.Size = new Size(nWidth, nHeight - this.GbICCardLst.Location.Y);
            this.DgvICCard.Size = new Size(nWidth - minGap, this.GbICCardLst.Height - this.DgvICCard.Location.Y - this.CupttsICCard.Height - minGap);
            this.CupttsICCard.Location = new Point(4, this.DgvICCard.Location.Y + this.DgvICCard.Height + minGap);// 翻页

            // tabPage2-操作日志
            this.tabPage2.Size = this.tabPage1.Size;
            this.GbDoFind.Size = this.GbICCardFind.Size;
            this.GbDoLst.Size = this.GbICCardLst.Size;
            this.DgvDo.Size = this.DgvICCard.Size;
            this.CupttsDo.Location = this.CupttsICCard.Location;// 翻页

            // tabPage3-故障日志
            this.tabPage3.Size = this.tabPage1.Size;
            this.GbFaultFind.Size = this.GbICCardFind.Size;
            this.GbFaultLst.Size = this.GbICCardLst.Size;
            this.DgvFault.Size = this.DgvICCard.Size;
            this.CupttsFault.Location = this.CupttsICCard.Location;// 翻页

            // tabPage4-报文日志
            this.tabPage4.Size = this.tabPage1.Size;
            this.GbBusinessFind.Size = this.GbICCardFind.Size;
            this.GbBusinessLst.Size = this.GbICCardLst.Size;
            this.DgvBusiness.Size = this.DgvICCard.Size;
            this.CupttsBusiness.Location = this.CupttsICCard.Location;// 翻页

            // tabPage5-设备状态日志
            this.tabPage5.Size = this.tabPage1.Size;
            this.GbStatusFind.Size = this.GbICCardFind.Size;
            this.GbStatusLst.Size = this.GbICCardLst.Size;
            this.DgvStatus.Size = this.DgvICCard.Size;
            this.CupttsStatus.Location = this.CupttsICCard.Location;// 翻页
        }

        #region 事件处理
        /// <summary>
        /// 登陆界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CFormFindStatistics_Load(object sender, EventArgs e)
        {
            this.DtpICCardStart.Value = CStaticClass.CurruntDateTime().AddMinutes(-30);
            this.DtpDoStart.Value = CStaticClass.CurruntDateTime().AddMinutes(-30);
            this.DtpFaultStart.Value = CStaticClass.CurruntDateTime().AddMinutes(-30);
            this.DtpBusinessStart.Value = CStaticClass.CurruntDateTime().AddMinutes(-30);
            this.DtpStatusStart.Value = CStaticClass.CurruntDateTime().AddMinutes(-30);
            this.DtpICCardEnd.Value = CStaticClass.CurruntDateTime();
            this.DtpDoEnd.Value = CStaticClass.CurruntDateTime();
            this.DtpFaultEnd.Value = CStaticClass.CurruntDateTime();
            this.DtpBusinessEnd.Value = CStaticClass.CurruntDateTime();
            this.DtpStatusEnd.Value = CStaticClass.CurruntDateTime();
            /*QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                DateTime dtStart = this.DtpTempStart.Value;// 起始日期
                DateTime dtEnd = this.DtpTempEnd.Value;// 截止日期
                if (CStaticClass.ConfigBillingFlag() && this.CboTempCondition.SelectedIndex + 1 == this.CboTempCondition.Items.Count)
                { // 获取所有IC卡缴费日志列表
                    m_lstICCardLog = proxy.GetICCardLogList();
                    m_lstICCardLog = m_lstICCardLog.FindAll(s => CompareDateTime(s.starttime, dtStart, dtEnd));
                    this.DgvICCard.DataSource = new BindingList<CICCardLogDto>(m_lstICCardLog);
                }
                if (this.CboDoCondition.SelectedIndex + 1 == this.CboDoCondition.Items.Count)
                {// 获取所有系统日志列表
                    m_lstSystemLog = proxy.GetSystemLogList();
                    m_lstSystemLog = m_lstSystemLog.FindAll(s => CompareDateTime(s.curtime, dtStart, dtEnd));
                    this.DgvDo.DataSource = new BindingList<CSystemLogDto>(m_lstSystemLog);
                }
                if (this.CboFaultCondition.SelectedIndex + 1 == this.CboFaultCondition.Items.Count)
                {// 获取所有设备故障日志列表
                    m_lstDeviceFaultLog = proxy.GetDeviceFaultLogList();
                    m_lstDeviceFaultLog = m_lstDeviceFaultLog.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd));
                    this.DgvFault.DataSource = new BindingList<CDeviceFaultLogDto>(m_lstDeviceFaultLog);
                }
                if (this.CboBusinessCondition.SelectedIndex + 1 == this.CboBusinessCondition.Items.Count)
                {// 获取所有报文日志列表
                    m_lstTelegramLog = proxy.GetTelegramLogList();
                    m_lstTelegramLog = m_lstTelegramLog.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd));
                    this.DgvBusiness.DataSource = new BindingList<CTelegramLogDto>(m_lstTelegramLog);
                }
                if (this.CboStatusCondition.SelectedIndex + 1 == this.CboStatusCondition.Items.Count)
                {// 获取所有设备状态日志列表
                    m_lstDeviceStatusLog = proxy.GetDeviceStatusLogList();
                    m_lstDeviceStatusLog = m_lstDeviceStatusLog.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd));
                    this.DgvStatus.DataSource = new BindingList<CDeviceStatusLogDto>(m_lstDeviceStatusLog);
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
            //BtnICCardFind_Click(sender, e);
            //BtnDoFind_Click(sender, e);
            //BtnDefaultFind_Click(sender, e);
            //BtnBusinessFind_Click(sender, e);
            //BtnStatusFind_Click(sender, e);*/
        }

        /// <summary>
        /// 点击键盘“Esc”关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 查询处理
        /// <summary>
        /// 查询IC卡缴费日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnICCardFind_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                int nIndex = this.CboICCardCondition.SelectedIndex;
                string strContent = "所有";
                if (this.TxtICCardContent.Visible && this.TxtICCardContent.Enabled)
                {
                    if (string.IsNullOrEmpty(this.TxtICCardContent.Text))
                    {
                        strContent = string.Empty;
                    }
                    else
                    {
                        strContent = this.TxtICCardContent.Text.Trim();
                    }
                }
                else if (this.CboICCardContent.Visible)
                {
                    if (string.IsNullOrEmpty(this.CboICCardContent.Text))
                    {
                        strContent = string.Empty;
                    }
                    else
                    {
                        strContent = this.CboICCardContent.Text.Trim();
                    }
                }

                if (0 > nIndex || string.IsNullOrEmpty(strContent))
                {
                    MessageBox.Show("查询条件和查询内容不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime dtStart = this.DtpICCardStart.Value;// 起始日期
                DateTime dtEnd = this.DtpICCardEnd.Value;// 截止日期

                if (1 == nIndex)
                {// 根据卡类型查询
                    strContent = ((int)CStaticClass.ConvertICCardType(strContent)).ToString();
                }

                // 获取所有IC卡缴费日志列表(根据查询条件查询)
                List<CICCardLogDto> lstLogDto = proxy.GetICCardLogListByContent(nIndex, dtStart, dtEnd, strContent);
                //// 根据查询条件查询
                //switch (nIndex)
                //{
                //    case 0:// 根据卡号查询
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.paymenttime, dtStart, dtEnd) && s.iccode == strContent);
                //            break;
                //        }
                //    case 1:// 根据卡类型查询
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.paymenttime, dtStart, dtEnd) && CStaticClass.ConvertICCardType(s.ictype) == strContent);
                //            break;
                //        }
                //    case 2:// 根据车主姓名查询
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.paymenttime, dtStart, dtEnd) && s.username == strContent);
                //            break;
                //        }
                //    case 3:// 根据操作员
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.paymenttime, dtStart, dtEnd) && s.optcode == strContent);
                //            break;
                //        }
                //    default:
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.paymenttime, dtStart, dtEnd));
                //            break;
                //        }
                //}

                m_lstICCardLog = lstLogDto;
                this.DgvICCard.DataSource = new BindingList<CICCardLogDto>(lstLogDto);
                if (null == lstLogDto || 0 == lstLogDto.Count)
                {
                    MessageBox.Show("抱歉，没有找到符合条件的记录！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
        /// 查询操作日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDoFind_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                int nIndex = this.CboDoCondition.SelectedIndex;

                if (0 > nIndex || (string.IsNullOrEmpty(this.TxtDoContent.Text) && this.TxtDoContent.Enabled))
                {
                    MessageBox.Show("查询条件和关键字不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string strContent = this.TxtDoContent.Text.Trim();
                DateTime dtStart = this.DtpDoStart.Value;// 起始日期
                DateTime dtEnd = this.DtpDoEnd.Value;// 截止日期
                // 获取所有系统日志列表(根据查询条件查询)
                List<CSystemLogDto> lstLogDto = proxy.GetSystemLogListByContent(nIndex, dtStart, dtEnd, strContent);

                //// 根据查询条件查询
                //switch (nIndex) 
                //{
                //    case 0:// 根据描述关键字查询
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.curtime, dtStart, dtEnd) && s.logdescp.Contains(strContent));
                //            break;
                //        }
                //    case 1:// 根据操作员
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.curtime, dtStart, dtEnd) && s.optcode == strContent);
                //            break;
                //        }
                //    default:
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.curtime, dtStart, dtEnd));
                //            break;
                //        }
                //}

                m_lstSystemLog = lstLogDto;
                this.DgvDo.DataSource = new BindingList<CSystemLogDto>(lstLogDto);
                if (null == lstLogDto || 0 == lstLogDto.Count)
                {
                    MessageBox.Show("抱歉，没有找到符合条件的记录！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
        /// 查询设备故障日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefaultFind_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                int nIndex = this.CboFaultCondition.SelectedIndex;
                string strContent = "所有";
                if (this.TxtFaultContent.Visible && this.TxtFaultContent.Enabled)
                {
                    if (string.IsNullOrEmpty(this.TxtFaultContent.Text))
                    {
                        strContent = string.Empty;
                    }
                    else
                    {
                        strContent = this.TxtFaultContent.Text.Trim();
                    }
                }
                else if (this.CboFaultContent.Visible)
                {
                    if (string.IsNullOrEmpty(this.CboFaultContent.Text))
                    {
                        strContent = string.Empty;
                    }
                    else
                    {
                        strContent = this.CboFaultContent.Text.Trim();
                    }
                }

                if (0 > nIndex || string.IsNullOrEmpty(strContent))
                {
                    MessageBox.Show("查询条件和查询内容不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime dtStart = this.DtpFaultStart.Value;// 起始日期
                DateTime dtEnd = this.DtpFaultEnd.Value;// 截止日期
              
                if (0 == nIndex)
                {// 根据库区查询
                    strContent = CStaticClass.ConvertWareHouse(strContent).ToString();
                }

                // 获取所有设备故障日志列表(根据查询条件查询)
                List<CDeviceFaultLogDto> lstLogDto = proxy.GetDeviceFaultLogListByContent(nIndex, dtStart, dtEnd, strContent);

                //// 根据查询条件查询
                //switch (nIndex)
                //{
                //    case 0:// 根据库区查询
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd) && CStaticClass.ConvertWareHouse(s.warehouse) == strContent);
                //            break;
                //        }
                //    case 1:// 根据设备号查询
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd) && s.devicecode.ToString() == strContent);
                //            break;
                //        }
                //    case 2:// 根据故障描述
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd) && s.faultdescp.Contains(strContent));
                //            break;
                //        }
                //    case 3:// 根据操作员
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd) && s.optcode == strContent);
                //            break;
                //        }
                //    default:
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd));
                //            break;
                //        }
                //}

                m_lstDeviceFaultLog = lstLogDto;
                this.DgvFault.DataSource = new BindingList<CDeviceFaultLogDto>(lstLogDto);
                if (null == lstLogDto || 0 == lstLogDto.Count)
                {
                    MessageBox.Show("抱歉，没有找到符合条件的记录！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
        /// 查询报文日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBusinessFind_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                int nIndex = this.CboBusinessCondition.SelectedIndex;
                string strContent = "所有";
                int nWareHouse = 0;
                if (this.TxtBusinessContent.Visible && this.TxtBusinessContent.Enabled)
                {
                    if (string.IsNullOrEmpty(this.TxtBusinessContent.Text))
                    {
                        strContent = string.Empty;
                    }
                    else
                    {
                        strContent = this.TxtBusinessContent.Text.Trim();
                    }
                }
                else if (this.CboBusinessContent.Visible)
                {
                    if (!string.IsNullOrEmpty(this.CboBusinessContent.Text))
                    {
                        nWareHouse = CStaticClass.ConvertWareHouse(this.CboBusinessContent.Text.Trim());
                    }

                    if (4 == nIndex)
                    {// 查询存车次数
                        strContent = "1|9|0";
                    }
                    else if (5 == nIndex)
                    {// 查询取车次数
                        strContent = "3|1|0";
                    }
                }

                if (0 > nIndex || string.IsNullOrEmpty(strContent))
                {
                    MessageBox.Show("查询条件和查询内容不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime dtStart = this.DtpBusinessStart.Value;// 起始日期
                DateTime dtEnd = this.DtpBusinessEnd.Value;// 截止日期
                // 获取所有报文日志列表(根据查询条件查询)
                List<CTelegramLogDto> lstLogDto = new List<CTelegramLogDto>();

                if (0 == nWareHouse)
                {
                    lstLogDto = proxy.GetTelegramLogListByContent(nIndex, dtStart, dtEnd, strContent);
                }
                else
                {// 根据库号
                    lstLogDto = proxy.GetTelegramLogListByPLCContent(nIndex, dtStart, dtEnd, nWareHouse, strContent);
                }

                //// 根据查询条件查询
                //switch (nIndex)
                //{
                //    case 0:// 根据日志类型查询
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd) && s.logtype == strContent);
                //            break;
                //        }
                //    case 1:// 根据报文
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd) && s.telegramhead.Contains(strContent));
                //            break;
                //        }
                //    case 2:// 根据设备号
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd) && s.devicecode.ToString() == strContent);
                //            break;
                //        }
                //    case 3:// 根据IC卡号
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd) && s.iccode == strContent);
                //            break;
                //        }
                //    default:
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd));
                //            break;
                //        }
                //}

                m_lstTelegramLog = lstLogDto;
                this.DgvBusiness.DataSource = new BindingList<CTelegramLogDto>(lstLogDto);
                if (null == lstLogDto || 0 == lstLogDto.Count)
                {
                    MessageBox.Show("抱歉，没有找到符合条件的记录！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
        /// 查询设备状态日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStatusFind_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                int nIndex = this.CboStatusCondition.SelectedIndex;
                string strContent = "所有";
                if (this.TxtStatusContent.Visible && this.TxtStatusContent.Enabled)
                {
                    if (string.IsNullOrEmpty(this.TxtStatusContent.Text))
                    {
                        strContent = string.Empty;
                    }
                    else
                    {
                        strContent = this.TxtStatusContent.Text.Trim();
                    }
                }
                else if (this.CboStatusContent.Visible)
                {
                    if (string.IsNullOrEmpty(this.CboStatusContent.Text))
                    {
                        strContent = string.Empty;
                    }
                    else
                    {
                        strContent = this.CboStatusContent.Text.Trim();
                    }
                }

                if (0 > nIndex || string.IsNullOrEmpty(strContent))
                {
                    MessageBox.Show("查询条件和查询内容不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime dtStart = this.DtpStatusStart.Value;// 起始日期
                DateTime dtEnd = this.DtpStatusEnd.Value;// 截止日期
                if (0 == nIndex)
                {// 根据库区查询
                    strContent = CStaticClass.ConvertWareHouse(strContent).ToString();
                }

                // 获取所有设备状态日志列表(根据查询条件查询)
                List<CDeviceStatusLogDto> lstLogDto = proxy.GetDeviceStatusLogListByContent(nIndex, dtStart, dtEnd, strContent);

                //// 根据查询条件查询
                //switch (nIndex)
                //{
                //    case 0:// 根据库区查询
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd) && CStaticClass.ConvertWareHouse(s.warehouse) == strContent);
                //            break;
                //        }
                //    case 1:// 根据设备号
                //        {
                //            lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd) && s.devicecode.ToString() == strContent);
                //            break;
                //        }
                //    default:
                //        lstLogDto = lstLogDto.FindAll(s => CompareDateTime(s.time, dtStart, dtEnd));
                //        break;
                //}

                m_lstDeviceStatusLog = lstLogDto;
                this.DgvStatus.DataSource = new BindingList<CDeviceStatusLogDto>(lstLogDto);
                if (null == lstLogDto || 0 == lstLogDto.Count)
                {
                    MessageBox.Show("抱歉，没有找到符合条件的记录！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
        #endregion

        #region 报表处理
        /// <summary>
        /// IC卡缴费日志报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnICCardReport_Click(object sender, EventArgs e)
        {
            // "WindowsFormLib.ReportTempCardLog.rdlc" 表示临时卡缴费报表命名空间
            m_formReport.SetReportBindingSource(m_lstICCardLog, "WindowsFormLib.ReportTempCardLog.rdlc");
            m_formReport.ShowDialog();
        }

        /// <summary>
        /// 操作日志报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDoReport_Click(object sender, EventArgs e)
        {
            m_formReport.SetReportBindingSource(m_lstSystemLog, "WindowsFormLib.ReportSystemLog.rdlc");
            m_formReport.ShowDialog();
        }

        /// <summary>
        /// 设备故障日志报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefaultReport_Click(object sender, EventArgs e)
        {
            m_formReport.SetReportBindingSource(m_lstDeviceFaultLog, "WindowsFormLib.ReportDeviceFaultLog.rdlc");
            m_formReport.ShowDialog();
        }

        /// <summary>
        /// 报文日志报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBusinessReport_Click(object sender, EventArgs e)
        {
            m_formReport.SetReportBindingSource(m_lstTelegramLog, "WindowsFormLib.ReportTelegramLog.rdlc");
            m_formReport.ShowDialog();
        }

        /// <summary>
        /// 设备状态日志报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStatusReport_Click(object sender, EventArgs e)
        {
            m_formReport.SetReportBindingSource(m_lstDeviceStatusLog, "WindowsFormLib.ReportDeviceStatusLog.rdlc");
            m_formReport.ShowDialog();
        }
        #endregion

        #region 查询条件值改变事件
        /// <summary>
        /// 查询条件值改变事件(“所有”条件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboICCardCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nSelectedIndex = this.CboICCardCondition.SelectedIndex;
            if (nSelectedIndex + 1 == this.CboICCardCondition.Items.Count)
            {
                this.CboICCardContent.Visible = false;
                this.TxtICCardContent.Visible = true;
                this.TxtICCardContent.Text = "";
                this.TxtICCardContent.Enabled = false;
            }
            else
            {
                switch (this.CboICCardCondition.SelectedIndex)
                {
                    case 1:// 卡类型
                        {
                            this.TxtICCardContent.Visible = false;
                            this.CboICCardContent.Visible = true;
                            this.CboICCardContent.Items.Clear();
                            this.CboICCardContent.Items.AddRange(new object[] { "临时卡", "定期卡", "固定车位卡" });
                            break;
                        }
                    default:
                        {
                            this.CboICCardContent.Visible = false;
                            this.TxtICCardContent.Visible = true;
                            this.TxtICCardContent.Enabled = true;
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// 查询条件值改变事件(“所有”条件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboDoCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nSelectedIndex = this.CboDoCondition.SelectedIndex;
            if (nSelectedIndex + 1 == this.CboDoCondition.Items.Count)
            {
                this.TxtDoContent.Text = "";
                this.TxtDoContent.Enabled = false;
            }
            else
            {
                this.TxtDoContent.Enabled = true;
            }
        }

        /// <summary>
        /// 查询条件值改变事件(“所有”条件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboFaultCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nSelectedIndex = this.CboFaultCondition.SelectedIndex;
            if (nSelectedIndex + 1 == this.CboFaultCondition.Items.Count)
            {
                this.CboFaultContent.Visible = false;
                this.TxtFaultContent.Visible = true;
                this.TxtFaultContent.Text = "";
                this.TxtFaultContent.Enabled = false;
            }
            else
            {
                switch (nSelectedIndex)
                {
                    case 0:// 库区
                        {
                            this.TxtFaultContent.Visible = false;
                            this.CboFaultContent.Visible = true;
                            this.CboFaultContent.Items.Clear();
                            this.CboFaultContent.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
                            this.CboFaultContent.SelectedIndex = 0;
                            break;
                        }
                    default:
                        {
                            this.CboFaultContent.Visible = false;
                            this.TxtFaultContent.Visible = true;
                            this.TxtFaultContent.Enabled = true;
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// 查询条件值改变事件(“所有”条件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboBusinessCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nSelectedIndex = this.CboBusinessCondition.SelectedIndex;
            if (nSelectedIndex + 1 == this.CboBusinessCondition.Items.Count)
            {
                this.CboBusinessContent.Visible = false;
                this.TxtBusinessContent.Visible = true;
                this.TxtBusinessContent.Text = "";
                this.TxtBusinessContent.Enabled = false;
            }
            else
            {
                switch (nSelectedIndex)
                {
                    case 4:// 存车次数库区
                    case 5:// 取车次数库区
                    case 6:// 库区
                        {
                            this.TxtBusinessContent.Visible = false;
                            this.CboBusinessContent.Visible = true;
                            this.CboBusinessContent.Items.Clear();
                            this.CboBusinessContent.Items.Add("");
                            this.CboBusinessContent.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
                            this.CboBusinessContent.SelectedIndex = 0;
                            break;
                        }
                    default:
                        {
                            this.CboBusinessContent.Visible = false;
                            this.TxtBusinessContent.Visible = true;
                            this.TxtBusinessContent.Enabled = true;
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// 查询条件值改变事件(“所有”条件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboStatusCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nSelectedIndex = this.CboStatusCondition.SelectedIndex;
            if (nSelectedIndex + 1 == this.CboStatusCondition.Items.Count)
            {
                this.CboStatusContent.Visible = false;
                this.TxtStatusContent.Visible = true;
                this.TxtStatusContent.Text = "";
                this.TxtStatusContent.Enabled = false;
            }
            else
            {
                switch (nSelectedIndex)
                {
                    case 0:// 库区
                        {
                            this.TxtStatusContent.Visible = false;
                            this.CboStatusContent.Visible = true;
                            this.CboStatusContent.Items.Clear();
                            this.CboStatusContent.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
                            this.CboStatusContent.SelectedIndex = 0;
                            break;
                        }
                    default:
                        {
                            this.CboStatusContent.Visible = false;
                            this.TxtStatusContent.Visible = true;
                            this.TxtStatusContent.Enabled = true;
                            break;
                        }
                }
            }
        }
        #endregion

        #region 单元格格式设置
        /// <summary>
        /// 固定卡缴费日志单元格格式设置（卡类型1）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvICCard_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (null == e.Value)
            {
                return;
            }

            if (e.Value.GetType() != typeof(int))
            {
                return;
            }

            if (2 == e.ColumnIndex)
            {
                e.Value = CStaticClass.ConvertICCardType((int)e.Value);
            }
        }

        /// <summary>
        /// 设备故障日志单元格格式设置（库区1）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvFault_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (null == e.Value)
            {
                return;
            }

            if (e.Value.GetType() != typeof(int))
            {
                return;
            }

            if (0 == e.ColumnIndex)
            {
                e.Value = CStaticClass.ConvertWareHouse((int)e.Value);
            }
        }

        /// <summary>
        /// 设备状态日志单元格格式设置（库区1、设备模式4、设备类型5、车厅类型6、当前作业状态7、是否可接受指令8、是否可刷取车9）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvStatus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (null == e.Value)
            {
                return;
            }
            
            if (e.Value.GetType() != typeof(int))
            {
                return;
            }

            switch(e.ColumnIndex)
            {
                case 0:
                    {
                        e.Value = CStaticClass.ConvertWareHouse((int)e.Value);
                        break;
                    }
                case 4:
                    {
                        e.Value = CStaticClass.ConvertDeviceMode((int)e.Value);
                        break;
                    }
                case 5:
                    {
                        e.Value = CStaticClass.ConvertDeviceType((int)e.Value);
                        break;
                    }
                case 6:
                    {
                        e.Value = CStaticClass.ConvertHallType((int)e.Value);
                        break;
                    }
                case 7:
                    {
                        e.Value = CStaticClass.ConvertTaskType((int)e.Value);
                        break;
                    }
                case 8:
                    {
                        e.Value = CStaticClass.ConvertBoolType((int)e.Value);
                        break;
                    }
                case 9:
                    {
                        e.Value = CStaticClass.ConvertBoolType((int)e.Value);
                        break;
                    }
                //case 10:
                //    {
                //        e.Value = CStaticClass.ConvertBoolType((int)e.Value);
                //        break;
                //    }
                default:
                    { 
                        break; 
                    }
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 判断dtCur当前时间是否在stStart开始时间和dtEnd截止时间之间
        /// </summary>
        /// <param name="dtCur"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private bool CompareDateTime(DateTime? dtCur, DateTime dtStart, DateTime dtEnd)
        {
            if (null == dtCur)
            {
                return false;
            }

            // DateTime.CompareTo值说明小于零此实例早于 value。零此实例与 value 相同。大于零此实例晚于 value。
            if (dtCur.Value.CompareTo(dtStart) > 0 && dtCur.Value.CompareTo(dtEnd) < 0)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
