using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.PushService;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;
using CustomControlLib;
using BaseMethodLib;

namespace WindowsFormLib
{
    public partial class CFormCustomer : Form
    {
        Form m_formCarLocationStatus = new CFormCarLocation();
        private CFormTariff m_formTariff = new CFormTariff();
        private CFormCarSize m_formSize = new CFormCarSize();
        private bool m_isBill = true;
        private DateTime m_fixStartTime = CStaticClass.DefDatetime;
        private DateTime m_fixEndTime = CStaticClass.DefDatetime;

        public CFormCustomer()
        {
            InitializeComponent();
            this.CboWareHouse.Items.Clear();
            this.CboWareHouse.Items.Add("");
            this.CboWareHouse.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CancelButton = this.BtnCancel;
            m_isBill = CStaticClass.ConfigBillingFlag();
            // 没有收费管理系统,  删除标准收费
            this.LblTariff.Visible = m_isBill;
            this.CTxtTariff.Visible = m_isBill;
            this.LblFixActualFee.Visible = m_isBill;
            this.CTxtFixActualFee.Visible = m_isBill;
            this.LblFixChange.Visible = m_isBill;
            this.TxtFixChange.Visible = m_isBill;
            this.BtnFixPay.Visible = m_isBill;
            this.BtnSave.Visible = !m_isBill;
            SetFixPayControls(m_isBill);
            this.BtnModify.Enabled = false;

            // 设置键盘“Esc”按钮
            this.CancelButton = this.BtnCancel;
            m_formCarLocationStatus.ClientSize = new System.Drawing.Size(CStaticClass.ConfigMinWidth(), CStaticClass.ConfigMinHeight());
            m_formCarLocationStatus.FormBorderStyle = FormBorderStyle.FixedDialog;
            m_formCarLocationStatus.StartPosition = FormStartPosition.CenterScreen;
        }
       
        #region 公有函数
        /// <summary>
        /// 填充车主信息
        /// </summary>
        /// <param name="obj"></param>
        public void FillCustomerInfo(struCustomerInfo struCustomer)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                this.BtnFixPay.Visible = false;
                this.BtnSave.Visible = true;
                this.BtnSave.Enabled = false;
                this.BtnModify.Enabled = true;
                this.CTxtICCardID.Text = struCustomer.strICCardID;
                this.CTxtName.Text = struCustomer.strName;
                this.CTxtCarNumber.Text = struCustomer.strLicPlteNbr;
                this.CboWareHouse.Text = CStaticClass.ConvertWareHouse(struCustomer.nWareHouse);
                this.CboICCardStatus.Text = CStaticClass.ConvertICCardStatus(struCustomer.nICCardStatus);
                if (!string.IsNullOrEmpty(this.CboICCardStatus.Text))
                {
                    this.CboICCardStatus.Enabled = false;
                }
                else
                {
                    this.CboICCardStatus.Enabled = true;
                }
                this.CboICCardType.Text = CStaticClass.ConvertICCardType(struCustomer.nICCardType);
                this.TxtAddr.Text = struCustomer.strAddress;
                this.CTxtPhone.Text = struCustomer.strTelphone;
                this.CTxtMobile.Text = struCustomer.strMobile;
                this.CTxtCarLocAddr.Text = struCustomer.strCarPOSN;
                this.CTxtTariff.Text = "";
                this.CTxtFixActualFee.Text = "";
                this.CboPriorityID.Text = CStaticClass.ConvertPriorityID(struCustomer.nPriorityID);

                // 收费标准
                List<CTariffDto> lstTariff = proxy.GetTariffList();
                CTariffDto tariff = lstTariff.Find(s => s.id == struCustomer.nTariffID);
                this.CTxtTariff.Tag = tariff;

                if (null == tariff)
                {
                    return;
                }

