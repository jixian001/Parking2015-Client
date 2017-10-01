using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;
using BaseMethodLib;
using LOGManagementLib;
using CarLocationPanelLib.PushService;
using System.Threading;
using System.IO.Ports;

namespace WindowsFormLib
{
    public partial class CFormBillManagement : Form
    {
        private CFormCustomer m_formCustomer = new CFormCustomer();
        private CFormTariff m_formTariff = new CFormTariff();
        private string iccdCom = "";
        private string clientID = "";
        private string VTCom = "";  //顾客显示屏

        private bool isAutoRd = false;
        private Thread iccdThread;
        private IicCardRW iccdObj;
        private bool isConn;
        private SerialPort TV_VFD8C_Port;

        public CFormBillManagement()
        {
            InitializeComponent();
           
            this.CucbipCustomer.CUserCustomerInfoPanel_Load(null, null);

            this.CboHall.Items.AddRange(CStaticClass.ConfigLstHallDeviceIDDescp(1).ToArray());
            this.CboHall.SelectedIndex = 0;

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 界面登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CFormBillManagement_Load(object sender, EventArgs e)
        {
            this.CucbipCustomer.CUserCustomerInfoPanel_Load(sender, e);
            SetLogin();
            this.BtnPay.Enabled = false;
            try 
            {
                iccdCom = CReadIniFile.ReadSectionData("刷卡器", "ICCardCom");
                clientID = CReadIniFile.ReadSectionData("编号", "ClientID");
                VTCom = CReadIniFile.ReadSectionData("顾客显示屏", "VTCom");
            }
            catch (Exception ex) 
            {
                CLOGException.Trace(ex.ToString());
            }
            if (iccdCom != "")
            {
                iccdObj = new CIcCardRW(Convert.ToInt32(iccdCom), 38400, 0, 0);
                isConn = false;
            }

            if (VTCom != "") 
            {
                TV_VFD8C_Port = new SerialPort();
                TV_VFD8C_Port.PortName = VTCom;
                TV_VFD8C_Port.BaudRate = 2400;
                TV_VFD8C_Port.WriteTimeout = 500;               
            }

            if (TctlBill.SelectedTab.Name == "TpICCard") 
            {                
                isAutoRd = true;
                iccdThread = new Thread(new ThreadStart(ReadCardAndFindInfo));
                iccdThread.Start();
                BtnAutoRead.Text = "取消读卡";

                ClearICCardControls();
            }
        }

        /// <summary>
        /// 窗体首次显示时触发(窗体大小改变触发 OnResize OnShown)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Maximized)
            //{// 最大化状态时
            //HandResize();
            //}
        }

