using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.PushService;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using CustomControlLib;
using System.ServiceModel;
using BaseMethodLib;

namespace WindowsFormLib
{
    public partial class CFormSystemMtc : Form
    {
        Form m_formCarLocationStatus = new CFormCarLocation();

        public CFormSystemMtc()
        {
            InitializeComponent();

            this.CboWareHouse.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CboWareHouseFind.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CboWareHouseDis.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CboWareHouseInJog.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CboWareHouseHandIn.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CboWareHouseHandOut.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CboWareHouseTask.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CboWareHouse.SelectedIndex = 0;
            this.CboWareHouseFind.SelectedIndex = 0;
            this.CboWareHouseDis.SelectedIndex = 0;
            this.CboWareHouseInJog.SelectedIndex = 0;
            this.CboWareHouseHandIn.SelectedIndex = 0;
            this.CboWareHouseHandOut.SelectedIndex = 0;
            this.CboWareHouseTask.SelectedIndex = 0;
           
            this.DgvTask.AutoGenerateColumns = false;
            this.DgvTask.AllowDrop = true;
            // 设置键盘“Esc”按钮
            Button BtnCancel = new Button();
            this.CancelButton = BtnCancel;
            BtnCancel.Click += new EventHandler(BtnCancel_Click);
            m_formCarLocationStatus.ClientSize = new System.Drawing.Size(CStaticClass.ConfigMinWidth(), CStaticClass.ConfigMinHeight());
            m_formCarLocationStatus.FormBorderStyle = FormBorderStyle.FixedDialog;
            m_formCarLocationStatus.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// 登陆界面事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CFormSystemMtc_Load(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                this.CboTaskType.Items.Clear();
                // 所有作业类型
                List<CarLocationPanelLib.QueryService.CDeviceStatusDto> lstTaskType = proxy.GetTaskTypeList();
                this.CboTaskType.Items.AddRange(lstTaskType.ToArray());

                if (null == lstTaskType || 1 > lstTaskType.Count)
                {
                    ClearFaultControls(null);
                }

                // 作业维护
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseTask.Text);
                int nDeviceCode = CStaticClass.ConvertHallDescp(nWareHouse, this.CboDeviceCode.Text);
                List<CWorkQueueDto> lstWorkQueue = proxy.QueryWorkQueue(nWareHouse, nDeviceCode);
                this.DgvTask.DataSource = new BindingList<CWorkQueueDto>(lstWorkQueue);
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
            //CboTaskType_Click(sender, e);
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

        #region 车位地址通过车位状态获取
        /// <summary>
        /// 设置手动挪移源地址值
        /// </summary>
        /// <param name="strSrcLocAddr"></param>
        public void SetInJogSrcLocAddr(string strSrcLocAddr)
        {
            this.TxtSrcAddrInJog.Text = strSrcLocAddr;
            m_formCarLocationStatus.Close();
        }

        /// <summary>
        /// 设置手动挪移目的地址值
        /// </summary>
        /// <param name="strDestLocAddr"></param>
        public void SetInJogDestLocAddr(string strDestLocAddr)
        {
            this.TxtDestAddrInJog.Text = strDestLocAddr;
            m_formCarLocationStatus.Close();
        }

        /// <summary>
        /// 设置禁用车位地址值
        /// </summary>
        /// <param name="strDisLocAddr"></param>
        public void SetDisLocAddr(string strDisLocAddr)
        {
            this.TxtCarLocAddrDis.Text = strDisLocAddr;
            m_formCarLocationStatus.Close();
        }

        /// <summary>
        /// 设置手动入库车位地址值
        /// </summary>
        /// <param name="strHandInLocAddr"></param>
        public void SetHandInLocAddr(string strHandInLocAddr)
        {
            this.TxtCarLocAddrHandIn.Text = strHandInLocAddr;
            m_formCarLocationStatus.Close();
        }

        /// <summary>
        /// 设置手动出库车位地址值
        /// </summary>
        /// <param name="strHandOutLocAddr"></param>
        public void SetHandOutLocAddr(string strHandOutLocAddr)
        {
            this.TxtCarLocAddrHandOut.Text = strHandOutLocAddr;
            m_formCarLocationStatus.Close();
        }