                // 无限额时，按照一天费用计算
                float fee = null == tariff.fee ? 0.0f : (float)tariff.fee;
                if (null != tariff.workdayquotafee)
                {
                    fee = tariff.workdayquotafee < 0.1 ? (float)tariff.fee : (float)tariff.workdayquotafee;//(float)tariff.fee;
                }
                this.CTxtTariff.Text = CStaticClass.ConvertTariffFee(fee, tariff.feetype);
                CTxtICCardID_TextChanged(null, null);
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
        /// 清空车主信息控件
        /// </summary>
        /// <returns></returns>
        public void ClearCoutomerControls()
        {
            this.BtnFixPay.Visible = false;
            this.BtnSave.Visible = true;
            this.BtnSave.Enabled = true;
            this.BtnModify.Enabled = false;
            this.CTxtICCardID.Text = "";
            this.CTxtName.Text = "";
            this.CTxtCarNumber.Text = "";
            this.CboWareHouse.SelectedIndex = 0;
            this.CboICCardStatus.SelectedIndex = 0;
            this.CboICCardStatus.Enabled = true;
            this.CboICCardType.SelectedIndex = 0;
            this.TxtAddr.Text = "";
            this.CTxtPhone.Text = "";
            this.CTxtMobile.Text = "";
            this.CTxtCarLocAddr.Text = "";
            this.CTxtTariff.Text = "";
            this.CTxtFixActualFee.Text = "";
            this.CboPriorityID.SelectedIndex = 0;
            this.CTxtTariff.Tag = null;
        }
       
        /// <summary>
        /// 清空车主信息控件
        /// </summary>
        /// <returns></returns>
        public void ClearCoutomerControls(string strICCard)
        {
            this.BtnFixPay.Visible = false;
            this.BtnSave.Visible = true;
            this.BtnSave.Enabled = true;
            this.BtnModify.Enabled = false;
            this.CTxtICCardID.Text = strICCard;
            this.CTxtName.Text = "";
            this.CTxtCarNumber.Text = "";
            this.CboWareHouse.Text = null;// "";
            this.CboICCardStatus.Text = "正常";// "";
            this.CboICCardStatus.Enabled = true;
            this.CboICCardType.Text = null;// "";
            this.TxtAddr.Text = "";
            this.CTxtPhone.Text = "";
            this.CTxtMobile.Text = "";
            this.CTxtCarLocAddr.Text = "";
            this.CTxtTariff.Text = "";
            this.CTxtFixActualFee.Text = "";
            this.CboPriorityID.Text = null;
            this.CTxtTariff.Tag = null;
        }

        /// <summary>
        /// 设置收费标准
        /// </summary>
        /// <param name="tariff"></param>
        public void SetTariffID(CTariffDto tariff)
        {
            this.CTxtFixActualFee.Text = "";
            this.CTxtTariff.Tag = tariff; 
            if (null == tariff)
            {
                this.CTxtTariff.Text = "";
                return;
            }
            // 无限额时，按照一天费用计算
            float fee = null == tariff.fee ? 0.0f : (float)tariff.fee;
            if (null != tariff.workdayquotafee)
            {
                fee = tariff.workdayquotafee < 0.1 ? fee : (float)tariff.workdayquotafee;//(float)tariff.fee;
            }
            this.CTxtTariff.Text = CStaticClass.ConvertTariffFee(fee, tariff.feetype); 
        }
       
        /// <summary>
        /// 设置收费标准、固定卡起始日期和截止日期
        /// </summary>
        /// <param name="tariff"></param>
        public void SetTariffID(CTariffDto tariff, DateTime fixStartTime, DateTime fixEndTime)
        {
            this.CTxtFixActualFee.Text = "";
            this.CTxtTariff.Tag = tariff;
            m_fixStartTime = fixStartTime;
            m_fixEndTime = fixEndTime;
            if (null == tariff)
            {
                this.CTxtTariff.Text = "";
                return;
            }
            // 无限额时，按照一天费用计算
            float fee = null == tariff.fee ? 0.0f : (float)tariff.fee;
            if (null != tariff.workdayquotafee)
            {
                fee = tariff.workdayquotafee < 0.1 ? fee : (float)tariff.workdayquotafee;//(float)tariff.fee;
            }
            this.CTxtTariff.Text = CStaticClass.ConvertTariffFee(fee, tariff.feetype);
        }

