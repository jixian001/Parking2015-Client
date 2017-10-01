using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.PushService;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;
using LOGManagementLib;

namespace WindowsFormLib
{
    public partial class CFormHandOrder : Form
    {
        Form m_formCarLocationStatus = new CFormCarLocation();

        public CFormHandOrder()
        {
            InitializeComponent();
            this.CboWareHouse.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CboWareHouse.SelectedIndex = 0;

            // 设置键盘“Esc”按钮
            Button BtnCancel = new Button();
            this.CancelButton = BtnCancel;
            BtnCancel.Click += new EventHandler(BtnCancel_Click);
            m_formCarLocationStatus.ClientSize = new System.Drawing.Size(CStaticClass.ConfigMinWidth(), CStaticClass.ConfigMinHeight());
            m_formCarLocationStatus.FormBorderStyle = FormBorderStyle.FixedDialog;
            m_formCarLocationStatus.StartPosition = FormStartPosition.CenterScreen;

            btnRdICCard.Enabled = false;
            try
            {
                iccdCom = CReadIniFile.ReadSectionData("刷卡器", "ICCardCom");               
            }
            catch (Exception ex)
            {
                CLOGException.Trace(ex.ToString());
            }
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

        #region 公有函数
        /// <summary>
        /// 设置源地址值
        /// </summary>
        /// <param name="strSrcLocAddr"></param>
        public void SetSrcLocAddr(string strSrcLocAddr)
        {
            this.CTxtSrcLocAddr.Text = strSrcLocAddr;
            m_formCarLocationStatus.Close();
        }

        /// <summary>
        /// 设置目的地址值
        /// </summary>
        /// <param name="strDestLocAddr"></param>
        public void SetDestLocAddr(string strDestLocAddr)
        {
            this.CTxtDestLocAddr.Text = strDestLocAddr;
            m_formCarLocationStatus.Close();
        }
        #endregion

        #region 单击车位地址文本框、确认按钮事件
        /// <summary>
        /// 单击源地址文本框根据当前库车位状态选择源地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSrcLocAddr_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouse.Text))
                {
                    MessageBox.Show("库区不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!this.RbtnInJog.Checked && !this.RbtnMove.Checked && !this.RbtnOut.Checked)
                {
                    MessageBox.Show("选择车位时，请先选择具体动作指令!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EnmTxtCarLocationAddr enmCarLocAddr = EnmTxtCarLocationAddr.HandOrderSrc;

                if (this.RbtnInJog.Checked || this.RbtnOut.Checked)
                {
                    enmCarLocAddr = EnmTxtCarLocationAddr.HandInJogSrc;
                }
                // 显示当前车库车位状态对话框
                CStaticClass.showFormCarLocationStatus(this, m_formCarLocationStatus, this.CboWareHouse.Text.Trim(), enmCarLocAddr);
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
        /// 单击目的地址文本框根据当前库车位状态选择目的地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtDestLocAddr_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouse.Text))
                {
                    MessageBox.Show("库区不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!this.RbtnInJog.Checked && !this.RbtnMove.Checked && !this.RbtnOut.Checked)
                {
                    MessageBox.Show("选择车位时，请先选择具体动作指令!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EnmTxtCarLocationAddr enmCarLocAddr = EnmTxtCarLocationAddr.HandOrderDest;

                if (this.RbtnInJog.Checked)
                {
                    enmCarLocAddr = EnmTxtCarLocationAddr.HandInJogDest;
                }
                // 显示当前车库车位状态对话框
                CStaticClass.showFormCarLocationStatus(this, m_formCarLocationStatus, this.CboWareHouse.Text.Trim(), enmCarLocAddr);
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
        /// 确定手动指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CboWareHouse.Text))
                {
                    MessageBox.Show("库区不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouse.Text);

                if (this.RbtnInJog.Checked)
                {
                    DialogResult dr = MessageBox.Show("确认车辆挪移？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                    #region 车辆挪移
                    if (string.IsNullOrEmpty(this.CTxtSrcLocAddr.Text) || string.IsNullOrEmpty(this.CTxtDestLocAddr.Text))
                    {
                        MessageBox.Show("源地址、目的地址都不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    CarLocationPanelLib.PushService.EnmFaultType type = push.VehicleMove(nWareHouse, this.CTxtSrcLocAddr.Text.Trim(), this.CTxtDestLocAddr.Text.Trim());
                    
                    switch (type)
                    {
                        case CarLocationPanelLib.PushService.EnmFaultType.Success:
                            {
                                MessageBox.Show("车辆挪移成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                this.Close();
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotFoundEquip:
                            {
                                MessageBox.Show("没有空闲设备，分配设备失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                            {
                                MessageBox.Show("指定的源或目的车位不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotNormalCarPOSN:
                            {
                                MessageBox.Show("源车位或目的车位不是正常车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.HallEquip:
                            {
                                MessageBox.Show("源车位或目的车位是车厅！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NoCarInGarage:
                            {
                                MessageBox.Show("源车位没有车！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.CarInGarage:
                            {
                                MessageBox.Show("目的车位有车！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.TaskOnICCard:
                            {
                                MessageBox.Show("目的车位的车辆正在作业！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NoICCardInfo:
                            {
                                MessageBox.Show("源车位的IC卡没有制卡！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FixedCarPOSN:
                            {
                                MessageBox.Show("目的车位是其他车主的固定车位，临时卡或定期卡无法挪移到固定车位卡的车位上！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.OverCarInSize:
                            {
                                MessageBox.Show("目的车位尺寸无法满足源车位的车辆！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.InvalidWareHouseID:
                            {
                                MessageBox.Show("无效库区号！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.InvalidEquipID:
                            {
                                MessageBox.Show("无效设备！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotMatch:
                            {
                                MessageBox.Show("目的车位是其他车主的固定车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotAvailable:
                            {
                                MessageBox.Show("设备不可接收指令！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToSendTelegram:
                            {
                                MessageBox.Show("发送报文失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                            {
                                MessageBox.Show("更新数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                MessageBox.Show("车辆挪移失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                    }
                    #endregion
                }
                else if (this.RbtnMove.Checked)
                {
                    DialogResult dr = MessageBox.Show("确认移动设备？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                    #region 移动设备
                    if (string.IsNullOrEmpty(this.CboDeviceID.Text) || string.IsNullOrEmpty(this.CTxtDestLocAddr.Text))
                    {
                        MessageBox.Show("设备、目的地址都不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int nEquipID = CStaticClass.ConvertETVDescp(this.CboDeviceID.Text);
                    CarLocationPanelLib.PushService.EnmFaultType type = push.EquipMove(nWareHouse, nEquipID, this.CTxtDestLocAddr.Text);
                   
                    switch (type)
                    {
                        case CarLocationPanelLib.PushService.EnmFaultType.Success:
                            {
                                MessageBox.Show("移动设备成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                this.Close();
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotFoundEquip:
                            {
                                MessageBox.Show("没有指定设备，需要确认库区号和设备号无误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.IsNotETVEquip:
                            {
                                MessageBox.Show("要移动的设备不是ETV或TV设备！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotAutomatic:
                            {
                                MessageBox.Show("非全自动模式！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                            {
                                MessageBox.Show("无效目的地址，请选择一个有效的车位地址！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotAvailable:
                            {
                                MessageBox.Show("设备不可接收指令！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotAllowed:
                            {
                                MessageBox.Show("设备不可用！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.InvalidWareHouseID:
                            {
                                MessageBox.Show("无效库区号！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.InvalidEquipID:
                            {
                                MessageBox.Show("无效设备号！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToSendTelegram:
                            {
                                MessageBox.Show("发送报文失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                            {
                                MessageBox.Show("更新数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                MessageBox.Show("移动设备失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                    }
                    #endregion
                }
                else if (this.RbtnOut.Checked)
                {
                    DialogResult dr = MessageBox.Show("确认车辆出库？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                    #region 车辆出库
                    if (string.IsNullOrEmpty(this.CTxtSrcLocAddr.Text) || string.IsNullOrEmpty(this.CboHallID.Text))
                    {
                        MessageBox.Show("源地址、出库车厅都不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    CarLocationPanelLib.PushService.EnmFaultType type = push.VehicleExit(nWareHouse, this.CTxtSrcLocAddr.Text, CStaticClass.ConvertHallDescp(nWareHouse, this.CboHallID.Text).ToString());
                   
                    switch (type)
                    {
                        case CarLocationPanelLib.PushService.EnmFaultType.Success:
                            {
                                MessageBox.Show("车辆出库成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                this.Close();
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                            {
                                MessageBox.Show("指定的源车位不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotFoundEquip:
                            {
                                MessageBox.Show("没有找到指定目的车厅！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.IsNotHallEquip:
                            {
                                MessageBox.Show("指定的目的地址不是车厅！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.HallEnter:
                            {
                                MessageBox.Show("车厅是进车厅不允许出车！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotAvailable:
                            {
                                MessageBox.Show("当前卡没有车存在车库！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NoCarInGarage:
                            {
                                MessageBox.Show("车厅设备不可接收指令！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToSendTelegram:
                            {
                                MessageBox.Show("发送报文失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToInsert:
                            {
                                MessageBox.Show("插入数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.TaskOnICCard:
                        case CarLocationPanelLib.PushService.EnmFaultType.Exit:
                            {
                                MessageBox.Show("正在为您出车，请稍后！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.Wait:
                            {
                                MessageBox.Show("已经将您加到取车队列，请排队等候！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.Add:
                            {
                                MessageBox.Show("前面有人正在取车，已经将您加到取车队列，请排队等候！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.InvalidEquipID:
                            {
                                MessageBox.Show("无效设备号！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.InvalidWareHouseID:
                            {
                                MessageBox.Show("无效库区号！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotNormalCarPOSN:
                            {
                                MessageBox.Show("源车位不是正常车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                MessageBox.Show("车辆出库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("请选择具体动作指令！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }
        #endregion

        #region 组合框和具体动作指令改变事件
        /// <summary>
        /// 库区选择框时设备号和车厅号选择值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                this.CboDeviceID.Items.Clear();
                this.CboHallID.Items.Clear();
                int nCurWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouse.Text);
                if (this.CboDeviceID.Visible)
                {
                    List<object> lstETV = CStaticClass.ConfigLstETVOrTVDeviceIDDescp(nCurWareHouse);// 根据库号获取对应所有ETV设备号
                    this.CboDeviceID.Items.AddRange(lstETV.ToArray());
                    this.CboDeviceID.SelectedIndex = 0;
                }
                if (this.CboHallID.Visible)
                {
                    List<object> lstHall = CStaticClass.ConfigLstHallDeviceIDDescp(nCurWareHouse);// 根据库号获取对应所有Hall设备号
                    this.CboHallID.Items.AddRange(lstHall.ToArray());
                    this.CboHallID.SelectedIndex = 0;
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
        /// 挪移指令选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnInJog_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RbtnInJog.Checked)
            {
                this.LblSrc.Text = "源地址";
                this.LblDest.Text = "目的地址";
                this.CboDeviceID.Visible = false;
                this.CboHallID.Visible = false;
                this.CTxtSrcLocAddr.Visible = true;
                this.CTxtDestLocAddr.Visible = true;
                btnRdICCard.Enabled = false;
                // 塔库无挪移
                List<object> lstObj = CStaticClass.ConfigLstWareHouseDescp();
                this.CboWareHouse.Items.Clear();
                foreach (object obj in lstObj)
                {
                    if (obj.Equals("塔库"))
                    {
                        continue;
                    }
                    this.CboWareHouse.Items.Add(obj);
                }
                this.CboWareHouse.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 移动指令选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnMove_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.RbtnMove.Checked)
            {
                return;
            }

            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }
            btnRdICCard.Enabled = false;
            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                this.LblSrc.Text = "设备";
                this.LblDest.Text = "目的地址";
                this.CboDeviceID.Visible = true;
                this.CboHallID.Visible = false;
                this.CTxtSrcLocAddr.Visible = false;
                this.CTxtDestLocAddr.Visible = true;

                // 塔库无挪移
                List<object> lstObj = CStaticClass.ConfigLstWareHouseDescp();
                this.CboWareHouse.Items.Clear();
                foreach (object obj in lstObj)
                {
                    if (obj.Equals("塔库"))
                    {
                        continue;
                    }
                    this.CboWareHouse.Items.Add(obj);
                }
                this.CboWareHouse.SelectedIndex = 0;
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
        /// 出库指令选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnOut_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.RbtnOut.Checked)
            {
                return;
            }
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }
            btnRdICCard.Enabled = true;
            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                this.LblSrc.Text = "源地址";
                this.LblDest.Text = "出库车厅";
                this.CboDeviceID.Visible = false;
                this.CboHallID.Visible = true;
                this.CTxtSrcLocAddr.Visible = true;
                this.CTxtDestLocAddr.Visible = false;

                this.CboWareHouse.Items.Clear();
                this.CboWareHouse.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
                this.CboWareHouse.SelectedIndex = 0;
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

        private string iccdCom = "";
        private void btnRdICCard_Click(object sender, EventArgs e)
        {
            CTxtSrcLocAddr.Clear();

            string physCode = this.readIccard();
            if (physCode == null)
            {
                return;
            }
            if (!CStaticClass.CheckPushService())
            {
                return;
            }
            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                string iccode = proxy.QueryICCodeByPhysic(physCode);              
                if (iccode != null)
                {
                    CarLocationPanelLib.QueryService.CCarLocationDto carLoc = null;
                    CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryCarPOSNByCardID(out carLoc, iccode);
                    if (type == CarLocationPanelLib.QueryService.EnmFaultType.Success) 
                    {
                        if (carLoc != null) 
                        {
                            this.CTxtSrcLocAddr.Text = carLoc.carlocaddr;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            proxy.Close();
        }

        /// <summary>
        /// 读卡操作,返回物理卡号 
        /// </summary>
        /// <returns></returns>
        private string readIccard()
        {
            #region 读卡
            if (string.IsNullOrEmpty(iccdCom))
            {
                return null;
            }
            IicCardRW iccdObj = new CIcCardRW(Convert.ToInt32(iccdCom), 38400, 0, 0);
            bool isConn = false;

            try
            {
                isConn = iccdObj.ConnectCOM();
            }
            catch (Exception ex)
            {
                MessageBox.Show("刷卡器建立异常：" + ex.ToString());
            }

            try
            {
                if (isConn)
                {
                    int nback = 1;
                    Int16 nICType = 0;
                    uint ICNum = 0;
                    byte[] IcData = new byte[16];

                    nback = iccdObj.RequestICCard(ref nICType); //寻卡
                    if (nback == 0)
                    {
                        nback = iccdObj.SelectCard(ref ICNum);  //读取物理卡号
                        if (nback == 0)
                        {
                            #region 读取指定扇区：1，指定DB块：0 的数据
                            //nback = iccdObj.ReadCard(1, 0, ref IcData); 
                            //if (nback == 0)
                            //{
                            //    string data = "";
                            //    for (int i = 0; i < IcData.Length; i++)
                            //    {
                            //        string a = Convert.ToString(IcData[i], 16);
                            //        if (a.Length < 2)
                            //        {
                            //            a = "0" + a;
                            //        }
                            //        data += a;
                            //    }
                            //    iccode = data.Substring(0, 4); //4位数卡号                            
                            //}
                            #endregion
                            return ICNum.ToString();
                        }
                    }
                }
                iccdObj.disConnectCOM();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return null;

            #endregion
        }
    }
}
