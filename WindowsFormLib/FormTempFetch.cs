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
    public partial class CFormTempFetch : Form
    {
        private string iccdCom = "";

        public CFormTempFetch()
        {
            InitializeComponent();
            this.CboWareHouse.SelectedIndexChanged += new EventHandler(CboWareHouse_SelectedIndexChanged);
            InitializeComboxItems();
            // 设置键盘“Esc”按钮
            Button BtnCancel = new Button();
            this.CancelButton = BtnCancel;
            BtnCancel.Click += new EventHandler(BtnCancel_Click);
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
        /// 初始化组合框集合值(库区)
        /// </summary>
        private void InitializeComboxItems()
        {
            this.CboWareHouse.Items.Clear();
            this.CboWareHouse.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
        }

        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadICCard_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {
                return;
            }

            string physCode = this.readIccard();
            if (physCode == null)
            {
                return;
            }           
            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                string iccode = proxy.QueryICCodeByPhysic(physCode);
                this.TxtICCardID.Text = iccode;// 用户卡号 
                if (iccode != null) 
                {
                    QueryCarPOSN(iccode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            proxy.Close();

            #region
            //QueryServiceClient proxy = new QueryServiceClient();
            //try
            //{
            //    string strICCardID = this.TxtICCardID.Text.Trim();
            //    string strPhysicalCardID = string.Empty;

            //    CarLocationPanelLib.QueryService.EnmFaultType type = proxy.ReadICCard(out strPhysicalCardID, out strICCardID);
            //    this.TxtICCardID.Text = strICCardID;// 用户卡号

            //    switch (type)
            //    {
            //        // 获取的是逻辑卡号
            //        case CarLocationPanelLib.QueryService.EnmFaultType.LogicCardID:
            //            {
            //                QueryCarPOSN(strICCardID);
            //                break;
            //            }
            //        // 获取的逻辑卡号是空值
            //        case CarLocationPanelLib.QueryService.EnmFaultType.Null:
            //            {
            //                MessageBox.Show("获取的用户卡号是空值", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                break;
            //            }
            //        // 连接读卡器失败，获取的是空值
            //        case CarLocationPanelLib.QueryService.EnmFaultType.FailConnection:
            //            {
            //                MessageBox.Show("连接读卡器失败，获取的是空值", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                break;
            //            }
            //        // 读卡失败
            //        case CarLocationPanelLib.QueryService.EnmFaultType.FailToReadICCard:
            //            {
            //                MessageBox.Show("读取IC卡失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                break;
            //            }
            //        case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
            //            {
            //                MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                break;
            //            }
            //        default:
            //            {
            //                MessageBox.Show("读卡失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                break;
            //            }
            //    }
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
            #endregion
        }

        /// <summary>
        /// 确认临时取物
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
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouse.Text);// -1;
                int nHallID = CStaticClass.ConvertHallDescp(nWareHouse, this.CboHallID.Text);// -1

                if (string.IsNullOrEmpty(this.TxtICCardID.Text))
                {
                    MessageBox.Show("用户卡号不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(this.CboHallID.Text) || string.IsNullOrEmpty(this.CboWareHouse.Text)  || 0 > nHallID || 0 > nWareHouse)
                {
                    MessageBox.Show("出车库区、出车车厅都不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (this.CboWareHouse.Text.Contains("塔库"))
                { // 塔库无临时取物
                    MessageBox.Show("塔库无临时取物!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dr = MessageBox.Show("确认临时取物？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                string strICCardID = this.TxtICCardID.Text.Trim();
                CarLocationPanelLib.PushService.EnmFaultType type = push.TmpFetch(strICCardID, nWareHouse, nHallID);
                CLOGException.Trace("CFormTempFetch 临时取物 nWareHouse:" + nWareHouse + "strICCardID:" + strICCardID);
                 
                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            MessageBox.Show(CStaticClass.ConvertHallDescp(nWareHouse, nHallID) + "出车", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.Close();
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
                    case CarLocationPanelLib.PushService.EnmFaultType.NoCarInGarage:
                        {
                            MessageBox.Show("当前卡没有车存在车库", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundEquip:
                        {
                            MessageBox.Show("没有找到指定设备", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotAvailable:
                        {
                            MessageBox.Show("车厅不可接收指令", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.IsNotHallEquip:
                        {
                            MessageBox.Show("指定的设备不是车厅", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.EquipIncorrect:
                        {
                            MessageBox.Show("车厅不是进出两用车厅", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToSendTelegram:
                        {
                            MessageBox.Show("发送报文失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.InvalidWareHouseID:
                        {
                            MessageBox.Show("无效库区号", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.InvalidEquipID:
                        {
                            MessageBox.Show("无效设备号", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotAutomatic:
                        {
                            MessageBox.Show("非全自动模式", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToInsert:
                        {
                            MessageBox.Show("插入数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.TaskOnICCard:
                        {
                            MessageBox.Show("正在为您作业，请稍后", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            MessageBox.Show("临时取物失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 出车库区改变值时触发
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
                this.CboHallID.Items.Clear();
                if (!string.IsNullOrEmpty(this.CboWareHouse.Text))
                {
                    int nCurWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouse.Text);
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
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TxtICCardID.Text))
            {
                MessageBox.Show("用户卡号不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string strICCardID = this.TxtICCardID.Text.Trim();
            // 查询当前卡车位
            QueryCarPOSN(strICCardID);
        }

        /// <summary>
        /// 根据逻辑卡号查询车位
        /// </summary>
        /// <param name="strICCardID"></param>
        private void QueryCarPOSN(string strICCardID)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                // 查询当前卡车位
                CarLocationPanelLib.QueryService.CCarLocationDto carLoc = new CarLocationPanelLib.QueryService.CCarLocationDto();
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryCarPOSNByCardID(out carLoc, strICCardID);
                this.CboHallID.Text = null; // "";
                this.CboWareHouse.Text = null;// "";

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            if (null == carLoc)
                            {
                                MessageBox.Show("当前用户没有存车!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else if (carLoc.carloctype != (int)EnmLocationType.Normal || carLoc.carlocstatus != (int)CarLocationPanelLib.PushService.EnmLocationStatus.Occupy)
                            {
                                MessageBox.Show("该卡存车的车位非正常占用车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                this.CboWareHouse.Text = CStaticClass.ConvertWareHouse(carLoc.warehouse);//.ToString();
                                MessageBox.Show("查询IC卡成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                            MessageBox.Show("查询IC卡失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void CFormTempFetch_Load(object sender, EventArgs e)
        {
            try
            {
                iccdCom = CReadIniFile.ReadSectionData("刷卡器", "ICCardCom");
            }
            catch (Exception ex)
            {
                CLOGException.Trace(ex.ToString());
            }
        }
    }
}
