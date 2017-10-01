using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using BaseMethodLib;
using System.ServiceModel;
using CarLocationPanelLib.PushService;
using LOGManagementLib;

namespace WindowsFormLib
{
    public partial class FormRotation : Form
    {
        private int m_nWareHouse;
        private DateTime m_dtStart;
        private DateTime m_dtEnd;
        Form m_formCarLocationStatus = new CFormCarLocation();
        public FormRotation()
        {
            InitializeComponent();
            m_nWareHouse = 1;
            this.timer1.Interval = 30000;

            // 设置键盘“Esc”按钮
            Button BtnCancel = new Button();
            this.CancelButton = BtnCancel;
            BtnCancel.Click += new EventHandler(BtnCancel_Click);
            m_formCarLocationStatus.ClientSize = new System.Drawing.Size(CStaticClass.ConfigMinWidth(), CStaticClass.ConfigMinHeight());
            m_formCarLocationStatus.FormBorderStyle = FormBorderStyle.FixedDialog;
            m_formCarLocationStatus.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRotation_Load(object sender, EventArgs e)
        {
            this.DtpEnd.Value = CStaticClass.CurruntDateTime().AddMinutes(30);
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

        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime timeNow = DateTime.Now;
                if (0 <= timeNow.CompareTo(m_dtStart) && 0 >= timeNow.CompareTo(m_dtEnd))
                {// 在空闲时段
                    if (!IsHasTask())
                    {// 无作业
                        VehicleRotation();
                    }
                }
                else
                {
                    this.timer1.Stop();
                    this.BtnStart.Enabled = true;
                    this.BtnStop.Enabled = false;
                    Refresh();
                }
            }
            catch (Exception ex)
            {//打印日志
                CLOGException.Trace("RotationPoc.RotationManagemet.RunRotation 异常", CBaseMethods.MyBase.GetExceptionInfo(ex));
            }
        }

        /// <summary>
        /// 旋转操作
        /// </summary>
        public void RunRotation()
        {
            try
            {
                DateTime timeNow = DateTime.Now;
                if (0 <= timeNow.CompareTo(m_dtStart) && 0 >= timeNow.TimeOfDay.CompareTo(m_dtEnd))
                {// 在空闲时段
                    if (!IsHasTask())
                    {// 无作业
                        VehicleRotation();
                    }
                }
            }
            catch (Exception ex)
            {//打印日志
                CLOGException.Trace("RotationPoc.RotationManagemet.RunRotation 异常", CBaseMethods.MyBase.GetExceptionInfo(ex));
            }
        }

        /// <summary>
        /// 判断该库区是否有作业
        /// </summary>
        /// <returns>true:有作业  false：无作业</returns>
        public bool IsHasTask()
        {
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            bool bReturn =  push.IsHasTask(m_nWareHouse);
            push.Close();
            return bReturn;
        }

        /// <summary>
        /// 车辆旋转（掉头）
        /// </summary>
        /// <returns></returns>
        public void VehicleRotation()
        {
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            push.VehicleRotation(m_nWareHouse);
            push.Close();
        }

        /// <summary>
        /// 确认车辆掉头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            m_dtStart = this.DtpStart.Value;
            m_dtEnd = this.DtpEnd.Value;
            this.timer1.Start();
            timer1_Tick(sender, e);
            this.BtnStart.Enabled = false;
            this.BtnStop.Enabled = true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStop_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this.BtnStart.Enabled = true;
            this.BtnStop.Enabled = false;
        }

        /// <summary>
        /// 确认车辆掉头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRotation_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (CBaseMethods.MyBase.IsEmpty(m_nWareHouse) || string.IsNullOrEmpty(this.CTxtSrcLocAddr.Text))
                {
                    MessageBox.Show("库区、源地址都不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dr = MessageBox.Show("确认车辆掉头？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                if (push.IsHasTask(m_nWareHouse))
                {
                    MessageBox.Show("当前库区有作业，不允许掉头!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CarLocationPanelLib.PushService.EnmFaultType type = push.VehicleRotationByAddr(m_nWareHouse, this.CTxtSrcLocAddr.Text.Trim());

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            MessageBox.Show("车辆掉头成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                            MessageBox.Show("车辆掉头失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 单击源地址文本框根据当前库车位状态选择源地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSrcLocAddr_Click(object sender, EventArgs e)
        {
            try
            {
                EnmTxtCarLocationAddr enmCarLocAddr = EnmTxtCarLocationAddr.HandOrderSrc;
                // 显示当前车库车位状态对话框
                CStaticClass.showFormCarLocationStatus(this, m_formCarLocationStatus, CStaticClass.ConvertWareHouse(m_nWareHouse), enmCarLocAddr);
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
        /// 设置源地址值
        /// </summary>
        /// <param name="strSrcLocAddr"></param>
        public void SetSrcLocAddr(string strSrcLocAddr)
        {
            this.CTxtSrcLocAddr.Text = strSrcLocAddr;
            m_formCarLocationStatus.Close();
        }
    }
}