        /// <summary>
        /// 单击车位文本框根据当前库车位状态选择车位地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCarLocAddr_Click(object sender, EventArgs e)
        {
            try
            {
                string strWareHouse = string.Empty;
                EnmTxtCarLocationAddr enmType = EnmTxtCarLocationAddr.Init;
                CUserTextButton tempSender = (CUserTextButton)sender;

                if (tempSender == this.TxtSrcAddrInJog)
                {
                    if (string.IsNullOrEmpty(this.CboWareHouseInJog.Text))
                    {
                        MessageBox.Show("库区不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    strWareHouse = this.CboWareHouseInJog.Text.Trim(); 
                    enmType = EnmTxtCarLocationAddr.HandInJogSrc;
                }
                else if (tempSender == this.TxtDestAddrInJog)
                {
                    if (string.IsNullOrEmpty(this.CboWareHouseInJog.Text))
                    {
                        MessageBox.Show("库区不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    strWareHouse = this.CboWareHouseInJog.Text.Trim(); 
                    enmType = EnmTxtCarLocationAddr.HandInJogDest;
                }
                else if (tempSender == this.TxtCarLocAddrDis)
                {
                    if (string.IsNullOrEmpty(this.CboWareHouseDis.Text))
                    {
                        MessageBox.Show("库区不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    strWareHouse = this.CboWareHouseDis.Text.Trim(); 
                    enmType = EnmTxtCarLocationAddr.Dis;
                }
                else if (tempSender == this.TxtCarLocAddrHandIn)
                {
                    if (string.IsNullOrEmpty(this.CboWareHouseHandIn.Text))
                    {
                        MessageBox.Show("库区不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    strWareHouse = this.CboWareHouseHandIn.Text.Trim(); 
                    enmType = EnmTxtCarLocationAddr.HandIn;
                }
                else if (tempSender == this.TxtCarLocAddrHandOut)
                {
                    if (string.IsNullOrEmpty(this.CboWareHouseHandOut.Text))
                    {
                        MessageBox.Show("库区不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    strWareHouse = this.CboWareHouseHandOut.Text.Trim(); 
                    enmType = EnmTxtCarLocationAddr.HandOut;
                }

                // 显示当前车库车位状态对话框
                CStaticClass.showFormCarLocationStatus(this, m_formCarLocationStatus, strWareHouse, enmType);
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

        #region button按钮单击触发事件
        /// <summary>
        /// 手动完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFinish_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (0 > this.CboTaskType.SelectedIndex)
                {
                    MessageBox.Show("作业类型不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatusQuery = (CarLocationPanelLib.QueryService.CDeviceStatusDto)this.CboTaskType.SelectedItem;

                if (null == deviceStatusQuery)
                {
                    MessageBox.Show("作业类型不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CarLocationPanelLib.PushService.CDeviceStatusDto deviceStatus = CStaticClass.ConvertDeviceStatus(deviceStatusQuery);
                DialogResult dr = MessageBox.Show("确认手动完成吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                // 手动完成作业
                CarLocationPanelLib.PushService.EnmFaultType type = push.HandCompleteTask(deviceStatus.warehouse,deviceStatus.devicecode);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            InsertSysLog(proxy, deviceStatusQuery, this.BtnFinish.Text + "作业：");
                            MessageBox.Show(this.BtnFinish.Text + "作业成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.WorkQueueNotEmpty:
                        {
                            InsertSysLog(proxy, deviceStatusQuery, this.BtnFinish.Text + "作业：");
                            DeviceTelegramQueue(deviceStatus);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("没有制卡!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("没有找到合适车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.TaskOnICCard:
                        {
                            MessageBox.Show("当前卡有车存在车库!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NoCarInGarage:
                        {
                            MessageBox.Show("当前卡没有车存在车库!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.HallEquip:
                        {
                            MessageBox.Show("当前车位是车厅设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotNormalCarPOSN:
                        {
                            MessageBox.Show("不是正常车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.OverCarInSize:
                        {
                            MessageBox.Show("入库车辆尺寸超限!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToDelete:
                        {
                            MessageBox.Show("删除数据库失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToAllocETVorTV:
                        {
                            InsertSysLog(proxy, deviceStatusQuery, this.BtnFinish.Text + "作业：");
                            MessageBox.Show("分配ETV或TV失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show(this.BtnFinish.Text + "作业失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

        /// <summary>
        /// 手动复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (0 > this.CboTaskType.SelectedIndex)
                {
                    MessageBox.Show("作业类型不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatusQuery = (CarLocationPanelLib.QueryService.CDeviceStatusDto)this.CboTaskType.SelectedItem;

                if (null == deviceStatusQuery)
                {
                    MessageBox.Show("作业类型不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CarLocationPanelLib.PushService.CDeviceStatusDto deviceStatus = CStaticClass.ConvertDeviceStatus(deviceStatusQuery);
                DialogResult dr = MessageBox.Show("确认手动复位吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                // 手动复位
                CarLocationPanelLib.PushService.EnmFaultType type = push.HandResetTask(deviceStatus.warehouse,deviceStatus.devicecode);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            InsertSysLog(proxy, deviceStatusQuery, this.BtnReset.Text + "作业：");
                            MessageBox.Show(this.BtnReset.Text + "作业成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.WorkQueueNotEmpty:
                        {
                            InsertSysLog(proxy, deviceStatusQuery, this.BtnReset.Text + "作业：");
                            DeviceTelegramQueue(deviceStatus);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Fail:
                        {
                            MessageBox.Show(this.BtnReset.Text + "作业失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        } 
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("没有找到指定车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.OverCarInSize:
                        {
                            MessageBox.Show("入库车辆尺寸超限!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.HallEquip:
                        {
                            MessageBox.Show("当前车位是车厅设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotNormalCarPOSN:
                        {
                            MessageBox.Show("不是正常车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToDelete:
                        {
                            MessageBox.Show("删除数据库失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToAllocETVorTV:
                        {
                            InsertSysLog(proxy, deviceStatusQuery, this.BtnReset.Text + "作业：");
                            MessageBox.Show("分配ETV或TV失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show(this.BtnReset.Text + "作业失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

        /// <summary>
        /// 查询车位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFind_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (string.IsNullOrEmpty(this.CTxtICCardFind.Text))
                {
                    MessageBox.Show("用户卡号不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.TxtCarLocAddrFind.Text = "";
                this.CboWareHouseFind.Text = null;// "";
                CarLocationPanelLib.QueryService.CCarLocationDto carLoc = null;
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryCarPOSNByCardID(out carLoc, this.CTxtICCardFind.Text.Trim());

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            if (null != carLoc)
                            {
                                this.TxtCarLocAddrFind.Text = carLoc.carlocaddr;
                                this.CboWareHouseFind.Text = CStaticClass.ConvertWareHouse(carLoc.warehouse);//.ToString();
                                MessageBox.Show("查询车位成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else
                            {
                                MessageBox.Show("查询车位失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                        }
                    // 获取的逻辑卡号是空值
                    case CarLocationPanelLib.QueryService.EnmFaultType.Null:
                        {
                            MessageBox.Show("传入的参数卡号为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 没有制卡
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("没有制卡", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // IC卡注销或挂失
                    case CarLocationPanelLib.QueryService.EnmFaultType.LossORCancel:
                        {
                            MessageBox.Show("IC卡注销或挂失", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // IC卡注销或挂失
                    case CarLocationPanelLib.QueryService.EnmFaultType.NotMatch:
                        {
                            MessageBox.Show("IC卡类型不正确", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 当前IC卡没有存车
                    case CarLocationPanelLib.QueryService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("当前IC卡没有存车", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("查询车位失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 手动挪移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInJog_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouseInJog.Text) ||
                    string.IsNullOrEmpty(this.TxtSrcAddrInJog.Text) || 
                    string.IsNullOrEmpty(this.TxtDestAddrInJog.Text))
                {
                    MessageBox.Show("库区，源地址，目的地址都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string strSrcCarPOSNAddr = this.TxtSrcAddrInJog.Text.Trim();
                string strDestCarPOSNAddr = this.TxtDestAddrInJog.Text.Trim();
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseInJog.Text);
                CarLocationPanelLib.PushService.EnmFaultType type = push.ManualMoveVEH(nWareHouse, strSrcCarPOSNAddr, strDestCarPOSNAddr);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "手动挪移成功：源车位-" + strSrcCarPOSNAddr + " 目标车位-" + strDestCarPOSNAddr + " 库号-" + nWareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("挪移成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("没有找到源车位或目的车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotNormalCarPOSN:
                        {
                            MessageBox.Show("源车位或目的车位不是正常车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NoCarInGarage:
                        {
                            MessageBox.Show("没有车存在源车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.CarInGarage:
                        {
                            MessageBox.Show("有车存在目的车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.TaskOnICCard:
                        {
                            MessageBox.Show("目的车位的车辆正在作业", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("源车位的IC卡没有制卡", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FixedCarPOSN:
                        {
                            MessageBox.Show("目的车位是其他车主的固定车位，临时卡或定期卡无法挪移到固定车位卡的车位上", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotMatch:
                        {
                            MessageBox.Show("目的车位是其他车主的固定车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.OverCarInSize:
                        {
                            MessageBox.Show("目的车位尺寸不满足车辆尺寸", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("获取源车位或目的车位信息失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("挪移失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

        /// <summary>
        /// 禁用车位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDisable_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouseDis.Text) || string.IsNullOrEmpty(this.TxtCarLocAddrDis.Text))
                {
                    MessageBox.Show("库区，车位都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseDis.Text);
                string strCarPOSNAddr = this.TxtCarLocAddrDis.Text.Trim();
                EnmLocationType enmCarPOSNType = EnmLocationType.Disable;
                CarLocationPanelLib.PushService.EnmFaultType type = push.ModifyCarPOSNType(nWareHouse, strCarPOSNAddr, enmCarPOSNType);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "禁用车位成功：车位-" + strCarPOSNAddr + "  库号-" + nWareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("禁用成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("没有找到指定车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotAllowed:
                        {
                            MessageBox.Show("该车位非空闲正常车位，不允许禁用该车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("禁用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("禁用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

        /// <summary>
        /// 启用车位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnable_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouseDis.Text) || string.IsNullOrEmpty(this.TxtCarLocAddrDis.Text))
                {
                    MessageBox.Show("库区，车位都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseDis.Text);
                string strCarPOSNAddr = this.TxtCarLocAddrDis.Text.Trim();
                EnmLocationType enmCarPOSNType = EnmLocationType.Normal;
                CarLocationPanelLib.PushService.EnmFaultType type = push.ModifyCarPOSNType(nWareHouse, strCarPOSNAddr, enmCarPOSNType);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "启用车位成功：车位-" + strCarPOSNAddr + "  库号-" + nWareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("启用成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("没有找到指定车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotAllowed:
                        {
                            MessageBox.Show("该车位非禁用车位，不允许启用该车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("启用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

        /// <summary>
        /// 手动入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHandIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CStaticClass.CheckPushService())
                {// 检查服务
                    return;
                }

                QueryServiceClient proxy = new QueryServiceClient();
                PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
                try
                {
                    if (string.IsNullOrEmpty(this.CboWareHouseHandIn.Text))
                    {
                        MessageBox.Show("库区不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseHandIn.Text);

                    if (string.IsNullOrEmpty(this.TxtCarLocAddrHandIn.Text))
                    {
                        if (!string.IsNullOrEmpty(this.TxtICCardHand.Text))
                        {
                            CICCardDto iccard = new CICCardDto();
                            iccard.iccode = this.TxtICCardHand.Text.Trim();
                            proxy.QueryICCardInfo(ref iccard);

                            if (null == iccard || (int)EnmICCardType.FixedLocation != iccard.ictype)
                            {
                                MessageBox.Show("车位不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("车位不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(this.TxtICCardHand.Text))
                    {
                        MessageBox.Show("用户卡号不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(TxtCarSize.Text.Trim()))
                    {
                        MessageBox.Show("车辆外形不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(TxtWheelBase.Text.Trim()))
                    {
                        MessageBox.Show("车辆轴距不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(txtCarleght.Text.Trim()))
                    {
                        MessageBox.Show("全车长度不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(txtOverRang.Text.Trim()))
                    {
                        MessageBox.Show("前悬长度不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 手动入库
                    CarLocationPanelLib.PushService.CCarLocationDto editDto = new CarLocationPanelLib.PushService.CCarLocationDto();
                    editDto.warehouse = nWareHouse;
                    editDto.carlocaddr = this.TxtCarLocAddrHandIn.Text.Trim();
                    editDto.iccode = this.TxtICCardHand.Text.Trim();
                    editDto.direction = 0;
                    editDto.carsize = this.TxtCarSize.Text.Trim();
                    int nWheelBase;
                    CBaseMethods.MyBase.StringToUInt32(this.TxtWheelBase.Text, out nWheelBase);
                    editDto.carwheelbase = nWheelBase;
                    editDto.overallLg = Convert.ToInt32(txtCarleght.Text.Trim());
                    editDto.overhang = Convert.ToInt32(txtOverRang.Text.Trim());
                    editDto.carintime = this.DtpInTime.Value;

                    CarLocationPanelLib.PushService.EnmFaultType type = push.ManualVEHEntry(editDto);

                    switch (type)
                    {
                        case CarLocationPanelLib.PushService.EnmFaultType.Success:
                            {
                                CSystemLogDto log = new CSystemLogDto();
                                log.curtime = CStaticClass.CurruntDateTime();
                                log.logdescp = "手动入库成功：用户卡号-" + editDto.iccode + 
                                    " 车位-" + editDto.carlocaddr + 
                                    " 轴距-"+editDto.carwheelbase+
                                    " 全车-"+editDto.overallLg+
                                    " 前悬-"+editDto.overhang+
                                    " 入库时间-" + editDto.carintime + 
                                    "  库号-" + editDto.warehouse;
                                log.optcode = CStaticClass.myOperator.optcode;
                                log.optname = CStaticClass.myOperator.optname;
                                proxy.InsertSysLog(log);
                                MessageBox.Show("入库成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.Null:
                            {
                                MessageBox.Show("传入的参数卡号为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NoICCardInfo:
                            {
                                MessageBox.Show("当前IC卡未制卡！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.LossORCancel:
                            {
                                MessageBox.Show("IC卡注销或挂失！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotMatch:
                            {
                                MessageBox.Show("IC卡类型不正确！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.Fail:
                            {
                                MessageBox.Show("获取车位信息失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotNormalCarPOSN:
                            {
                                MessageBox.Show("不是正常车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.HallEquip:
                            {
                                MessageBox.Show("当前车位是车厅设备！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.CarInGarage:
                            {
                                MessageBox.Show("当前车位有车，不允许将车放到该车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.TaskOnICCard:
                            {
                                MessageBox.Show("当前卡有车存在车库！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.OverCarInSize:
                            {
                                MessageBox.Show("入库车辆尺寸超限！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                            {
                                MessageBox.Show("更新数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                            {
                                MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("入库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                push.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 手动出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHandOut_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouseHandOut.Text) || string.IsNullOrEmpty(this.TxtCarLocAddrHandOut.Text))
                {
                    MessageBox.Show("库区，车位都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dr = MessageBox.Show("确认出库？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseHandOut.Text);
                string strAddr = this.TxtCarLocAddrHandOut.Text.Trim();
                
                CarLocationPanelLib.PushService.EnmFaultType type = push.ManualVEHExit(nWareHouse, strAddr);
                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "手动出库成功：车位-" + strAddr + "  库号-" + nWareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("出库成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("获取车位信息失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.HallEquip:
                        {
                            MessageBox.Show("当前车位是车厅设备", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotNormalCarPOSN:
                        {
                            MessageBox.Show("不是正常车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.CarInGarage:
                        {
                            MessageBox.Show("当前车位无车，无车出库", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Null:
                        {
                            MessageBox.Show("空闲车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("出库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

        /// <summary>
        /// 查询作业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFindTask_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouseTask.Text) || this.CboDeviceCode.SelectedIndex == -1)
                {
                    MessageBox.Show("库区，设备都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CboDeviceCode.SelectedItem.ToString() == "所有")
                {
                    int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseTask.Text);
                    List<CWorkQueueDto> lstQueue = proxy.GetAllQueueForDisp(nWareHouse);
                    this.DgvTask.DataSource = new BindingList<CWorkQueueDto>(lstQueue);
                }
                else
                {
                    int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseTask.Text);
                    int nDeviceCode = CStaticClass.ConvertHallDescp(nWareHouse, this.CboDeviceCode.Text);
                    List<CWorkQueueDto> lstWorkQueue = proxy.QueryWorkQueue(nWareHouse, nDeviceCode);
                    this.DgvTask.DataSource = new BindingList<CWorkQueueDto>(lstWorkQueue);
                    if (null == lstWorkQueue || 0 == lstWorkQueue.Count)
                    {
                        MessageBox.Show("抱歉，没有找到符合条件的记录！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
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
        /// 删除作业
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
                if (null == this.DgvTask.CurrentRow)
                {
                    MessageBox.Show("作业不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dr = MessageBox.Show("确认删除作业？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                // 删除数据库作业
                CWorkQueueDto workQueue = (CWorkQueueDto)this.DgvTask.CurrentRow.DataBoundItem;
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.DeleteWorkQueueObject(workQueue.id);

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {                           
                            int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseTask.Text);
                            int nDeviceCode = CStaticClass.ConvertHallDescp(nWareHouse, this.CboDeviceCode.Text);
                            List<CWorkQueueDto> lstWorkQueue = proxy.QueryWorkQueue(nWareHouse, nDeviceCode);
                            this.DgvTask.DataSource = new BindingList<CWorkQueueDto>(lstWorkQueue);
                            CSystemLogDto systemLogTBL = new CSystemLogDto();
                            systemLogTBL.curtime = CStaticClass.CurruntDateTime();
                            systemLogTBL.logdescp = string.Format("删除作业成功:IC卡号-{0}, 库区号-{1}, 车厅号-{2}；", workQueue.iccode, nWareHouse, nDeviceCode);
                            systemLogTBL.optcode = CStaticClass.myOperator.optcode;
                            systemLogTBL.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(systemLogTBL);
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
                            MessageBox.Show("删除作业失败-"+(CarLocationPanelLib.QueryService.EnmFaultType)type, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 一键出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAllHandOut_Click(object sender, EventArgs e)
        {
            DialogResult dia = new FrmPassword().ShowDialog();
            if (dia != DialogResult.OK) 
            {
                return;
            }

            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouseHandOut.Text))
                {
                    MessageBox.Show("库区不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dr = MessageBox.Show("确定一键出库吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseHandOut.Text);
                CarLocationPanelLib.PushService.EnmFaultType type = push.InitAllCarPOSN(nWareHouse);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "一键出库成功： 库号-" + nWareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("一键出库成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("一键出库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

        /// <summary>
        /// 一键禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAllDisable_Click(object sender, EventArgs e)
        {
            DialogResult dia = new FrmPassword().ShowDialog();
            if (dia != DialogResult.OK)
            {
                return;
            }

            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouseDis.Text))
                {
                    MessageBox.Show("库区不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dr = MessageBox.Show("确定一键禁用吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseDis.Text);
                CarLocationPanelLib.PushService.EnmFaultType type = push.ModifyAllCarPOSNType(nWareHouse, EnmLocationType.Disable);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "一键禁用成功： 库号-" + nWareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("一键禁用成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("一键禁用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

        /// <summary>
        /// 一键启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAllEnable_Click(object sender, EventArgs e)
        {
            DialogResult dia = new FrmPassword().ShowDialog();
            if (dia != DialogResult.OK)
            {
                return;
            }

            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouseDis.Text))
                {
                    MessageBox.Show("库区不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dr = MessageBox.Show("确定一键启用吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouseDis.Text);
                CarLocationPanelLib.PushService.EnmFaultType type = push.ModifyAllCarPOSNType(nWareHouse, EnmLocationType.Normal);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "一键启用成功： 库号-" + nWareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("一键启用成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("一键启用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }
        #endregion

        #region 故障处理+作业维护
        /// <summary>
        /// 故障处理-作业类型选择改变触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTaskType_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                this.CboTaskType.Items.Clear();
                // 所有作业类型
                List<CarLocationPanelLib.QueryService.CDeviceStatusDto> lstTaskType = proxy.GetTaskTypeList();
                this.CboTaskType.Items.AddRange(lstTaskType.ToArray());
                
                if (null == lstTaskType || 1 > lstTaskType.Count)
                {
                    ClearFaultControls(null);
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
        /// 故障处理-作业类型选择改变触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus = (CarLocationPanelLib.QueryService.CDeviceStatusDto)this.CboTaskType.SelectedItem;

                if (null == deviceStatus)
                {
                    return;
                }

                this.CboWareHouse.Enabled = false;
                this.TxtHall.Enabled = false;
                this.TxtICCardID.Enabled = false;
                this.TxtTaskStatus.Enabled = false;
                this.CboWareHouse.Text = CStaticClass.ConvertWareHouse(deviceStatus.warehouse);//.ToString();
                this.btnMuro.Enabled = false;

                if ((int)EnmSMGType.Hall == deviceStatus.devicetype)
                {
                    this.LblHall.Text = "所在车厅";
                    this.TxtHall.Text = CStaticClass.ConvertHallDescp(deviceStatus.warehouse, deviceStatus.devicecode);//.ToString();
                }
                else
                {
                    this.LblHall.Text = "所在ETV";
                    this.TxtHall.Text = CStaticClass.ConvertETVDescp(deviceStatus.devicecode);//.ToString();
                }

                this.TxtICCardID.Text = deviceStatus.iccode;
                this.BtnFinish.Text = "手动完成";
                this.BtnReset.Text = "手动复位";

                switch((EnmTaskType)deviceStatus.tasktype)
                {
                    case EnmTaskType.EntryTask:
                        {
                            this.TxtTaskStatus.Text = "存车作业:" + CStaticClass.ConvertFlowNodeDescpType(deviceStatus.currentnode);
                            break;
                        }
                    case EnmTaskType.ExitTask:
                        {
                            this.TxtTaskStatus.Text = "取车作业:" + CStaticClass.ConvertFlowNodeDescpType(deviceStatus.currentnode);
                            break;
                        }
                    case EnmTaskType.MoveCarTask:
                        {
                            this.TxtTaskStatus.Text = "库内挪移作业:" + CStaticClass.ConvertFlowNodeDescpType(deviceStatus.currentnode);
                            break;
                        }
                    case EnmTaskType.MoveEquipTask:
                        {
                            this.TxtTaskStatus.Text = "移动设备作业:" + CStaticClass.ConvertFlowNodeDescpType(deviceStatus.currentnode);
                            break;
                        }
                    case EnmTaskType.TmpFetch:
                        {
                            this.TxtTaskStatus.Text = "临时取物作业:" + CStaticClass.ConvertFlowNodeDescpType(deviceStatus.currentnode);
                            this.BtnFinish.Text = "手动完成出库";
                            this.BtnReset.Text = "手动复位入库";
                            break;
                        }
                    case EnmTaskType.VehRotationTask:
                        {
                            this.TxtTaskStatus.Text = "车辆旋转作业:" + CStaticClass.ConvertFlowNodeDescpType(deviceStatus.currentnode);
                            break;
                        }
                    default:
                        {
                            this.TxtTaskStatus.Text = string.Empty;
                            break;
                        }
                }
                if (deviceStatus.tasktype == (int)EnmTaskType.EntryTask ||
                    deviceStatus.tasktype == (int)EnmTaskType.ExitTask ||
                    deviceStatus.tasktype == (int)EnmTaskType.TmpFetch) 
                {
                    if (deviceStatus.currentnode == (int)EnmFlowNodeDescp.TMURO) 
                    {
                        btnMuro.Enabled = true;
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
        }
   
        /// <summary>
        /// 故障处理-作业类型绑定到数据值时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTaskType_Format(object sender, ListControlConvertEventArgs e)
        {
            try
            {
                CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus = (CarLocationPanelLib.QueryService.CDeviceStatusDto)e.ListItem;

                switch ((EnmTaskType)deviceStatus.tasktype)
                {
                    case EnmTaskType.EntryTask:
                        {
                            e.Value = deviceStatus.iccode + "存车";//SaveCar
                            break;
                        }
                    case EnmTaskType.ExitTask:
                        {
                            e.Value = deviceStatus.iccode + "取车";//GetCar
                            break;
                        }
                    case EnmTaskType.MoveCarTask:
                        {
                            e.Value = deviceStatus.iccode + "挪移";//MoveCar
                            break;
                        }
                    case EnmTaskType.MoveEquipTask:
                        {
                            e.Value = deviceStatus.iccode + "移动设备";//MoveEquip
                            break;
                        }
                    case EnmTaskType.TmpFetch:
                        {
                            e.Value = deviceStatus.iccode + "临时取物";//TmpFetch
                            break;
                        }
                    case EnmTaskType.VehRotationTask:
                        {
                            e.Value = deviceStatus.iccode + "车辆旋转";//VehRotationTask
                            break;
                        }
                    case EnmTaskType.AvoidMove:
                        {
                            e.Value = deviceStatus.iccode + "避让";//VehRotationTask
                            break;
                        }
                    default:
                        {
                            e.Value = "待命";//Error
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
                this.CboDeviceCode.Items.AddRange(lstHall.ToArray());
                this.CboDeviceCode.Items.Add("所有");
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
        /// 作业维护-鼠标在单元格里移动时激活拖放功能，只有单击才执行拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvTask_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        /// <summary>
        /// 作业维护-单元格拖放设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvTask_DragOver(object sender, DragEventArgs e)
        {

        }

        /// <summary>
        /// 作业维护行拖动时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvTask_DragDrop(object sender, DragEventArgs e)
        {

        }

        /// <summary>
        /// 作业维护-作业类型对应解释（作业类型3）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvTask_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                    e.Value = CStaticClass.ConvertTaskType((int)e.Value);
                }
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 清空故障处理控件内容
        /// </summary>
        /// <param name="deviceStatus"></param>
        private void ClearFaultControls(CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus)
        {
            if (null != deviceStatus)
            {
                this.CboTaskType.Items.Remove(deviceStatus);
            }

            this.CboTaskType.SelectedIndex = -1;
            this.CboWareHouse.Enabled = true;
            this.CboWareHouse.SelectedIndex = -1;
            this.TxtHall.Enabled = true;
            this.TxtHall.Text = "";
            this.TxtICCardID.Enabled = true;
            this.TxtICCardID.Text = "";
            this.TxtTaskStatus.Enabled = true;
            this.TxtTaskStatus.Text = "";
        }

        /// <summary>
        /// 根据坐标获取列表行索引
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int GetRowFromPoint(int x, int y)
        {
            for (int i = 0; i < this.DgvTask.RowCount; i++)
            {
                Rectangle rec = this.DgvTask.GetRowDisplayRectangle(i, false);

                if (this.DgvTask.RectangleToScreen(rec).Contains(x, y))
                { 
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 故障处理后是否启动下一作业
        /// </summary>
        /// <param name="deviceStatus"></param>
        private void DeviceTelegramQueue(CarLocationPanelLib.PushService.CDeviceStatusDto deviceStatus)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                string str = string.Format("故障处理后，是否启动下一作业？");
                DialogResult dr = MessageBox.Show(str, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                // 是否启动下一作业
                CarLocationPanelLib.PushService.EnmFaultType type = push.StartWorkQueue(deviceStatus);
                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            MessageBox.Show("无下一作业!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundEquip:
                        {
                            MessageBox.Show("没有找到设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.InvalidWareHouseID:
                        {
                            MessageBox.Show("无效库区号!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Null:
                        {
                            MessageBox.Show("下一取车队列的传入的参数卡号为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("下一取车队列的IC卡没有制卡!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.LossORCancel:
                        {
                            MessageBox.Show("下一取车队列的IC卡注销或挂失!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotMatch:
                        {
                            MessageBox.Show("下一取车队列的IC卡类型不正确!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NoCarInGarage:
                        {
                            MessageBox.Show("下一取车队列的用户没有车存在库内!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToSendTelegram:
                        {
                            MessageBox.Show("下一取车队列发送报文失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.WorkQueueNotEmpty:
                        {
                            MessageBox.Show("正在为取车排队的车主取车，请稍后片刻!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailConnection:
                        {
                            MessageBox.Show("PLC网络连接中断，请检查网络连接再继续", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("启动下一作业失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(CBaseMethods.MyBase.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CBaseMethods.MyBase.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CBaseMethods.MyBase.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            push.Close();
        }

        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="deviceStatus"></param>
        /// <param name="strDescp"></param>
        private void InsertSysLog(QueryServiceClient proxy, CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatusQuery, string strDescp)
        {
            string strTask = this.CboTaskType.Text;
            string strCarLocAddr = "";
            string strICCardID = deviceStatusQuery.iccode;
            ClearFaultControls(deviceStatusQuery);

            // 查询车位根据IC卡号
            CarLocationPanelLib.QueryService.CCarLocationDto carLoc = null;
            proxy.QueryCarPOSNByCardID(out carLoc, strICCardID);

            if (null != carLoc)
            {
                strCarLocAddr = carLoc.carlocaddr;
            }

            CSystemLogDto log = new CSystemLogDto() 
            { 
                curtime = CStaticClass.CurruntDateTime(),
                logdescp = string.Format("{0}{1} 车位-{2} 车厅-{3} 库号-{4}", strDescp, strTask, strCarLocAddr, deviceStatusQuery.devicecode, deviceStatusQuery.warehouse),
                optcode = CStaticClass.myOperator.optcode, 
                optname = CStaticClass.myOperator.optname 
            };
            proxy.InsertSysLog(log);
        }
        #endregion

        private void CboDeviceCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CboDeviceCode.SelectedItem.ToString() == "所有")
            {
                BtnDelete.Enabled = false;
            }
            else 
            {
                BtnDelete.Enabled = true;
            }
        }

        private void btnAllDelete_Click(object sender, EventArgs e)
        {
            try 
            {
                if (!CStaticClass.CheckPushService()) 
                {
                    return;
                }
                QueryServiceClient proxy = new QueryServiceClient();
                DialogResult dr = MessageBox.Show("你确认删除所有队列作业，删除后将不存在任何的作业！", "谨慎使用此操作", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) 
                {
                    return;
                }
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.DeleteAllWorkQueue();
                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        MessageBox.Show("全部删除所有队列信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.DgvTask.DataSource = new BindingList<CWorkQueueDto>(new List<CWorkQueueDto>());

                        CSystemLogDto systemLogTBL = new CSystemLogDto();
                        systemLogTBL.curtime = CStaticClass.CurruntDateTime();
                        systemLogTBL.logdescp = "全部删除所有队列信息";
                        systemLogTBL.optcode = CStaticClass.myOperator.optcode;
                        systemLogTBL.optname = CStaticClass.myOperator.optname;
                        proxy.InsertSysLog(systemLogTBL);

                        break;
                    default:
                        MessageBox.Show("操作失败！");
                        break;
                }
                proxy.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnMuro_Click(object sender, EventArgs e)
        {
            btnMuro.Enabled = false;
             QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (0 > this.CboTaskType.SelectedIndex)
                {
                    MessageBox.Show("作业类型不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatusQuery = (CarLocationPanelLib.QueryService.CDeviceStatusDto)this.CboTaskType.SelectedItem;
                if (null == deviceStatusQuery)
                {
                    MessageBox.Show("作业类型不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CarLocationPanelLib.PushService.CDeviceStatusDto deviceStatus = CStaticClass.ConvertDeviceStatus(deviceStatusQuery);
                if (deviceStatus.currentnode != (int)EnmFlowNodeDescp.TMURO) 
                {
                    MessageBox.Show("作业状态不是处于故障中，无法执行该操作，请与管理员联系！");
                    return;
                }

                DialogResult dr = MessageBox.Show("是否要继续执行？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                int result = push.MuroDeviceTask(deviceStatus.warehouse, deviceStatus.devicecode);
                #region
                switch (result) 
                {
                    case 100:
                        MessageBox.Show("操作成功！");
                        break;
                    case 101:
                        MessageBox.Show("找不到相应的移动设备！");
                        break;
                    case 102:
                        MessageBox.Show("设备不处于全自动状态！");
                        break;
                    case 103:
                        MessageBox.Show("设备不可用！");
                        break;
                    case 104:
                        MessageBox.Show("设备不接收新指令！");
                        break;
                    case 105:
                        MessageBox.Show("设备作业为空！");
                        break;
                    case 106:
                        MessageBox.Show("设备作业卡号为空！");
                        break;
                    case 109:
                        MessageBox.Show("找不相应的车位号");
                        break;
                    case 110:
                        MessageBox.Show("操作失误！存车时，车辆应该在车厅上，再启用MURO继续，请将车辆放至对应车位上！");
                        break;
                    case 301:
                        MessageBox.Show("与PLC通信失败！");
                        break;
                    default:
                        if (result > 199)
                        {
                            MessageBox.Show("查询搬运器三个状态（1、搬运器上有车。2、A夹臂状态。3、B夹臂状态）失败！ " + result.ToString());
                        }
                        else 
                        {
                            MessageBox.Show(result.ToString());
                        }
                        break;
                }
                #endregion
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
            push.Close();
        }

        private void btnMuroFini_Click(object sender, EventArgs e)
        {
           
        }
    }
}