        /// <summary>
        /// Tab页码切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TctlBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != this.TctlBill.SelectedTab)
            {
                if (this.TctlBill.SelectedTab.Name == "TpTariff")
                {
                    this.CutpTariff.CUserTariffPanel_Load(sender, e);
                }

                if (this.TctlBill.SelectedTab.Name == "TpICCard")
                {
                    isAutoRd = true;
                }
                else 
                {
                    isAutoRd = false;
                }
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
        #endregion

        #region IC卡缴费
        /// <summary>
        /// IC卡读卡及查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadAndFind_Click(object sender, EventArgs e)
        {
            #region
            //ClearICCardControls();
            //if (!CStaticClass.CheckPushService())
            //{// 检查服务
            //    return;
            //}
            //string physCode = this.readIccard();
            //if (physCode == null) 
            //{
            //    return;
            //}
            //QueryServiceClient proxy = new QueryServiceClient();
            //try 
            //{
            //    string iccode = proxy.QueryICCodeByPhysic(physCode);
            //    this.CTxtICCardID.Text = iccode;
            //    if (iccode != null) 
            //    {
            //        //查询卡信息
                    
            //    }
            //}
            //catch (Exception ex) 
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //proxy.Close();
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAutoRead_Click(object sender, EventArgs e)
        {
            if (BtnAutoRead.Text.Contains("取消"))
            {
                BtnAutoRead.Text = "开启读卡";
                isAutoRd = false;               
            }
            else 
            {
                BtnAutoRead.Text = "取消读卡";
                isAutoRd = true;
            }
        }

        /// <summary>
        /// 自动读卡
        /// </summary>
        private void ReadCardAndFindInfo() 
        {
            while (isAutoRd) 
            {
                if (isAutoRd) 
                {
                    try
                    {
                        string physCode = this.readIccard();                     
                        if (physCode == null)
                        {
                            Thread.Sleep(1000);
                            continue;
                        }

                        this.Invoke((EventHandler)delegate
                        {
                            ClearICCardControls();
                        });

                        if (!CStaticClass.CheckPushService())
                        {// 检查服务
                            return;
                        }
                        QueryServiceClient proxy = new QueryServiceClient();
                        string iccode = proxy.QueryICCodeByPhysic(physCode);
                        if (iccode != null)
                        {
                            DisplayICCode(iccode);
                            BtnPay.Enabled = false;
                            BtnVipOut.Enabled = false;
                            #region 查询卡信息
                            CarLocationPanelLib.QueryService.struBillInfo billInfo = new CarLocationPanelLib.QueryService.struBillInfo();
                            billInfo.nICCardType = -1;
                            billInfo.dtStartTime = CStaticClass.DefDatetime;
                            billInfo.dtEndTime = CStaticClass.DefDatetime;
                            billInfo.strICCardID = iccode;
                            int region = 0;
                            #region 南侧客户端配置为1,北侧客户端为2
                            if (clientID == "1")
                            {
                                region = 1;
                            }
                            else if (clientID == "2")
                            {
                                region = 2;
                            }
                            #endregion
                            CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryParkingInfo(ref billInfo, region);
                            if (type == CarLocationPanelLib.QueryService.EnmFaultType.Success)
                            {
                                this.Invoke((EventHandler)delegate
                                {
                                    #region
                                    if (billInfo.nICCardType == (int)EnmICCardType.Temp)
                                    {
                                        this.LblStartTime.Text = "入库时间";
                                        this.LblEndTime.Text = "出库时间";
                                        this.LblCalculateDays.Text = "停车时间";
                                        TimeSpan ts = billInfo.dtEndTime - billInfo.dtStartTime;
                                        this.TxtCalculateDays.Text = (ts.Days > 0 ? ts.Days + "天" : "") + (ts.Hours > 0 ? ts.Hours + "小时" : "") + (ts.Minutes > 0 ? ts.Minutes + "分钟" : "");
                                       
                                        this.BtnPay.Text = "确认缴费出车";
                                        this.BtnVipOut.Text = "关闭";
                                        vipOutButton(false);
                                    }
                                    else
                                    {
                                        this.LblStartTime.Text = "缴费日期";
                                        this.LblEndTime.Text = "截止日期";
                                        this.LblCalculateDays.Text = "剩余时间";
                                        TimeSpan ts = billInfo.dtEndTime - CStaticClass.CurruntDateTime();
                                        this.TxtCalculateDays.Text = ts.Days + "天";
                                        this.LblFeeType.Text = "收费类型";                                       
                                        this.CTxtTariff.Text = CStaticClass.ConvertTariffFee(billInfo.fTariffNorm, billInfo.nFeeType);
                                        this.BtnPay.Text = "确认缴费";
                                        this.BtnVipOut.Text = "确认出车";
                                        vipOutButton(true);
                                    }
                                    this.TxtWareHouse.Text = CStaticClass.ConvertWareHouse(billInfo.nWareHouse);
                                    this.CboHall.Text = CStaticClass.ConvertHallDescp(billInfo.nWareHouse, billInfo.nHallID);
                                    payButton(true);
                                    this.CboFeeType.Text = CStaticClass.ConvertFeeType(billInfo.nFeeType);  
                                    this.CTxtICCardID.Text = billInfo.strICCardID;
                                    this.TxtICCardType.Text = CStaticClass.ConvertICCardType(billInfo.nICCardType);
                                    this.DtpStartTime.Value = billInfo.dtStartTime;
                                    this.DtpEndTime.Value = billInfo.dtEndTime;
                                    this.TxtPayableFee.Text = CStaticClass.ConvertPayableFee(billInfo.fPayableFee, billInfo.nFeeType);
                                    this.TxtActualFee.Text = billInfo.fPayableFee.ToString();
                                    this.LblDescpCalu.Text = string.Empty;
                                    displayCharges(billInfo.fPayableFee.ToString("0.0"));
                                    if (!CStaticClass.GetPushServiceConnectFlag())
                                    {// 服务器通道断开时
                                        this.CTxtTariff.Tag = null;
                                        return;
                                    }

                                    // 赋值计费标准实例
                                    QueryServiceClient proxyc = new QueryServiceClient();
                                    CTariffDto tariff = proxyc.GetTariffByID(billInfo.nTariffID);
                                    proxyc.Close();
                                    this.CTxtTariff.Tag = tariff;
                                    if (null == tariff || (int)EnmICCardType.Temp != tariff.iccardtype)
                                    {
                                        return;
                                    }

                                    #endregion
                                });
                            }
                            else
                            {
                                this.Invoke((EventHandler)delegate
                               {
                                   this.TxtICCardType.Text = CStaticClass.ConvertICCardType(billInfo.nICCardType);
                               });
                            }                          
                            #endregion
                        }
                        proxy.Close();
                    }
                    catch (Exception ex)
                    {
                        CLOGException.Trace(ex.ToString());
                    }
                }
                Thread.Sleep(1000);
            }
        }

        private delegate void displayDelegate(string msg);
        private void DisplayICCode(string code) 
        {
            if (this.CTxtICCardID.InvokeRequired)
            {
                displayDelegate dl = DisplayICCode;
                CTxtICCardID.Invoke(dl, new object[] { code });
            }
            else 
            {
                CTxtICCardID.Text = code;
            }
        }

        private delegate void payDelegate(bool bl);
        private void payButton(bool en) 
        {
            if (this.BtnPay.InvokeRequired)
            {
                payDelegate pd = payButton;
                BtnPay.Invoke(pd, new object[] { en });
            }
            else 
            {
                BtnPay.Enabled = en;
            }
        }

        private delegate void vipoutDelegate(bool bl);
        private void vipOutButton(bool en) 
        {
            if (this.BtnVipOut.InvokeRequired)
            {
                vipoutDelegate dl = vipOutButton;
                BtnVipOut.Invoke(dl, new object[] { en });
            }
            else 
            {
                BtnVipOut.Enabled = en;
            }
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

                if (fActualFee < fPayableFee)
                {
                    MessageBox.Show("缴纳费用不足，请补交余额！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dr = MessageBox.Show("确认缴费？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }
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
                clearVTMoneyDisplay();

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
                this.BtnPay.Enabled = false;
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
        /// 固定卡确认出车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnVipOut_Click(object sender, EventArgs e)
        {
            //先查询有没有相关车位，再依手动出库样式出库
            BtnVipOut.Enabled = false;
            try
            {
                if (CTxtICCardID.Text.Trim() == "")
                {
                    return;
                }
                if (CboHall.Text.Trim() == "")
                {
                    return;
                }
                if (!CStaticClass.CheckPushService())
                {
                    return;
                }
                QueryServiceClient proxy = new QueryServiceClient();
                CarLocationPanelLib.QueryService.CCarLocationDto carLoc = null;
                CarLocationPanelLib.QueryService.EnmFaultType tp = proxy.QueryCarPOSNByCardID(out carLoc, CTxtICCardID.Text.Trim());
                if (tp == CarLocationPanelLib.QueryService.EnmFaultType.Success)
                {
                    if (carLoc != null)
                    {
                        int warehouse = (int)carLoc.warehouse;
                        int nHallID = CStaticClass.ConvertHallDescp(warehouse, this.CboHall.Text);
                        string addres = carLoc.carlocaddr;
                      
                        PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
                        #region
                        DialogResult result = MessageBox.Show("确认该固定车位-" + addres + " 出车？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                        if (result == DialogResult.Cancel) 
                        {
                            return;
                        }

                        CarLocationPanelLib.PushService.EnmFaultType type = push.VehicleExit(warehouse, addres,nHallID.ToString());
                        switch (type)
                        {
                            case CarLocationPanelLib.PushService.EnmFaultType.Success:
                                {
                                    MessageBox.Show("车辆出库成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    this.Close();
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
                        MessageBox.Show("该卡没有存车");
                    }
                }
                else 
                {
                    MessageBox.Show("查找停车位异常！");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            CboHall.Enabled = true;
            ClearICCardControls();
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
                billInfo.strICCardID = this.CTxtICCardID.Text.Trim();
                int region = 0;
                #region 南侧客户端配置为1,北侧客户端为2
                if (clientID == "1") 
                {
                    region = 1;
                }
                else if (clientID == "2") 
                {
                    region = 2;
                }
                #endregion
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryParkingInfo(ref billInfo,region);
                this.BtnPay.Enabled = false;

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            FillICCardControls(billInfo);
							
                            if (0 == billInfo.nHallID)
                            {
                                MessageBox.Show("没有可用的车厅", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);                               
                                return;
                            }
                            MessageBox.Show("查询成功,出车厅： " + this.CboHall.Text, "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
							//add by suhan 20150728
                            if ((int)EnmICCardType.Temp != billInfo.nICCardType)
                            {
                                FillICCardControls(billInfo);
                                this.BtnPay.Enabled = false;
                            }
							//end
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
            int nHeight = this.ClientSize.Height > 560 ? this.ClientSize.Height : 560;// CStaticClass.ConfigMinHeight();
            int gap = CStaticClass.ConfigMainGap();
            int minGap = CStaticClass.ConfigMinGap();
            int nGap = CStaticClass.ConfigControlSize();
            this.TctlBill.Size = new System.Drawing.Size(nWidth - gap, nHeight);
            Size sTabSize = new Size(this.TctlBill.Width - gap, nHeight - 4 * minGap);
           
            #region IC卡缴费
            //this.GbxICCard.Size = sTabSize;
            //int nWithLen = this.LblICCardID.Width + this.CTxtICCardID.Width + this.LblICCardType.Width + this.TxtICCardType.Width + nGap;
            //int nLeft = Math.Max((this.GbxICCard.Width - nWithLen) / 2, minGap);
            //int nHeightLen = this.LblICCardID.Height + this.LblStartTime.Height + this.LblCalculateDays.Height + this.LblTariff.Height + this.BtnPay.Height + 6 * nGap;
            //int nTop = Math.Max((this.GbxICCard.Height - nHeightLen) / 2, minGap);
            //this.LblICCardID.Location = new Point(nLeft, nTop);
            //this.CTxtICCardID.Location = new Point(LblICCardID.Location.X + LblICCardID.Width, nTop);
            //this.LblICCardType.Location = new Point(nGap + CTxtICCardID.Location.X + CTxtICCardID.Width, nTop);
            //this.TxtICCardType.Location = new Point(LblICCardType.Location.X + LblICCardType.Width, nTop);
            //nTop += LblICCardID.Height + nGap;
            //this.LblStartTime.Location = new Point(nLeft, nTop);
            //this.DtpStartTime.Location = new Point(CTxtICCardID.Location.X, nTop);
            //this.LblEndTime.Location = new Point(LblICCardType.Location.X, nTop);
            //this.DtpEndTime.Location = new Point(TxtICCardType.Location.X, nTop);
            //nTop += LblStartTime.Height + nGap;
            //this.LblCalculateDays.Location = new Point(nLeft, nTop);
            //this.TxtCalculateDays.Location = new Point(CTxtICCardID.Location.X, nTop);
            //this.LblFeeType.Location = new Point(LblICCardType.Location.X, nTop);
            //this.CboFeeType.Location = new Point(TxtICCardType.Location.X, nTop);
            //this.TxtWareHouse.Location = new Point(TxtICCardType.Location.X, nTop);
            //this.LblHall.Location = new Point(TxtWareHouse.Location.X + TxtWareHouse.Width, nTop);
            //this.CboHall.Location = new Point(LblHall.Location.X + LblHall.Width, nTop);
            //this.CTxtTariff.Location = new Point(LblHall.Location.X + LblHall.Width, nTop);
            //nTop += LblCalculateDays.Height + nGap;
            //this.LblTariff.Location = new Point(nLeft, nTop);
            //this.TxtPayableFee.Location = new Point(CTxtICCardID.Location.X, nTop);
            //this.LblActualFee.Location = new Point(TxtPayableFee.Location.X + TxtPayableFee.Width, nTop);
            //this.TxtActualFee.Location = new Point(LblActualFee.Location.X + LblActualFee.Width, nTop);
            //this.LblChange.Location = new Point(TxtActualFee.Location.X + TxtActualFee.Width, nTop);
            //this.TxtChange.Location = new Point(LblChange.Location.X + LblChange.Width, nTop);
            //nTop += LblTariff.Height + 2 * gap;
            //this.LblDescpCalu.Location = new Point(2, nTop);
            //this.LblDescpCalu.Width = nWidth - gap;
            //nLeft = (this.GbxICCard.Width - this.BtnAutoRead.Width - this.BtnFind.Width - this.BtnPay.Width - this.BtnCancel.Width - 5 * nGap) / 2;
            //nTop += LblDescpCalu.Height + 3 * gap;
            //this.BtnAutoRead.Location = new Point(nLeft, nTop);
            //this.BtnFind.Location = new Point(BtnAutoRead.Location.X + BtnAutoRead.Width + 2 * nGap, nTop);
            //this.BtnPay.Location = new Point(BtnFind.Location.X + BtnFind.Width + 2 * nGap, nTop);
            //this.BtnCancel.Location = new Point(BtnPay.Location.X + BtnPay.Width + 2 * nGap, nTop);
            #endregion

            #region 车主信息
            this.TpCustomer.Size = sTabSize;
            this.CucbipCustomer.Size = this.TpCustomer.Size;
            this.CucbipCustomer.OnResize();
            #endregion

            #region 计费标准
            //this.TpTariff.Size = sTabSize;
            //this.CutpTariff.Size = this.TpTariff.Size;
            //this.CutpTariff.OnResize();
            #endregion
        }

        /// <summary>
        /// 填充IC卡缴费界面控件内容
        /// </summary>
        /// <param name="billInfo"></param>
        private void FillICCardControls(CarLocationPanelLib.QueryService.struBillInfo billInfo)
        {           
            if (billInfo.nICCardType == (int)EnmICCardType.Temp)
            {
                this.LblStartTime.Text = "入库时间";
                this.LblEndTime.Text = "出库时间";
                this.LblCalculateDays.Text = "停车时间";
                TimeSpan ts = billInfo.dtEndTime - billInfo.dtStartTime;
                this.TxtCalculateDays.Text = (ts.Days > 0 ? ts.Days + "天" : "") + (ts.Hours > 0 ? ts.Hours + "小时" : "") + (ts.Minutes > 0 ? ts.Minutes + "分钟" : "");                                        
                this.BtnPay.Text = "确认缴费出车";
                this.BtnVipOut.Text = "关闭";
                this.BtnVipOut.Enabled = false;               
            }
            else
            {
                this.LblStartTime.Text = "缴费日期";
                this.LblEndTime.Text = "截止日期";
                this.LblCalculateDays.Text = "剩余时间";
                TimeSpan ts = billInfo.dtEndTime - CStaticClass.CurruntDateTime();
                this.TxtCalculateDays.Text = ts.Days + "天";
                this.LblFeeType.Text = "收费类型";               
                this.CTxtTariff.Text = CStaticClass.ConvertTariffFee(billInfo.fTariffNorm, billInfo.nFeeType);
                this.BtnPay.Text = "确认缴费";
                this.BtnVipOut.Text = "确认出车";
                this.BtnVipOut.Enabled = true;                
            }
            this.TxtWareHouse.Text = CStaticClass.ConvertWareHouse(billInfo.nWareHouse);           
            this.CboHall.Text = CStaticClass.ConvertHallDescp(billInfo.nWareHouse, billInfo.nHallID);
            this.BtnPay.Enabled = true;
            this.CTxtICCardID.Text = billInfo.strICCardID;
            this.TxtICCardType.Text = CStaticClass.ConvertICCardType(billInfo.nICCardType);
            this.DtpStartTime.Value = billInfo.dtStartTime;
            this.DtpEndTime.Value = billInfo.dtEndTime;           
            this.TxtPayableFee.Text = CStaticClass.ConvertPayableFee(billInfo.fPayableFee, billInfo.nFeeType);
            this.TxtActualFee.Text = billInfo.fPayableFee.ToString();
            this.CboFeeType.Text = CStaticClass.ConvertFeeType(billInfo.nFeeType);   
            this.LblDescpCalu.Text = string.Empty;
            //发送显示金额
            displayCharges(billInfo.fPayableFee.ToString("0.0"));

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
       /// 显示金额
       /// </summary>
       /// <param name="money"></param>
        private void displayCharges(string money) 
        {
            try
            {
                if (TV_VFD8C_Port != null)
                {
                    clearVTMoneyDisplay();
                    if (!TV_VFD8C_Port.IsOpen)
                    {
                        TV_VFD8C_Port.Open();
                    }                   
                    if (TV_VFD8C_Port.IsOpen)
                    {
                       
                        byte[] dataBuf = new byte[10];
                        int a = 0;
                        foreach (char mon in money)
                        {
                            dataBuf[a++] = Convert.ToByte(mon);
                        }
                        byte[] headBuf = new byte[] { Convert.ToByte(27), Convert.ToByte(81), Convert.ToByte(65) };
                        byte[] endBuf = new byte[] { Convert.ToByte(13) };

                        byte[] buffer = new byte[a + 4];
                        Array.Copy(headBuf, buffer, headBuf.Length);
                        Array.Copy(dataBuf, 0, buffer, 3, a);
                        Array.Copy(endBuf, 0, buffer, a + 3, 1);
                        TV_VFD8C_Port.Write(buffer, 0, buffer.Length);

                        byte[] LedBuffer = new byte[] { Convert.ToByte(2), Convert.ToByte(76), Convert.ToByte(48), Convert.ToByte(49), Convert.ToByte(48), Convert.ToByte(48) };
                        TV_VFD8C_Port.Write(LedBuffer, 0, LedBuffer.Length);
                    }                   
                }
            }
            catch (Exception ex) 
            {
                CLOGException.Trace(ex.ToString());
            }
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void clearVTMoneyDisplay() 
        {
            try
            {
                if (TV_VFD8C_Port == null)
                {
                    return;
                }
                if (!TV_VFD8C_Port.IsOpen)
                {
                    TV_VFD8C_Port.Open();
                }
                if (TV_VFD8C_Port.IsOpen)
                {
                    Int16 i = 12;      //清屏
                    byte[] buffer = new byte[] { Convert.ToByte(i) };
                    TV_VFD8C_Port.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                CLOGException.Trace(ex.ToString());
            }          
        }

        /// <summary>
        /// 清空IC卡缴费界面控件内容
        /// </summary>
        private void ClearICCardControls()
        {
            //this.CTxtICCardID.Text = "";
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

            if (null != CStaticClass.myOperator)
            {
                nPermission = (int)CStaticClass.myOperator.optpermission;
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
        }
        
        /// <summary>
        /// 读卡操作,返回物理卡号 
        /// </summary>
        /// <returns></returns>
        private string readIccard()
        {
            #region 读卡              
           
            if (iccdObj == null) 
            {
                return null;
            }
            try
            {
                if (isConn == false)
                {
                    isConn = iccdObj.ConnectCOM();
                }

            }
            catch (Exception ex)
            {
                CLOGException.Trace("刷卡器建立异常：" + ex.ToString());
                return null;
            }
            if (!isConn) 
            {
                return null;
            }

            try
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
            catch (Exception ex)
            {
                CLOGException.Trace(ex.ToString());
                try 
                {
                    isConn = false;
                    iccdObj.disConnectCOM();                   
                }
                catch { }
            }

            return null;

            #endregion
        }

        #endregion 

        private void CFormBillManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            isAutoRd = false;
            try 
            {
                isConn = false;
                iccdObj.disConnectCOM();
                iccdThread.Abort();
                clearVTMoneyDisplay();
                if (TV_VFD8C_Port.IsOpen)
                {
                    TV_VFD8C_Port.Close();
                }
                TV_VFD8C_Port.Dispose();
            }
            catch { }
        }
    }
}
