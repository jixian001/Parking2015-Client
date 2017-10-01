using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using System.ServiceModel;
using CarLocationPanelLib;
using CustomControlLib;
using BaseMethodLib;

namespace WindowsFormLib
{
    public partial class CFormCIMCWorker : Form
    {
        // 代理实现异步调用以设置TextBox控件text属性
        private delegate void CUserFlowChartControlCallback(CarLocationPanelLib.PushService.CDeviceStatusDto dr, CUserFlowChartControl cufccTempE1, CUserFlowChartControl cufccTempE2);

        public CFormCIMCWorker()
        {
            InitializeComponent();
            // tabPage1-车厅运行流程架构
            InitializeGbHall();
            // tabPage3-语音配置
            this.DgvSound.AutoGenerateColumns = false;
            this.DgvSound.DataSourceChanged += new EventHandler(this.CupttsSound.UpdateLayout);
            this.CupttsSound.Tag = this.DgvSound;
            // tabPage2-Led配置
            this.DgvLed.AutoGenerateColumns = false;
            this.DgvLed.DataSourceChanged += new EventHandler(this.CupttsLed.UpdateLayout);
            this.CupttsLed.Tag = this.DgvLed;
            // tabPage4-LED内容修改
            this.tabControl1.Controls.Remove(this.tabPage4);
            // tabPage6-报文队列
            this.DgvQueue.AutoGenerateColumns = false;
            this.CboWareHouseTask.Items.Add("");
            this.CboWareHouseTask.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());

            // 设置键盘“Esc”按钮
            Button BtnCancel = new Button();
            this.CancelButton = BtnCancel;
            BtnCancel.Click += new EventHandler(BtnClose_Click);
        }

        private void CFormCIMCWorker_Load(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                // 语音配置查询列表
                List<CSoundDto> lstSoundDto = proxy.GetSoundList();
                this.DgvSound.DataSource = new BindingList<CSoundDto>(lstSoundDto);

                // Led配置查询列表
                List<CLedContentDto> lstLedContentDto = proxy.GetLEDContentList();
                this.DgvLed.DataSource = new BindingList<CLedContentDto>(lstLedContentDto);

                // 报文队列查询列表
                GetFindQueueLst(proxy);

                OnResize(null);
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
        /// 大小自适应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            int nMargin = 2;
            int nGap = 20;
            int nCurrentHeight = 10;

            this.tabControl1.Size = new Size(this.Width - nGap, this.Height - 2 * nGap);
            this.tabPage1.Size = new Size(this.Width - nGap, this.Height - 3 * nGap);
            int nWidth = this.tabPage1.Width - 10 * nMargin;
            int nHeight = this.tabPage1.Height;

            // tabPage1-车厅运行流程架构
            foreach(Control control in this.tabPage1.Controls)
            {
                control.Location = new Point(nMargin, nCurrentHeight);
                int nTempHeight = nGap;
                foreach (Control Cuffcc in control.Controls)
                {
                    if (Cuffcc.Visible)
                    {
                        Cuffcc.Location = new Point(nMargin, nTempHeight);
                        nTempHeight += Cuffcc.Height + nGap;
                    }
                }
                nCurrentHeight += nTempHeight;
                control.Size = new Size(nWidth, nTempHeight);
            }

            // tabPage2-Led配置
            this.tabPage2.Size = this.tabPage1.Size;
            this.DgvLed.Size = new Size(nWidth, nHeight - this.DgvLed.Location.Y - this.CupttsLed.Height - nGap);
            this.CupttsLed.Location = new Point(0, this.DgvLed.Location.Y + this.DgvLed.Height + 3 * nGap);// 翻页

            // tabPage3-语音配置
            this.tabPage3.Size = this.tabPage1.Size;
            this.DgvSound.Size = new Size(nWidth, nHeight - this.DgvSound.Location.Y - this.CupttsSound.Height - nGap);
            this.CupttsSound.Location = new Point(-2, this.DgvSound.Location.Y + this.DgvSound.Height + 3 * nGap);// 翻页
            this.CbSelectAllSound.Location = new Point(nWidth - this.CbSelectAllSound.Width, this.CbSelectAllSound.Location.Y);

            // tabPage4-LED内容修改
            this.tabPage4.Size = this.tabPage1.Size;
            this.GbLED.Location = new Point((nWidth - this.GbLED.Width) / 2, (nHeight - this.GbLED.Height) / 2 );

            // tabPage6-报文队列
            this.tabPage6.Size = this.tabPage1.Size;
            this.GbQueue.Size = new Size(nWidth, nHeight - this.DgvQueue.Location.Y);
            this.DgvQueue.Size = new Size(nWidth, nHeight - this.DgvQueue.Location.Y);

            // tabPage5-读取IC卡内存
            this.tabPage5.Size = this.tabPage1.Size;
            this.GbICCard.Location = new Point((nWidth - this.GbICCard.Width) / 2, (nHeight - this.GbICCard.Height) / 2);
        }

