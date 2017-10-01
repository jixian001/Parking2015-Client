using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib;
using CarLocationPanelLib.QueryService;
using System.Reflection;
using System.ServiceModel;
using LOGManagementLib;

namespace WindowsFormLib
{
    public partial class CFormMain : Form, IDisposable
    {
        // 界面初始化
        private CFormLogin m_formLogin = null;
        private CFormDeviceFault m_formDeviceFault = null;
        private CFormCarLocation m_formCarLocation = null;
        private CFormHall m_formHall = null;
        private CFormETVorTV m_formETV = null;
        private CFormSystemConfig m_formSystemConfig = null;
        private CFormSystemMtc m_formSystemMtc = null;
        private CFormCustomerManage m_formCustomerManage = null;
        private CFormBillManagement m_formBillManagement = null;
        private CFormOperatorManage m_formOperatorManage = null;
        private CFormFindStatistics m_formFindStatistics = null;
        private CFormTempFetch m_formTempFetch = null;
        private CFormHandOrder m_formHandOrder = null;
        private CFormCIMCWorker m_formCIMCWorker = null;
        private FormRotation m_formRotation = null;

        // 主界面车位状态区域控件
        private List<Panel> m_ltpWareHouse = new List<Panel>();
        // 车位颜色说明标签
        private List<Label> m_llblDescp = new List<Label>();
        private string m_strTitle = string.Empty;

