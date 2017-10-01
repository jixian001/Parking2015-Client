using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.PushService;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;
using BaseMethodLib;
using System.Reflection;
using LOGManagementLib;
using ICCardManagementLib;

namespace WindowsFormLib
{
    public partial class CFormBilling : Form, IDisposable
    {
        private CFormLogin m_formLogin = null;
        private CFormCustomer m_formCustomer = null;
        private CFormTariff m_formTariff = null;
        private string m_strTitle = CStaticClass.ConfigBillTitle();
        private CFormReport m_formReport = null;
        List<CICCardLogDto> m_lstICCardLog = new List<CICCardLogDto>();
        //CICCardReaderObj cardReader;

        private string iccdCom = "";
        private int region;

        public CFormBilling()
        {
            m_formLogin = new CFormLogin(m_strTitle);
            CStaticClass.SetBillingFlag(true);
            InitializeComponent();
            InitializeCarLocationPanels();

            m_formCustomer = new CFormCustomer();
            m_formTariff = new CFormTariff();
            m_formReport = new CFormReport();

            this.FormClosing += new FormClosingEventHandler(CFormBillManagement_FormClosing);
            this.Tag = false;
            this.CucbipCustomer.CUserCustomerInfoPanel_Load(null, null);
            this.DgvICCard.DataSourceChanged += new EventHandler(this.CupttsICCard.UpdateLayout);
            this.DgvICCard.AutoGenerateColumns = false;
            this.DtpICCardStart.Value = CStaticClass.CurruntDateTime().AddMinutes(-30);
            this.CboICCardCondition.Text = "所有";

            this.timer1.Interval = 1000;// 毫秒
            CStaticClass.myCallback.PushCallbackEvent += new Action<object>(callback_CallbackEvent);
            CStaticClass.myPushProxy.Register(Environment.MachineName);

            this.CboHall.Items.AddRange(CStaticClass.ConfigLstHallDeviceIDDescp(1).ToArray());
            this.CboHall.SelectedIndex = 0;

            //cardReader = GetReaderObj(0);
            //if (null != cardReader)
            //{
            //    cardReader.ConnectCOM();
            //}
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public new void Dispose()
        {
            //if (cardReader.isComConnected)
            //{// 释放IC卡对象
            //    cardReader.DisConnectCOM();
            //}
            //CLOGException.Trace("——————IEGBillingSystemProcess.Dispose");
        }

        /// <summary>
        /// 界面登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CFormBilling_Load(object sender, EventArgs e)
        {
            DialogResult dr = m_formLogin.ShowDialog();
            if (dr == DialogResult.Yes)
            {
                this.WindowState = FormWindowState.Maximized;
                this.Tag = true;
                this.CucbipCustomer.CUserCustomerInfoPanel_Load(sender, e);
                SetStatusStripValue(null);// 更新车位状态栏
                SetLogin();
                this.timer1.Start();

                try
                {
                    iccdCom = CReadIniFile.ReadSectionData("刷卡器", "ICCardCom");

                    string clientID = CReadIniFile.ReadSectionData("编号", "ClientID");
                    if (clientID == "1") 
                    {
                        region = 1;
                    }
                    else if (clientID == "2")
                    {
                        region = 2;
                    }
                    else 
                    {
                        region = 0;
                    }
                }
                catch (Exception ex)
                {
                    CLOGException.Trace(ex.ToString());
                }
            }
            else if (dr == DialogResult.Cancel)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 窗体首次显示时触发(窗体大小改变触发 OnShown-OnResize)
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
        private void CFormBillManagement_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
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
        }

        /// <summary>
        /// Tab页码切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TctlBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != this.TctlBill.SelectedTab && this.TctlBill.SelectedTab.Name == "TpTariff")
            {
                this.CutpTariff.CUserTariffPanel_Load(sender, e);
            }
        }