        #region 车厅流程图
        /// <summary>
        /// 更新当前流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateFlowChart(CarLocationPanelLib.PushService.CDeviceStatusDto deviceStatus)
        {
            try
            {
                if (null == deviceStatus || (int)EnmSMGType.Hall != deviceStatus.devicetype)
                {
                    return;
                }

                string strKey = "GbW" + deviceStatus.warehouse + "Hall" + deviceStatus.devicecode;
                Control control = this.tabPage1.Controls[strKey];// GbW1Hall11
                if (null == control)
                {
                    return;
                }
                Control controlE1 = null;
                Control controlE2 = null;

                if (1 < control.Controls.Count)
                {
                    controlE1 = control.Controls[1];
                }
                if (0 < control.Controls.Count)
                {
                    controlE2 = control.Controls[0];
                }

                if (null == controlE1 || typeof(CUserFlowChartControl) != controlE1.GetType() ||
                    null == controlE2 || typeof(CUserFlowChartControl) != controlE2.GetType())
                {
                    return;
                }

                CUserFlowChartControl cufccTempE1 = (CUserFlowChartControl)controlE1;
                CUserFlowChartControl cufccTempE2 = (CUserFlowChartControl)controlE2;
                cufccTempE1.Visible = true;
                cufccTempE2.Visible = true;

                // InvokeRequired需要比较调用线程ID和创建线程ID
                // 如果它们不相同则返回true
                if (cufccTempE1.InvokeRequired || cufccTempE2.InvokeRequired)
                {
                    CUserFlowChartControlCallback d = new CUserFlowChartControlCallback(SetHallFlowChart);
                    this.Invoke(d, deviceStatus, cufccTempE1, cufccTempE2);
                }
                else
                {
                    SetHallFlowChart(deviceStatus, cufccTempE1, cufccTempE2);
                }

                #region 流程处理
                int nPrevNode = 0;
                int nCurrentNode = 0;
                int nQueuePrevNode = 0;
                int nQueueCurrentNode = 0;

                if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.prevnode))
                {
                    nPrevNode = (int)deviceStatus.prevnode;
                }

