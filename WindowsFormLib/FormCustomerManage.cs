using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib;
using System.ServiceModel;
using CarLocationPanelLib.PushService;
using CarLocationPanelLib.QueryService;
using LOGManagementLib;

namespace WindowsFormLib
{
    public partial class CFormCustomerManage : Form
    {
        private CFormCustomer m_formCustomer = null;
        private CICCardDto m_icCardDto = null;
        private string iccdCom = "";

        public CFormCustomerManage()
        {
            InitializeComponent();

            try
            {
                iccdCom = CReadIniFile.ReadSectionData("刷卡器", "ICCardCom");
            }
            catch (Exception ex)
            {
                CLOGException.Trace(ex.ToString());
            }

            m_formCustomer = new CFormCustomer();
            m_formCustomer.Tag = this.CucipCustomer;
            // 设置键盘“Esc”按钮
            Button BtnCancel = new Button();
            this.CancelButton = BtnCancel;
            BtnCancel.Click += new EventHandler(BtnCustomerClose_Click);
            // 设置键盘“Enter”按钮
            this.AcceptButton = this.CucipCustomer.BtnFind;

            if (CStaticClass.ConfigBillingFlag())
            {// 有收费管理系统,  删除车主信息tab
                this.TctlCustomer.Controls.Remove(this.TpCustomer);
            }
            this.CucipCustomer.CUserCustomerInfoPanel_Load(null, null);
            // 修改用户卡号的物理号时不需要
            if (CStaticClass.myPara.ReplacePhyCardIDFlag)
            {
                this.LblICCardIDOld.Visible = false;
                this.CTxtICCardIDOld.Visible = false;
            }
        }

        /// <summary>
        /// 显示所有车主信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CFormUserManage_Load(object sender, EventArgs e)
        {
            this.CucipCustomer.CUserCustomerInfoPanel_Load(sender, e);
        }