        public CFormMain()
        {
            try
            {
                m_strTitle = CStaticClass.ConfigMainTitle();
                m_formLogin = new CFormLogin(m_strTitle);
                HandInitalizeComponent();
                this.FormClosing += new FormClosingEventHandler(CFormMain_FormClosing);
                this.Tag = false;
                this.timer1.Interval = 1000;//毫秒
                CStaticClass.myCallback.PushCallbackEvent += new Action<object>(callback_CallbackEvent);
                CStaticClass.myPushProxy.Register(Environment.MachineName);
                CStaticClass.myClient.UpdateCarLocCallbackEvent += new Action(UpdateCarLocationStatus);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        ///// <summary>
        ///// 释放资源
        ///// </summary>
        //public void Dispose()
        //{ }

        /// <summary>
        /// 界面登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                SetLogout();
                DialogResult dr = m_formLogin.ShowDialog();
                if (dr == DialogResult.Yes)
                {
				    // 获取车主信息列表
                    List<struCustomerInfo> lstStruCUSTInfo = new List<struCustomerInfo>();
                    QueryServiceClient proxy = new QueryServiceClient();
                    proxy.QueryCUSTInfo(ref lstStruCUSTInfo);
                    proxy.Close();
                    CStaticClass.SetLstStruCUSTInfo(lstStruCUSTInfo);

                    this.Visible = true;
                    InitializeInfo();
                    SetLogin();

                    this.WindowState = FormWindowState.Maximized;
                    this.Tag = true;
                    this.timer1.Start();
                }
                else if (dr == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// 窗体首次显示时触发(窗体大小改变触发 OnResize  OnShown)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Maximized)
            //{// 最大化状态时
                HandResize();
            //}
        }

        /// <summary>
        /// 退出确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CFormMain_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //try
            //{
                if (null != this.Tag && typeof(bool) == this.Tag.GetType() && !(bool)this.Tag)
                {
                    return;
                }

                DialogResult dr = MessageBox.Show("确认退出本系统吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr != DialogResult.Yes)
                {
                    //3.Cancel 取得或設定數值，表示是否應該取消事件。        
                    e.Cancel = true;
                }
                //else if (null != CStaticClass.myPushProxy)
                //{
                //    CStaticClass.myPushProxy.Dispose();
                //}
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Application.Exit();
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Application.Exit();
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Application.Exit();
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Application.Exit();
            //}
        }

        /// <summary>
        /// 计时器在任务栏显示当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.TsslCurTime.Text = CStaticClass.CurruntDateTime().ToString("yyyy-MM-dd HH:mm:ss");

                // 状态栏显示服务器连接状态处理
                if (CStaticClass.GetPushServiceConnectFlag())
                {
                    if (this.TsslConnected.Name != "connected")
                    {
                        this.TsslConnected.Name = "connected";
                        this.TsslConnected.Image = global::WindowsFormLib.Properties.Resources.connected;
                        // 服务器重新连接上需要初始化
                        InitializeInfo();
                    }
                }
                else
                {
                    if (this.TsslConnected.Name != "disconnceted")
                    {
                        this.TsslConnected.Name = "disconnceted";
                        this.TsslConnected.Image = global::WindowsFormLib.Properties.Resources.disconnceted;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #region 工具栏按钮单击事件 
        /// <summary>
        /// 系统维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbSystemMtc_Click(object sender, EventArgs e)
        {
            if (null == m_formSystemMtc)
            {
                m_formSystemMtc = new CFormSystemMtc();
            }
            m_formSystemMtc.ShowDialog();
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbSystemConfig_Click(object sender, EventArgs e)
        {
            if (null == m_formSystemConfig)
            {
                m_formSystemConfig = new CFormSystemConfig();
            }
            m_formSystemConfig.ShowDialog();
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbUserManage_Click(object sender, EventArgs e)
        {
            if (null == m_formCustomerManage)
            {
                m_formCustomerManage = new CFormCustomerManage();
            }
            m_formCustomerManage.ShowDialog();
        }

        /// <summary>
        /// 缴费管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbTollManage_Click(object sender, EventArgs e)
        {
            if (null == m_formBillManagement)
            {
                m_formBillManagement = new CFormBillManagement();
            }
            m_formBillManagement.ShowDialog();
        }

        /// <summary>
        /// 操作员管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbOperatorManage_Click(object sender, EventArgs e)
        {
            if (null == m_formOperatorManage)
            {
                m_formOperatorManage = new CFormOperatorManage();
            }
            m_formOperatorManage.ShowDialog();
        }

        /// <summary>
        /// 查询统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabQueryStatistics_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == m_formFindStatistics)
                {
                    m_formFindStatistics = new CFormFindStatistics();
                }
                m_formFindStatistics.ShowDialog();
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
        /// 临时取物
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbTempFetch_Click(object sender, EventArgs e)
        {
            if (null == m_formTempFetch)
            {
                m_formTempFetch = new CFormTempFetch();
            }
            m_formTempFetch.ShowDialog();
        }

        /// <summary>
        /// 手动指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbHandOrder_Click(object sender, EventArgs e)
        {
            if (null == m_formHandOrder)
            {
                m_formHandOrder = new CFormHandOrder();
            }
            m_formHandOrder.ShowDialog();
        }

        /// <summary>
        /// 维保管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbCIMCWorker_Click(object sender, EventArgs e)
        {
            if (null == m_formCIMCWorker)
            {
                m_formCIMCWorker = new CFormCIMCWorker();
            }
            m_formCIMCWorker.ShowDialog();
        }

        /// <summary>
        /// 切换用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbLogout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认切换用户吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
            {
                //3.Cancel 取得或設定數值，表示是否應該取消事件。        
                return;
            }

            this.Hide();
            FormMain_Load(null,null);
            
            if (null != this && !this.IsDisposed)
            {
                this.Show();
            }
        }

        /// <summary>
        /// 全屏模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmtFull_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TsbClose.Visible = true;
        }

        /// <summary>
        /// 普遍模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmtFix_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.TsbClose.Visible = false;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbModifyPassWord_Click(object sender, EventArgs e)
        {
            (new CFormModifyPassWord()).ShowDialog();
        }

        /// <summary>
        /// 设置状态故障汇总
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbDeviceFault_Click(object sender, EventArgs e)
        {
            if (null == m_formDeviceFault)
            {
                m_formDeviceFault = new CFormDeviceFault();
            }
            m_formDeviceFault.ShowDialog();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 车辆调头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbRotation_Click(object sender, EventArgs e)
        {
            if (null == m_formRotation)
            {
                m_formRotation = new FormRotation();
            }
            m_formRotation.ShowDialog();
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 界面布局
        /// </summary>
        private void HandInitalizeComponent()
        {
            InitializeComponent();

            List<struCarPSONLayoutInfo> lstRect = CStaticClass.ConfigLstRectProject();
            int curWidth = 450;

            #region 初始化
            m_formDeviceFault = new CFormDeviceFault();
            m_formCarLocation = new CFormCarLocation();
            m_formHall = new CFormHall(m_formDeviceFault);
            m_formETV = new CFormETVorTV(m_formDeviceFault);
            m_formSystemConfig = new CFormSystemConfig();
            #endregion

            if (!CStaticClass.ConfigBillingFlag())
            {
                this.TlsManage.Items.Remove(this.TsbTollManage);
            }

            this.LblTitle.Text = m_strTitle;

            #region 添加所有车位颜色说明标签
            // 添加所有车位颜色说明标签
            for (int i = 0; i < 9; i++)
            {
                Label label = new Label();
                label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label.Name = "label" + (i + 1);
                label.Size = new System.Drawing.Size(45, 30);
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                label.Location = new System.Drawing.Point(curWidth, 12);
                curWidth += label.Width + 12;
                switch (i)
                {
                    case 0:
                        {
                            label.BackColor = System.Drawing.Color.LightYellow;
                            label.Text = "空闲";
                            break;
                        }
                    case 1:
                        {
                            label.BackColor = System.Drawing.Color.Purple;
                            label.ForeColor = System.Drawing.Color.White;
                            label.Text = "占用";
                            break;
                        }
                    case 2:
                        {
                            label.BackColor = System.Drawing.Color.Violet;
                            label.Text = "入库";
                            break;
                        }
                    case 3:
                        {
                            label.BackColor = System.Drawing.Color.SkyBlue;
                            label.Text = "出库";
                            break;
                        }
                    case 4:
                        {
                            label.BackColor = System.Drawing.Color.GreenYellow;
                            label.Text = "挪移";
                            break;
                        }
                    case 5:
                        {
                            label.BackColor = System.Drawing.Color.Blue;
                            label.Text = "取物";
                            break;
                        }
                    case 6:
                        {
                            label.BackColor = System.Drawing.Color.DarkKhaki;
                            label.ForeColor = System.Drawing.Color.White;
                            label.Text = "车厅";
                            break;
                        }
                    case 7:
                        {
                            label.BackColor = System.Drawing.Color.DarkGray;
                            label.ForeColor = System.Drawing.Color.White;
                            label.Text = "无效";// 禁用
                            break;
                        }
                    case 8:
                        {
                            label.BackColor = System.Drawing.Color.DimGray;
                            label.ForeColor = System.Drawing.Color.White;
                            label.Text = "禁用";// 无效
                            break;
                        }
                    //case 9:
                    //    {
                    //        label.BackColor = System.Drawing.Color.DarkSalmon;
                    //        label.Text = "旋转";
                    //        break;
                    //    }
                    default:
                        { 
                            break; 
                        }
                }

                m_llblDescp.Add(label);
                this.GbColor.Controls.Add(label);
            }
            #endregion

            #region 添加所有库车位状态
            for (int i = 0; i < lstRect.Count; i++)
            {
                string[] strs = lstRect[i].strPanelName.Split('_');
                string typeName = string.Empty;//lstRect[i].strPanelName;
                string strText = string.Empty;//lstRect[i].strPanelName;
                if (0 < strs.Count())
                {
                    typeName = strs[0];
                } 
                if (1 < strs.Count())
                {
                    strText = strs[1];
                }
                object[] args = new object[] { lstRect[i].rectInfo };
                CWareHousePanel tp = (CWareHousePanel)Assembly.Load("CarLocationPanelLib").CreateInstance("CarLocationPanelLib." + typeName, false, BindingFlags.Default, null, args, null, null);
                tp.EnmSrcLocAddr = EnmTxtCarLocationAddr.FormCarLocation;
                tp.CallbackCarLocationEvent += new CallbackCarLocationEventHandler(CWareHousePanel_CallbackCarLocationEvent);
                tp.Text = strText;
                m_ltpWareHouse.Add(tp);

                if (1< lstRect.Count)
                {
                    tp.Location = this.carLocationStatus.Location;
                    this.carLocationStatus.Visible = false;
                    GroupBox GbTP = new GroupBox();
                    GbTP.Text = tp.Text;
                    GbTP.Controls.Add(tp);
                    this.Controls.Add(GbTP);
                }
                else
                {
                    tp.Location = this.carLocationStatus.Location;
                    this.carLocationStatus.Visible = false;
                    GroupBox GbTP = new GroupBox();
                    GbTP.Text = tp.Text;
                    GbTP.Controls.Add(tp);
                    this.Controls.Add(GbTP);
                }

                // 手动指令根据车位状态获取地址初始化
                CWareHousePanel tpHand = (CWareHousePanel)Assembly.Load("CarLocationPanelLib").CreateInstance("CarLocationPanelLib." + typeName, false, BindingFlags.Default, null, args, null, null);
                tpHand.CallbackCarLocationEvent += new CallbackCarLocationEventHandler(CWareHousePanel_CallbackCarLocationEvent);
                tpHand.Text = strText;
                CStaticClass.myPanelCarLocation.Add(tpHand);
            }
            #endregion
        }

        /// <summary>
        /// 手动调整整体大小
        /// </summary>
        private void HandResize()
        {
            #region 常量
            int nWidth = this.ClientSize.Width > CStaticClass.ConfigMinWidth() ? this.ClientSize.Width : CStaticClass.ConfigMinWidth();
            int nHeight = this.ClientSize.Height > CStaticClass.ConfigMinHeight() ? this.ClientSize.Height : CStaticClass.ConfigMinHeight();
            int nGap = CStaticClass.ConfigMainGap();
            int nSideSize = 0;// m_ltpWareHouse.Count() > 1 ? CStaticClass.ConfigControlSize() : 0;
            // 中央区域高度（除工具栏、状态栏、间隔）
            int nCarLocationHeight = nHeight - this.GbColor.Height - this.statusStrip.Height;//- this.LblTitle.Height
            int nCcarLocationWidth = nWidth;
            int curHigh = 0;
            int curWidth = 0;
            int nLeft = 0;
            #endregion

            #region 工具条TlsManage
            if (this.TlsManage.Dock == DockStyle.Top)
            {
                curHigh += this.TlsManage.Height;
                nCarLocationHeight -= this.TlsManage.Height;
                this.TlsManage.Size = new System.Drawing.Size(nWidth, this.TlsManage.Height);
            }
            else if (this.TlsManage.Dock == DockStyle.Bottom)
            {
                nCarLocationHeight -= this.TlsManage.Height;
                this.TlsManage.Size = new System.Drawing.Size(nWidth, this.TlsManage.Height);
            }
            else if (this.TlsManage.Dock == DockStyle.Left || this.TlsManage.Dock == DockStyle.Right)
            {
                nCcarLocationWidth -= this.TlsManage.Width;
                this.TlsManage.Size = new System.Drawing.Size(this.TlsManage.Width, nHeight - 2 * this.statusStrip.Height);
            }

            if (this.TlsManage.Dock == DockStyle.Left)
            {
                nLeft = this.TlsManage.Width;
            }
            #endregion

            #region 车位颜色说明标签GbColor
            this.GbColor.Location = new System.Drawing.Point(0, curHigh);
            this.GbColor.Size = new System.Drawing.Size(nWidth, this.GbColor.Height);
            for (int i = 0; i < m_llblDescp.Count(); i++)
            {
                Label label = m_llblDescp[i];

                if (null == label)
                {
                    continue;
                }

                if (0 == i)
                {
                    curWidth = (nCcarLocationWidth - m_llblDescp.Count() * label.Width - (m_llblDescp.Count() - 1) * nGap) / 2;
                }
                else
                {
                    curWidth += label.Width + nGap;
                }

                label.Location = new System.Drawing.Point(curWidth, 18);
            }
            curHigh += this.GbColor.Height;
            #endregion

            #region 标题标签LblTitle
            if (this.LblTitle.Visible)
            {
                this.LblTitle.Location = new System.Drawing.Point(nLeft, curHigh);
                this.LblTitle.Size = new System.Drawing.Size(nWidth, this.LblTitle.Height);
                curHigh += this.LblTitle.Height;
                nCarLocationHeight -= this.LblTitle.Height;
            }
            #endregion

            #region 车位状态布局carLocationStatus
            this.carLocationStatus.Location = new System.Drawing.Point(nLeft, curHigh);
            this.carLocationStatus.Size = new System.Drawing.Size(nCcarLocationWidth, nCarLocationHeight);
            nCarLocationHeight -= nSideSize;

            curWidth = nLeft;
            int nColumnSum = 0;
            foreach (CWareHousePanel tabPage in m_ltpWareHouse)
            {// 获取所有库列总个数
                nColumnSum += tabPage.RectProjectData.Width;
            }

            int nTabWidth = nColumnSum > 0 ? nCcarLocationWidth / nColumnSum : nCcarLocationWidth;
            foreach (CWareHousePanel tabPage in m_ltpWareHouse)
            {// 设置车位布局大小
                //tabPage.Size = new System.Drawing.Size(nCcarLocationWidth, nCarLocationHeight - nSideSize);
                if (null != tabPage.Parent)
                {
                    tabPage.Parent.Location = new System.Drawing.Point(curWidth, curHigh);
                    tabPage.Parent.Size = new System.Drawing.Size(nTabWidth * tabPage.RectProjectData.Width, nCarLocationHeight);
                }

                tabPage.Location = new System.Drawing.Point(0, 2 * nGap);
                tabPage.Size = new System.Drawing.Size(nTabWidth * tabPage.RectProjectData.Width, nCarLocationHeight - 2 * nGap);
                curWidth += tabPage.Width;
            }
            curHigh += nCarLocationHeight;
            #endregion

            #region 任务状态栏 statusStrip
            this.statusStrip.Location = new System.Drawing.Point(0, nHeight - this.statusStrip.Height);
            #endregion
        }

        /// <summary>
        /// 登录
        /// </summary>
        private void SetLogin()
        {
            this.TsbRotation.Visible = CStaticClass.myPara.VehicleRotationFlag;
            this.TlsManage.Enabled = true;
            int nPermission = 0;
            this.TsslOptTxt.Text = "";

            if (null != CStaticClass.myOperator)
            {
                nPermission = (int)CStaticClass.myOperator.optpermission;
                this.TsslOptTxt.Text = CStaticClass.myOperator.optcode;
            }

            if (1 == (nPermission & 1))
            {
                this.TsbSystemMtc.Visible = true;
            }
            else
            {
                this.TsbSystemMtc.Visible = false;
            }

            if (2 == (nPermission & 2))
            {
                this.TsbSystemConfig.Visible = true;
            } 
            else
            {
                this.TsbSystemConfig.Visible = false;
            }

            if (4 == (nPermission & 4))
            {
                this.TsbUserManage.Visible = true;
            }
            else
            {
                this.TsbUserManage.Visible = false;
            }

            if (8 == (nPermission & 8))
            {
                this.TsbTollManage.Visible = true;
            }
            else
            {
                this.TsbTollManage.Visible = false;
            }

            if (16 == (nPermission & 16))
            {
                this.TsbOperatorManage.Visible = true;
            }
            else
            {
                this.TsbOperatorManage.Visible = false;
            }

            if (32 == (nPermission & 32))
            {
                this.TabQueryStatistics.Visible = true;
            }
            else
            {
                this.TabQueryStatistics.Visible = false;
            }

            if (64 == (nPermission & 64))
            {
                this.TsbTempFetch.Visible = true;
            }
            else
            {
                this.TsbTempFetch.Visible = false;
            }

            if (128 == (nPermission & 128))
            {
                this.TsbHandOrder.Visible = true;
            }
            else
            {
                this.TsbHandOrder.Visible = false;
            }

            if (256 == (nPermission & 256))
            {
                this.TsbDeviceFault.Visible = true;
            }
            else
            {
                this.TsbDeviceFault.Visible = false;
            }

            if (512 == (nPermission & 512))
            {
                this.TsbCIMCWorker.Visible = true;
            }
            else
            {
                this.TsbCIMCWorker.Visible = false;
            }
            //this.TsbSystemConfig.Enabled = (1 == (nPermission & 1)) ? true : false;
            //this.TsbSystemMtc.Enabled = (2 == (nPermission & 2)) ? true : false;
            //this.TsbUserManage.Enabled = (4 == (nPermission & 4)) ? true : false;
            //this.TsbTollManage.Enabled = (8 == (nPermission & 8)) ? true : false;
            //this.TsbOperatorManage.Enabled = (16 == (nPermission & 16)) ? true : false;
            //this.TabQueryStatistics.Enabled = (32 == (nPermission & 32)) ? true : false;
            //this.TsbTempFetch.Enabled = (64 == (nPermission & 64)) ? true : false;
            //this.TsbHandOrder.Enabled = (128 == (nPermission & 128)) ? true : false;
        }

        /// <summary>
        /// 注销
        /// </summary>
        private void SetLogout()
        {
            this.TlsManage.Enabled = false;
        }

        /// <summary>
        /// 初始化主界面各信息
        /// </summary>
        private void InitializeInfo()
        {
            int nSum = 0;
            int nOccupy = 0;
            int nSpace = 0;
            int nMaxSpace = 0;
            string strSumDescp = "(";
            string strOccupyDescp = "(";
            string strSpaceDescp = "(";
            string strMaxSpaceDescp = "(";
            int i = 1;
            if (null == m_formCIMCWorker)
            {
                m_formCIMCWorker = new CFormCIMCWorker();
            }

            foreach (Panel tabPage in m_ltpWareHouse)
            {
                CWareHousePanel wareHouseTabPage = (CWareHousePanel)tabPage;
                wareHouseTabPage.UpdateCarLocationStatus();
                wareHouseTabPage.UpdateDeviceStatus();
                // 初始化车厅流程图
                List<CDeviceStatusDto> lstDeviceStatus = wareHouseTabPage.UpdateDeviceFault();
                foreach (CDeviceStatusDto deviceStatus in lstDeviceStatus)
                {
                    m_formCIMCWorker.UpdateFlowChart(CStaticClass.ConvertDeviceStatus(deviceStatus));
                }

                nSum += wareHouseTabPage.RectVehCount.X;
                nOccupy += wareHouseTabPage.RectVehCount.Y;
                nSpace += wareHouseTabPage.RectVehCount.Width;
                nMaxSpace += wareHouseTabPage.RectVehCount.Height;

                string strFlag = ";";
                if (i++ == m_ltpWareHouse.Count)
                {
                    strFlag = ")";
                }
                strSumDescp += wareHouseTabPage.Text + ":" + wareHouseTabPage.RectVehCount.X + strFlag;
                strOccupyDescp += wareHouseTabPage.Text + ":" + wareHouseTabPage.RectVehCount.Y + strFlag;
                strSpaceDescp += wareHouseTabPage.Text + ":" + wareHouseTabPage.RectVehCount.Width + strFlag;
                strMaxSpaceDescp += wareHouseTabPage.Text + ":" + wareHouseTabPage.RectVehCount.Height + strFlag;
            }

            if (2 > m_ltpWareHouse.Count)
            {// 只有一个库时
                strSumDescp = string.Empty;
                strOccupyDescp = string.Empty;
                strSpaceDescp = string.Empty;
                strMaxSpaceDescp = string.Empty;
            }
            //this.TsslSumTxt.Text = nSum.ToString() + strSumDescp;
            //this.TsslOccupyTxt.Text = nOccupy.ToString() + strOccupyDescp;
            //this.TsslSpaceTxt.Text = nSpace.ToString() + strSpaceDescp;
            //this.TsslSpaceMaxTxt.Text = nMaxSpace.ToString() + strMaxSpaceDescp;
            SetStatusStripValue();
        }

        /// <summary>
        /// 更新车位状态
        /// </summary>
        private void UpdateCarLocationStatus()
        {
            foreach (Panel tabPage in m_ltpWareHouse)
            {
                CWareHousePanel wareHouseTabPage = (CWareHousePanel)tabPage;
                wareHouseTabPage.UpdateCarLocationStatus();
            }
        }

        /// <summary>
        /// 车位状态Panel回调事件实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CWareHousePanel_CallbackCarLocationEvent(object sender, CCarLocationEventArgs e)
        {
            if (null == e)
            {
                return;
            }

            switch (e.EnmSrcLocAddr)
            {
                case EnmTxtCarLocationAddr.FormCarLocation:
                    {// 双击车位弹出车位信息对话框
                        if (null == e.ParamDto || typeof(CarLocationPanelLib.QueryService.CCarLocationDto) != e.ParamDto.GetType())
                        {
                            return;
                        }
                        m_formCarLocation.FillFormCarLocation((CarLocationPanelLib.QueryService.CCarLocationDto)e.ParamDto, e.NICType);
                        m_formCarLocation.ShowDialog();
                        break;
                    }
                case EnmTxtCarLocationAddr.FormHall:
                    {// 双击车位弹出车厅信息对话框
                        if (null == e.ParamDto || typeof(CarLocationPanelLib.QueryService.CDeviceStatusDto) != e.ParamDto.GetType())
                        {
                            return;
                        }
                        m_formHall.FillFormHall((CarLocationPanelLib.QueryService.CDeviceStatusDto)e.ParamDto);
                        m_formHall.ShowDialog();
                        break;
                    }
                case EnmTxtCarLocationAddr.FormETV:
                    {// 双击车位弹出ETV信息对话框
                        if (null == e.ParamDto || typeof(CarLocationPanelLib.QueryService.CDeviceStatusDto) != e.ParamDto.GetType())
                        {
                            return;
                        }
                        m_formETV.FillFormEquip((CarLocationPanelLib.QueryService.CDeviceStatusDto)e.ParamDto);
                        m_formETV.ShowDialog();
                        break;
                    }
                case EnmTxtCarLocationAddr.HandOrderSrc:
                    {
                        if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormHandOrder))
                        {
                            ((CFormHandOrder)e.ParentForm).SetSrcLocAddr(e.StrLocAddr);
                        }
                        else if (null != e.ParentForm && e.ParentForm.GetType() == typeof(FormRotation))
                        {
                            ((FormRotation)e.ParentForm).SetSrcLocAddr(e.StrLocAddr);
                        }
                        break;
                    }
                case EnmTxtCarLocationAddr.HandOrderDest:
                    {
                        if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormHandOrder))
                        {
                            ((CFormHandOrder)e.ParentForm).SetDestLocAddr(e.StrLocAddr);
                        }
                        break; ;
                    }
                case EnmTxtCarLocationAddr.HandInJogSrc:
                    {
                        if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormSystemMtc))
                        {
                            ((CFormSystemMtc)e.ParentForm).SetInJogSrcLocAddr(e.StrLocAddr);
                        }
                        else if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormHandOrder))
                        {
                            ((CFormHandOrder)e.ParentForm).SetSrcLocAddr(e.StrLocAddr);
                        }
                        break; ;
                    }
                case EnmTxtCarLocationAddr.HandInJogDest:
                    {
                        if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormSystemMtc))
                        {
                            ((CFormSystemMtc)e.ParentForm).SetInJogDestLocAddr(e.StrLocAddr);
                        }
                        else if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormHandOrder))
                        {
                            ((CFormHandOrder)e.ParentForm).SetDestLocAddr(e.StrLocAddr);
                        }
                        break; ;
                    }
                case EnmTxtCarLocationAddr.Dis:
                    {
                        if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormSystemMtc))
                        {
                            ((CFormSystemMtc)e.ParentForm).SetDisLocAddr(e.StrLocAddr);
                        }
                        break; ;
                    }
                case EnmTxtCarLocationAddr.HandIn:
                    {
                        if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormSystemMtc))
                        {
                            ((CFormSystemMtc)e.ParentForm).SetHandInLocAddr(e.StrLocAddr);
                        }
                        break; ;
                    }
                case EnmTxtCarLocationAddr.HandOut:
                    {
                        if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormSystemMtc))
                        {
                            ((CFormSystemMtc)e.ParentForm).SetHandOutLocAddr(e.StrLocAddr);
                        }
                        break; ;
                    }
                case EnmTxtCarLocationAddr.Customer:
                    {
                        if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormCustomer))
                        {
                            ((CFormCustomer)e.ParentForm).SetCarLocAddr(e.StrLocAddr);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// 设置状态栏值
        /// </summary>
        /// <param name="obj"></param>
        private void SetStatusStripValue()
        {
            int nSum = 0;
            int nOccupy = 0;
            int nSpace = 0;
            int nMaxSpace = 0;
            string strSumDescp = "(";
            string strOccupyDescp = "(";
            string strSpaceDescp = "(";
            string strMaxSpaceDescp = "(";
            int i = 1;
            QueryServiceClient proxy = new QueryServiceClient();

            foreach (object obj in CStaticClass.ConfigLstWareHouse())
            {
                if (typeof(int) != obj.GetType())
                {
                    continue;
                }
                Rectangle rect = new Rectangle();
                proxy.GetCarPOSNFreeOccupyCount((int)obj, ref rect);
                nSum += rect.X;
                nOccupy += rect.Y;
                nSpace += rect.Width;
                nMaxSpace += rect.Height;

                string strFlag = ";";
                if (i++ == m_ltpWareHouse.Count)
                {
                    strFlag = ")";
                }
                string strWareHouse = CStaticClass.ConvertWareHouse((int)obj);
                strSumDescp += strWareHouse + ":" + rect.X + strFlag;
                strOccupyDescp += strWareHouse + ":" + rect.Y + strFlag;
                strSpaceDescp += strWareHouse + ":" + rect.Width + strFlag;
                strMaxSpaceDescp += strWareHouse + ":" + rect.Height + strFlag;
            }

            if (2 > m_ltpWareHouse.Count)
            {// 只有一个库时
                strSumDescp = string.Empty;
                strOccupyDescp = string.Empty;
                strSpaceDescp = string.Empty;
                strMaxSpaceDescp = string.Empty;
            }
            this.TsslSumTxt.Text = nSum.ToString() + strSumDescp;
            this.TsslOccupyTxt.Text = nOccupy.ToString() + strOccupyDescp;
            this.TsslSpaceTxt.Text = nSpace.ToString() + strSpaceDescp;
            this.TsslSpaceMaxTxt.Text = nMaxSpace.ToString() + strMaxSpaceDescp;
        }
     
        /// <summary>
        /// WCF回调事件
        /// </summary>
        /// <param name="e"></param>
        private void callback_CallbackEvent(object e)
        {
            try
            {
                if (null == e)
                {
                    return;
                }
                this.BeginInvoke((MethodInvoker)delegate
                {
                    CallBackSubFunction(e);
                });
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///WCF回调子函数
        /// </summary>
        /// <param name="e"></param>
        private void CallBackSubFunction(object e)
        {
            if (e.GetType() == typeof(CarLocationPanelLib.PushService.CCarLocationDto))
            {// 更新当前某一车位状态
                CarLocationPanelLib.PushService.CCarLocationDto carLocation = (CarLocationPanelLib.PushService.CCarLocationDto)e;
                string strName = carLocation.warehouse.ToString();
                CWareHousePanel wareHouseTabPage = (CWareHousePanel)m_ltpWareHouse.Find(a => a.Name == strName);
                wareHouseTabPage.UpdateCarLocationStatus(carLocation);
                SetStatusStripValue();
                //CLOGException.Trace("CallBackSubFunction, carLocation.carlocaddr:" + carLocation.carlocaddr);
                //SetStatusStripValue(e);
            }
            else if (e.GetType() == typeof(CarLocationPanelLib.PushService.CDeviceStatusDto))
            {// 更新当前某一设备状态
                CarLocationPanelLib.PushService.CDeviceStatusDto deviceStatus = (CarLocationPanelLib.PushService.CDeviceStatusDto)e;
                CLOGException.Trace(0, "callback_CallbackEvent, e:", string.Format("deviceStatus.warehouse-{0} deviceStatus.devicecode-{1}", deviceStatus.warehouse, deviceStatus.devicecode));
                if ((int)EnmSMGType.ETV == deviceStatus.devicetype)
                {
                    // 客户端界面ETV位置更新
                    string strName = deviceStatus.warehouse.ToString();
                    CWareHousePanel wareHouseTabPage = (CWareHousePanel)m_ltpWareHouse.Find(a => a.Name == strName);
                    wareHouseTabPage.UpdateDeviceStatus(deviceStatus);
                    if (m_formETV.Visible && null != m_formETV.EquipDeviceStatus
                        && m_formETV.EquipDeviceStatus.warehouse == deviceStatus.warehouse
                        && m_formETV.EquipDeviceStatus.devicecode == deviceStatus.devicecode)
                    {// 更新ETV设备信息界面
                        m_formETV.FillFormEquip(CStaticClass.ConvertDeviceStatus(deviceStatus)); 
                    }
                }
                else if (m_formHall.Visible && null != m_formHall.EquipDeviceStatus
                        && m_formHall.EquipDeviceStatus.warehouse == deviceStatus.warehouse
                        && m_formHall.EquipDeviceStatus.devicecode == deviceStatus.devicecode)
                {// 更新车厅设备信息界面
                    m_formHall.FillFormHall(CStaticClass.ConvertDeviceStatus(deviceStatus));
                }

                List<object> lstETV = CStaticClass.ConfigLstETVOrTVDeviceID(deviceStatus.warehouse);
                if (null != lstETV && 1 > lstETV.Count)
                {
                    // 无ETV时，客户端界面ETV颜色更新
                    string strName = deviceStatus.warehouse.ToString();
                    CWareHousePanel wareHouseTabPage = (CWareHousePanel)m_ltpWareHouse.Find(a => a.Name == strName);
                    wareHouseTabPage.UpdateDeviceStatus(deviceStatus);
                }

                if (null != m_formSystemConfig)
                {
                    m_formSystemConfig.UpdateDeviceIsable(deviceStatus);
                }

                if (null == m_formCIMCWorker)
                {
                    m_formCIMCWorker = new CFormCIMCWorker();
                }

                m_formCIMCWorker.UpdateFlowChart(deviceStatus);
            }
            else if (e.GetType() == typeof(CarLocationPanelLib.PushService.CDeviceFaultDto))
            {// 更新当前某一label设备故障
                CarLocationPanelLib.PushService.CDeviceFaultDto deviceFault = (CarLocationPanelLib.PushService.CDeviceFaultDto)e;

                // 客户端界面设备故障状态更新
                string strName = deviceFault.warehouse.ToString();
                CWareHousePanel wareHouseTabPage = (CWareHousePanel)m_ltpWareHouse.Find(a => a.Name == strName);
                string str = wareHouseTabPage.UpdateDeviceFault(deviceFault);

                if (!this.TsslPLC.Text.Contains(str))
                {// 获取当前故障设备
                    this.TsslPLC.Text += str;
                }

                // 客户端设备故障界面更新
                if (null != m_formDeviceFault)
                {
                    m_formDeviceFault.UpdateDeviceFault(deviceFault);
                }
            }
        }
        #endregion
    }
}