                if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.currentnode))
                {
                    nCurrentNode = (int)deviceStatus.currentnode;
                }

                if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.queueprevnode))
                {
                    nQueuePrevNode = (int)deviceStatus.queueprevnode;
                }

                if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.queuecurrentnode))
                {
                    nQueueCurrentNode = (int)deviceStatus.queuecurrentnode;
                }

                if (CBaseMethods.MyBase.IsEmpty(deviceStatus.tasktype))
                {
                    if (EnmFlowTaskType.Init == cufccTempE1.EnumFlowTaskType)
                    {
                        cufccTempE1.SetFlowChartRunStatus(nPrevNode, nCurrentNode);
                    }
                    else
                    {
                        cufccTempE1.EnumFlowTaskType = EnmFlowTaskType.Init;
                    }

                    cufccTempE2.Visible = false;
                    return;
                }

                EnmFlowTaskType enmFlow = EnmFlowTaskType.Init;

                bool IsTower = false;// 塔库标志

                if (2 == deviceStatus.warehouse)
                {// 塔库
                    IsTower = true;
                }

                switch ((EnmTaskType)deviceStatus.tasktype)
                {
                    case EnmTaskType.EntryTask:
                        {
                            if (IsTower)
                            {
                                enmFlow = EnmFlowTaskType.TowerEnter;
                            }
                            else
                            {
                                enmFlow = EnmFlowTaskType.NormEnter;
                            }
                            cufccTempE2.Visible = false;
                            break;
                        }
                    case EnmTaskType.ExitTask:
                        {
                            if (IsTower)
                            {
                                enmFlow = EnmFlowTaskType.TowerExit;
                            }
                            else
                            {
                                enmFlow = EnmFlowTaskType.NormExit;
                            }

                            // 取车排队处理
                            if (0 == nQueuePrevNode && 0 == nQueueCurrentNode)
                            {
                                cufccTempE2.Visible = false;
                            }
                            else
                            {
                                cufccTempE2.Visible = true;
                                if (cufccTempE2.EnumFlowTaskType == EnmFlowTaskType.QueueExit)
                                {
                                    cufccTempE2.SetFlowChartRunStatus(nQueuePrevNode, nQueueCurrentNode);
                                }
                                else
                                {
                                    cufccTempE2.EnumFlowTaskType = EnmFlowTaskType.QueueExit;
                                }
                            }
                            break;
                        }
                    case EnmTaskType.TmpFetch:
                        {
                            enmFlow = EnmFlowTaskType.TmpFetch;
                            cufccTempE2.Visible = false;
                            break;
                        }
                    case EnmTaskType.MoveCarTask:
                        {
                            enmFlow = EnmFlowTaskType.MoveCar;
                            cufccTempE2.Visible = false;
                            break;
                        }
                    case EnmTaskType.MoveEquipTask:
                        {
                            enmFlow = EnmFlowTaskType.MoveEquip;
                            cufccTempE2.Visible = false;
                            break;
                        }
                    default:
                        {
                            cufccTempE2.Visible = false;
                            break;
                        }
                }

                if (enmFlow == cufccTempE1.EnumFlowTaskType)
                {
                    cufccTempE1.SetFlowChartRunStatus(nPrevNode, nCurrentNode);
                }
                else
                {
                    cufccTempE1.EnumFlowTaskType = enmFlow;
                }
                #endregion
            }
            catch (System.Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 语音配置
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 语音保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveSound_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认修改否？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            if (dr == DialogResult.Cancel)
            {
                return;
            }
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                List<CSoundDto> lstSound = ((BindingList<CSoundDto>)this.DgvSound.DataSource).ToList();

                // 修改语音列表
                bool flag = proxy.UpdateSoundDtoList(lstSound);

                if (flag)
                {
                    MessageBox.Show("修改语音成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("修改语音失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 全选“人工否”复选框选择否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbSelectAllSound_CheckedChanged(object sender, EventArgs e)
        {
            int flag = this.CbSelectAllSound.Checked ? 1 : 0;
            foreach (DataGridViewRow dgvr in this.DgvSound.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvr.Cells["Column4"];
                cell.Value = flag;
            }
        }
        #endregion

        #region LED配置
        /// <summary>
        /// LED修改关键字确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLEDOk_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (string.IsNullOrEmpty(this.TxtLEDOld.Text) || string.IsNullOrEmpty(this.TxtLEDNew.Text))
                {
                    MessageBox.Show("关键字、修改后内容都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.ModifyLEDContent(this.TxtLEDOld.Text.Trim(), this.TxtLEDNew.Text.Trim());

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            MessageBox.Show("修改LED内容成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("LED文本未有当前关键字!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("修改数据库失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("修改LED内容失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// LED保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveLed_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认修改否？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            if (dr == DialogResult.Cancel)
            {
                return;
            }
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                List<CLedContentDto> lstSound = ((BindingList<CLedContentDto>)this.DgvLed.DataSource).ToList();

                // 修改LED列表
                bool flag = proxy.UpdateLEDContentDtoList(lstSound);

                if (flag)
                {
                    MessageBox.Show("修改LED成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("修改LED失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        #region 私有函数
        /// <summary>
        /// 设置某个车厅的流程图
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="cufccTempE1"></param>
        /// <param name="cufccTempE2"></param>
        private void SetHallFlowChart(CarLocationPanelLib.PushService.CDeviceStatusDto deviceStatus, CUserFlowChartControl cufccTempE1, CUserFlowChartControl cufccTempE2)
        {
            int nPrevNode = 0;
            int nCurrentNode = 0;
            int nQueuePrevNode = 0;
            int nQueueCurrentNode = 0;

            if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.prevnode))
            {
                nPrevNode = (int)deviceStatus.prevnode;
            }

            if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.currentnode))
            {
                nCurrentNode = (int)deviceStatus.currentnode;
            }

            if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.queueprevnode))
            {
                nQueuePrevNode = (int)deviceStatus.queueprevnode;
            }

            if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.queuecurrentnode))
            {
                nQueueCurrentNode = (int)deviceStatus.queuecurrentnode;
            }

            if (CBaseMethods.MyBase.IsEmpty(deviceStatus.tasktype))
            {
                if (EnmFlowTaskType.Init != cufccTempE1.EnumFlowTaskType)
                {
                    cufccTempE1.EnumFlowTaskType = EnmFlowTaskType.Init;
                }

                cufccTempE2.Visible = false;
                return;
            }

            EnmFlowTaskType enmFlow = EnmFlowTaskType.Init;

            bool IsTower = false;// 塔库标志

            if (2 == deviceStatus.warehouse)
            {// 塔库
                IsTower = true;
            }

            switch ((EnmTaskType)deviceStatus.tasktype)
            {
                case EnmTaskType.EntryTask:
                    {
                        if (IsTower)
                        {
                            enmFlow = EnmFlowTaskType.TowerEnter;
                        }
                        else
                        {
                            enmFlow = EnmFlowTaskType.NormEnter;
                        }
                        cufccTempE2.Visible = false;
                        break;
                    }
                case EnmTaskType.ExitTask:
                    {
                        if (IsTower)
                        {
                            enmFlow = EnmFlowTaskType.TowerExit;
                        }
                        else
                        {
                            enmFlow = EnmFlowTaskType.NormExit;
                        }

                        // 取车排队处理
                        if (0 == nQueuePrevNode && 0 == nQueueCurrentNode)
                        {
                            cufccTempE2.Visible = false;
                        }
                        else
                        {
                            cufccTempE2.Visible = true;
                            if (cufccTempE2.EnumFlowTaskType == EnmFlowTaskType.QueueExit)
                            {
                                cufccTempE2.SetFlowChartRunStatus(nQueuePrevNode, nQueueCurrentNode);
                            }
                            else
                            {
                                cufccTempE2.EnumFlowTaskType = EnmFlowTaskType.QueueExit;
                                cufccTempE2.SetFlowChartRunStatus(nQueuePrevNode, nQueueCurrentNode);
                            }
                        }
                        break;
                    }
                case EnmTaskType.TmpFetch:
                    {
                        enmFlow = EnmFlowTaskType.TmpFetch;
                        cufccTempE2.Visible = false;
                        break;
                    }
                case EnmTaskType.MoveCarTask:
                    {
                        enmFlow = EnmFlowTaskType.MoveCar;
                        cufccTempE2.Visible = false;
                        break;
                    }
                case EnmTaskType.MoveEquipTask:
                    {
                        enmFlow = EnmFlowTaskType.MoveEquip;
                        cufccTempE2.Visible = false;
                        break;
                    }
                default:
                    {
                        cufccTempE2.Visible = false;
                        break;
                    }
            }

            if (enmFlow == cufccTempE1.EnumFlowTaskType)
            {
                if (EnmFlowTaskType.Init != enmFlow)
                {
                    cufccTempE1.SetFlowChartRunStatus(nPrevNode, nCurrentNode);
                }
            }
            else
            {
                cufccTempE1.EnumFlowTaskType = enmFlow;
                cufccTempE1.SetFlowChartRunStatus(nPrevNode, nCurrentNode);
            }
            this.OnResize(null);
        }

        /// <summary>
        /// 自适应回调函数实现
        /// </summary>
        private void CallBackOnResize()
        {
            OnResize(null);
        }

        /// <summary>
        /// 初始化车厅流程图界面
        /// </summary>
        private void InitializeGbHall()
        {
            foreach(int nWareHouse in CStaticClass.ConfigLstWareHouse())
            {
                foreach(int nHall in CStaticClass.ConfigLstHallDeviceID(nWareHouse))
                {
                    GroupBox GbWH = new GroupBox();
                    CUserFlowChartControl CuffccWHE1 = new CUserFlowChartControl();
                    CUserFlowChartControl CuffccWHE2 = new CUserFlowChartControl();
                    // 
                    // GbWH
                    // 
                    GbWH.Controls.Add(CuffccWHE1);
                    GbWH.Controls.Add(CuffccWHE2);
                    GbWH.Name = "GbW" + nWareHouse + "Hall" + nHall;
                    GbWH.Size = new Size(1040, 93);
                    GbWH.TabStop = false;
                    GbWH.Text = CStaticClass.ConvertWareHouse(nWareHouse) + CStaticClass.ConvertHallDescp(nWareHouse, nHall) + "流程图：";
                    // 
                    // CuffccWHE1
                    // 
                    CuffccWHE1.EnumFlowTaskType = EnmFlowTaskType.Init;
                    CuffccWHE1.Font = new Font("宋体", 9F);
                    CuffccWHE1.Name = "CuffccW" + nWareHouse + "Hall" + nHall + "E1";
                    CuffccWHE1.Size = new Size(80, 50);
                    CuffccWHE1.Text = "cUserFlowChartControl1";
                    CuffccWHE1.CallbackResize += new RefreshCallback(CallBackOnResize);
                    // 
                    // CuffccWHE2
                    // 
                    CuffccWHE2.EnumFlowTaskType = EnmFlowTaskType.Init;
                    CuffccWHE2.Font = new Font("宋体", 9F);
                    CuffccWHE2.Name = "CuffccW" + nWareHouse + "Hall" + nHall + "E2";
                    CuffccWHE2.Size = new Size(80, 50);
                    CuffccWHE2.Visible = false;
                    CuffccWHE2.Text = "cUserFlowChartControl1";
                    CuffccWHE2.CallbackResize += new RefreshCallback(CallBackOnResize);
                    this.tabPage1.Controls.Add(GbWH);
                }
            }
        }
        #endregion

        #region 队列报文
        /// <summary>
        /// 查询队列报文列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFindQueue_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                //if (string.IsNullOrEmpty(this.CboWareHouseTask.Text) || string.IsNullOrEmpty(this.CboDeviceCode.Text))
                //{
                //    MessageBox.Show("库区，设备都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                List<CWorkQueueDto> lstWorkQueue = GetFindQueueLst(proxy);
                if (null == lstWorkQueue || 0 == lstWorkQueue.Count)
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
        /// 删除报文
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
                if (null == this.DgvQueue.CurrentRow)
                {
                    MessageBox.Show("作业不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 删除数据库作业
                CWorkQueueDto workQueue = (CWorkQueueDto)this.DgvQueue.CurrentRow.DataBoundItem;
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.DeleteWorkQueueObject(workQueue.id);

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            GetFindQueueLst(proxy);
                            MessageBox.Show("删除作业成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("没有刷卡信息", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.FailToDelete:
                        {
                            MessageBox.Show("删除数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("删除作业失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 库区改变值时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboWareHouseTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                this.CboDeviceCode.Items.Clear();
                int nCurWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseTask.Text);
                List<object> lstHall = CStaticClass.ConfigLstHallDeviceIDDescp(nCurWareHouse);// 根据库区获取对应所有Hall设备号
                List<object> lstETV = CStaticClass.ConfigLstETVOrTVDeviceIDDescp(nCurWareHouse);// 根据库区获取对应所有Hall设备号
                this.CboDeviceCode.Items.Add("");
                this.CboDeviceCode.Items.AddRange(lstHall.ToArray());
                this.CboDeviceCode.Items.AddRange(lstETV.ToArray());
                this.CboDeviceCode.SelectedIndex = 0;
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
        /// 作业维护-作业类型对应解释（作业类型3）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvQueue_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (null == e.Value)
            {
                return;
            }

            // 作业类型
            if (3 == e.ColumnIndex)
            {
                if (e.Value.GetType() == typeof(int))
                {
                    e.Value = CStaticClass.ConvertSwipeCountType((int)e.Value);
                }
            }
        }

        /// <summary>
        /// 查询报文列表处理
        /// </summary>
        /// <param name="proxy"></param>
        private List<CWorkQueueDto> GetFindQueueLst(QueryServiceClient proxy)
        {
            List<CWorkQueueDto> lstWorkQueue = null;
            if (string.IsNullOrEmpty(this.CboWareHouseTask.Text) || string.IsNullOrEmpty(this.CboDeviceCode.Text))
            {// 查询所有队列报文
                lstWorkQueue = proxy.GetSendTelegramQueue();
            }
            else
            {
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseTask.Text);
                int nDeviceCode = 0;
                if (this.CboDeviceCode.Text.Contains("ETV"))
                {
                    nDeviceCode = CStaticClass.ConvertETVDescp(this.CboDeviceCode.Text);
                }
                else
                {
                    nDeviceCode = CStaticClass.ConvertHallDescp(nWareHouse, this.CboDeviceCode.Text);
                }
                lstWorkQueue = proxy.GetSendTelegramQueueByEquipID(nWareHouse, nDeviceCode);
            }

            this.DgvQueue.DataSource = new BindingList<CWorkQueueDto>(lstWorkQueue);
            return lstWorkQueue;
        }
        #endregion

        #region 读取IC卡内存数据
        /// <summary>
        /// 读取IC卡内存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadICCardData_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                struICCardData iccardData = new struICCardData();
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.ReadICCardData(out iccardData);

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            this.TxtICCardID.Text = iccardData.strICCardID;
                            this.TxtFeeType.Text = CStaticClass.ConvertFeeType((int)iccardData.enmFeeType);
                            this.TxtFeeStartTime.Text = iccardData.dtFeeStartTime.ToString();
                            this.TxtFeeEndTime.Text = iccardData.dtFeeEndTime.ToString();
                            MessageBox.Show("读取IC卡内存数据成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.FailConnection:
                        {
                            MessageBox.Show("连接刷卡器失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("获取刷卡器对象失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("读取IC卡内存数据失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        #endregion
    }
}