        #region 车主信息
        /// <summary>
        /// 车主信息修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCustomerModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == sender)
                {
                    if (null == m_formCustomer)
                    {
                        m_formCustomer = new CFormCustomer();
                    }
                    m_formCustomer.ClearCoutomerControls();
                    m_formCustomer.ShowDialog(this);
                    //formCustomer.Tag = this.CucipCustomer;
                    //formCustomer.ShowDialog(this);
                }
                else if (typeof(struCustomerInfo) == sender.GetType())
                {
                    struCustomerInfo Customer = (struCustomerInfo)sender;
                    if (null == m_formCustomer)
                    {
                        m_formCustomer = new CFormCustomer();
                    }

                    //m_formCustomer.Tag = this.CucipCustomer;
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

        #region IC卡信息
        /// <summary>
        /// IC卡信息读卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnICCardRead_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }
            string physCode = this.readIccard();
            if (physCode == null)
            {
                return;
            }
            this.TxtICCardPhy.Text = physCode;
            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                string iccode = proxy.QueryICCodeByPhysic(physCode);
                this.TxtICCardID.Text = iccode;// 用户卡号
                this.CTxtICCardIDFind.Text = iccode;// 用户卡号
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            proxy.Close();

            #region
            //try
            //{
            //    string strPhysicalCardID = string.Empty;
            //    string strICCardID = string.Empty;
            //    ClearICCardControls();

            //    CarLocationPanelLib.QueryService.EnmFaultType type = proxy.ReadICCard(out strPhysicalCardID, out strICCardID);

            //    this.TxtICCardID.Text = strICCardID;// 用户卡号
            //    this.CTxtICCardIDFind.Text = strICCardID;// 用户卡号
            //    this.TxtICCardPhy.Text = strPhysicalCardID;// 物理卡号

            //    switch (type)
            //    {
            //        // 获取的是逻辑卡号
            //        case CarLocationPanelLib.QueryService.EnmFaultType.LogicCardID:
            //            {
            //                MessageBox.Show("读卡成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                break;
            //            }
            //        // 连接读卡器失败，获取的是空值
            //        case CarLocationPanelLib.QueryService.EnmFaultType.FailConnection:
            //            {
            //                MessageBox.Show("连接读卡器失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                break;
            //            }
            //        // 读卡失败
            //        case CarLocationPanelLib.QueryService.EnmFaultType.FailToReadICCard:
            //            {
            //                MessageBox.Show("读取IC卡失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                break;
            //            }
            //        // 获取的用户卡号是空值
            //        case CarLocationPanelLib.QueryService.EnmFaultType.NoICCardInfo:
            //            {
            //                MessageBox.Show("当前IC卡未制卡！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                break;
            //            }
            //        // 获取的用户卡号是空值
            //        case CarLocationPanelLib.QueryService.EnmFaultType.Null:
            //            {
            //                MessageBox.Show("当前卡片未制卡！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// IC卡信息制卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnICCardCreate_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }
            if (CTxtICCardIDFind.Text.Trim().Length != 4) 
            {
                MessageBox.Show("请输入四位用户卡号！","提示",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (string.IsNullOrEmpty(this.TxtICCardPhy.Text) || string.IsNullOrEmpty(this.CTxtICCardIDFind.Text))
                {
                    MessageBox.Show("用户卡号、物理卡号都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string strPhysicalCardID = this.TxtICCardPhy.Text.Trim();
                string strICCardID = CStaticClass.ConvertICCardID(this.CTxtICCardIDFind.Text.Trim());
                // 写入数据格式（最大可写入长度是15个字节）：卡号 + 类型("1") + 制卡时间("yyyyMMdd") + 收费标准("01")
                //                 strData += "1" + CStaticClass.CurruntDateTime().ToString("yyyyMMdd") + "01";
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.AddNewICCard(strPhysicalCardID, strICCardID);

                switch (type)
                {
                    // 制卡成功
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            if (CStaticClass.ConfigBillingFlag())
                            {
                                MessageBox.Show("制卡成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                            ICCardCreateAddCustomer(strICCardID);
                            this.TxtICCardPhy.Clear();
                            break;
                        }
                    // 当前IC卡已经制卡
                    case CarLocationPanelLib.QueryService.EnmFaultType.HasICCardInfo:
                        {
                            MessageBox.Show("当前IC卡已经制卡:" + strICCardID, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 逻辑卡号不一致确认是否修改逻辑卡号
                    case CarLocationPanelLib.QueryService.EnmFaultType.ModifyICCardID:
                        {
                            ModifyICCardID(strPhysicalCardID, strICCardID);
                            break;
                        }
                    // 输入的逻辑卡号已经存在
                    case CarLocationPanelLib.QueryService.EnmFaultType.ICCardIDNotAllowed:
                        {
                            MessageBox.Show("输入的用户卡号已经存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 插入数据库失败
                    case CarLocationPanelLib.QueryService.EnmFaultType.FailToInsert:
                        {
                            MessageBox.Show("插入数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 传入的物理卡号或逻辑卡号为空
                    case CarLocationPanelLib.QueryService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("传入的物理卡号或逻辑卡号为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    // 连接读卡器失败
                    //                     case EnmFaultType.FailConnection:
                    //                         {
                    //                             MessageBox.Show("连接读卡器失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //                             break;
                    //                         }
                    //                     // 写入IC卡失败
                    //                     case EnmFaultType.FailToWriteICCard:
                    //                         {
                    //                             MessageBox.Show("写入IC卡失败:" + strData, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //                             break;
                    //                         }
                    default:
                        {
                            MessageBox.Show("制卡失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 换卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnICCardChange_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }
            if (CTxtICCardIDFind.Text.Trim().Length != 4)
            {
                MessageBox.Show("请输入四位用户卡号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                CarLocationPanelLib.PushService.EnmFaultType type = CarLocationPanelLib.PushService.EnmFaultType.Fail;
                if (this.CTxtICCardIDOld.Visible)
                {
                    if (string.IsNullOrEmpty(this.CTxtICCardIDFind.Text) || string.IsNullOrEmpty(this.CTxtICCardIDOld.Text))
                    {
                        MessageBox.Show("用户卡号、换卡旧卡号都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string strICCardID = CStaticClass.ConvertICCardID(this.CTxtICCardIDFind.Text.Trim());
                    string strICCardIDOld = CStaticClass.ConvertICCardID(this.CTxtICCardIDOld.Text.Trim());
                    ClearICCardControls();

                    type = push.ModifyICCard(strICCardIDOld, strICCardID);
                    this.TxtICCardID.Text = strICCardID;// 用户卡号
                    this.CTxtICCardIDFind.Text = strICCardID;// 用户卡号
                }
                else
                {
                    if (string.IsNullOrEmpty(this.CTxtICCardIDFind.Text))
                    {
                        MessageBox.Show("用户卡号不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string strPhysicalCardID = this.TxtICCardPhy.Text.Trim();
                    string strICCardID = CStaticClass.ConvertICCardID(this.CTxtICCardIDFind.Text.Trim());
                    ClearICCardControls();

                    type = push.ModifyPhyCard(strPhysicalCardID, strICCardID);
                    this.TxtICCardID.Text = strICCardID;// 用户卡号
                    this.CTxtICCardIDFind.Text = strICCardID;// 用户卡号
                }

                switch (type)
                {
                    // 换卡成功
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            MessageBox.Show("换卡成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    // 传入的参数卡号为空
                    case CarLocationPanelLib.PushService.EnmFaultType.Null:
                        {
                            MessageBox.Show("传入的参数卡号为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 当前IC卡未制卡
                    case CarLocationPanelLib.PushService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("当前IC卡未制卡！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // IC卡注销或挂失
                    case CarLocationPanelLib.PushService.EnmFaultType.LossORCancel:
                        {
                            MessageBox.Show("IC卡注销或挂失！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // IC卡类型不正确
                    case CarLocationPanelLib.PushService.EnmFaultType.NotMatch:
                        {
                            MessageBox.Show("IC卡类型不正确！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 新的IC卡有绑定用户
                    case CarLocationPanelLib.PushService.EnmFaultType.BoundUser:
                        {
                            MessageBox.Show("新的IC卡有绑定用户！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 旧卡或新卡正在作业
                    case CarLocationPanelLib.PushService.EnmFaultType.TaskOnICCard:
                        {
                            MessageBox.Show("旧卡或新卡正在作业！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 旧卡或新卡正在排队取车
                    case CarLocationPanelLib.PushService.EnmFaultType.Wait:
                        {
                            MessageBox.Show("旧卡或新卡正在排队取车！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 新的IC卡有车存在车库
                    case CarLocationPanelLib.PushService.EnmFaultType.CarInGarage:
                        {
                            MessageBox.Show("新的IC卡有车存在车库！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 更新数据库失败
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
                            MessageBox.Show("换卡失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// IC卡信息查询
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
                if (string.IsNullOrEmpty(this.CTxtICCardIDFind.Text))
                {
                    m_icCardDto = null;
                    ClearICCardControls();
                    MessageBox.Show("用户卡号不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string strICCardID = CStaticClass.ConvertICCardID(this.CTxtICCardIDFind.Text.Trim());
                this.GbxICCardInfo.Enabled = true;
                CICCardDto icCardTable = new CICCardDto();
                icCardTable.iccode = strICCardID;
                // 查询卡
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryICCardInfo(ref icCardTable);
                m_icCardDto = icCardTable;

                if (null != icCardTable && CarLocationPanelLib.QueryService.EnmFaultType.Success == type)
                {
                    this.TxtICCardID.Text = icCardTable.iccode;
                    this.TxtICCardType.Text = CStaticClass.ConvertICCardType(icCardTable.ictype);

                    switch (icCardTable.icstatus)
                    {
                        case (int)EnmICCardStatus.Lost:
                            {
                                this.TxtICCardStatus.Text = "挂失";
                                this.BtnICCardLoss.Enabled = false;
                                this.BtnICCardCancelLoss.Enabled = true;
                                this.BtnICCardLogout.Enabled = true;
                                break;
                            }
                        case (int)EnmICCardStatus.Normal:
                            {
                                this.TxtICCardStatus.Text = "正常";
                                this.BtnICCardLoss.Enabled = true;
                                this.BtnICCardCancelLoss.Enabled = false;
                                this.BtnICCardLogout.Enabled = true;
                                break;
                            }
                        case (int)EnmICCardStatus.Disposed:
                            {
                                this.TxtICCardStatus.Text = "注销";
                                this.BtnICCardLoss.Enabled = false;
                                this.BtnICCardCancelLoss.Enabled = false;
                                this.BtnICCardLogout.Enabled = false;
                                break;
                            }
                        default:
                            {
                                this.TxtICCardStatus.Text = "";
                                this.BtnICCardLoss.Enabled = true;
                                this.BtnICCardCancelLoss.Enabled = false;
                                this.BtnICCardLogout.Enabled = true;
                                break;
                            }
                    }

                    this.TxtICCardNewTime.Text = icCardTable.icnewtime.ToString();
                    this.TxtICCardLossTime.Text = icCardTable.iclosstime.ToString();
                    this.TxtICCardLogoutTime.Text = icCardTable.iclogouttime.ToString();
                    this.TxtCarLocAddr.Text = icCardTable.carlocaddr;
                    this.TxtWareHouse.Text = icCardTable.warehouse.ToString();
                    MessageBox.Show("查询成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    switch (type)
                    {
                        // 传入的参数卡号为空
                        case CarLocationPanelLib.QueryService.EnmFaultType.Null:
                            {
                                MessageBox.Show("传入的参数卡号为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        // 当前IC卡未制卡
                        case CarLocationPanelLib.QueryService.EnmFaultType.NoICCardInfo:
                            {
                                MessageBox.Show("当前IC卡未制卡！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        // IC卡注销或挂失
                        case CarLocationPanelLib.QueryService.EnmFaultType.LossORCancel:
                            {
                                MessageBox.Show("IC卡注销或挂失！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        // IC卡类型不正确
                        case CarLocationPanelLib.QueryService.EnmFaultType.NotMatch:
                            {
                                MessageBox.Show("IC卡类型不正确！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                            {
                                MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("未查询到当前IC卡", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
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
        /// IC卡信息关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnICCardClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// IC卡信息挂失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnICCardLoss_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (null == m_icCardDto || string.IsNullOrEmpty(this.TxtICCardID.Text))
                {
                    MessageBox.Show("该IC卡为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 更新卡状态
                m_icCardDto.icstatus = (int)EnmICCardStatus.Lost;
                m_icCardDto.iclosstime = CStaticClass.CurruntDateTime();
                bool flag = proxy.UpdateICCardInfo(m_icCardDto);
                if (flag)
                {
                    this.TxtICCardStatus.Text = "挂失";
                    this.TxtICCardLossTime.Text = CStaticClass.CurruntDateTime().ToString();
                    this.BtnICCardLoss.Enabled = false;
                    this.BtnICCardCancelLoss.Enabled = true;
                    MessageBox.Show("挂失成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("挂失失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// IC卡信息取消挂失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnICCardCancelLoss_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (null == m_icCardDto || string.IsNullOrEmpty(this.TxtICCardID.Text))
                {
                    MessageBox.Show("该IC卡为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //// 更新卡状态
                m_icCardDto.icstatus = (int)EnmICCardStatus.Normal;
                m_icCardDto.iclosstime = null;
                bool flag = proxy.UpdateICCardInfo(m_icCardDto);
                if (flag)
                {
                    this.TxtICCardStatus.Text = "正常";
                    this.TxtICCardLossTime.Text = CStaticClass.CurruntDateTime().ToString();
                    this.BtnICCardLoss.Enabled = true;
                    this.BtnICCardCancelLoss.Enabled = false;
                    MessageBox.Show("取消挂失成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("取消挂失失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// IC卡信息注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnICCardLogout_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (null == m_icCardDto || string.IsNullOrEmpty(this.TxtICCardID.Text))
                {
                    MessageBox.Show("该IC卡为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //// 更新卡状态
                m_icCardDto.icstatus = (int)EnmICCardStatus.Disposed;
                m_icCardDto.iclogouttime = CStaticClass.CurruntDateTime();
                bool flag = proxy.UpdateICCardInfo(m_icCardDto);
                if (flag)
                {
                    this.TxtICCardStatus.Text = "注销";
                    this.TxtICCardLogoutTime.Text = CStaticClass.CurruntDateTime().ToString();
                    this.BtnICCardLoss.Enabled = false;
                    this.BtnICCardCancelLoss.Enabled = false;
                    this.GbxICCardInfo.Enabled = false;
                    MessageBox.Show("注销成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("注销失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        ///<summary>
        /// 清空IC卡控件内容
        /// </summary>
        private void ClearICCardControls()
        {
            this.TxtICCardID.Text = "";// 用户卡号
            this.CTxtICCardIDFind.Text = "";// 用户卡号
            this.TxtICCardPhy.Text = "";// 物理卡号
            this.TxtICCardType.Text = "";
            this.TxtICCardStatus.Text = "";
            this.TxtICCardNewTime.Text = "";
            this.TxtICCardLossTime.Text = "";
            this.TxtICCardLogoutTime.Text = "";
            this.TxtCarLocAddr.Text = "";
            this.TxtWareHouse.Text = "";
            this.BtnICCardLoss.Enabled = false;
            this.BtnICCardCancelLoss.Enabled = false;
            this.BtnICCardLogout.Enabled = false;
        }

        /// <summary>
        /// 制卡时, 逻辑卡号不一致确认是否修改逻辑卡号
        /// </summary>
        private void ModifyICCardID(string strPhysicalCardID, string strData)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                string str = string.Format("当前卡片已经是本系统卡，确认是否将用户卡号修改为{0}？", strData);//用户卡号是{1}，
                DialogResult dr = MessageBox.Show(str, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                CarLocationPanelLib.PushService.EnmFaultType type = push.ModifyICCardID(strPhysicalCardID, strData);

                switch (type)
                {
                    // 修改成功
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            MessageBox.Show("修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    // 当前IC卡没有制卡
                    case CarLocationPanelLib.PushService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("当前IC卡未制卡:" + strData, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 更新数据库失败
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // 传入的参数卡号为空
                    case CarLocationPanelLib.PushService.EnmFaultType.Null:
                        {
                            MessageBox.Show("传入的参数卡号为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // IC卡注销或挂失
                    case CarLocationPanelLib.PushService.EnmFaultType.LossORCancel:
                        {
                            MessageBox.Show("IC卡注销或挂失", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    // IC卡类型不正确
                    case CarLocationPanelLib.PushService.EnmFaultType.NotMatch:
                        {
                            MessageBox.Show("IC卡类型不正确", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("修改失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 制卡成功,是否添加用户信息
        /// </summary>
        private void ICCardCreateAddCustomer(string strData)
        {
            string str = string.Format("制卡成功，是否添加{0}用户信息？", strData);//用户卡号是{1}，
            DialogResult dr = MessageBox.Show(str, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            if (null == m_formCustomer)
            {
                m_formCustomer = new CFormCustomer();
            }
            m_formCustomer.ClearCoutomerControls(strData);
            m_formCustomer.ShowDialog(this);
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
            #endregion

            return null;
        }
        #endregion
    }
}