        /// <summary>
        /// 设置定期卡预留车位尺寸
        /// </summary>
        /// <param name="strCarSize"></param>
        public void SetReserveCarSize(string strCarSize)
        {
            this.CTxtCarLocAddr.Text = strCarSize;
        }

        /// <summary>
        /// 设置分配车位车位地址值
        /// </summary>
        /// <param name="strHandOutLocAddr"></param>
        public void SetCarLocAddr(string strCarLocAddr)
        {
            this.CTxtCarLocAddr.Text = strCarLocAddr;
            m_formCarLocationStatus.Close();
        }
        #endregion

        #region 事件触发（单击、组合框选择改变、文本框双击）
        /// <summary>
        /// 登陆界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CFormCustomer_Load(object sender, EventArgs e)
        { // 设置键盘“Esc”按钮
            this.CancelButton = this.BtnCancel;
            m_formCarLocationStatus.ClientSize = new System.Drawing.Size(CStaticClass.ConfigMinWidth(), CStaticClass.ConfigMinHeight());
            m_formCarLocationStatus.FormBorderStyle = FormBorderStyle.FixedDialog;
            m_formCarLocationStatus.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// 车主信息保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (string.IsNullOrEmpty(this.CTxtICCardID.Text) || string.IsNullOrEmpty(this.CboICCardType.Text) || string.IsNullOrEmpty(this.CTxtName.Text))
                {
                    MessageBox.Show("用户卡号，姓名和卡类型都不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EnmICCardStatus enmICStatus = CStaticClass.ConvertICCardStatus(this.CboICCardStatus.Text.Trim());
                EnmICCardType enmICType = CStaticClass.ConvertICCardType(this.CboICCardType.Text.Trim());
                if (EnmICCardStatus.Normal != enmICStatus)
                {
                    MessageBox.Show("卡状态非正常，不允许绑定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (EnmICCardType.FixedLocation == enmICType && (string.IsNullOrEmpty(this.CboWareHouse.Text) || string.IsNullOrEmpty(this.CTxtCarLocAddr.Text)))
                {
                    MessageBox.Show("固定车位卡的库区和分配车位不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (EnmICCardType.Fixed == enmICType && (!string.IsNullOrEmpty(this.CTxtCarLocAddr.Text) && !CStaticClass.ConfigIsProjectSize(this.CTxtCarLocAddr.Text.Trim())))
                {
                    MessageBox.Show("预留车位尺寸非本项目车位尺寸，请重新输入", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (m_isBill && null == this.CTxtTariff.Tag)
                {
                    MessageBox.Show("收费标准不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                struCustomerInfo customerInfo = GetstruCustomerInfo();
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.BoundICCardForCUST(customerInfo);

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            if (null != this.Tag && this.Tag.GetType() == typeof(CUserCustomerInfoPanel))
                            {
                                ((CUserCustomerInfoPanel)this.Tag).SaveDgvCustomerInfo(customerInfo);
                            }
                            if (null != this.Tag && this.Tag.GetType() == typeof(CUserCustomerBillInfoPanel))
                            {
                                ((CUserCustomerBillInfoPanel)this.Tag).SaveDgvCustomerInfo(customerInfo);
                            }

                            CStaticClass.myClient.UpdateCarLoc();
                            DialogResult dr = MessageBox.Show("保存成功，是否继续添加？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                            if (dr == DialogResult.OK)
                            {
                                ClearCoutomerControls();
                                return;
                            }
                            //MessageBox.Show("保存成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.Close();
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("当前IC卡未制卡！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("IC卡卡号为空，绑定卡IC卡卡号不允许为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.CarInGarage:
                        {
                            MessageBox.Show("有车存在车库且要绑定的车位与存车的车位不一致！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.TaskOnICCard:
                        {
                            MessageBox.Show("当前卡有作业正在操作！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.LossORCancel:
                        {
                            MessageBox.Show("IC卡挂失或注销！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.FailToInsert:
                        {
                            MessageBox.Show("插入数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("没有找到要绑定的车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.FixedCarPOSN:
                        {
                            MessageBox.Show("指定车位已经绑定其他车主！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.BoundUser:
                        {
                            MessageBox.Show("当前卡已经绑定车主信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoTariffInfo:
                        {
                            MessageBox.Show("未绑定计费标准！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("保存失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 车主信息修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModify_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (string.IsNullOrEmpty(this.CTxtICCardID.Text) || string.IsNullOrEmpty(this.CboICCardType.Text)|| string.IsNullOrEmpty(this.CTxtName.Text))
                {
                    MessageBox.Show("用户卡号，姓名和卡类型都不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EnmICCardStatus enmICStatus = CStaticClass.ConvertICCardStatus(this.CboICCardStatus.Text.Trim());
                EnmICCardType enmICType = CStaticClass.ConvertICCardType(this.CboICCardType.Text.Trim());
                if (EnmICCardStatus.Normal != enmICStatus)
                {
                    MessageBox.Show("卡状态非正常，不允许绑定", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (EnmICCardType.FixedLocation == enmICType && (string.IsNullOrEmpty(this.CboWareHouse.Text) || string.IsNullOrEmpty(this.CTxtCarLocAddr.Text)))
                {
                    MessageBox.Show("固定车位卡的库区和分配车位不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (EnmICCardType.Fixed == enmICType && (!string.IsNullOrEmpty(this.CTxtCarLocAddr.Text) && !CStaticClass.ConfigIsProjectSize(this.CTxtCarLocAddr.Text.Trim())))
                {
                    MessageBox.Show("预留车位尺寸非本项目车位尺寸，请重新输入", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } 
                if (m_isBill && null == this.CTxtTariff.Tag)
                {
                    MessageBox.Show("收费标准不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                struCustomerInfo customerInfo = GetstruCustomerInfo();
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.ModifyCUSTInfo(customerInfo);

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            if (null != this.Tag && this.Tag.GetType() == typeof(CUserCustomerInfoPanel))
                            {
                                ((CUserCustomerInfoPanel)this.Tag).ModifyDgvCustomerInfo(customerInfo);
                            }
                            if (null != this.Tag && this.Tag.GetType() == typeof(CUserCustomerBillInfoPanel))
                            {
                                ((CUserCustomerBillInfoPanel)this.Tag).ModifyDgvCustomerInfo(customerInfo);
                            }

                            CStaticClass.myClient.UpdateCarLoc();
                            MessageBox.Show("修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.Close();
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("IC卡卡号为空，绑定卡IC卡卡号不允许为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("当前IC卡未制卡！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.CarInGarage:
                        {
                            MessageBox.Show("有车存在车库且要绑定的车位与存车的车位不一致！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }

                    case CarLocationPanelLib.QueryService.EnmFaultType.TaskOnICCard:
                        {
                            MessageBox.Show("当前卡有作业正在操作！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.LossORCancel:
                        {
                            MessageBox.Show("IC卡挂失或注销！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("没有找到要绑定的车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.FixedCarPOSN:
                        {
                            MessageBox.Show("指定车位已经绑定其他车主！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoBoundCustomer:
                        {
                            MessageBox.Show("当前卡没有绑定车主信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("修改失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 车主信息删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnDelete_Click(object sender, EventArgs e)
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

                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.DeleteCustomer(CStaticClass.ConvertICCardID(this.CTxtICCardID.Text.Trim()));

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            if (null != this.Tag && this.Tag.GetType() == typeof(CUserCustomerInfoPanel))
                            {
                                ((CUserCustomerInfoPanel)this.Tag).DeleteDgvCustomerInfo(GetstruCustomerInfo());
                            }
                            if (null != this.Tag && this.Tag.GetType() == typeof(CUserCustomerBillInfoPanel))
                            {
                                ((CUserCustomerBillInfoPanel)this.Tag).DeleteDgvCustomerInfo(GetstruCustomerInfo());
                            }

                            MessageBox.Show("删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.Close();
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("没有制卡！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.CarInGarage:
                        {
                            MessageBox.Show("当前卡有车存在车库", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.TaskOnICCard:
                        {
                            MessageBox.Show("当前卡有作业正在操作", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoBoundCustomer:
                        {
                            MessageBox.Show("当前卡没有绑定车主", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.NoCustomerInfo:
                        {
                            MessageBox.Show("没有车主信息", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            MessageBox.Show("删除失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 车主信息取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 固定卡确认缴费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFixPay_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CTxtICCardID.Text) || null == this.CTxtTariff.Tag)
                {
                    MessageBox.Show("用户卡号、收费标准不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                float fPayableFee = CStaticClass.ConvertTariffFee(this.CTxtTariff.Text); 
                float fActualFee;
                float.TryParse(this.CTxtFixActualFee.Text.Trim(), out fActualFee);

                if (fActualFee < fPayableFee)
                {
                    MessageBox.Show("缴纳费用不足，请补交余额！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CTariffDto tariff = (CTariffDto)this.CTxtTariff.Tag;
                CarLocationPanelLib.PushService.struBillInfo billInfo = new CarLocationPanelLib.PushService.struBillInfo();
                billInfo.strICCardID = CStaticClass.ConvertICCardID(this.CTxtICCardID.Text.Trim());
                billInfo.nICCardType = (int)CStaticClass.ConvertICCardType(this.CboICCardType.Text.Trim());
                billInfo.dtStartTime = CStaticClass.CurruntDateTime();
                billInfo.dtEndTime = CStaticClass.DefDatetime;
                billInfo.strCalculateDays = "-1";
                billInfo.nFeeType = (int)tariff.feetype;
                billInfo.nTariffID = tariff.id;
                billInfo.fPayableFee = fPayableFee;
                billInfo.fActualFee = fActualFee;
                billInfo.strOptCode = CStaticClass.myOperator.optcode;

                // 需要判断卡类型和计费类型是否对应，如不对应返回异常，客户端提示。如定期卡的计费类型配置的是临时卡的计费类型
                if (((int)EnmICCardType.Temp == billInfo.nICCardType && (int)EnmFeeType.Hour != billInfo.nFeeType) ||
                    ((int)EnmICCardType.Temp != billInfo.nICCardType && (int)EnmFeeType.Hour == billInfo.nFeeType))
                {
                    MessageBox.Show("请选择与卡类型对应的计费类型！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CarLocationPanelLib.PushService.EnmFaultType type = push.PayFeesAndTakeCar(billInfo);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CICCardDto icCardTBL = new CICCardDto() { iccode = billInfo.strICCardID };
                            proxy.QueryICCardInfo(ref icCardTBL);
                            if (null == icCardTBL)
                            {
                                MessageBox.Show("没有制卡", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else if (CBaseMethods.MyBase.IsEmpty(icCardTBL.userid))
                            {
                                BtnSave_Click(sender, e);
                            }
                            else
                            {
                                BtnModify_Click(sender, e);
                            }
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
                    #region 临时卡缴费异常提醒——注释
                    //case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                    //    {
                    //        MessageBox.Show("指定的源或目的车位不存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.NotFoundEquip:
                    //    {
                    //        MessageBox.Show("没有找到指定目的车厅", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.IsNotHallEquip:
                    //    {
                    //        MessageBox.Show("指定的目的地址不是车厅", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.HallEnter:
                    //    {
                    //        MessageBox.Show("车厅是进车厅不允许出车", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.NoCarInGarage:
                    //    {
                    //        MessageBox.Show("源车位没有车", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.NotAvailable:
                    //    {
                    //        MessageBox.Show("车厅设备不可接收指令", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.FailToSendTelegram:
                    //    {
                    //        MessageBox.Show("发送报文失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.NotSameWareHouse:
                    //    {
                    //        MessageBox.Show("刷卡库区与车所在库区不同", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.FailToInsert:
                    //    {
                    //        MessageBox.Show("插入数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.Exit:
                    //    {
                    //        MessageBox.Show("正在为您出车，请稍后", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.Wait:
                    //    {
                    //        MessageBox.Show("已经将您加到取车队列，请排队等候出车", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.Add:
                    //    {
                    //        MessageBox.Show("前面有人正在取车，已经将您加到取车队列，请排队等候出车", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.InvalidEquipID:
                    //    {
                    //        MessageBox.Show("无效设备号", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.InvalidWareHouseID:
                    //    {
                    //        MessageBox.Show("无效库区号", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    //case CarLocationPanelLib.PushService.EnmFaultType.NotNormalCarPOSN:
                    //    {
                    //        MessageBox.Show("源车位不是正常车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        break;
                    //    }
                    #endregion
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
                            MessageBox.Show("固定卡确认缴费失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 固定车位卡类型才可分配车位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboICCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                this.LblCarLocAddr.ForeColor = System.Drawing.Color.Black;
                EnmICCardType ICCardType = CStaticClass.ConvertICCardType(this.CboICCardType.Text.Trim());
                switch (ICCardType)
                {
                    case EnmICCardType.Fixed:
                        {
                            int nReservedSizeCount = proxy.GetFixReservedSizeCount();//通过WCF读取当前剩余定期卡预留车位的个数
                            if (0 < nReservedSizeCount || (this.BtnModify.Enabled))
                            {
                                this.LblCarLocAddr.Text = "预留车位尺寸";
                                this.CTxtCarLocAddr.Enabled = true;
                                this.CboWareHouse.Text = null;// "";
                                this.CboWareHouse.Enabled = false;
                            }
                            else
                            {
                                this.LblCarLocAddr.Text = "无剩余预留车位的定期卡";
                                this.LblCarLocAddr.ForeColor = System.Drawing.Color.Red;
                                this.CTxtCarLocAddr.Enabled = false;
                                this.CboWareHouse.Text = null;// "";
                                this.CboWareHouse.Enabled = false;
                            }
                            break;
                        }
                    case EnmICCardType.FixedLocation:
                        {
                            this.LblCarLocAddr.Text = "分配车位";
                            this.CTxtCarLocAddr.Enabled = true;
                            this.CboWareHouse.Enabled = true;
                            break;
                        }
                    default:
                        {
                            this.LblCarLocAddr.Text = "分配车位";
                            this.CTxtCarLocAddr.Enabled = false;
                            this.CboWareHouse.Text = null;// "";
                            this.CboWareHouse.Enabled = false;
                            break;
                        }
                }

                this.CTxtCarLocAddr.Text = "";
                this.CTxtTariff.Text = "";
                // 未缴费的固定卡才有确认缴费功能
                CTxtICCardID_TextChanged(sender, e);
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
        /// 收费标准文本框双击弹出“收费标准”对话框并赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CTxtTariff_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (null == this.CTxtTariff.Tag || typeof(CTariffDto) != this.CTxtTariff.Tag.GetType())
                {
                    m_formTariff.SetCboCardType(CStaticClass.ConvertICCardType(this.CboICCardType.Text), 0);
                }
                else
                {
                    CTariffDto tariff = (CTariffDto)this.CTxtTariff.Tag;
                    m_formTariff.SetCboCardType(CStaticClass.ConvertICCardType(this.CboICCardType.Text), tariff.id);
                }
                m_formTariff.ShowDialog(this);
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
        /// 固定卡实收金额值变化触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CTxtFixActualFee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float fPayableFee = CStaticClass.ConvertTariffFee(this.CTxtTariff.Text);
                float fActualFee;
                float.TryParse(this.CTxtFixActualFee.Text.Trim(), out fActualFee);
                this.TxtFixChange.Text = (fActualFee - fPayableFee).ToString();
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
        /// IC卡号值改变时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CTxtICCardID_TextChanged(object sender, EventArgs e)
        {
            //QueryServiceClient proxy = new QueryServiceClient();
            try
            {// 未缴费的固定卡才有确认缴费功能
                if (string.IsNullOrEmpty(this.CTxtICCardID.Text) ||
                    EnmICCardType.Temp == CStaticClass.ConvertICCardType(this.CboICCardType.Text))
                {
                    SetFixPayControls(false);
                    return;
                }
                SetFixPayControls(true);

                //CICCardDto icCardTable = new CICCardDto();
                //icCardTable.iccode = CStaticClass.ConvertICCardID(this.CTxtICCardID.Text.Trim());
                //proxy.QueryICCardInfo(ref icCardTable);

                //bool isFix = true;// 未缴费的固定卡才有确认缴费功能
                //if (null != icCardTable && null != icCardTable.tariffid && 0 != icCardTable.tariffid)
                //{
                //    isFix = false;
                //}

                //SetFixPayControls(isFix);
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
            //proxy.Close();
        }

        /// <summary>
        /// 定期卡预留车位文本框双击弹出“车辆尺寸选择”对话框并赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CTxtCarLocAddr_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (EnmICCardType.Fixed == CStaticClass.ConvertICCardType(this.CboICCardType.Text.Trim()))
                {// 定期卡
                    //CFormCarSize formSize = new CFormCarSize();
                    Point pos = this.Location;
                    pos.Offset(this.CTxtCarLocAddr.Location);
                    m_formSize.Location = new Point(pos.X, pos.Y + 2 * this.CTxtCarLocAddr.Height);
                    m_formSize.ShowDialog(this);
                }
                else if (EnmICCardType.FixedLocation == CStaticClass.ConvertICCardType(this.CboICCardType.Text.Trim()))
                {// 固定车位卡
                    // 车位布局对话框
                    if (string.IsNullOrEmpty(this.CboWareHouse.Text))
                    {
                        MessageBox.Show("库区不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 显示当前车库车位状态对话框
                    CStaticClass.showFormCarLocationStatus(this, m_formCarLocationStatus, this.CboWareHouse.Text.Trim(), EnmTxtCarLocationAddr.Customer);
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
        #endregion
   
        #region 私有函数
        /// <summary>
        /// 获取车主信息实例
        /// </summary>
        /// <returns></returns>
        private struCustomerInfo GetstruCustomerInfo()
        {
            struCustomerInfo customerInfo = new struCustomerInfo();
            customerInfo.strName = this.CTxtName.Text.Trim();
            customerInfo.strICCardID = CStaticClass.ConvertICCardID(this.CTxtICCardID.Text.Trim());
            customerInfo.nICCardStatus = (int)CStaticClass.ConvertICCardStatus(this.CboICCardStatus.Text.Trim());
            customerInfo.nICCardType = (int)CStaticClass.ConvertICCardType(this.CboICCardType.Text.Trim());
            customerInfo.nWareHouse = CStaticClass.ConvertWareHouse(this.CboWareHouse.Text);
            customerInfo.strCarPOSN = this.CTxtCarLocAddr.Text.Trim();
            customerInfo.strTelphone = this.CTxtPhone.Text.Trim();
            customerInfo.strMobile = this.CTxtMobile.Text.Trim();
            customerInfo.strLicPlteNbr = this.CTxtCarNumber.Text.Trim();
            customerInfo.strAddress = this.TxtAddr.Text.Trim();
            customerInfo.nPriorityID = CStaticClass.ConvertPriorityID(this.CboPriorityID.Text);
            customerInfo.dtStartTime = m_fixStartTime;
            customerInfo.dtDeadLine = m_fixEndTime;

            if (null != this.CTxtTariff.Tag)
            {
                customerInfo.nTariffID = ((CTariffDto)this.CTxtTariff.Tag).id;
            }

            return customerInfo;
        }

        /// <summary>
        /// 设置固定卡确认缴费控件可见否
        /// </summary>
        /// <param name="isFix"></param>
        private void SetFixPayControls(bool isFix)
        {
            if (!m_isBill)
            {// 无收费系统，不处理
                return;
            }

            this.LblFixActualFee.Visible = isFix;
            this.CTxtFixActualFee.Visible = isFix;
            this.LblFixChange.Visible = isFix;
            this.TxtFixChange.Visible = isFix;
            this.BtnFixPay.Visible = isFix;
            this.BtnSave.Visible = !isFix;
            //this.BtnModify.Enabled = !isFix;
        }
        #endregion

        private void CFormCustomer_Load_1(object sender, EventArgs e)
        {
            CboPriorityID.SelectedIndex = 0;
        }
    }
}