        #region 车主信息
        /// <summary>
        /// 车主信息添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCustomerModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == sender)
                {
                    CFormCustomer formCustomer = new CFormCustomer();
                    formCustomer.Tag = this.CucbipCustomer;
                    formCustomer.ShowDialog(this);
                }
                else if (typeof(struCustomerInfo) == sender.GetType())
                {
                    struCustomerInfo Customer = (struCustomerInfo)sender;
                    if (null == m_formCustomer)
                    {
                        m_formCustomer = new CFormCustomer();
                    }

                    m_formCustomer.Tag = this.CucbipCustomer;
                    m_formCustomer.FillCustomerInfo(Customer);
                    m_formCustomer.ShowDialog(this);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCustomerClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region IC卡缴费
        /// <summary>
        /// IC卡读卡及查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadAndFind_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                CarLocationPanelLib.QueryService.struBillInfo billInfo = new CarLocationPanelLib.QueryService.struBillInfo() 
                { 
                    nICCardType = -1, 
                    dtStartTime = CStaticClass.DefDatetime, 
                    dtEndTime = CStaticClass.DefDatetime 
                };

                CarLocationPanelLib.QueryService.EnmFaultType type = CarLocationPanelLib.QueryService.EnmFaultType.Fail;
                string strICCardID = string.Empty;
                bool bReturn = ReadICCardByComID(out strICCardID);
                if (bReturn)
                {
                    CTxtICCardID.Text = strICCardID;
                    billInfo.strICCardID = strICCardID;
                    type = proxy.QueryParkingInfo(ref billInfo,region);
                }
                else
                {
                    type = CarLocationPanelLib.QueryService.EnmFaultType.FailToReadICCard;

                }
                this.BtnPay.Enabled = false;

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            FillICCardControls(billInfo);
                            //add by suhan 20150728
                            this.CboHall.Enabled = true;
                            this.BtnPay.Enabled = true;
                            //end
                            MessageBox.Show("读卡及查询成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.FailToReadICCard:
                        {
                            MessageBox.Show("读取IC卡失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Null:
                        {
                            MessageBox.Show("获取的用户卡号是空值", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.FailConnection:
                        {
                            MessageBox.Show("连接读卡器失败，获取的是空值", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("没有制卡", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoTariffInfo:
                        {
                            MessageBox.Show("未绑定计费标准", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoCarInGarage:
                        {
                            MessageBox.Show("当前临时卡没有车存在车库", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoBoundCustomer:
                        {
                            MessageBox.Show("当前固定卡没有绑定车主计费信息", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("计算停车费用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("读卡及查询失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                }

                ClearICCardControls();
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
        /// IC卡确认缴费出车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPay_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CTxtICCardID.Text))
                {
                    MessageBox.Show("用户卡号不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                float fPayableFee = CStaticClass.ConvertTariffFee(this.TxtPayableFee.Text);
                float fActualFee;
                float.TryParse(this.TxtActualFee.Text.Trim(), out fActualFee);

                //modify by suhan 20150728
                //if (fActualFee < fPayableFee)
                //{
                //    MessageBox.Show("缴纳费用不足，请补交余额！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                //DialogResult dr = MessageBox.Show("确认缴费出车？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                //if (dr == DialogResult.Cancel)
                //{
                //    return;
                //}
                //modify end by suhan 20150728
                CarLocationPanelLib.PushService.struBillInfo billInfo = new CarLocationPanelLib.PushService.struBillInfo();
                billInfo.strICCardID = CStaticClass.ConvertICCardID(this.CTxtICCardID.Text.Trim());
                billInfo.nICCardType = (int)CStaticClass.ConvertICCardType(this.TxtICCardType.Text.Trim());
                billInfo.dtStartTime = this.DtpStartTime.Value;
                billInfo.dtEndTime = this.DtpEndTime.Value;
                billInfo.strCalculateDays = this.TxtCalculateDays.Text.Trim();
                billInfo.fPayableFee = fPayableFee;
                billInfo.fActualFee = fActualFee;
                billInfo.nFeeType = CStaticClass.ConvertFeeType(this.CboFeeType.Text.Trim());
                int nWareHouse = CStaticClass.ConvertWareHouse(this.TxtWareHouse.Text);
                int nHallID = CStaticClass.ConvertHallDescp(nWareHouse, this.CboHall.Text);
                billInfo.nWareHouse = nWareHouse;
                billInfo.nHallID = nHallID;
                billInfo.strOptCode = CStaticClass.myOperator.optcode;

                //modify by suhan 20150730
                if (null != this.CTxtTariff.Tag)
                {
                    billInfo.nTariffID = ((CTariffDto)this.CTxtTariff.Tag).id;
                }

                // 需要判断卡类型和计费类型是否对应，如不对应返回异常，客户端提示。如定期卡的计费类型配置的是临时卡的计费类型
                if (((int)EnmICCardType.Temp == billInfo.nICCardType && (int)EnmFeeType.Hour != billInfo.nFeeType) ||
                    ((int)EnmICCardType.Temp != billInfo.nICCardType && (int)EnmFeeType.Hour == billInfo.nFeeType))
                {
                    MessageBox.Show("请选择与卡类型对应的计费类型！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //modify end by suhan 20150730

                //modify by suhan 20150728
                if ((int)EnmICCardType.Temp == billInfo.nICCardType)
                {
                    //modify by suhan 20150730
                    if (fActualFee < fPayableFee)
                    {
                        MessageBox.Show("缴纳费用不足，请补交余额！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult dr = MessageBox.Show("确认缴费出车？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                    //modify end by suhan 20150730

                    CarLocationPanelLib.PushService.EnmFaultType type = push.PayFeesAndTakeCar(billInfo);

                    switch (type)
                    {
                        case CarLocationPanelLib.PushService.EnmFaultType.Success:
                            {
                                ClearICCardControls();
                                MessageBox.Show("IC卡确认缴费成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.Null:
                            {
                                MessageBox.Show("传入的参数卡号为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NoICCardInfo:
                            {
                                MessageBox.Show("没有制卡", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.LossORCancel:
                            {
                                MessageBox.Show("IC卡注销或挂失", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotMatch:
                            {
                                MessageBox.Show("IC卡类型不正确", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                            {
                                MessageBox.Show("指定的源或目的车位不存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotFoundEquip:
                            {
                                MessageBox.Show("没有找到指定目的车厅", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.IsNotHallEquip:
                            {
                                MessageBox.Show("指定的目的地址不是车厅", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.HallEnter:
                            {
                                MessageBox.Show("车厅是进车厅不允许出车", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NoCarInGarage:
                            {
                                MessageBox.Show("源车位没有车", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotAvailable:
                            {
                                MessageBox.Show("车厅设备不可接收指令", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToSendTelegram:
                            {
                                MessageBox.Show("发送报文失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotSameWareHouse:
                            {
                                MessageBox.Show("刷卡库区与车所在库区不同", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToInsert:
                            {
                                MessageBox.Show("插入数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.Exit:
                            {
                                MessageBox.Show("正在为您出车，请稍后", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.Wait:
                            {
                                MessageBox.Show("已经将您加到取车队列，请排队等候", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.Add:
                            {
                                MessageBox.Show("前面有人正在取车，已经将您加到取车队列，请排队等候", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.InvalidEquipID:
                            {
                                MessageBox.Show("无效设备号", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.InvalidWareHouseID:
                            {
                                MessageBox.Show("无效库区号", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.NotNormalCarPOSN:
                            {
                                MessageBox.Show("源车位不是正常车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                            {
                                MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.InvalidFeeType:
                            {
                                MessageBox.Show("无效计费类型", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.Fail:
                            {
                                MessageBox.Show("计算截止日期失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.PushService.EnmFaultType.FailToFixBill:
                            {
                                MessageBox.Show("当前卡已存车且IC卡已过期，请补交费用", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                MessageBox.Show("IC卡确认缴费出车失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                    }
                }
                else
                {
                    //当前日期小于缴费时间或大于截止时间时不允许取车
                    DateTime dtTimeNow = DateTime.Now;
                    if (dtTimeNow < billInfo.dtStartTime || dtTimeNow > billInfo.dtEndTime)
                    {
                        MessageBox.Show("卡片已过期，请缴费出车！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DialogResult dr2 = MessageBox.Show("确认出车？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                    if (dr2 == DialogResult.Cancel)
                    {
                        return;
                    }

                    //发送取车报文或加到取车队列
                    CarLocationPanelLib.PushService.EnmFaultType type = push.VehicleExitForBilling(billInfo.strICCardID, nHallID);

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
                                MessageBox.Show("车没有存在库内！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                }
                //modify end by suhan 20150728
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
        /// 收费标准文本框双击弹出收费标准对话框并赋值(固定卡情况)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTariff_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (null == this.CTxtTariff.Tag || typeof(CTariffDto) != this.CTxtTariff.Tag.GetType())
                {
                    m_formTariff.SetCboCardType(CStaticClass.ConvertICCardType(this.TxtICCardType.Text), 0);
                }
                else
                {
                    CTariffDto tariff = (CTariffDto)this.CTxtTariff.Tag;
                    m_formTariff.SetCboCardType(CStaticClass.ConvertICCardType(this.TxtICCardType.Text), tariff.id);
                }
                m_formTariff.ShowDialog(this);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///IC卡实收金额值变化触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtActualFee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float fPayableFee = CStaticClass.ConvertTariffFee(this.TxtPayableFee.Text);
                float fActualFee;
                float.TryParse(this.TxtActualFee.Text.Trim(), out fActualFee);
                this.TxtChange.Text = (fActualFee - fPayableFee).ToString();
                //modify by suhan
				if (fPayableFee > fActualFee)
                {
                    this.BtnPay.Enabled = false;
                }
                else
                {
                    this.BtnPay.Enabled = true;
                }
				//end by suhan
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// IC查询
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
                if (string.IsNullOrEmpty(this.CTxtICCardID.Text))
                {
                    MessageBox.Show("用户卡号不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CarLocationPanelLib.QueryService.struBillInfo billInfo = new CarLocationPanelLib.QueryService.struBillInfo();
                billInfo.nICCardType = -1;
                billInfo.dtStartTime = CStaticClass.DefDatetime;
                billInfo.dtEndTime = CStaticClass.DefDatetime;
                billInfo.strICCardID = CStaticClass.ConvertICCardID(this.CTxtICCardID.Text.Trim());
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryParkingInfo(ref billInfo,region);
                this.BtnPay.Enabled = false;

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            FillICCardControls(billInfo);
                            //add by suhan 201507728
                            if (0 == billInfo.nHallID)
                            {
                                MessageBox.Show("没有可用的车厅", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.CboHall.Enabled = false;
                                return;
                            }
                            this.CboHall.Enabled = true;
                            this.BtnPay.Enabled = true;
                            //end by suhan
                            MessageBox.Show("查询成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("没有制卡", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NotMatch:
                        {
                            MessageBox.Show("IC卡类型不匹配 ", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoTariffInfo:
                        {
                            MessageBox.Show("未绑定计费标准", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoCarInGarage:
                        {
                            ///add by suhan 20150729
                            if ((int)EnmICCardType.Temp != billInfo.nICCardType)
                            {
                                FillICCardControls(billInfo);
                            }
                            //end by suhhan
                            MessageBox.Show("没有车存在车库", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoBoundCustomer:
                        {
                            MessageBox.Show("当前固定卡没有绑定车主计费信息", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("计算停车费用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("查询失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                }

                ClearICCardControls();
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
        /// 手动调整整体大小
        /// </summary>
        private void HandResize()
        {
            int nWidth = this.ClientSize.Width > CStaticClass.ConfigMinWidth() ? this.ClientSize.Width : CStaticClass.ConfigMinWidth();
            int nHeight = this.ClientSize.Height > CStaticClass.ConfigMinHeight() ? this.ClientSize.Height : CStaticClass.ConfigMinHeight();
            int gap = CStaticClass.ConfigMainGap();
            int minGap = CStaticClass.ConfigMinGap();
            int nGap = CStaticClass.ConfigControlSize();
            this.TctlBill.Size = new System.Drawing.Size(nWidth - gap - 60, nHeight - this.statusStrip.Height);
            Size sTabSize = new Size(this.TctlBill.Width - gap, nHeight - this.statusStrip.Height - 4 * minGap);

            #region IC卡缴费
            this.GbxICCard.Size = sTabSize;
            int nWithLen = this.LblICCardID.Width + this.CTxtICCardID.Width + this.LblICCardType.Width + this.TxtICCardType.Width + nGap;
            int nLeft = Math.Max((this.GbxICCard.Width - nWithLen) / 2, minGap);
            int nHeightLen = this.LblICCardID.Height + this.LblStartTime.Height + this.LblCalculateDays.Height + this.LblTariff.Height + this.BtnPay.Height + 6 * nGap;
            int nTop = Math.Max((this.GbxICCard.Height - nHeightLen) / 2, minGap);
            this.LblICCardID.Location = new Point(nLeft, nTop);
            this.CTxtICCardID.Location = new Point(LblICCardID.Location.X + LblICCardID.Width, nTop);
            this.LblICCardType.Location = new Point(nGap + CTxtICCardID.Location.X + CTxtICCardID.Width, nTop);
            this.TxtICCardType.Location = new Point(LblICCardType.Location.X + LblICCardType.Width, nTop);
            nTop += LblICCardID.Height + nGap;
            this.LblStartTime.Location = new Point(nLeft, nTop);
            this.DtpStartTime.Location = new Point(CTxtICCardID.Location.X, nTop);
            this.LblEndTime.Location = new Point(LblICCardType.Location.X, nTop);
            this.DtpEndTime.Location = new Point(TxtICCardType.Location.X, nTop);
            nTop += LblStartTime.Height + nGap;
            this.LblCalculateDays.Location = new Point(nLeft, nTop);
            this.TxtCalculateDays.Location = new Point(CTxtICCardID.Location.X, nTop);
            this.LblWareHouse.Location = new Point(LblICCardType.Location.X, nTop);
            //this.CboFeeType.Location = new Point(TxtICCardType.Location.X, nTop);
            this.TxtWareHouse.Location = new Point(TxtICCardType.Location.X, nTop);
            this.LblHall.Location = new Point(TxtWareHouse.Location.X + TxtWareHouse.Width, nTop);
            this.CboHall.Location = new Point(LblHall.Location.X + LblHall.Width, nTop);
            //this.CTxtTariff.Location = new Point(LblHall.Location.X + LblHall.Width, nTop);
            nTop += LblCalculateDays.Height + nGap;
            this.LblTariff.Location = new Point(nLeft, nTop);
            this.TxtPayableFee.Location = new Point(CTxtICCardID.Location.X, nTop);
            this.LblActualFee.Location = new Point(TxtPayableFee.Location.X + TxtPayableFee.Width, nTop);
            this.TxtActualFee.Location = new Point(LblActualFee.Location.X + LblActualFee.Width, nTop);
            this.LblChange.Location = new Point(TxtActualFee.Location.X + TxtActualFee.Width, nTop);
            this.TxtChange.Location = new Point(LblChange.Location.X + LblChange.Width, nTop);
            nTop += LblTariff.Height + nGap;
            this.LblFeeType.Location = new Point(nLeft, nTop);
            this.CboFeeType.Location = new Point(CTxtICCardID.Location.X, nTop);
            this.LblFeeStand.Location = new Point(CboFeeType.Location.X + CboFeeType.Width, nTop);
            this.CTxtTariff.Location = new Point(LblFeeStand.Location.X + LblFeeStand.Width, nTop);
            nTop += LblFeeType.Height + 2 * gap;
            this.LblDescpCalu.Location = new Point(2, nTop);
            this.LblDescpCalu.Width = nWidth - gap;
            nLeft = (this.GbxICCard.Width - this.BtnReadAndFind.Width - this.BtnFind.Width - this.BtnPay.Width - 4 * nGap) / 2;
            nTop += LblDescpCalu.Height + 3 * gap;
            this.BtnReadAndFind.Location = new Point(nLeft, nTop);
            this.BtnFind.Location = new Point(BtnReadAndFind.Location.X + BtnReadAndFind.Width + 2 * nGap, nTop);
            this.BtnPay.Location = new Point(BtnFind.Location.X + BtnFind.Width + 2 * nGap, nTop);
            #endregion

            #region 车主信息
            this.TpCustomer.Size = sTabSize;
            this.CucbipCustomer.Size = this.TpCustomer.Size;
            this.CucbipCustomer.OnResize();
            #endregion

            #region 计费标准
            this.TpTariff.Size = sTabSize;
            this.CutpTariff.Size = this.TpTariff.Size;
            this.CutpTariff.OnResize();
            #endregion

            #region IC卡缴费日志
            this.TpICCardLog.Size = sTabSize;
            this.GbxFindCondition.Size = new System.Drawing.Size(sTabSize.Width, this.GbxFindCondition.Height);
            this.GbxLogList.Size = new System.Drawing.Size(sTabSize.Width, this.TpICCardLog.Height - this.GbxLogList.Location.Y);
            this.DgvICCard.Size = new System.Drawing.Size(sTabSize.Width - minGap, this.GbxLogList.Height - this.DgvICCard.Location.Y - this.CupttsICCard.Height - minGap);
            this.CupttsICCard.Location = new Point(4, this.DgvICCard.Location.Y + this.DgvICCard.Height + minGap);// 翻页
            #endregion

            #region 任务状态栏 statusStrip
            this.statusStrip.Location = new System.Drawing.Point(0, nHeight - this.statusStrip.Height);
            #endregion

            #region 修改密码图片按钮 add by suhan 20150720
            this.PicEditModifyPassword.Location = new System.Drawing.Point(nWidth - 2 * nGap, 26);
            this.PicEditLogout.Location = new System.Drawing.Point(nWidth - 2 * nGap, 96);
            this.LBModifyPassword.Location = new System.Drawing.Point(nWidth - nGap - 37, 66);
            this.LBLogout.Location = new System.Drawing.Point(nWidth - nGap - 37, 136);
            #endregion 修改密码图片按钮
        }

        /// <summary>
        /// 填充IC卡缴费界面控件内容
        /// </summary>
        /// <param name="billInfo"></param>
        private void FillICCardControls(CarLocationPanelLib.QueryService.struBillInfo billInfo)
        {
            //modify by suhan 201507729
            if (billInfo.nICCardType == (int)EnmICCardType.Temp)
            {
                this.LblStartTime.Text = "入库时间";
                this.LblEndTime.Text = "出库时间";
                this.LblCalculateDays.Text = "停车时间";
                TimeSpan ts = billInfo.dtEndTime - billInfo.dtStartTime;
                this.TxtCalculateDays.Text = (ts.Days > 0 ? ts.Days + "天" : "") + (ts.Hours > 0 ? ts.Hours + "小时" : "") + (ts.Minutes > 0 ? ts.Minutes + "分钟" : "");
                //this.LblWareHouse.Text = "库区";
                this.CboFeeType.Visible = false;
                //this.TxtWareHouse.Visible = true;
                this.TxtWareHouse.Text = CStaticClass.ConvertWareHouse(billInfo.nWareHouse);
                //this.LblHall.Text = "出库车厅";
                this.CTxtTariff.Visible = false;
                //this.CboHall.Visible = true;
                this.CboHall.Text = CStaticClass.ConvertHallDescp(billInfo.nWareHouse, billInfo.nHallID);
                this.BtnPay.Text = "确认缴费出车";
                this.LblFeeType.Visible = false;
                this.LblFeeStand.Visible = false;
                //add by suhan 201507730
                this.TxtPayableFee.Text = CStaticClass.ConvertPayableFee(billInfo.fPayableFee, billInfo.nFeeType);
                this.TxtActualFee.Text = billInfo.fPayableFee.ToString();
                //add over 201507730

            }
            else
            {
                this.LblStartTime.Text = "缴费日期";
                this.LblEndTime.Text = "截止日期";
                this.LblCalculateDays.Text = "剩余时间";
                TimeSpan ts = billInfo.dtEndTime - CStaticClass.CurruntDateTime();
                this.TxtCalculateDays.Text = ts.Days + "天";
                //this.LblWareHouse.Text = "收费类型";
                this.CboFeeType.Visible = true;
                //this.TxtWareHouse.Visible = false;
                this.TxtWareHouse.Text = CStaticClass.ConvertWareHouse(billInfo.nWareHouse);
                //this.LblHall.Text = "收费标准";
                this.CTxtTariff.Visible = true;
                //this.CboHall.Visible = true;
                this.CTxtTariff.Text = CStaticClass.ConvertTariffFee(billInfo.fTariffNorm, billInfo.nFeeType);
                //this.BtnPay.Text = "确认缴费";
                this.CboHall.Text = CStaticClass.ConvertHallDescp(billInfo.nWareHouse, billInfo.nHallID);
                this.BtnPay.Text = "确认出车";
                this.LblFeeType.Visible = true;
                this.LblFeeStand.Visible = true;
            }
            //end

            this.CTxtICCardID.Text = billInfo.strICCardID;
            this.TxtICCardType.Text = CStaticClass.ConvertICCardType(billInfo.nICCardType);
            this.DtpStartTime.Value = billInfo.dtStartTime;
            this.DtpEndTime.Value = billInfo.dtEndTime;
            this.CboFeeType.Text = CStaticClass.ConvertFeeType(billInfo.nFeeType);

            this.TxtPayableFee.Text = CStaticClass.ConvertPayableFee(billInfo.fPayableFee, billInfo.nFeeType);
            this.TxtActualFee.Text = billInfo.fPayableFee.ToString();
            //modify by suhan 201507728
            //if (0.000001 > billInfo.fPayableFee)
            //{
            //    this.BtnPay.Enabled = false;
            //}
            //else
            //{
            //    this.BtnPay.Enabled = true;
            //}
            //end
            //this.BtnPay.Enabled = true;
            this.LblDescpCalu.Text = string.Empty;
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                this.CTxtTariff.Tag = null;
                return;
            }

            // 赋值计费标准实例
            QueryServiceClient proxy = new QueryServiceClient();
            CTariffDto tariff = proxy.GetTariffByID(billInfo.nTariffID);
            this.CTxtTariff.Tag = tariff;
            if (null == tariff || (int)EnmICCardType.Temp != tariff.iccardtype)
            {
                return;
            }

            #region 显示临时卡计算费用方式
            if (!CBaseMethods.MyBase.IsEmpty(tariff.isworkday))
            {
                // 无限额时，按照一天费用计算
                float fDayFee = (float)tariff.workdayquotafee < 0.1 ? (float)tariff.fee : (float)tariff.workdayquotafee;
                this.LblDescpCalu.Text += string.Format("工作日(周一到周五)限额{0}元：", fDayFee);
                if (!string.IsNullOrEmpty(tariff.workpeakperiod))
                {
                    // 工作日
                    if (!string.IsNullOrEmpty(tariff.workpeakfirstunit))
                    {
                        this.LblDescpCalu.Text += string.Format("高峰期({0})首{1}内{2}、首{1}外{3}", tariff.workpeakperiod, tariff.workpeakfirstunit, tariff.workpeakinunitfee, tariff.workpeakoutunitfee);
                    }
                    else
                    {
                        this.LblDescpCalu.Text += string.Format("高峰期({0}){1}", tariff.workpeakperiod, tariff.workpeakoutunitfee);
                    }
                    if (!string.IsNullOrEmpty(tariff.worknonpeakfirstunit))
                    {
                        this.LblDescpCalu.Text += string.Format("，非高峰期首{0}内{1}、首{0}外{2}", tariff.worknonpeakfirstunit, tariff.worknonpeakinunitfee, tariff.worknonpeakoutunitfee);
                    }
                    else
                    {
                        this.LblDescpCalu.Text += string.Format("，非高峰期{0}", tariff.worknonpeakoutunitfee);
                    }
                }
                else
                {
                    this.LblDescpCalu.Text += tariff.workpeakoutunitfee;
                }

                if (!string.IsNullOrEmpty(tariff.nonworkpeakperiod))
                {
                    // 无限额时，按照一天费用计算
                    TimeSpan tsNonworkPeakStart, tsNonworkPeakEnd;
                    // 非工作日高峰首单位外单价、非工作日非高峰首单位外单价
                    float fNonworkPeakOutFee, fNonworkNonpeakOutFee;
                    // 非工作日高峰首单位外单位、非工作日非高峰首单位外单位（以分钟为单元）
                    int nNonworkPeakOutUnit, nNonworkNonpeakOutUnit;
                    CBaseMethods.MyBase.ConvertTimePeriod(tariff.nonworkpeakperiod, out tsNonworkPeakStart, out tsNonworkPeakEnd);
                    float dPeakMinutes = (float)(tsNonworkPeakEnd.Subtract(tsNonworkPeakStart)).TotalMinutes;
                    fNonworkPeakOutFee = CBaseMethods.MyBase.ConvertUnitToInt(tariff.nonworkpeakoutunitfee, out nNonworkPeakOutUnit);
                    fNonworkNonpeakOutFee = CBaseMethods.MyBase.ConvertUnitToInt(tariff.nonworknonpeakoutunitfee, out nNonworkNonpeakOutUnit);
                    fDayFee = dPeakMinutes / nNonworkPeakOutUnit * fNonworkPeakOutFee + (1440 - dPeakMinutes) / nNonworkNonpeakOutUnit * fNonworkNonpeakOutFee;
                    fDayFee = (float)tariff.nonworkdayquotafee < 0.1 ? fDayFee : (float)tariff.nonworkdayquotafee;
                    this.LblDescpCalu.Text += string.Format("\n        非工作日限额{0}元：", fDayFee);

                    // 非工作日
                    if (!string.IsNullOrEmpty(tariff.nonworkpeakfirstunit))
                    {
                        this.LblDescpCalu.Text += string.Format("高峰期({0})首{1}内{2}、首{1}外{3}", tariff.nonworkpeakperiod, tariff.nonworkpeakfirstunit, tariff.nonworkpeakinunitfee, tariff.nonworkpeakoutunitfee);
                    }
                    else
                    {
                        this.LblDescpCalu.Text += string.Format("高峰期({0}){1}", tariff.nonworkpeakperiod, tariff.nonworkpeakoutunitfee);
                    }
                    if (!string.IsNullOrEmpty(tariff.nonworknonpeakfirstunit))
                    {
                        this.LblDescpCalu.Text += string.Format("，非高峰期首{0}内{1}、首{0}外{2}", tariff.nonworknonpeakfirstunit, tariff.nonworknonpeakinunitfee, tariff.nonworknonpeakoutunitfee);
                    }
                    else
                    {
                        this.LblDescpCalu.Text += string.Format("，非高峰期{0}", tariff.nonworknonpeakoutunitfee);
                    }
                }
                else
                {
                    // 非工作日高峰首单位外单价
                    float fNonworkPeakOutFee;
                    // 非工作日高峰首单位外单位（以分钟为单元）
                    int nNonworkPeakOutUnit;
                    // 无限额时，按照一天费用计算
                    fNonworkPeakOutFee = CBaseMethods.MyBase.ConvertUnitToInt(tariff.nonworkpeakoutunitfee, out nNonworkPeakOutUnit);
                    fDayFee = 1440 / nNonworkPeakOutUnit * fNonworkPeakOutFee;
                    fDayFee = (float)tariff.nonworkdayquotafee < 0.1 ? fDayFee : (float)tariff.nonworkdayquotafee;
                    this.LblDescpCalu.Text += string.Format("\n        非工作日限额{0}元：", fDayFee);
                    this.LblDescpCalu.Text += tariff.nonworkpeakoutunitfee;
                }
            }
            else
            {
                // 无限额时，按照一天费用计算
                float fDayFee = (float)tariff.workdayquotafee < 0.1 ? (float)tariff.fee : (float)tariff.workdayquotafee;
                this.LblDescpCalu.Text += string.Format("限额{0}元：", fDayFee);
                // 工作日
                if (!string.IsNullOrEmpty(tariff.workpeakperiod))
                {
                    if (!string.IsNullOrEmpty(tariff.workpeakfirstunit))
                    {
                        this.LblDescpCalu.Text += string.Format("高峰期({0})首{1}内{2}、首{1}外{3}", tariff.workpeakperiod, tariff.workpeakfirstunit, tariff.workpeakinunitfee, tariff.workpeakoutunitfee);
                    }
                    else
                    {
                        this.LblDescpCalu.Text += string.Format("高峰期({0}){1}", tariff.workpeakperiod, tariff.workpeakoutunitfee);
                    }
                    if (!string.IsNullOrEmpty(tariff.worknonpeakfirstunit))
                    {
                        this.LblDescpCalu.Text += string.Format("，非高峰期首{0}内{1}、首{0}外{2}", tariff.worknonpeakfirstunit, tariff.worknonpeakinunitfee, tariff.worknonpeakoutunitfee);
                    }
                    else
                    {
                        this.LblDescpCalu.Text += string.Format("，非高峰期{0}", tariff.worknonpeakoutunitfee);
                    }
                }
                else
                {
                    this.LblDescpCalu.Text += tariff.workpeakoutunitfee;
                }
            }
            #endregion
        }

        /// <summary>
        /// 清空IC卡缴费界面控件内容
        /// </summary>
        private void ClearICCardControls()
        {
            this.CTxtICCardID.Text = "";
            this.DtpStartTime.Value = CStaticClass.DefDatetime;
            this.TxtCalculateDays.Text = "";
            this.DtpEndTime.Value = CStaticClass.DefDatetime;
            this.TxtICCardType.Text = "";
            this.CboFeeType.Text = null;// "";
            this.TxtWareHouse.Text = "";
            this.CboHall.Text = "";
            this.CTxtTariff.Text = "";
            this.TxtPayableFee.Text = "";
            this.TxtActualFee.Text = "";
        }

        /// <summary>
        /// 登录
        /// </summary>
        private void SetLogin()
        {
            int nPermission = 0;
            this.TsslOptTxt.Text = "";

            if (null != CStaticClass.myOperator)
            {
                nPermission = (int)CStaticClass.myOperator.optpermission;
                this.TsslOptTxt.Text = CStaticClass.myOperator.optcode;
            }

            bool bFlag = this.TctlBill.Controls.Contains(this.TpICCard);
            if (1024 == (nPermission & 1024))
            {
                if (!bFlag)
                {
                    this.TctlBill.Controls.Add(this.TpICCard);
                }
            }
            else if (bFlag)
            {
                this.TctlBill.Controls.Remove(this.TpICCard);
            }

            bFlag = this.TctlBill.Controls.Contains(this.TpCustomer);
            if (2048 == (nPermission & 2048))
            {
                if (!bFlag)
                {
                    this.TctlBill.Controls.Add(this.TpCustomer);
                }
            }
            else if (bFlag)
            {
                this.TctlBill.Controls.Remove(this.TpCustomer);
            }

            bFlag = this.TctlBill.Controls.Contains(this.TpTariff);
            if (4096 == (nPermission & 4096))
            {
                if (!bFlag)
                {
                    this.TctlBill.Controls.Add(this.TpTariff);
                }
            }
            else if (bFlag)
            {
                this.TctlBill.Controls.Remove(this.TpTariff);
            }

            bFlag = this.TctlBill.Controls.Contains(this.TpICCardLog);
            if (8192 == (nPermission & 8192))
            {
                if (!bFlag)
                {
                    this.TctlBill.Controls.Add(this.TpICCardLog);
                }
            }
            else if (bFlag)
            {
                this.TctlBill.Controls.Remove(this.TpICCardLog);
            }
        }
        #endregion

        #region IC卡缴费日志
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
        #endregion

        #region 车位状态布局
        /// <summary>
        /// 初始化所有库车位状态
        /// </summary>
        private void InitializeCarLocationPanels()
        {
            #region 添加所有库车位状态
            List<struCarPSONLayoutInfo> lstRect = CStaticClass.ConfigLstRectProject();
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
                // 手动指令根据车位状态获取地址初始化
                CWareHousePanel tpHand = (CWareHousePanel)Assembly.Load("CarLocationPanelLib").CreateInstance("CarLocationPanelLib." + typeName, false, BindingFlags.Default, null, args, null, null);
                tpHand.CallbackCarLocationEvent += new CallbackCarLocationEventHandler(CWareHousePanel_CallbackCarLocationEvent);
                tpHand.Text = strText;
                CStaticClass.myPanelCarLocation.Add(tpHand);
            }
            #endregion
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
                case EnmTxtCarLocationAddr.HandOrderSrc:
                    {
                        if (null != e.ParentForm && e.ParentForm.GetType() == typeof(CFormHandOrder))
                        {
                            ((CFormHandOrder)e.ParentForm).SetSrcLocAddr(e.StrLocAddr);
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
                        break; ;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion

        #region WCF推送服务
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
                    if (this.TsslConnected.Image != global::WindowsFormLib.Properties.Resources.connected)
                    {
                        this.TsslConnected.Image = global::WindowsFormLib.Properties.Resources.connected;

                        SetStatusStripValue(null);// 更新车位状态栏
                    }
                }
                else
                {
                    if (this.TsslConnected.Image != global::WindowsFormLib.Properties.Resources.disconnceted)
                    {
                        this.TsslConnected.Image = global::WindowsFormLib.Properties.Resources.disconnceted;
                    }
                }
                //string strICCardID = string.Empty;
                //bool bReturn = ReadICCardByComID(out strICCardID);
                //if (bReturn && !string.IsNullOrEmpty(strICCardID))
                //{
                //    GetBillingInfo(strICCardID);
                //}
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 设置状态栏值
        /// </summary>
        /// <param name="obj"></param>
        private void SetStatusStripValue(object obj)
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

            foreach (Panel tabPage in CStaticClass.myPanelCarLocation)
            {
                CWareHousePanel wareHouseTabPage = (CWareHousePanel)tabPage;

                if (null != obj && typeof(CarLocationPanelLib.PushService.CCarLocationDto) == obj.GetType())
                {
                    CarLocationPanelLib.PushService.CCarLocationDto carLocation = (CarLocationPanelLib.PushService.CCarLocationDto)obj;

                    if (wareHouseTabPage.Name == carLocation.warehouse.ToString())
                    {
                        wareHouseTabPage.UpdateCarLocationStatus(carLocation);
                    }
                }
                else if (null == obj)
                {
                    wareHouseTabPage.UpdateCarLocationStatus();
                }

                nSum += wareHouseTabPage.RectVehCount.X;
                nOccupy += wareHouseTabPage.RectVehCount.Y;
                nSpace += wareHouseTabPage.RectVehCount.Width;
                nMaxSpace += wareHouseTabPage.RectVehCount.Height;

                string strFlag = ";";
                if (i++ == CStaticClass.myPanelCarLocation.Count)
                {
                    strFlag = ")";
                }
                strSumDescp += wareHouseTabPage.Text + ":" + wareHouseTabPage.RectVehCount.X + strFlag;
                strOccupyDescp += wareHouseTabPage.Text + ":" + wareHouseTabPage.RectVehCount.Y + strFlag;
                strSpaceDescp += wareHouseTabPage.Text + ":" + wareHouseTabPage.RectVehCount.Width + strFlag;
                strMaxSpaceDescp += wareHouseTabPage.Text + ":" + wareHouseTabPage.RectVehCount.Height + strFlag;
            }

            if (2 > CStaticClass.myPanelCarLocation.Count)
            {// 只有一个库时
                strSumDescp = string.Empty;
            }
            this.TsslSumTxt.Text = nSum.ToString() + strSumDescp;
            this.TsslOccupyTxt.Text = nOccupy.ToString() + strOccupyDescp;
            this.TsslSpaceTxt.Text = nSpace.ToString() + strSpaceDescp;
            this.TsslSpaceMaxTxt.Text = nMaxSpace.ToString() + strMaxSpaceDescp;
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
            List<object> lstWareHouse = CStaticClass.ConfigLstWareHouse();

            foreach (object obj in lstWareHouse)
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
                if (i++ == lstWareHouse.Count)
                {
                    strFlag = ")";
                }
                string strWareHouse = CStaticClass.ConvertWareHouse((int)obj);
                strSumDescp += strWareHouse + ":" + rect.X + strFlag;
                strOccupyDescp += strWareHouse + ":" + rect.Y + strFlag;
                strSpaceDescp += strWareHouse + ":" + rect.Width + strFlag;
                strMaxSpaceDescp += strWareHouse + ":" + rect.Height + strFlag;
            }

            if (2 > lstWareHouse.Count)
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
                //CLOGException.Trace("callback_CallbackEvent, e:" + e.GetType().ToString());
                this.BeginInvoke((MethodInvoker)delegate
                {
                    CallBackSubFunction(e);
                });
            }
            catch (Exception e1)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(e1), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CWareHousePanel wareHouseTabPage = (CWareHousePanel)(CStaticClass.myPanelCarLocation.Find(a => a.Name == strName));
                wareHouseTabPage.UpdateCarLocationStatus(carLocation);
                SetStatusStripValue();
            }
        }
        #endregion

        #region IC卡自动读卡
        private void GetBillingInfo(string strICCardID)
        {
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                CarLocationPanelLib.QueryService.struBillInfo billInfo = new CarLocationPanelLib.QueryService.struBillInfo() 
                { 
                    nICCardType = -1, 
                    dtStartTime = CStaticClass.DefDatetime,
                    dtEndTime = CStaticClass.DefDatetime, 
                    strICCardID = strICCardID 
                };

                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryParkingInfo(ref billInfo,region);
                this.BtnPay.Enabled = false;
                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            FillICCardControls(billInfo);

                            //add by suhan 20150730
                            if (0 == billInfo.nHallID)
                            {
                                MessageBox.Show("没有可用的车厅", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.CboHall.Enabled = false;
                                return;
                            }
                            this.CboHall.Enabled = true;
                            this.BtnPay.Enabled = true;
                            //end
                            LBBillingInfoReturn.Text = "查询成功";
                            //MessageBox.Show("查询成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoICCardInfo:
                        {
                            LBBillingInfoReturn.Text = "没有制卡";
                            //MessageBox.Show("没有制卡", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NotMatch:
                        {
                            LBBillingInfoReturn.Text = "IC卡类型不匹配";
                            //MessageBox.Show("IC卡类型不匹配 ", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoTariffInfo:
                        {
                            LBBillingInfoReturn.Text = "获取计费标准信息失败";
                            //MessageBox.Show("获取计费标准信息失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoCarInGarage:
                        {
                            ///add by suhan 20150729
                            if ((int)EnmICCardType.Temp != billInfo.nICCardType)
                            {
                                FillICCardControls(billInfo);
                            }
                            //end
                            LBBillingInfoReturn.Text = "没有车存在车库";
                            //MessageBox.Show("当前临时卡没有车存在车库", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoBoundCustomer:
                        {
                            LBBillingInfoReturn.Text = "当前固定卡没有绑定车主计费信息";
                            //MessageBox.Show("当前固定卡没有绑定车主计费信息", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Fail:
                        {
                            LBBillingInfoReturn.Text = "计算停车费用失败";
                            //MessageBox.Show("计算停车费用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            LBBillingInfoReturn.Text = "连接异常";
                            //MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            LBBillingInfoReturn.Text = "查询失败";
                            //MessageBox.Show("查询失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                }

                ClearICCardControls();
            }
            catch (TimeoutException)
            {
                LBBillingInfoReturn.Text = "异常：超时";
                //MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException)
            {
                LBBillingInfoReturn.Text = "异常：SOAP错误";
                //MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException)
            {
                LBBillingInfoReturn.Text = "异常：通信错误";
                //MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                LBBillingInfoReturn.Text = "异常：应用程序异常";
                //MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            proxy.Close();

        }
        /// <summary>
        /// 读取IC卡卡号(逻辑卡号)；
        /// 成功：true
        /// 失败：false
        /// </summary>
        /// <param name="strICCardID">IC卡卡号</param>
        private bool ReadICCardByComID(out string strICCardID)
        {
            #region
            //string strPhysicalCardID = string.Empty;
            //strICCardID = string.Empty;
            //if (!CStaticClass.GetPushServiceConnectFlag())
            //{// 服务器通道断开时
            //    LBBillingInfoReturn.Text = "服务断开,确认服务器端是否正常运行";
            //    return false;//服务器通道断开
            //}

            //if (null == cardReader)
            //{
            //    cardReader = GetReaderObj(0);
            //    if (null != cardReader)
            //    {
            //        cardReader.ConnectCOM();
            //    }
            //    if (null == cardReader)
            //    {
            //        LBBillingInfoReturn.Text = "刷卡器注册失败";
            //        return false;//刷卡器注册失败
            //    }
            //}

            //QueryServiceClient proxy = new QueryServiceClient();

            //try
            //{
            //    if (cardReader.isComConnected)
            //    {
            //        bool bReadOK = cardReader.GetICCardID(out strPhysicalCardID);
            //        if (!bReadOK)
            //        {
            //            proxy.Close();
            //            LBBillingInfoReturn.Text = "读取IC卡失败";
            //            return false;//读取IC卡失败
            //        }
            //        CICCardDto ICCardTable = new CICCardDto() { phycode = strPhysicalCardID };
            //        CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryICCardInfoByPhyID(ref ICCardTable);

            //        if (null != ICCardTable)
            //        {
            //            strICCardID = ICCardTable.iccode;
            //            proxy.Close();
            //            LBBillingInfoReturn.Text = "获取IC卡卡号成功，IC卡号：" + strICCardID;
            //            return true;//逻辑卡号
            //        }
            //        proxy.Close();
            //        LBBillingInfoReturn.Text = "当前IC卡没有制卡";
            //        return false;//当前IC卡没有制卡
            //    }
            //    else
            //    {
            //        cardReader.ConnectCOM();
            //    }
            //    proxy.Close();
            //    LBBillingInfoReturn.Text = "连接读卡器失败";
            //    return false;//连接读卡器失败
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //proxy.Close();
            //return false;
            #endregion

            strICCardID = null;
            try
            {                
                string physCode = this.readIccard();
                if (physCode == null)
                {
                    return false;
                }
                QueryServiceClient proxy = new QueryServiceClient();
                strICCardID = proxy.QueryICCodeByPhysic(physCode);
                proxy.Close();
                return true;
            }
            catch (Exception ex) 
            {
                LOGManagementLib.CLOGException.Trace(ex.ToString());
            }
            return false;
        }

        /// <summary>
        /// 获取刷卡器对象
        /// </summary>
        /// <returns></returns>
        private CICCardReaderObj GetReaderObj(int nICCardComID)
        {
            CICCardReaderObj readerObj = new CICCardReaderObj(nICCardComID);

            string strClassName = "CQingTongReader";// CConfigManagement.myPara.ReaderClassName;
            object[] arrPara1 = new object[3];//CConfigManagement.myPara.ReaderPara;
            object[] arrPara = new object[3];//CConfigManagement.myPara.ReaderPara;
            arrPara1[0] = "115200";
            arrPara1[1] = "1";
            arrPara1[2] = "0";
            arrPara = arrPara1;
            if (string.IsNullOrEmpty(strClassName))
            {
                CLOGException.Trace("GetReaderObj", "获取刷卡器对象失败，请查看配置文件是否正确");
                return null; //获取通信模式块失败，请查看配置文件是否正确
            }

            readerObj.SetParameter(strClassName, arrPara);// 读取配置文件
            return readerObj;
        }

        #endregion

        #region 修改密码 add by suhan 20150720
        /// <summary>
        /// 点击修改密码图片按钮时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicEditModifyPassword_Click(object sender, EventArgs e)
        {
            (new CFormModifyPassWord()).ShowDialog();
        }

        /// <summary>
        /// 鼠标移动到修改密码按钮上时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicEditModifyPassword_MouseMove(object sender, MouseEventArgs e)
        {
            LBModifyPassword.Text = "修改密码";
        }

        /// <summary>
        /// 鼠标从修改密码按钮上移开时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicEditModifyPassword_MouseLeave(object sender, EventArgs e)
        {
            LBModifyPassword.Text = "";
        }

        #endregion 修改密码

        #region 切换用户 add by suhan 20150720
        /// <summary>
        /// 切换用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicEditLogout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认切换用户吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
            {
                //3.Cancel 取得或設定數值，表示是否應該取消事件。        
                return;
            }

            this.Hide();
            CFormBilling_Load(null, null);

            if (null != this && !this.IsDisposed)
            {
                this.Show();
            }
        }

        /// <summary>
        /// 鼠标移动到切换用户按钮上时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicEditLogout_MouseMove(object sender, MouseEventArgs e)
        {
            LBLogout.Text = "切换用户";
        }

        /// <summary>
        /// 鼠标从修改切换用户上移开时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicEditLogout_MouseLeave(object sender, EventArgs e)
        {
            LBLogout.Text = "";
        }
        #endregion 切换用户

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
